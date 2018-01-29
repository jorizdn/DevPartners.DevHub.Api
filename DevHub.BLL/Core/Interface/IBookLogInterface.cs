using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Interface
{
    public interface IBookLogInterface
    {
        IEnumerable<BookLog> GetBookLog();
        Task<BookLogInfo> AddBookLogAsync(UserInfo model,string uri);
        BookLog GetBookLogById(string Id);
        Task<BookLog> ConfirmBookAsync(string id, string username);
    }
}
