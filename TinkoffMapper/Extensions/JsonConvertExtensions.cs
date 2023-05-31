using Google.Protobuf;
using Newtonsoft.Json;
using TinkoffMapper.Handlers;

namespace TinkoffMapper.Extensions
{
    public static class JsonConvertExtensions
    {
        public static T Deserialize<T>(this string json)
        {
            var settings = JsonParser.Settings.Default.WithIgnoreUnknownFields(true);
            var t = new JsonParser(settings).Parse<>(json);
        }

        // JsonConvert.DeserializeObject<T>(json, new ProtoMessageConverter());
        public static string Serialize(this object obj) =>
            JsonConvert.SerializeObject(obj, new ProtoMessageConverter());
    }
}
