using System;
using System.Collections.Generic;

namespace DevHub.DAL.Entities
{
    public partial class ClientMaster
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
    }
}
