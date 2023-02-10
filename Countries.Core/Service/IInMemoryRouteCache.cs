using Countries.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Service
{
    public interface IInMemoryRouteCache
    {
        public Task<Route?> GetRouteAsync(string from, string to);
        public Task SetRouteAsync(Route route);
        public Task<Route> GetRouteOrSetAsync(string from, string to, Func<Task<Route>> func);
    }
}
