using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cursus_Business.Common
{
    public static class Validator
    {
        private static readonly Regex regexMail = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private static readonly Regex regexPassword = new(@"^(?=.*[A-Z])(?=.*\W).+$");
        private static readonly Regex regexName = new(@"^([a-zA-Z]+( [a-zA-Z]+)*)$");
        //private static readonly Regex regexPhone = new(@"(^\+?84+([0-9]{9})\b)");
        private static readonly Regex regexPhone = new(@"^(?:\+84|0)([3|5|7|8|9])+([0-9]{8,9})\b");


        public static bool IsValidName(string value)
        {
            bool result;
            Match match = regexName.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

        public static bool IsValidPassword(string value)
        {
            bool result;
            Match match = regexPassword.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }
        public static bool IsMinLengthPassword(string value)
        {
            return value.Length >= 6;
        }

        public static bool IsValidEmail(string value)
        {
            bool result;
            Match match = regexMail.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

        public static bool IsValidPhone(string value)
        {
            bool result;
            Match match = regexPhone.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

        public static bool CheckPhoneLength(string phoneNumber)
        {
            return phoneNumber.Length >= 10 && phoneNumber.Length <= 12;
        }
        //public static bool CheckPhoneLength(string phoneNumber)
        //{      
        //    return phoneNumber.Length == 12;
        //} 

        public static bool IsValidDOB(DateTime dob)
        {
            return dob != default(DateTime);
        }

        public static bool IsEnoughAge(DateTime dob)
        {
            DateTime compareDate = DateTime.Now.AddYears(-5);
            return dob <= compareDate;
        }
    }
}
