using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using DevHub.BLL.Helpers;
using static DevHub.BLL.Helpers.HttpResponses;
using System.Globalization;

namespace DevHub.BLL.Methods
{
    public class QueryMethod : DataManager
    {
        private readonly DevHubContext _context;
        private readonly IOptions<AppSettingModel> _options;

        public QueryMethod(DevHubContext context, IOptions<AppSettingModel> options)
        {
            _context = context;
            _options = options;
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
