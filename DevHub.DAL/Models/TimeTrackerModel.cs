using DevHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.DAL.Models
{
    public class TimeTrackerInfo
    {
        public TimeTrackerDetails TimeTracker { get; set; }
        public StatusResponse State { get; set; }
    }

    public class TimeTrackerDetails : TimeTrackingLogger
    {
        public string LogStatusName { get; set; }
    }
}
