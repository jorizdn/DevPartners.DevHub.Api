using System;
using System.Collections.Generic;

namespace DevHub.DAL.Entities
{
    public partial class BillingTransaction
    {
        public int BillingId { get; set; }
        public int TimeTrackerId { get; set; }
        public decimal TransaciontAmount { get; set; }
        public decimal? OtherTransactionAmount { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? Change { get; set; }
        public DateTime? BillingDate { get; set; }
        public int? ProcessedBy { get; set; }
    }
}
