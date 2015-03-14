using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.QuickStart.SampleServer;
using SuperSocket.SocketEngine;
using SuperSocket.SocketBase;

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
                Console.WriteLine("初始化失败");
                return;
            }

            StartResult result = bootstrap.Start();
            Console.WriteLine(string.Format("Start Result:{0}", result));
            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                return;
            }
            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                System.Threading.Thread.Sleep(1000);
                continue;
            }

            Console.WriteLine();

            bootstrap.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }
    }
}
