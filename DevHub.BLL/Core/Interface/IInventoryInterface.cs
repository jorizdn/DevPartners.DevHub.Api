using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.BLL.Core.Interface
{
    public interface IInventoryInterface
    {
        InventoryReturnModel CreateUpdate(InventoryModel model, string username);
    }
}
