using Countries.Core.Gateway;
using Countries.Core.Models;
using Countries.Core.Repositories;
using Countries.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Repository
{
    public class CountriesRepository : ICountriesRepository
    {
        private IDistributedCountriesCache _distributedCache;
        private IInMemoryCountriesCache _memoryCache;
        private ICountriesGateway _gateway;

        public CountriesRepository(ICountriesGateway gateway,
            IDistributedCountriesCache distributedCache,
            IInMemoryCountriesCache memoryCache)
            => (_gateway,
            _distributedCache,
            _memoryCache) = (gateway,
            distributedCache,
            memoryCache);

        public async Task<IList<Country>> GetAll()
            => await _memoryCache.GetAllOrSetAsync(async () =>
                await _distributedCache.GetAllOrSetAsync(async () =>
                    await _gateway.GetAllAsync()));

        public async Task<Country?> Get(string id)
            => (await GetAll()).FirstOrDefault(c => c.Id.Equals(id));
    }
}
