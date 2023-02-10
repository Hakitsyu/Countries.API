using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Gateway
{
    public sealed class CountriesGatewayOptions
    {
        public string Url { get; }

        public CountriesGatewayOptions([NotNull] string url = "https://restcountries.com/v2/")
            => Url = url;
    }
}
