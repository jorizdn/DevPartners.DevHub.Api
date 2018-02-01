using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.DAL.Models
{
    public class AppSettingModel
    {
        //SwaggerAuth
        public string ApiKey { get; set; }

        //Email
        public string SenderEmail { get; set; }
        public string Password { get; set; }

        //WebConfig
        public string Protocol { get; set; }

        //ConnectionString
        public string DefaultConnection { get; set; }
        public string IdentityConnection { get; set; }
    }
}
