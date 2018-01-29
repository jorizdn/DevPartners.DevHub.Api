using DevHub.DAL.Entities;
using System.Collections.Generic;

namespace DevHub.BLL.Core.Interface
{
    public interface IClientInterface
    {
        ClientMaster GetClientByEmail(string email);
        IEnumerable<ClientMaster> GetClients();
        ClientMaster GetClientById(int id);
    }
}
