using DevHub.DAL.Entities;
using DevHub.DAL.Identity;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevHub.BLL.Methods
{
    public class MethodLibrary
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;

        public MethodLibrary(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<string>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public string GetSpaceName(int space)
        {
            switch (space)
            {
                case (int)SpaceEnum.OpenSpace:
                    return "Open Space";

                case (int)SpaceEnum.PrivateSpace:
                    return "Private Space";

                case (int)SpaceEnum.ConferenceMeeting:
                    return "Conference/Meeting Room";

                default:
                    return "";
            }
        }

        public string GetFrequencyName(int frequency)
        {
            switch (frequency)
            {
                case (int)FrequencyEnum.Hourly:
                    return "Hourly";
                case (int)FrequencyEnum.Daily:
                    return "Daily";
                case (int)FrequencyEnum.Weekly:
                    return "Weekly";
                case (int)FrequencyEnum.Monthly:
                    return "Monthly";
                default:
                    return "";
            }
        }

        public string GetBookingStatusName(int status)
        {
            switch (status)
            {
                case (int)BookStatusEnum.Pending:
                    return "Pending";
                case (int)BookStatusEnum.Confirmed:
                    return "Confirmed";
                case (int)BookStatusEnum.Forfeited:
                    return "Forfeited";
                default:
                    return "";
            }
        }

        public string GetBookingTypeName(int type)
        {
            switch (type)
            {
                case (int)BookingTypeEnum.Walkin:
                    return "Walk-In";
                case (int)BookingTypeEnum.Facebook:
                    return "Facebook";
                case (int)BookingTypeEnum.DevHub:
                    return "DevHub";
                default:
                    return "";
            }
        }

        public string GetLogTypeName(int log)
        {
            switch (log)
            {
                case (int)LogTypeEnum.TimeIn:
                    return "Time-In";
                case (int)LogTypeEnum.TimeOut:
                    return "Time-Out";
                case (int)LogTypeEnum.NA:
                    return "Unidentified";
                default:
                    return "";
            }
        }

        public string GetBookRate(int freq, int space)
        {
            switch (space)
            {
                case (int)SpaceEnum.OpenSpace:
                    switch (freq)
                    {
                        case (int)FrequencyEnum.Hourly:
                            return "PHP 50 / Hour";
                        case (int)FrequencyEnum.Daily:
                            return "PHP 300 / Day";
                        case (int)FrequencyEnum.Weekly:
                            return "PHP 1200 / Week";
                        default:
                            return "";
                    };

                case (int)SpaceEnum.PrivateSpace:
                    switch (freq)
                    {
                        case (int)FrequencyEnum.Hourly:
                            return "PHP 75 / Hour";
                        case (int)FrequencyEnum.Daily:
                            return "PHP 350 / Day";
                        case (int)FrequencyEnum.Weekly:
                            return "PHP 1500 / Week";
                        default:
                            return "";
                    };

                case (int)SpaceEnum.ConferenceMeeting:
                    return "PHP 300 / Hour ";

                default:
                    return "";
            }
        }

        public EmailParameters GetApproveEmailParameter(UserInfo model, bool isAdmin, ClientMaster client, bool haveBookedBefore)
        {
            var space = GetSpaceName(model.SpaceType);
            var firstname = haveBookedBefore ? client.FirstName : model.FirstName;
            var lastname = haveBookedBefore ? client.LastName : model.LastName;
            var rate = GetBookRate(model.FrequencyType, model.SpaceType);

            if (!isAdmin)
            {
                var emailParams = new EmailParameters()
                {
                    Subject = $"Dev Hub: Confirming Booking!",
                    Firstname = firstname,
                    Lastname = lastname,
                    Email = client.Email,
                    Recipient = client.Email,
                    Template = "Add-Booking-Template",
                    Date = model.DateOfArrival.ToString("MMMM dd, yyyy"),
                    Message = !string.IsNullOrEmpty(model.Remarks) ? model.Remarks : "No Message",
                    ContactNumber = client.ContactNumber1 + ", " + client.ContactNumber2,
                    IsAdmin = false,
                    Rate = rate
                };

                switch (model.SpaceType)
                {
                    case (int)SpaceEnum.ConferenceMeeting:

                        emailParams.Template = "Add-Booking-Conference-Template";
                        return emailParams;

                    default:
                        return emailParams;
                }
            }
            else
            {
                var emailParams = new EmailParameters()
                {
                    Subject = $"Dev Hub: New Booking Request!",
                    Firstname = firstname,
                    Lastname = lastname,
                    Email = client.Email,
                    Space = space,
                    Recipient = client.Email,
                    Template = "Add-Admin-Booking-Template",
                    Date = model.DateOfArrival.ToString("MMMM dd, yyyy"),
                    IsFromDevhub = true,
                    Message = !string.IsNullOrEmpty(model.Remarks) ? model.Remarks : "No Message",
                    ContactNumber = client.ContactNumber1 + ", " + client.ContactNumber2,
                    IsAdmin = true,
                    Rate = rate
                };

                switch (model.SpaceType)
                {
                    case (int)SpaceEnum.ConferenceMeeting:
                        emailParams.Template = "Add-Admin-Booking-Conference-Template";
                        return emailParams;

                    default:
                        return emailParams;
                }
            }
        }

        public EmailParameters GetConfirmEmailParameter(BookLog book, bool isAdmin, ClientMaster client)
        {
            var bookDate = GetPeriod(book.DateOfArrival.Value, book.DateOfDeparture.Value, book.TimeIn.Value, book.TimeOut.Value);
            var duration = GetDuration(book);
            var bill = "₱" + GetBill(book);
            if (!isAdmin)
            {
                var emailParams = new EmailParameters()
                {
                    Subject = $"Dev Hub: Book Confirmed!",
                    Firstname = client.FirstName,
                    Lastname = client.LastName,
                    Email = client.Email,
                    Space = GetSpaceName(book.SpaceType.Value),
                    Recipient = client.Email,
                    IsAdmin = false,
                    Date = bookDate.DateArrival,
                    Rate = GetBookRate(book.FrequencyType.Value, book.SpaceType.Value),
                    Template = "Confirm-Booking-Template",
                    ReferenceNumber = book.BookingRefCode,
                    Time = bookDate.TimeIn,
                    Period = bookDate.Period(),
                    ContactNumber = client.ContactNumber1 + " , " + client.ContactNumber2,
                    Duration = duration,
                    Bill = bill
                };
                switch (book.SpaceType)
                {
                    case (int)SpaceEnum.ConferenceMeeting:
                        emailParams.Template = "Confirm-Booking-Conference-Template";
                        return emailParams;

                    default:
                        return emailParams;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task AddRole(string Role, string ClaimValue, string ClaimType)
        {
            try
            {
                try
                {
                    await _roleManager.RoleExistsAsync(Role);
                }
                catch (Exception e)
                {

                    throw e;
                }


                if (!await _roleManager.RoleExistsAsync(Role))
                {
                    var role = new IdentityRole<string>(Role);

                    await _roleManager.AddClaimAsync(role, new Claim(ClaimType, ClaimValue));

                    await _roleManager.CreateAsync(role);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ApplicationUser> CheckAndReturnUserAsync(string input)
        {
            var user = await _userManager.FindByEmailAsync(input);
            if (user == null)
            {
                try
                {
                    return await _userManager.FindByNameAsync(input);
                }
                catch (Exception)
                {

                    return null;
                }
            }
            else
            {
                return user;
            }
        }

        public BookDate GetPeriod(DateTime arrival, DateTime departure, TimeSpan In, TimeSpan Out)
        {
            var dateArrival = arrival.ToString("ddd, MMMM dd yyyy");
            var dateDeparture = departure.ToString("ddd, MMMM dd yyyy");
            var timeIn = DateTime.MinValue.Add(In).ToString("hh:mm tt");
            var timeOut = DateTime.MinValue.Add(Out).ToString("hh:mm tt");

            return new BookDate() { DateArrival = dateArrival, DateDeparture = dateDeparture, TimeIn = timeIn, TimeOut = timeOut };
        }

        public string GetDuration(BookLog book)
        {
            string duration = "";
            var hourInterval = book.TimeOut.Value.Subtract(book.TimeIn.Value);
            var dayInterval = book.DateOfDeparture.Value.Subtract(book.DateOfArrival.Value);

            switch (book.FrequencyType)
            {
                case (int)FrequencyEnum.Hourly:
                    var hour = hourInterval.Hours;
                    var minutes = hourInterval.Minutes;
                    duration = hour + (hour == 1 ? " hour" : " hours") + (minutes > 0? (minutes == 1? " and " + minutes + " minute": " and " + minutes + " minutes") : "");
                    return duration;

                case (int)FrequencyEnum.Daily:
                    var day = dayInterval.Days;
                    hour = hourInterval.Hours;
                    duration = day + (day == 1 ? " day" : " days") + (hour > 0 ? (hour == 1 ? " and " + hour + " hour" : " and " + hour + " hours") : "");
                    return duration;

                case (int)FrequencyEnum.Weekly:
                    day = dayInterval.Days;
                    var isWeek = day >= 7;
                    var weeks = isWeek ? day / 7 : day;
                    var days = isWeek ? dayInterval.Days % 7 : day;
                    duration = isWeek ? weeks + (weeks > 1 ? " weeks" : " week") + (days > 0 ? (days == 1 ? " and " + days + " day" : " and " + days + " days") : "") : weeks > 1? weeks + " days" : " day";
                    return duration;
                default:
                    return duration;
            }
        }

        public string GetBill(BookLog book)
        {
            var bill = "";
            var hourInterval = book.TimeOut.Value.Subtract(book.TimeIn.Value);
            var dayInterval = book.DateOfDeparture.Value.Subtract(book.DateOfArrival.Value);

            switch (book.SpaceType)
            {
                case (int)SpaceEnum.OpenSpace:
                    switch (book.FrequencyType)
                    {
                        case (int)FrequencyEnum.Hourly:
                            bill = (hourInterval.Hours * 50).ToString();
                            return bill;
                        case (int)FrequencyEnum.Daily:
                            bill = (dayInterval.Days * 300).ToString();
                            return bill;
                        case (int)FrequencyEnum.Weekly:
                            var weeks = dayInterval.Days < 7 ? 1 : dayInterval.Days / 7;
                            var days = dayInterval.Days < 7 ? 0 : dayInterval.Days % 7;
                            weeks += weeks > 7 && days > 0 ? 1 : 0;
                            bill = (weeks * 1200).ToString();
                            return bill;
                        default:
                            return "";
                    }

                case (int)SpaceEnum.PrivateSpace:
                    switch (book.FrequencyType)
                    {
                        case (int)FrequencyEnum.Hourly:
                            bill = (hourInterval.Hours * 75).ToString();
                            return bill;
                        case (int)FrequencyEnum.Daily:
                            bill = (dayInterval.Days * 350).ToString();
                            return bill;
                        case (int)FrequencyEnum.Weekly:
                            var weeks = dayInterval.Days < 7 ? 1 : dayInterval.Days / 7;
                            var days = dayInterval.Days < 7 ? 0 : dayInterval.Days % 7;
                            weeks += weeks > 7 && days > 0 ? 1 : 0; 
                            bill = (weeks * 1500).ToString();
                            return bill;
                        default:
                            return "";
                    }

                case (int)SpaceEnum.ConferenceMeeting:
                    bill = (hourInterval.Hours * 300).ToString();
                    return bill;

                default:
                    return "";
            }
        }

    }
}
