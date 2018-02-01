using System;
using System.Collections.Generic;

namespace DevHub.DAL.Entities
{
    public partial class InvTransactionOthers
    {
        public int RecId { get; set; }
        public int TimeTrackerId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
