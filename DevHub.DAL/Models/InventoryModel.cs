using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.DAL.Models
{
    public class InventoryModel
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public string Username { get; set; }
    }

    public class InventoryReturnModel
    {
        public InventoryModel Inventory { get; set; }
        public int Action { get; set; }
    }

    public class InventoryInfo
    {
        public InventoryReturnModel Inventory { get; set; }
        public StatusResponse State { get; set; }
    }

    public class spInventoryModel
    {
        public int RecId { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
    }



}
