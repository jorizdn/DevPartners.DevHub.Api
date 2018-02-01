using DevHub.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DevHub.BLL.Methods
{
    public class Validators
    {
        public StatusResponse IsUserBookInfoValid(UserInfo model)
        {
            var state = new StatusResponse() { };
            state.isValid = true;

            if (!string.IsNullOrEmpty(model.Email))
            {
                var emailValidate = IsEmailValid(model.Email);
                if (emailValidate.isValid)
                {
                    state.isValid = true;
                }
                else
                {
                    state.isValid = false;
                    state.Message = emailValidate.Message;

                    return state;
                }
            }
            else
            {
                state.isValid = false;
                state.Message = "Email is empty";

                return state;
            }

            if (!string.IsNullOrEmpty(model.DateOfArrival.ToString()) || !string.IsNullOrEmpty(model.DateOfDeparture.ToString()))
            {
                if (model.DateOfArrival > model.DateOfDeparture)
                {
                    state.isValid = false;
                    state.Message = "Date of Arrival should not be beyond the Date of Departure";

                    return state;
                }
                else
                {
                    state.isValid = true;
                }
            }
            else
            {
                state.isValid = false;
                state.Message = "Date of Arrival or Date of Departure is empty";

                return state;
            }

            if (!string.IsNullOrEmpty(model.TimeIn.ToString()) || !string.IsNullOrEmpty(model.TimeOut.ToString()))
            {
                if (model.TimeIn > model.TimeOut)
                {
                    state.isValid = false;
                    state.Message = "Time In is not suppose to be set beyond of the Time Out.";

                    return state;
                }
                else
                {
                    state.isValid = true;
                }
            }
            else
            {
                state.isValid = false;
                state.Message = "Time In or Time Out field is empty";

                return state;
            }

            if (!model.HaveBookedBefore)
            {
                if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
                {
                    state.isValid = false;
                    state.Message = "Name fields required.";

                    return state;
                }

                foreach (var item in model.ContactNumber)
                {
                    var phoneIsValid = IsPhoneNumberValid(item.ContactNumber);
                    if (!phoneIsValid.isValid)
                    {
                        state.Message = phoneIsValid.Message + " : " + item.ContactNumber;
                        state.isValid = false;

                        return state;
                    }
                }
            }

            state.Message = "Model is Valid";
            return state;
        }

        public StatusResponse IsEmailValid(string email)
        {
            var state = new StatusResponse() { };
            state.isValid = true;

            if (!string.IsNullOrEmpty(email))
            {
                if (new EmailAddressAttribute().IsValid(email))
                {
                    state.isValid = true;
                }
                else
                {
                    state.isValid = false;
                    state.Message = "Email is not a valid email, Please check!";

                    return state;
                }
            }
            else
            {
                state.isValid = false;
                state.Message = "Email is empty";

                return state;
            }

            return state;

        }

        public StatusResponse IsPhoneNumberValid(string phone)
        {
            var state = new StatusResponse() { };
            state.isValid = true;

            if (phone != null)
            {
                if (phone.Any(a => !char.IsNumber(a)))
                {
                    state.Message = "Phone Number should not contain letters and symbols alike.";
                    state.isValid = false;

                    return state;
                }

                if (phone.Length > 11)
                {
                    state.Message = "Phone Number exceeded.";
                    state.isValid = false;

                    return state;
                }

                if (phone.Length < 11)
                {
                    state.Message = "Phone Number fall short of length.";
                    state.isValid = false;

                    return state;
                }
            }

            state.Message = "Model is Valid";
            return state;
        }

        public StatusResponse IsTimeIntervalValid(TimeSpan TimeIn, TimeSpan TimeOut)
        {
            var state = new StatusResponse() { };
            state.isValid = true;

            if (!string.IsNullOrEmpty(TimeIn.ToString()) || !string.IsNullOrEmpty(TimeOut.ToString()))
            {
                if (TimeIn > TimeOut)
                {
                    state.isValid = false;
                    state.Message = "Time In is not suppose to be set beyond of the Time Out.";

                    return state;
                }
                else
                {
                    state.isValid = true;
                }
            }
            else
            {
                state.isValid = false;
                state.Message = "Time In or Time Out field is empty";

                return state;
            }

            state.Message = "Model is Valid";
            return state;
        }

        public StatusResponse IsInventoryModelValid(InventoryModel model)
        {
            var state = new StatusResponse();
            state.isValid = true;
            state.Message = "Model is valid";

            if (string.IsNullOrEmpty(model.Username))
            {
                state.isValid = false;
                state.Message = "User not logged in.";

                return state;
            }

            return state;
        }

    }
}
