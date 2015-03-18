using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Metadata;

namespace SuperSocket.QuickStart.CommandFilter
{
    public class LogTimeCommandFilter : CommandFilterAttribute
    {
        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {
            Console.WriteLine(string.Format("{0}：LogTimeCommandFilter类中的OnCommandExecuting方法", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            commandContext.Session.Items["StartTime"] = DateTime.Now;
        }

        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            Console.WriteLine(string.Format("{0}：LogTimeCommandFilter类中的OnCommandExecuted方法", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            var session = commandContext.Session;
            var startTime = session.Items.GetValue<DateTime>("StartTime");
            var ts = DateTime.Now.Subtract(startTime);

            if (ts.TotalSeconds > 5 && session.Logger.IsInfoEnabled)
            {
                session.Logger.InfoFormat("A command '{0}' took {1} seconds!", commandContext.CurrentCommand.Name, ts.ToString());
            }
        }
    }
}
