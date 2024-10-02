using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Common
{
    public sealed record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly List<Error> Failure = new List<Error>();

    }
}
