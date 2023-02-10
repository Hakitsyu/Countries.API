using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure
{
    public class HttpErrorException : Exception
    {
        public int StatusCode { get; init; }

        public HttpErrorException(string message, int statusCode = 400) : base(message)
            => StatusCode = statusCode;
    }
}
