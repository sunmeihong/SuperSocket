using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;

namespace SuperSocket.QuickStart.BroadcastService
{
    public class BroadcastServer : AppServer<BroadcastSession>
    {
        /// <summary>
        /// 广播集合，key是某一个广播，value是需要广播的对象
        /// </summary>
        private Dictionary<string, List<string>> broadcastDict = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 用于lock的对象  同步broadcastDict
        /// </summary>
        private object broadcastSyncRoot = new object();

        /// <summary>
        /// session的集合,Key是C001,C002,C003以及V001,V002,V003这些
        /// </summary>
        private Dictionary<string, BroadcastSession> broadcastSessionDict = new Dictionary<string, BroadcastSession>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 用于lock的对象  同步broadcastSessionDict
        /// </summary>
        private object syncRoot = new object();

        public BroadcastServer()
        {
            lock (broadcastSyncRoot)
            {
                //It means that device V001 will receive broadcast messages from C001 and C002,
                //device V002  will receive broadcast messages from C002 and C003
                broadcastDict["C001"] = new List<string> { "V001" };
                broadcastDict["C002"] = new List<string> { "V001", "V002" };
                broadcastDict["C003"] = new List<string> { "V002" };
            }
        }

        internal void RegisterNewSession(BroadcastSession session)
        {
            if (string.IsNullOrEmpty(session.DeviceNumber))
                return;

            lock (syncRoot)
            {
                broadcastSessionDict[session.DeviceNumber] = session;
            }
        }

        internal void RemoveOnlineSession(BroadcastSession session)
        {
            if (string.IsNullOrEmpty(session.DeviceNumber))
                return;

            lock (syncRoot)
            {
                broadcastSessionDict.Remove(session.DeviceNumber);
            }
        }

        internal void BroadcastMessage(BroadcastSession session, string message)
        {
            List<string> targetDeviceNumbers;//不需要初始化，作为out参数的时候

            lock (broadcastSyncRoot)
            {
                //key: 要获取的值的键。
                //value:当此方法返回值时，如果找到该键，便会返回与指定的键相关联的值；
                //否则，则会返回 value 参数的类型默认值。该参数未经初始化即被传递。
                bool flag = broadcastDict.TryGetValue(session.DeviceNumber, out targetDeviceNumbers);
                if (flag == false)
                {
                    return;
                }
            }

            if (targetDeviceNumbers == null || targetDeviceNumbers.Count <= 0)
                return;

            List<BroadcastSession> sessions = new List<BroadcastSession>();

            lock (syncRoot)
            {
                BroadcastSession s;

                foreach(var key in targetDeviceNumbers)
                {
                    if (broadcastSessionDict.TryGetValue(key, out s))
                    {
                        sessions.Add(s);
                    }
                }
            }

            this.AsyncRun(() =>
                {
                    sessions.ForEach(s => s.Send(message));
                });
        }

        protected override void OnSessionClosed(BroadcastSession session, CloseReason reason)
        {
            RemoveOnlineSession(session);
            base.OnSessionClosed(session, reason);
        }
    }
}
