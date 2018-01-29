using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.DAL.Models
{
    public class MembershipModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
