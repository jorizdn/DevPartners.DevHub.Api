using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Interface
{
    public interface IBookLogInterface
    {
        IEnumerable<BookLogInfo> GetBookLog();
        Task<BookLogInfo> AddBookLogAsync(UserInfo model,string uri);
        BookLogInfo GetBookLogById(string Id);
        Task<BookLog> ConfirmBookAsync(string id, string username);
        BookModel GetBookLogSchedules(ScheduleModel model);
    }
}
