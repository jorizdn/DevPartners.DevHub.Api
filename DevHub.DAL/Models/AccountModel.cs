using DevHub.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.DAL.Models
{
    public class LoginModel
    {
        public string Input { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginInfoModel
    {
        public AspNetUser User { get; set; }
        public StatusResponse State { get; set; }
    }
}
