using DevHub.BLL.Core.Interface;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DevHub.BLL.Core.Repository
{
    public class ClientRepository : IClientInterface
    {
        private readonly DevHubContext _context;
        private readonly Validators _validate;
        private readonly QueryMethod _query;

        public ClientRepository(DevHubContext context, Validators validate, QueryMethod query)
        {
            _context = context;
            _validate = validate;
            _query = query;
        }

        public ClientMaster GetClientByEmail(string email)
        {
            var client = _query.GetClientByEmail(email);
            if (client != null)
            {
                return client;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<ClientMaster> GetClients()
        {
            return _context.ClientMaster;
        }

        public ClientMaster GetClientById(int id)
        {
            try
            {
                return _context.ClientMaster.Find(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}
