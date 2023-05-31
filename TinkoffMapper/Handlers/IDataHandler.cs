

namespace TinkoffMapper.Handlers
{
    public interface IDataHandler<out T> : ISingleMessageHandler<T>, ISnapshotHandler<T>
    {
    }
}
