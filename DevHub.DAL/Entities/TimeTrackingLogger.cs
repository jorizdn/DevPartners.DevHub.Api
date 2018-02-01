using System;
using System.Collections.Generic;

namespace DevHub.DAL.Entities
{
    public partial class TimeTrackingLogger
    {
        public int TimeTrackerId { get; set; }
        public int BookingId { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public byte? LogStatus { get; set; }
        public string Remarks { get; set; }
        public int? UserLoggedBy { get; set; }
        public DateTime LoggedDateTime { get; set; }
    }
}
