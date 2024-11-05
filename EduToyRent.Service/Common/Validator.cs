using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EduToyRent.Service.Common
{
    public static class Validator
    {
        private static readonly Regex regexPhone = new(@"^(?:\+84|0)([3|5|7|8|9])+([0-9]{8,9})\b");
        public static bool IsValidPhone(string value)
        {
            bool result;
            Match match = regexPhone.Match(value);
            result = value.Length != 0 && match.Success;
            return result;
        }

    }
}
