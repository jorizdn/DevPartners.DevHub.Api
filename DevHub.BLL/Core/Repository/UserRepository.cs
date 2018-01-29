using DevHub.BLL.Core.Interface;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using DevHub.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly MethodLibrary _method;

        public UserRepository(UserManager<AspNetUser> userManager, MethodLibrary method)
        {
            _userManager = userManager;
            _method = method;
        }

        public async Task<string> AddAsync()
        {
            await _method.AddRole("Admin", "IsAdmin", "IsAdmin");
            var user = new AspNetUser() { FirstName = "Dev", LastName = "Partners", UserName = "DevPartners", Email = "info@devpartners.co" };
            var claim = new Claim("IsAdmin", "True");

            try
            {
                var resultCreate = await _userManager.CreateAsync(user, "D3vP@rtn3rs");
                var resultRole = await _userManager.AddToRoleAsync(user, "Admin");
                var resultClaim = await _userManager.AddClaimAsync(user, claim);

                return "Yes";
            }
            catch (Exception e)
            {

                return e.StackTrace;
            }
        }


    }
}
