using DevHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHub.DAL.Models
{

    public class UserInfo
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public int SpaceType { get; set; }
        public int FrequencyType { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public ICollection<ContactUserInfo> ContactNumber { get; set; }
        public string Remarks { get; set; }
        public string RoomType { get; set; }
        public string Duration { get; set; }
        public string Bill { get; set; }
        public string Period { get; set; }
        public string Rate { get; set; }
        public string TimeInFormat { get; set; }
        public int PersonCount { get; set; }
        public bool HaveBookedBefore { get; set; }
        public int BookStatus { get; set; }
        public int BookType { get; set; }
        public string RefCode { get; set; }
    }

    public class ContactUserInfo
    {
        public string ContactNumber { get; set; }
    }

    public class BookLogInfo
    {
        public ClientMaster Client { get; set; }
        public BookLog BookLog { get; set; }
        public BookInfo BookInfo { get; set; }
        public StatusResponse State { get; set; }
    }

    public class BookInfo
    {
        public int SpaceType { get; set; }
        public int FrequencyType { get; set; }
        public int BookType { get; set; }
        public int BookStatus { get; set; }
        public string RoomType { get; set; }

        public string SpaceName { get; set; }
        public string FrequencyName { get; set; }
        public string BookStatusName { get; set; }
        public string BookTypeName { get; set; }
    }

    public class BookDate
    {
        public string DateArrival { get; set; }
        public string DateDeparture { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }

        public string Period()
        {
            return DateArrival + " " +  TimeIn + " - " + DateDeparture + " " + TimeOut;
        }
    }

    public class EmailParametersModel
    {
        public UserInfo UserInfo { get; set; }
        public ClientMaster Client { get; set; }
        public string Uri { get; set; }
        public int Id { get; set; }
    }

    public class ScheduleModel
    {
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public bool IsFutureSchedOnly { get; set; }
    }

    public class TimeConflictModel
    {
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public DateTime Date { get; set; }
        public bool IsConference { get; set; }
    }

    public class TimeConflictReturnModel
    {
        public bool IsConflict { get; set; }
        public IEnumerable<BookLog> ConflictedItem { get; set; }
    }

    public class BookModel
    {
        public IEnumerable<BookLog> Data { get; set; }
        public TimeConflictReturnModel ConflictItems { get; set; }
        public List<BookLog> Conference { get; set; }
        public List<BookLog> Meeting { get; set; }
    }

}
