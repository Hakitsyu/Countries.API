using Countries.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Dto
{
    public class SimplifiedCountry
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static SimplifiedCountry From(Country country)
            => new SimplifiedCountry
            {
                Id = country.Id,
                Name = country.Name
            };

        public static IList<SimplifiedCountry> From(IEnumerable<Country> countries)
            => countries.Select(country => From(country)).ToList();
    }
}
