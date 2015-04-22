using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldServer
{
    using NetMQ;
    using ZeroMQ;

    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            using (var responder = ctx.CreateResponseSocket())
            {
                responder.Bind("tcp://*:5555");

                while (true)
                {
                    Console.WriteLine("Wait for Received Hello\n");
                    string m1 = responder.ReceiveString();
                    Console.WriteLine("Received Hello\n");

                    responder.Send("World");
                }
            }
        }
    }
}
