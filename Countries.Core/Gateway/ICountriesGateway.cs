using Countries.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Gateway
{
    public interface ICountriesGateway
    {
        Task<IList<Country>> GetAllAsync(); 
    }
}
