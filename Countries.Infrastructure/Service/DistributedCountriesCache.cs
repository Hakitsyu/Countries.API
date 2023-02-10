using Countries.Core.Models;
using Countries.Core.Service;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Service
{
    public class DistributedCountriesCache : IDistributedCountriesCache
    {
        private IDistributedCache _cache;
        private ICountriesCacheKeyProvider _keyProvider;

        public DistributedCountriesCache(IDistributedCache cache, ICountriesCacheKeyProvider keyProvider)
            => (_cache, _keyProvider) = (cache, keyProvider);

        public async Task<IList<Country>?> GetAllAsync()
        {
            var content = await _cache.GetStringAsync(_keyProvider.Create());
            return !string.IsNullOrWhiteSpace(content)
                ? JsonConvert.DeserializeObject<IList<Country>>(content)
                : null;
        }

        public async Task SetAllAsync(IList<Country> countries)
        {
            if (countries == null)
                return;

            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) };
            var content = JsonConvert.SerializeObject(countries);
            await _cache.SetStringAsync(_keyProvider.Create(), content, options);
        }

        public async Task<IList<Country>> GetAllOrSetAsync(Func<Task<IList<Country>>> func)
        {
            var foundCountries = await GetAllAsync();
            if (foundCountries != null)
                return foundCountries;

            var countries = await func.Invoke();
            await SetAllAsync(countries);
            return countries;
        }
    }
}
