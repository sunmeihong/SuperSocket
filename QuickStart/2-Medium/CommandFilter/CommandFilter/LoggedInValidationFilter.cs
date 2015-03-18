using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Metadata;

namespace SuperSocket.QuickStart.CommandFilter
{
    public class LoggedInValidationFilter : CommandFilterAttribute
    {
        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {
            Console.WriteLine(string.Format("{0}：LoggedInValidationFilter类中的OnCommandExecuting方法", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            var session = commandContext.Session as MyAppSession;

            //If the session is not logged in, cancel the executing of the command
            if (!session.IsLoggedIn)
                commandContext.Cancel = true;
        }

        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            Console.WriteLine(string.Format("{0}：LoggedInValidationFilter类中的OnCommandExecuted方法", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        }
    }
}
