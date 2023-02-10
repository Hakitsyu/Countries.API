using Countries.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infrastructure.Service
{
    public class RouteCacheKeyProvider : IRouteCacheKeyProvider
    {
        private const string KEY = "route";

        public string Create(string from, string to)
            => $"{KEY}-{from}-{to}";
    }
}
