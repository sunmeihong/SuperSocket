using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocket.QuickStart.BroadcastService.Command
{
    /// <summary>
    /// 此命令由C001,C002,C003专用   其他连接，虽然也可以调用，但是BroadcastMessage是无法触发相应的广播的
    /// </summary>
    public class BROA : StringCommandBase<BroadcastSession>
    {
        public override void ExecuteCommand(BroadcastSession session, StringRequestInfo requestInfo)
        {
            string message = requestInfo.Body;
            session.AppServer.BroadcastMessage(session, message);
            session.Send(string.Format("\"{0}\" broadcasted", message));
        }
    }
}
