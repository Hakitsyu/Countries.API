using Countries.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Countries.Tests
{
    public class JsonConverterTests
    {
        [Fact]
        public void Deserialize_Currency()
        {
            string json = @"
                {
			        ""BRL"": {
				        ""name"": ""Brazilian real"",
				        ""symbol"": ""R$""
			        }
		        }
                ";

            var currencies = JsonConvert.DeserializeObject<IDictionary<string, Currency>>(json);
            var brl = currencies.FirstOrDefault().Value;
            Assert.Equal(brl.Name, "Brazilian real");
            Assert.Equal(brl.Symbol, "R$");
        }
    }
}
