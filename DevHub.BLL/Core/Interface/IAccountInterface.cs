using DevHub.DAL.Entities;
using DevHub.DAL.Identity;
using DevHub.DAL.Models;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Interface
{
    public interface IAccountInterface
    {
        Task<AspNetUsers> LoginAsync(LoginModel model);
        Task SignOut();
    }
}
