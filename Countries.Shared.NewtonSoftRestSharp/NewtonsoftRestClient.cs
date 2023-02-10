using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Shared.NewtonSoftRestSharp
{
    public class NewtonsoftRestClient : RestClient
    {
        public NewtonsoftRestClient()
            => Initialize();

        public NewtonsoftRestClient([NotNull] Uri baseUrl) : base(baseUrl)
            => Initialize();

        public NewtonsoftRestClient([NotNull] string baseUrl) : base(baseUrl)
            => Initialize();

        public NewtonsoftRestClient([NotNull] RestClientOptions options, 
            Action<HttpRequestHeaders>? configureDefaultHeaders = null) : base(options, configureDefaultHeaders)
            => Initialize();

        public NewtonsoftRestClient([NotNull] HttpClient httpClient, 
            bool disposeHttpClient = false) : base(httpClient, disposeHttpClient)
            => Initialize();

        public NewtonsoftRestClient([NotNull] HttpMessageHandler handler, 
            bool disposeHandler = true) : base(handler, disposeHandler)
            => Initialize();

        public NewtonsoftRestClient([NotNull] HttpClient httpClient, 
            [NotNull] RestClientOptions options, 
            bool disposeHttpClient = false) : base(httpClient, options, disposeHttpClient)
            => Initialize();

        private void Initialize()
            => UseSerializer<NewtonsoftJsonSerializer>();
    }
}
