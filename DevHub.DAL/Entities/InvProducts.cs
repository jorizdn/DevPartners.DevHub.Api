using System;
using System.Collections.Generic;

namespace DevHub.DAL.Entities
{
    public partial class InvProducts
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal? Srp { get; set; }
        public byte? UomId { get; set; }
    }
}
