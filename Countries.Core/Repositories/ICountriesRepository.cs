using Countries.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Repositories
{
    public interface ICountriesRepository
    {
        Task<IList<Country>> GetAll();
        Task<Country?> Get(string id);
    }
}
