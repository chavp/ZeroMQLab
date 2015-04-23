using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicksPubClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            using (var subscriber = ctx.CreateSubscriberSocket())
            {
                subscriber.Connect("tcp://localhost:5555");
                subscriber.Subscribe("ticks");

                while (true)
                {
                    string m2 = subscriber.ReceiveString();
                    Console.WriteLine("Receive Ticks: {0}", m2);

                }
            }
        }
    }
}
