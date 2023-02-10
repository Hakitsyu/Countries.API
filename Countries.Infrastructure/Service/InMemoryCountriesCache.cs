using Countries.Core.Models;
using Countries.Core.Service;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Service
{
    public class InMemoryCountriesCache : IInMemoryCountriesCache
    {
        private MemoryCache _cache;
        private ICountriesCacheKeyProvider _keyProvider;

        public InMemoryCountriesCache(ICountriesCacheKeyProvider keyProvider)
        {
            _cache = new(new MemoryCacheOptions
            {
                SizeLimit = 1024
            });
            _keyProvider = keyProvider;
        }

        public Task<IList<Country>?> GetAllAsync()
        {
            _cache.TryGetValue(_keyProvider.Create(), out IList<Country>? result);
            return Task.FromResult(result);
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

        public Task SetAllAsync(IList<Country> countries)
        {
            var options = new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
            };

            _cache.Set(_keyProvider.Create(), countries, options);
            return Task.CompletedTask;
        }
    }
}
