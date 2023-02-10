using Countries.Shared.NewtonSoftRestSharp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Services
{
    public class CountriesClient : NewtonsoftRestClient
    {
        // Whatever, but if u need a authorization token this client is util
        public CountriesClient(string url) : base(url)
        { }
    }
}
