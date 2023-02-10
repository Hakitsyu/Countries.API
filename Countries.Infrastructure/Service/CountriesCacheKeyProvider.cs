using Countries.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Service
{
    public class CountriesCacheKeyProvider : ICountriesCacheKeyProvider
    {
        public string Create()
            => "countries";
    }
}
