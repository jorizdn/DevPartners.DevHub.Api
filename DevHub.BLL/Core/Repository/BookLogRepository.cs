using DevHub.BLL.Core.Interface;
using DevHub.DAL.Entities;
using System.Collections.Generic;
using DevHub.DAL.Models;
using AutoMapper;
using DevHub.BLL.Methods;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevHub.BLL.Core.Repository
{
    public class BookLogRepository : IBookLogInterface
    {
        private readonly DevHubContext _context;
        private readonly IMapper _mapper;
        private readonly MethodLibrary _method;
        private readonly GuidMethod _guid;
        private readonly QueryMethod _query;
        private readonly EmailMethod _email;

        public BookLogRepository(DevHubContext context, IMapper mapper, MethodLibrary method, GuidMethod guid, QueryMethod query, EmailMethod email)
        {
            _context = context;
            _mapper = mapper;
            _method = method;
            _guid = guid;
            _query = query;
            _email = email;
        }

        public async Task<BookLogInfo> AddBookLogAsync(UserInfo model, string uri)
        {
            var clientEmail = _query.GetClientByEmail(model.Email);
            var isClientExists = clientEmail != null;
            var book = new BookLogInfo() { };
            var client = new ClientMaster();
            var isContactsEmpty = model.ContactNumber.Count < 1;

            var booklog = _mapper.Map<BookLog>(model);
            booklog.Guid = Guid.NewGuid();
            model.RefCode = booklog.Guid.ToString();
            booklog.BookingType = Convert.ToByte(model.BookType);
            model.HaveBookedBefore = isClientExists ? true : false;
            booklog.BookStatus = model.BookStatus < 1 ? Convert.ToByte((int)BookStatusEnum.Pending) : Convert.ToByte(model.BookStatus);
            if (!model.HaveBookedBefore && !isClientExists)
            {
                client = _mapper.Map<ClientMaster>(model);
                var contacts = model.ContactNumber.ToArray();

                client.ContactNumber1 = contacts[0].ContactNumber;
                if (model.ContactNumber.Count > 1)
                { 
                    client.ContactNumber2 = contacts[1].ContactNumber;
                }

                _context.Add(client);
                _context.SaveChanges();

                booklog.ClientId = client.ClientId;

            }
            else
            {
                if (isClientExists)
                {
                    booklog.ClientId = clientEmail.ClientId;

                    if (!isContactsEmpty)
                    {
                        clientEmail.ContactNumber1 = string.IsNullOrEmpty(model.ContactNumber.ElementAt(0).ContactNumber) ? client.ContactNumber1 : model.ContactNumber.ElementAt(0).ContactNumber;
                        if (model.ContactNumber.Count > 1)
                        {
                            clientEmail.ContactNumber2 = string.IsNullOrEmpty(model.ContactNumber.ElementAt(1).ContactNumber) ? client.ContactNumber2 : model.ContactNumber.ElementAt(1).ContactNumber;
                        }
                    }

                    _context.Update(clientEmail);
                    _context.SaveChanges();
                }
                else
                {
                    return new BookLogInfo()
                    {
                        State = new StatusResponse()
                        {
                            isValid = false,
                            Message = "Can't find client with the specified email."
                        }
                    };
                }
            }

            book = new BookLogInfo()
            {
               Client = isClientExists ? clientEmail : client,
               BookLog = booklog,
               BookInfo = new BookInfo()
                {
                    SpaceName = _method.GetSpaceName(model.SpaceType),
                    FrequencyName = _method.GetFrequencyName(model.FrequencyType),
                    BookStatusName = _method.GetBookingStatusName(model.BookStatus),
                    BookTypeName = _method.GetBookingTypeName(model.BookType),
                    SpaceType = model.SpaceType,
                    FrequencyType = model.FrequencyType,
                    BookStatus = model.BookStatus,
                    BookType = model.BookType,
                    RoomType = model.RoomType
                },
               State = new StatusResponse()
               {
                   isValid = true
               }
            };

            _context.Add(booklog);
            _context.SaveChanges();

            if (model.BookType == (int)BookingTypeEnum.DevHub)
            {
                var emailParams = new EmailParametersModel()
                {
                    UserInfo = model,
                    Client = isClientExists ? clientEmail : client,
                    Uri = uri,
                    Id = booklog.BookingId
                };
                await _email.SendEmail(_method.GetApproveEmailParameter(emailParams, false),"");
                await _email.SendEmail(_method.GetApproveEmailParameter(emailParams,  true),"");
            }

            return book;
        }

        public IEnumerable<BookLogInfo> GetBookLog()
        {
            var booklog = new List<BookLogInfo>();
            foreach (var item in _context.BookLog)
            {
                var period = _method.GetPeriod(item.DateOfArrival.Value, item.DateOfDeparture.Value, item.TimeIn.Value, item.TimeOut.Value);
                var transaction = new TransactionInfo()
                {
                    Bill = _method.GetBill(item),
                    Space = _method.GetSpaceName(item.SpaceType.Value),
                    Duration = _method.GetDuration(item),
                    Rate = _method.GetBookRate(item.FrequencyType.Value, item.SpaceType.Value),
                    Period = period.DateArrival + " " + period.TimeIn + " - " + period.DateDeparture + " " + period.TimeOut
                };

                booklog.Add(new BookLogInfo() { BookLog = item, Client = _context.ClientMaster.Find(item.ClientId), Transaction = transaction });
            }

            return booklog;
        }

        public BookLogInfo GetBookLogById(string Id)
        {
            var id = _guid.GetBookLogByGuid(Id);

            try
            {
                var booklog = _context.BookLog.Find(id.BookingId);
                var client = _context.ClientMaster.Find(booklog.ClientId);

                var period = _method.GetPeriod(booklog.DateOfArrival.Value, booklog.DateOfDeparture.Value, booklog.TimeIn.Value, booklog.TimeOut.Value);
                var transaction = new TransactionInfo()
                {
                    Bill = _method.GetBill(booklog),
                    Space = _method.GetSpaceName(booklog.SpaceType.Value),
                    Duration = _method.GetDuration(booklog),
                    Rate = _method.GetBookRate(booklog.FrequencyType.Value, booklog.SpaceType.Value),
                    Period = period.DateArrival + " " + period.TimeIn + " - " + period.DateDeparture + " " + period.TimeOut
                };

                return new BookLogInfo() { BookLog = booklog, Client = client, Transaction = transaction };
            }
            catch (Exception)
            {

                throw null;
            }
        }

        public async Task<BookLog> ConfirmBookAsync(string id, string username)
        {
            var book = _guid.GetBookLogByGuid(id);
            book.BookStatus = (int)BookStatusEnum.Confirmed;
            book.BookingRefCode = id.Substring(0, 8) + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yy");
            var client = _context.ClientMaster.Find(book.ClientId);

            await _email.SendEmail(_method.GetConfirmEmailParameter(book, false, client), username);

            _context.Update(book);
            _context.SaveChanges();

            return book;
        }

        public BookModel GetBookLogSchedules(ScheduleModel model)
        {
            var timeConflict = _mapper.Map<TimeConflictModel>(model);
            timeConflict.IsConference = true;
            BookModel book = new BookModel();
            book.Conference = new List<BookLog>();
            book.Meeting = new List<BookLog>();

            var timeConflictItems = _method.IsTimeConflict(timeConflict);
            if (model.TimeIn != TimeSpan.Zero && model.TimeOut != TimeSpan.Zero)
            {
                var data = _context.BookLog
                            .Where(a => a.SpaceType == (int)SpaceEnum.ConferenceMeeting && ((a.TimeIn == model.TimeIn || a.TimeIn > model.TimeIn) || (a.TimeOut == model.TimeOut || a.TimeIn < model.TimeOut)) && a.DateOfArrival >= DateTime.UtcNow)
                            .OrderBy(a => a.DateOfArrival)
                            .ToList();
                book.ConflictItems = timeConflictItems;
                foreach (var item in book.ConflictItems.ConflictedItem)
                {
                    if (item.RoomType == "Conference")
                    {
                        book.Conference.Add(item);
                    }
                    else
                    {
                        book.Meeting.Add(item);
                    }
                }
                return book;
            }
            else
            {
                book.Data = model.IsFutureSchedOnly ?
                                _context.BookLog
                                .Where(a => a.SpaceType == (int)SpaceEnum.ConferenceMeeting && a.DateOfArrival >= DateTime.UtcNow.AddDays(-6))
                                .OrderBy(a => a.DateOfArrival) :
                                _context.BookLog
                                .Where(a => a.SpaceType == (int)SpaceEnum.ConferenceMeeting)
                                .OrderBy(a => a.DateOfArrival);

                foreach (var item in book.Data)
                {
                    if (item.RoomType == "Conference")
                    {
                        book.Conference.Add(item);
                    }
                    else
                    {
                        book.Meeting.Add(item);
                    }
                }
                return book;
            }

        }

    }
}
