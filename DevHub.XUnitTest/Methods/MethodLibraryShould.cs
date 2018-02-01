using DevHub.BLL.Core.Interface;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using DevHub.DAL.Identity;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DevHub.XUnitTest.Methods
{
    public class MethodLibraryShould
    {
        private readonly Mock<UserManager<ApplicationUser>> _usermanager;
        private readonly Mock<RoleManager<IdentityRole<string>>> _rolemanager;
        private readonly Mock<IOptions<AppSettingModel>> _options;
        private readonly Mock<DevHubContext> _context;
        private readonly MethodLibrary _sut;

        public MethodLibraryShould()
        {
            _usermanager = new Mock<UserManager<ApplicationUser>>();
            _rolemanager = new Mock<RoleManager<IdentityRole<string>>>();
            _options = new Mock<IOptions<AppSettingModel>>();
            _context = new Mock<DevHubContext>();
            _sut = new MethodLibrary(_usermanager.Object, _rolemanager.Object, _options.Object, _context.Object);
        }

        [Fact]
        public void ReturnTimeConflictReturnModelFromIsTimeConflict()
        {
            //act
            var test = _sut.IsTimeConflict(It.IsAny<TimeConflictModel>());

            //assert
            Assert.IsType<TimeConflictReturnModel>(test);

        }
    }
}
