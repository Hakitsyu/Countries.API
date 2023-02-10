using Countries.Core.Commands;
using Countries.Core.Dto;
using Countries.Core.Models;
using Countries.Core.Repositories;
using Countries.Core.Service;
using Countries.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Service
{
    public class TraceRouteExecutor : ITraceRouteExecutor
    {
        private ICountriesRepository _repository;

        public TraceRouteExecutor(ICountriesRepository repository)
            => _repository = repository;

        public async Task<Route> Execute(TraceRouteCommand command)
        {
            var fromCountry = await _repository.Get(command.From);
            var toCountry = await _repository.Get(command.To);

            if (fromCountry == null || toCountry == null)
                throw new NotFoundCountryException();

            using (var recursiveExecutor = new RecursiveTraceRouteExecutor(_repository))
            {
                await recursiveExecutor.Execute(fromCountry, toCountry);
                if (!recursiveExecutor.ExistsEntry())
                    throw new NotFoundRouteException();

                var steps = recursiveExecutor.GetSmallerEntry();
                return new Route
                {
                    From = SimplifiedCountry.From(fromCountry),
                    To = SimplifiedCountry.From(toCountry),
                    Steps = SimplifiedCountry.From(steps)
                };
            };
        }

        internal class RecursiveTraceRouteExecutor : IDisposable
        {
            public List<IList<Country>> Entries { get; } = new();
            public int? MinEntry { get; set; }

            public RecursiveTraceRouteExecutorOptions Options { get; }
            public bool AlreadyReachedMaxIteratedCount { get; private set; }

            private ICountriesRepository _repository;

            public RecursiveTraceRouteExecutor(RecursiveTraceRouteExecutorOptions options, 
                ICountriesRepository repository)
                => (Options, _repository) = (options, repository);

            public RecursiveTraceRouteExecutor(ICountriesRepository repository) 
                : this(new RecursiveTraceRouteExecutorOptions(), repository) { }

            public async Task Execute(Country from, Country to)
                => await RecursiveExecute(from, to, new List<Country>());

            private async Task RecursiveExecute(Country current, Country expected, IList<Country> iteratedCountries)
            {
                if (AlreadyIterated(iteratedCountries, current) || AlreadyReachedMaxIteratedCount) return;
                if (ReachedMaxIteratedCount(iteratedCountries))
                    AlreadyReachedMaxIteratedCount = true;

                iteratedCountries.Add(current);
                if (MinEntry != null && iteratedCountries.Count >= MinEntry) return;
                
                if (current.Equals(expected))
                {
                    Entries.Add(iteratedCountries);
                    MinEntry = iteratedCountries.Count;
                    return;
                }

                if (current.Borders == null) return;
                foreach (var border in current.Borders)
                {
                    var borderCountry = await _repository.Get(border);
                    if (borderCountry == null) continue;

                    await RecursiveExecute(borderCountry, expected, new List<Country>(iteratedCountries));
                }
            }

            public bool ExistsEntry()
                => Entries.Any();

            public IList<Country>? GetSmallerEntry()
            {
                IList<Country> smaller = null;
                int? size = null;
                
                foreach (IList<Country> entry in Entries)
                {
                    if (size == null || entry.Count < size)
                    {
                        smaller = entry;
                        size = entry.Count;
                    }
                }

                return smaller;
            }

            public void Dispose()
            {
                Entries.Clear();
                GC.Collect();
            }

            private bool ReachedMaxIteratedCount(IList<Country> iteratedCountries)
                => iteratedCountries.Count >= Options.MaxIteratedCount;

            private bool AlreadyIterated(IList<Country> countries, Country country)
                => countries.Contains(country);
        }

        internal class RecursiveTraceRouteExecutorOptions
        {
            [Range(1, int.MaxValue)]
            public int RecursiveLimit { get; set; } = 100;

            [Range(1, int.MaxValue)]
            public int MaxIteratedCount { get; set; } = 200;
        }
    }

}
