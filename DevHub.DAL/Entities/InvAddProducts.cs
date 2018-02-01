using System;
using System.Collections.Generic;

namespace DevHub.DAL.Entities
{
    public partial class InvAddProducts
    {
        public int RecId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
