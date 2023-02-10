using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Models
{
    public class Country : IEquatable<Country>
    {
        [JsonProperty("alpha3Code")]
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("alpha2Code")]
        public string Symbol { get; set; }

        public IList<Currency> Currencies { get; set; }

        public string Capital { get; set; }

        public IList<Language> Languages { get; set; }

        public IDictionary<string, string> Flags { get; set; }

        [JsonProperty("regionalBlocs")]
        public IList<RegionalBlock> RegionalBlocks { get; set; }

        public IList<string> Borders { get; set; }

        public bool Equals(Country? other)
            => other != null && Id == other.Id;
    }
}
