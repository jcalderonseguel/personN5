using System;
using System.Collections.Generic;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.API.Models
{
    public class ValidatePhoneNumberDTO
    {
        public string Value { get; set; }
        public string Result { get; set; }
        public ValidatePhoneNumberDTO(string value, string result)
        {
            Value = value;
            Result = result;
        }
    }
}
