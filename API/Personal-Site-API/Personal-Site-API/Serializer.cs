using FluentCache.Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Personal_Site_API
{
    public class Serializer : ISerializer
    {
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}