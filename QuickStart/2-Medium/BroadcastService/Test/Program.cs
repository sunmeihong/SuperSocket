using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperSocket.QuickStart.BroadcastService;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrap = BootstrapFactory.CreateBootstrap();
            bool flag = bootstrap.Initialize();
            if (flag == false)
            {
                Console.WriteLine("初始化配置失败");
                return;
            }

            StartResult result = bootstrap.Start();
            Console.WriteLine(string.Format("Start Result:{0}", result));
            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                return;
            }
            Console.ReadKey();
        }
    }
}
