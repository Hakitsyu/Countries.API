using Countries.Core.Gateway;
using Countries.Core.Repositories;
using Countries.Core.Service;
using Countries.Infrastructure.Gateway;
using Countries.Infrastructure.Repository;
using Countries.Infrastructure.Service;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCountriesGateway(this IServiceCollection services, CountriesGatewayOptions options)
        {
            services.AddSingleton(options);
            services.AddSingleton<ICountriesGateway, CountriesGateway>();
            return services;
        }

        public static IServiceCollection AddCountriesService(this IServiceCollection services)
        {
            services.AddSingleton<ICountriesRepository, CountriesRepository>();
            services.AddSingleton<ITraceRouteRepository, TraceRouteRepository>();
            services.AddSingleton<ITraceRouteExecutor, TraceRouteExecutor>();
            services.AddSingleton<ICountriesService, CountriesService>();
            services.AddSingleton<ITraceRouteService, TraceRouteService>();
            services.AddSingleton<ICountriesCacheKeyProvider, CountriesCacheKeyProvider>();
            services.AddSingleton<IRouteCacheKeyProvider, RouteCacheKeyProvider>();
            return services;
        }

        public static IServiceCollection AddDistributedCountriesCache(this IServiceCollection services, Action<SqlServerCacheOptions> action)
        {
            services.AddDistributedSqlServerCache(action);
            services.AddSingleton<IDistributedCountriesCache, DistributedCountriesCache>();
            services.AddSingleton<IDistributedRouteCache, DistributedRouteCache>();
            return services;
        }

        public static IServiceCollection AddInMemoryCountriesCache(this IServiceCollection services)
        {
            services.AddSingleton<IInMemoryCountriesCache, InMemoryCountriesCache>();
            services.AddSingleton<IInMemoryRouteCache, InMemoryRouteCache>();
            return services;
        }
    }
}
