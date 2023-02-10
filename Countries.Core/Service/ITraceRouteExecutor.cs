using Countries.Core.Commands;
using Countries.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Service
{
    public interface ITraceRouteExecutor
    {
        public Task<Route> Execute(TraceRouteCommand command);
    }
}
