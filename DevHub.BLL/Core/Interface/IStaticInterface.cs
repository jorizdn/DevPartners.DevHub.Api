using System.Collections.Generic;
using DevHub.DAL.Entities;

namespace DevHub.BLL.Core.Interface
{
    public interface IStaticInterface
    {
        IEnumerable<InvProductCategories> GetCategories();
        IEnumerable<InvUnitOfMeasure> GetUnitOfMeasure();
    }
}