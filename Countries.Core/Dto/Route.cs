using Countries.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Dto
{
    public class Route
    {
        public SimplifiedCountry From { get; set; }
        public SimplifiedCountry To { get; set; }
        public IList<SimplifiedCountry> Steps { get; set; }
    }
}
