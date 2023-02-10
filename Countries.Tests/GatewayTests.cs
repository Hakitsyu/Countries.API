using Countries.Infrastructure.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Countries.Tests
{
    public class GatewayTests
    {
        [Fact]
        public void Instance_New_Gateway()
        {
            const string url = "https://url.com";
            CountriesGateway gateway = new CountriesGateway(new CountriesGatewayOptions(url));
            Assert.Equal(gateway.Options.Url, url);
        }
    }
}
