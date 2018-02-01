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
}
