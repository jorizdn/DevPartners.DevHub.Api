using DevHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevHub.BLL.Methods
{
    public class QueryMethod
    {
        private readonly DevHubContext _context;

        public QueryMethod(DevHubContext context)
        {
            _context = context;
        }

        public ClientMaster GetClientByEmail(string email)
        {
            try
            {
                return _context.ClientMaster.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
