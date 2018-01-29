using AutoMapper;
using DevHub.BLL.Core.Interface;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using DevHub.DAL.Models;
using System;
using System.Collections.Generic;

namespace DevHub.BLL.Core.Repository
{
    public class TimeTrackerRepository : ITimeTrackerInterface
    {
        private readonly DevHubContext _context;
        private readonly QueryMethod _query;
        private readonly GuidMethod _guid;
        private readonly IMapper _mapper;
        private readonly MethodLibrary _method;

        public TimeTrackerRepository(DevHubContext context, QueryMethod query, GuidMethod guid, IMapper mapper, MethodLibrary method)
        {
            _context = context;
            _query = query;
            _guid = guid;
            _mapper = mapper;
            _method = method;
        }

        public IEnumerable<TimeTrackingLogger> GetTimeTrackerLog()
        {
            return _context.TimeTrackingLogger;
        }

        public TimeTrackingLogger GetTimeTrackerLogById(int id)
        {
            try
            {
                return _context.TimeTrackingLogger.Find(id);
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public TimeTrackerInfo TimeinTimeTracker(string id)
        {
            var bookLog = _guid.GetBookLogByGuid(id);

            if (bookLog != null)
            {
                var timetrack = _mapper.Map<TimeTrackingLogger>(bookLog);
                timetrack.TimeIn = DateTime.Now.TimeOfDay;
                timetrack.TimeOut = null;
                timetrack.LogStatus = (int)LogTypeEnum.TimeIn;
                timetrack.LoggedDateTime = DateTime.Now;

                _context.Add(timetrack);
                _context.SaveChanges();

                var timetrackerDetails = _mapper.Map<TimeTrackerDetails>(timetrack);
                timetrackerDetails.LogStatusName = _method.GetLogTypeName(Convert.ToInt32(timetrackerDetails.LogStatus));

                return new TimeTrackerInfo() { TimeTracker = timetrackerDetails, State = new StatusResponse() {isValid = true } };
            }
            else
            {
                return new TimeTrackerInfo() {State = new StatusResponse() { isValid = false, Message = "Can't find Booklog, Check Id."} };
            }
        }

        public TimeTrackerInfo TimeoutTimeTracker(int id)
        {
            try
            {
                var timetrack = _context.TimeTrackingLogger.Find(id);
                timetrack.TimeOut = DateTime.Now.TimeOfDay;
                timetrack.LogStatus = (int)LogTypeEnum.TimeOut;

                _context.Update(timetrack);
                _context.SaveChanges();

                var timetrackerDetails = _mapper.Map<TimeTrackerDetails>(timetrack);
                timetrackerDetails.LogStatusName = _method.GetLogTypeName(Convert.ToInt32(timetrackerDetails.LogStatus));

                return new TimeTrackerInfo() { TimeTracker = timetrackerDetails, State = new StatusResponse() { isValid = true } };
            }
            catch (Exception e) 
            {
                return new TimeTrackerInfo() { State = new StatusResponse() { isValid = false, Message = "Time Tracker object not found."} };
            }
        }
    }
}
