using AutoMapper;
using DevHub.BLL.Core.Interface;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using DevHub.DAL.Identity;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Repository
{
    public class AccountRepository : IAccountInterface
    {
        private readonly MethodLibrary _method;
        private readonly SignInManager<ApplicationUser> _manager;
        private readonly DevHubContext _context;
        private readonly IdentityContext _identity;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly IMapper _mapper;

        public AccountRepository(MethodLibrary method, SignInManager<ApplicationUser> manager, DevHubContext context, RoleManager<IdentityRole<string>> roleManager, IdentityContext identity, IMapper mapper)
        {
            _method = method;
            _manager = manager;
            _context = context;
            _roleManager = roleManager;
            _identity = identity;
            _mapper = mapper;
        }

        public async Task<AspNetUsers> LoginAsync(LoginModel model)
        {
            var user = await _method.CheckAndReturnUserAsync(model.Input);

            if (user != null)
            {
                var signin = await _manager.PasswordSignInAsync(user, model.Password,
                    model.RememberMe, lockoutOnFailure: false);

                if (signin.Succeeded)
                {
                    var userRole = _identity.AspNetUserRoles.Where(ab => ab.UserId == user.Id).FirstOrDefault();
                    var userClaim = _identity.AspNetUserClaims.Where(a => a.UserId == user.Id).FirstOrDefault();

                    var userRoles = _mapper.Map<IdentityUserRole<string>>(userRole);
                    var userClaims = _mapper.Map<IdentityUserClaim<string>>(userClaim);

                    var asp = _mapper.Map<AspNetUsers>(user);
                    asp.AspNetUserRoles.Add(userRole);
                    asp.AspNetUserClaims.Add(userClaim);
                    return asp;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task SignOut()
        {
            try
            {
                await _manager.SignOutAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }

}
