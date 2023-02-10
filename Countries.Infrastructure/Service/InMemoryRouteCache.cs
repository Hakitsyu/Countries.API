using Countries.Core.Dto;
using Countries.Core.Service;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Service
{
    public class InMemoryRouteCache : IInMemoryRouteCache
    {
        private MemoryCache _cache;
        private IRouteCacheKeyProvider _keyProvider;

        public InMemoryRouteCache(IRouteCacheKeyProvider keyProvider)
        {
            _cache = new(new MemoryCacheOptions
            {
                SizeLimit = 1024
            });
            _keyProvider = keyProvider;
        }

        public Task<Route?> GetRouteAsync(string from, string to)
        {
            _cache.TryGetValue(_keyProvider.Create(from, to), out Route? result);
            return Task.FromResult(result);
        }

        public async Task<Route> GetRouteOrSetAsync(string from, string to, Func<Task<Route>> func)
        {
            var foundRoute = await GetRouteAsync(from, to);
            if (foundRoute != null)
                return foundRoute;

            var routes = await func.Invoke();
            await SetRouteAsync(routes);
            return routes;
        }

        public Task SetRouteAsync(Route route)
        {
            var options = new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
            };

            _cache.Set(_keyProvider.Create(route.From.Id, route.To.Id), route, options);
            return Task.CompletedTask;
        }
    }
}
