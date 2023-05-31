using JetBrains.Annotations;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;

namespace TinkoffMapper.Handlers
{
    internal abstract class CommonJsonHandler
    {
        [NotNull]
        protected static JsonSerializer Serializer { get; } = new JsonSerializer
        {
            Converters =
            {
                new StringEnumConverter(),
                new ProtoMessageConverter()
            },
        };

        [NotNull]
        protected FormatException NewFormatException([CanBeNull] string actualMessage) =>
            new FormatException($@"Unable to parse the message: {actualMessage}");

        [NotNull]
        protected FormatException NewFormatException([CanBeNull] string actualMessage, [NotNull] JsonException ex) =>
            new FormatException($@"Unable to parse the message: {actualMessage}", ex);
    }
}
