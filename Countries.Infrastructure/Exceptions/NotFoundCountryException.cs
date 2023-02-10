using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Exceptions
{
    public class NotFoundCountryException : Exception
    {
        public NotFoundCountryException() : base("Not found that country")
        { }
    }
}
