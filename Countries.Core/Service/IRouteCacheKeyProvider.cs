using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Service
{
    public interface IRouteCacheKeyProvider
    {
        public string Create(string from, string to);
    }
}
