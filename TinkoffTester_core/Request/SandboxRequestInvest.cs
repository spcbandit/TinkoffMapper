using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkoffMapper.Requests;

namespace TinkoffTester.Request
{
    /// <summary>
    /// Общий вид реквеста для песочницы
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class SandboxRequestInvest
    {
        internal abstract string EndPoint { get; }
        internal abstract RequestMethod Method { get; }
        public virtual IDictionary<string, string> Headers { get; set; }
        internal virtual object Body { get; }
    }
}
