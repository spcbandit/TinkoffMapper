using System.Collections.Generic;
using System.ComponentModel;

namespace TinkoffMapper.Requests
{
    /// <summary>
    /// Общий вид реквеста
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class RequestInvest
    {
        internal abstract string EndPoint { get; } 
        internal abstract RequestMethod Method { get; }
        public virtual IDictionary<string, string> Headers { get; set; }
        internal virtual object Body { get; }
        /// <summary>
        /// Эндпоинт для песочницы
        /// </summary>
        internal virtual string SandboxEndPoint { get; }
    }
}
