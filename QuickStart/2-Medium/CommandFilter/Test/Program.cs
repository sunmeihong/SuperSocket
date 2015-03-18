using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.QuickStart.CommandFilter;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var appServer = new MyAppServer();
            bool result = appServer.Setup(2012);
            if (result == false)
            {
                Console.WriteLine("Failed to setup");
                Console.ReadKey();
                return;
            }

            result = appServer.Start();
            if (result == false)
            {
                Console.WriteLine("Failed to start");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();
            //Stop the appServer
            appServer.Stop();

            Console.WriteLine("The server was stopped!");

            Console.ReadKey();
        }
    }
}
