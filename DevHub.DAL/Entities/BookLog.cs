using System;
using System.Collections.Generic;

namespace DevHub.DAL.Entities
{
    public partial class BookLog
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public byte? BookingType { get; set; }
        public Guid? Guid { get; set; }
        public int? SpaceType { get; set; }
        public int? FrequencyType { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public DateTime? DateOfDeparture { get; set; }
        public string Remarks { get; set; }
        public string RoomType { get; set; }
        public byte? BookStatus { get; set; }
        public string BookingRefCode { get; set; }
    }
}
