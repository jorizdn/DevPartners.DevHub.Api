using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using System.Collections.Generic;

namespace DevHub.BLL.Core.Interface
{
    public interface ITimeTrackerInterface
    {
        IEnumerable<TimeTrackingLogger> GetTimeTrackerLog();
        TimeTrackingLogger GetTimeTrackerLogById(int id);
        TimeTrackerInfo TimeinTimeTracker(string guid);
        TimeTrackerInfo TimeoutTimeTracker(int id);
    }
}
