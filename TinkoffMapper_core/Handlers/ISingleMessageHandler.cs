
namespace TinkoffMapper.Handlers
{
    public interface ISingleMessageHandler<out T>
    {
        T HandleSingle(string message);
    }
}
