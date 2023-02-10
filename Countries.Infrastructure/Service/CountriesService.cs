using Countries.Core.Gateway;
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
    public class CountriesService : ICountriesService
    {
        private ICountriesRepository _repository;

        public CountriesService(ICountriesRepository repository)
            => _repository = repository;

        public async Task<IList<Country>> GetAll()
            => await _repository.GetAll();

    }
}
