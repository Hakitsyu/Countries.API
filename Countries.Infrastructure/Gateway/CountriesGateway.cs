using Countries.Core.Gateway;
using Countries.Core.Models;
using Countries.Infrastructure.Services;
using Countries.Shared.NewtonSoftRestSharp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Gateway
{
    public class CountriesGateway : ICountriesGateway
    {
        public CountriesGatewayOptions Options { get; }
        private RestClient _client;

        public CountriesGateway([NotNull] CountriesGatewayOptions options)
        {
            Options = options;
            _client = new CountriesClient(options.Url);
        }

        public async Task<IList<Country>> GetAllAsync()
        {
            const string resource = "all";
            return await _client.GetAsync<IList<Country>>(new RestRequest(resource));
        }
    }
}
