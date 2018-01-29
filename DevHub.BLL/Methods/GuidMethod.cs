using DevHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevHub.BLL.Methods
{
    public class GuidMethod
    {
        private readonly DevHubContext _context;

        public GuidMethod(DevHubContext context)
        {
            _context = context;
        }

        public BookLog GetBookLogByGuid(string guid)
        {
            try
            {
                return _context.BookLog.Where(a => a.Guid == Guid.Parse(guid)).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}
