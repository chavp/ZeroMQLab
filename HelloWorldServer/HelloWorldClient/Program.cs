using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldClient
{
    using NetMQ;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            using (var requester = ctx.CreateRequestSocket())
            {
                requester.Connect("tcp://localhost:5555");

                while (true)
                {
                    Console.WriteLine("Request Hello\n");
                    requester.Send("Hello");

                    string m2 = requester.ReceiveString();
                    Console.WriteLine("From Server: {0}", m2);

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
        }
    }
}
