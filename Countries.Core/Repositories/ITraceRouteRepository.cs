using Countries.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Repositories
{
    public interface ITraceRouteRepository
    {
        public Task<Route?> GetRouteAsync(string from, string to);
        public Task SaveRouteAsync(Route route);
    }
}
