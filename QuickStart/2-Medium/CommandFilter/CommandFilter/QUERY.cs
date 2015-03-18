using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System.Collections;

namespace SuperSocket.QuickStart.CommandFilter
{
    [LoggedInValidationFilter(Order = 0)]
    [LogTimeCommandFilter(Order = 1)]
    public class QUERY : StringCommandBase<MyAppSession>
    {
        public override void ExecuteCommand(MyAppSession session, StringRequestInfo requestInfo)
        {
            try
            {
                //Your code
                if (requestInfo.Parameters == null || requestInfo.Parameters.Length == 0)
                    return;

                var parameter = requestInfo.Parameters[0];
                if (parameter.Equals("Count"))
                {
                    IEnumerable<MyAppSession> sessions = ((MyAppServer)session.AppServer).GetAllSessions();
                    session.Send(string.Format("Connected Session's Count is {0}", sessions.Count()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
