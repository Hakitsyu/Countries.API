using Countries.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Service
{
    public interface IInMemoryCountriesCache
    {
        public Task<IList<Country>?> GetAllAsync();
        public Task SetAllAsync(IList<Country> countries);
        public Task<IList<Country>> GetAllOrSetAsync(Func<Task<IList<Country>>> func);
    }
}
