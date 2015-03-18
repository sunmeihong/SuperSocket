using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Metadata;

namespace SuperSocket.QuickStart.CommandFilter
{
    public class CountCommandFilter : CommandFilterAttribute
    {
        private long m_Total = 0;

        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {
            Console.WriteLine(string.Format("{0}：CountCommandFilter类中的OnCommandExecuting方法", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        }

        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            Console.WriteLine(string.Format("{0}：CountCommandFilter类中的OnCommandExecuted方法", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            Interlocked.Increment(ref m_Total);
        }

        public long Total
        {
            get { return m_Total; }
        }
    }
}
