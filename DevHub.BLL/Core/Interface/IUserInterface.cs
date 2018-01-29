using DevHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Interface
{
    public interface IUserInterface
    {
        Task<string> AddAsync(MembershipModel model);
    }
}
