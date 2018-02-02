using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DevHub.DAL.Identity;
using DevHub.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DevHub.Core.Controllers
{  
    [Produces("application/json")]
    [Route("/Token")]
    public class TokenController : BaseController
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IPasswordHasher<ApplicationUser> _hasher;

        public TokenController(UserManager<ApplicationUser> usermanager, IPasswordHasher<ApplicationUser> hasher)
        {
            _usermanager = usermanager;
            _hasher = hasher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenAsync([FromBody]LoginModel model)
        {
            try
            {
                var user = await _usermanager.FindByNameAsync(model.Input);

                if (user == null)
                {
                    return Unauthorized();
                }

                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await _usermanager.GetClaimsAsync(user);
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: "http://localhost:61732",
                            audience: "http://localhost:61732",
                            claims: claims,
                            expires: DateTime.UtcNow.AddHours(24),
                            signingCredentials : creds
                            );

                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            Claim = userClaims[0].Type
                        });
                    }
                }
            }
            catch (Exception e)
            {

                return BadRequest(new
                {
                    error = e.Message
                });
            }

            return Unauthorized();
        }


    }
}