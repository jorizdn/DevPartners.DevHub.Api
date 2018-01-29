using DevHub.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DevHub.BLL.Methods
{
    public class Validators
    {
        public StatusResponse IsUserBookInfoValid(UserInfo model)
        {
            var error = new StatusResponse() { };
            error.isValid = true;

            if (!string.IsNullOrEmpty(model.Email))
            {
                var emailValidate = IsEmailValid(model.Email);
                if (emailValidate.isValid)
                {
                    error.isValid = true;
                }
                else
                {
                    error.isValid = false;
                    error.Message = emailValidate.Message;

                    return error;
                }
            }
            else
            {
                error.isValid = false;
                error.Message = "Email is empty";

                return error;
            }

            if (!string.IsNullOrEmpty(model.DateOfArrival.ToString()) || !string.IsNullOrEmpty(model.DateOfDeparture.ToString()))
            {
                if (model.DateOfArrival > model.DateOfDeparture)
                {
                    error.isValid = false;
                    error.Message = "Date of Arrival should not be beyond the Date of Departure";

                    return error;
                }
                else
                {
                    error.isValid = true;
                }
            }
            else
            {
                error.isValid = false;
                error.Message = "Date of Arrival or Date of Departure is empty";

                return error;
            }

            if (!string.IsNullOrEmpty(model.TimeIn.ToString()) || !string.IsNullOrEmpty(model.TimeOut.ToString()))
            {
                if (model.TimeIn > model.TimeOut)
                {
                    error.isValid = false;
                    error.Message = "Time In is not suppose to be set beyond of the Time Out.";

                    return error;
                }
                else
                {
                    error.isValid = true;
                }
            }
            else
            {
                error.isValid = false;
                error.Message = "Time In or Time Out field is empty";

                return error;
            }

            if (!model.HaveBookedBefore)
            {
                if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
                {
                    error.isValid = false;
                    error.Message = "Name fields required.";

                    return error;
                }

                foreach (var item in model.ContactNumber)
                {
                    var phoneIsValid = IsPhoneNumberValid(item.ContactNumber);
                    if (!phoneIsValid.isValid)
                    {
                        error.Message = phoneIsValid.Message + " : " + item.ContactNumber;
                        error.isValid = false;

                        return error;
                    }
                }
            }

            error.Message = "Model is Valid";
            return error;
        }
        public StatusResponse IsEmailValid(string email)
        {
            var error = new StatusResponse() { };
            error.isValid = true;

            if (!string.IsNullOrEmpty(email))
            {
                if (new EmailAddressAttribute().IsValid(email))
                {
                    error.isValid = true;
                }
                else
                {
                    error.isValid = false;
                    error.Message = "Email is not a valid email, Please check!";

                    return error;
                }
            }
            else
            {
                error.isValid = false;
                error.Message = "Email is empty";

                return error;
            }

            return error;

        }
        public StatusResponse IsPhoneNumberValid(string phone)
        {
            var error = new StatusResponse() { };
            error.isValid = true;

            if (phone.Any(a => !char.IsNumber(a)))
            {
                error.Message = "Phone Number should not contain letters and symbols alike.";
                error.isValid = false;

                return error;
            }

            if (phone.Length > 11)
            {
                error.Message = "Phone Number exceeded.";
                error.isValid = false;

                return error;
            }

            if (phone.Length < 11)
            {
                error.Message = "Phone Number fall short of length.";
                error.isValid = false;

                return error;
            }

            error.Message = "Model is Valid";
            return error;
        }
    }
}
