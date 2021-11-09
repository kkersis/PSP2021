using LibraryImplementation.Models;
using PSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryImplementation.Validation
{
    public static class UserValidator
    {
        public static UserValidatorResponse Validate(User user)
        {
            var response = new UserValidatorResponse
            {
                ErrorMessage = String.Empty,
                IsValid = true
            };
            if (!EmailValidator.Check(user.Email))
            {
                response.IsValid = false;
                response.ErrorMessage += "Provided email is invalid | ";
            }
            if (!PasswordChecker.Check(user.Password))
            {
                response.IsValid = false;
                response.ErrorMessage += "Password must contain at least 8 characters," +
                    " at least 1 uppercase letter and at least 1 special symbol | ";
            }
            if (!PhoneValidator.Check(user.PhoneNumber))
            {
                response.IsValid = false;
                response.ErrorMessage += "Provided phone number is invalid";
            }

            return response;
        }
        
    }

    public class UserValidatorResponse
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
