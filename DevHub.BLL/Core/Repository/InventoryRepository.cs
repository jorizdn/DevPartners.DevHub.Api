using DevHub.BLL.Core.Interface;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.BLL.Core.Repository
{
    public class InventoryRepository : IInventoryInterface
    {
        private readonly QueryMethod _query;

        public InventoryRepository(QueryMethod query)
        {
            _query = query;
        }

        public InventoryReturnModel CreateUpdate(InventoryModel model, string username)
        {
            var inventory = _query.CreateUpdateInventory(model, username);

            return inventory;
        }


    }
}
