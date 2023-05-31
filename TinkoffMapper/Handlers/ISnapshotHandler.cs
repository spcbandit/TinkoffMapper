using System.Collections.Generic;

namespace TinkoffMapper.Handlers
{
    public interface ISnapshotHandler<out T>
    {
        IReadOnlyList<T> HandleSnapshot(string message);
    }
}
