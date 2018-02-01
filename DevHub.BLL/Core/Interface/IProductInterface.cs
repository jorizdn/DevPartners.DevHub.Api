using DevHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.BLL.Core.Interface
{
    public interface IProductInterface
    {
        ProductReturnModel CreateUpdate(ProductModel model);
    }
}
