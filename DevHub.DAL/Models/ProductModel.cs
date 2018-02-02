using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.DAL.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int UnitMeasure { get; set; }
    }

    public class ProductReturnModel
    {
        public ProductModel Product { get; set; }
        public int Action { get; set; }
    }

    public class spProductModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDesc { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double SRP { get; set; }
        public int uom_Id { get; set; }
        public string uom_Desc { get; set; }
    }

}
