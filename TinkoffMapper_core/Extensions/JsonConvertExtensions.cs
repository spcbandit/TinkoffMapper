using Newtonsoft.Json;
using TinkoffMapper.Handlers;

namespace TinkoffMapper.Extensions
{
    public static class JsonConvertExtensions
    {
        public static T Deserialize<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(json, new ProtoMessageConverter());
        public static string Serialize(this object obj) =>
            JsonConvert.SerializeObject(obj, new ProtoMessageConverter());
    }
}
