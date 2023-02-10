using Countries.Core.Dto;
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
    public class DistributedRouteCache : IDistributedRouteCache
    {
        private IDistributedCache _cache;
        private IRouteCacheKeyProvider _keyProvider;

        public DistributedRouteCache(IDistributedCache cache, IRouteCacheKeyProvider keyProvider)
            => (_cache, _keyProvider) = (cache, keyProvider);

        public async Task<Route?> GetRouteAsync(string from, string to)
        {
            var key = _keyProvider.Create(from, to);
            var content = await _cache.GetStringAsync(key);
            return !string.IsNullOrWhiteSpace(content)
                ? JsonConvert.DeserializeObject<Route>(content)
                : null;
        }

        public async Task SetRouteAsync(Route route)
        {
            if (route == null)
                return;

            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24) };
            var key = _keyProvider.Create(route.From.Id, route.To.Id);
            var content = JsonConvert.SerializeObject(route);
            await _cache.SetStringAsync(key, content, options);
        }

        public async Task<Route> GetRouteOrSetAsync(string from, string to, Func<Task<Route>> func)
        {
            var foundRoute = await GetRouteAsync(from, to);
            if (foundRoute != null)
                return foundRoute;

            var route = await func.Invoke();
            await SetRouteAsync(route);
            return route;
        }
    }
}
