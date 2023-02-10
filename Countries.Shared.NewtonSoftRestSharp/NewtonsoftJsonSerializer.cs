using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Shared.NewtonSoftRestSharp
{
    internal class NewtonsoftJsonSerializer : IRestSerializer, ISerializer, IDeserializer
    {
        public ISerializer Serializer => this;

        public IDeserializer Deserializer => this;

        public string[] AcceptedContentTypes => new[] { "application/json" };

        public SupportsContentType SupportsContentType => contentType => true;

        public DataFormat DataFormat => DataFormat.Json;

        public string ContentType { get; set; }

        public T? Deserialize<T>(RestResponse response)
        {
            var content = response.Content;
            return string.IsNullOrWhiteSpace(content) ? default(T) : JsonConvert.DeserializeObject<T>(content);
        }

        public string? Serialize(Parameter parameter)
            => JsonConvert.SerializeObject(parameter.Value);

        public string? Serialize(object obj)
            => JsonConvert.SerializeObject(obj);
    }
}
