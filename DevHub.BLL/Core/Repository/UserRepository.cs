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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MethodLibrary _method;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, MethodLibrary method, IMapper mapper)
        {
            _userManager = userManager;
            _method = method;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(MembershipModel model)
        {
            if (model.IsAdmin)
                await _method.AddRole("Admin", "IsAdmin", "IsAdmin");
            else
                await _method.AddRole("Member", "IsMember", "IsMember");

            var user = _mapper.Map<ApplicationUser>(model);
            var claim = model.IsAdmin ? new Claim("IsAdmin", "True") : new Claim("IsMember", "True");

            try
            {
                var resultCreate = await _userManager.CreateAsync(user, model.Password);
                var resultRole = await _userManager.AddToRoleAsync(user, model.IsAdmin ?"Admin":"Member");
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
