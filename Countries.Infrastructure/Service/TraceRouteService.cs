using Countries.Core.Commands;
using Countries.Core.Dto;
using Countries.Core.Models;
using Countries.Core.Repositories;
using Countries.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Service
{
    public class TraceRouteService : ITraceRouteService
    {
        private ITraceRouteRepository _repository;
        private ITraceRouteExecutor _executor;

        public TraceRouteService(ITraceRouteRepository repository, ITraceRouteExecutor executor)
            => (_repository, _executor) = (repository, executor);

        public async Task<Route> Trace(TraceRouteCommand command)
        {
            Route? foundRoute = await _repository.GetRouteAsync(command.From, command.To);
            if (foundRoute != null)
                return foundRoute;

            Route? route = await _executor.Execute(command);
            await _repository.SaveRouteAsync(route);
            return route;
        }
    }
}
