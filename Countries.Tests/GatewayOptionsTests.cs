using Countries.Core.Gateway;
using Countries.Infrastructure.Gateway;
using Countries.Shared.BeautyTry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Countries.Tests
{
    public class GatewayOptionsTests
    {
        [Fact]
        public void Invalid_Authorization_Validation()
        {
            const string authorizationToken = "abcde";
            bool result = LinqTryResultable<bool>
                .Try(() =>
                {
                    new CountriesGatewayOptions(authorizationToken);
                    return true;
                })
                .Catch(() => false)
                .Handle();

            Assert.False(result);
        }

        [Fact]
        public void Valid_Authorization_Validation()
        {
            const string authorizationToken = "abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde";
            bool result = LinqTryResultable<bool>
                .Try(() =>
                {
                    new CountriesGatewayOptions(authorizationToken);
                    return true;
                })
                .Catch(() => false)
                .Handle();

            Assert.True(result);
        }
    }
}
