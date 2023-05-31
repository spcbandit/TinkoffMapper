using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;

namespace TinkoffMapper.Handlers
{
    internal class BaseJsonHandler<T> : CommonJsonHandler, IDataHandler<T>
    {
        public T HandleSingle(string message)
        {
            try
            {
                return Build(JToken.Parse(message));
            }
            catch (JsonException ex)
            {
                throw NewFormatException(message, ex);
            }
        }

        public IReadOnlyList<T> HandleSnapshot(string message)
        {
            try
            {
                return JToken.Parse(message).ToObject<List<T>>(Serializer);
            }
            catch (JsonException ex)
            {
                throw NewFormatException(message, ex);
            }
        }

        protected virtual T Build(JToken token)
        {
            using (var jsonReader = new JTokenReader(token))
            {
                return Serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
