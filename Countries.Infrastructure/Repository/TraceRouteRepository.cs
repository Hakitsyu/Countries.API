using Countries.Core.Dto;
using Countries.Core.Repositories;
using Countries.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Repository
{
    public class TraceRouteRepository : ITraceRouteRepository
    {
        private IDistributedRouteCache _distributedCache;
        private IInMemoryRouteCache _memoryCache;

        public TraceRouteRepository(IDistributedRouteCache distributedCache, IInMemoryRouteCache memoryCache)
            => (_distributedCache, _memoryCache) = (distributedCache, memoryCache);

        public async Task<Route?> GetRouteAsync(string from, string to)
        {
            var inMemoryRoute = await _memoryCache.GetRouteAsync(from, to);
            if (inMemoryRoute != null)
                return inMemoryRoute;

            var inDistributedRoute = await _distributedCache.GetRouteAsync(from, to);
            if (inDistributedRoute != null)
                await _memoryCache.SetRouteAsync(inDistributedRoute);

            return inDistributedRoute;
        }

        public async Task SaveRouteAsync(Route route)
            => await Task.WhenAll(_memoryCache.SetRouteAsync(route), _distributedCache.SetRouteAsync(route));
    }
}
