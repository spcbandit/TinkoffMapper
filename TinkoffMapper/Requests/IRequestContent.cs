using JetBrains.Annotations;

using System.Collections.Generic;

namespace TinkoffMapper.Requests
{
    public interface IRequestContent
    {
        RequestMethod Method { get; }

        [NotNull]
        string Query { get; }

        /// <summary>
        /// Authorized requests only; null otherwise
        /// </summary>
        [CanBeNull]
        IReadOnlyDictionary<string, string> Headers { get; }

        [CanBeNull]
        object Body { get; }
    }
}
