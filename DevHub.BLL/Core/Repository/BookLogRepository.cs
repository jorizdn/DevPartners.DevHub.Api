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

        public async Task<BookLogInfo> AddBookLogAsync(UserInfo model)
        {
            var clientEmail = _query.GetClientByEmail(model.Email);
            var isClientExists = clientEmail != null;
            var book = new BookLogInfo() { };
            var client = new ClientMaster();

            var booklog = _mapper.Map<BookLog>(model);
            booklog.Guid = Guid.NewGuid();
            booklog.BookingType = Convert.ToByte(model.BookType);
            booklog.BookStatus = model.BookStatus < 1 ? Convert.ToByte((int)BookStatusEnum.Pending) : Convert.ToByte(model.BookStatus);
            if (!model.HaveBookedBefore && !isClientExists)
            {
                client = _mapper.Map<ClientMaster>(model);
                var contacts = model.ContactNumber.ToArray();
                client.ContactNumber1 = contacts[0].ContactNumber;
                client.ContactNumber2 = contacts[1].ContactNumber;

                _context.Add(client);
                _context.SaveChanges();

                booklog.ClientId = client.ClientId;

            }
            else
            {
                if (isClientExists)
                {
                    booklog.ClientId = clientEmail.ClientId;
                    
                    clientEmail.ContactNumber1 = string.IsNullOrEmpty(model.ContactNumber.ElementAt(0).ContactNumber) ? client.ContactNumber1 : model.ContactNumber.ElementAt(0).ContactNumber;
                    clientEmail.ContactNumber2 = string.IsNullOrEmpty(model.ContactNumber.ElementAt(1).ContactNumber) ? client.ContactNumber2 : model.ContactNumber.ElementAt(1).ContactNumber; ;

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
                await _email.SendEmail(_method.GetApproveEmailParameter(model, false, isClientExists ? clientEmail : client, model.HaveBookedBefore),"");
                await _email.SendEmail(_method.GetApproveEmailParameter(model, true, isClientExists ? clientEmail : client, model.HaveBookedBefore),"");
            }

            return book;
        }

        public IEnumerable<BookLog> GetBookLog()
        {
            return _context.BookLog;
        }
        
        public BookLog GetBookLogById(string Id)
        {
            var id = _guid.GetBookLogByGuid(Id);

            try
            {
                return _context.BookLog.Find(id.Id);
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

            return book;
        }

    }
}
