using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DevHub.DAL.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class AspNetUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
