using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicksPubServer
{
    using NetMQ;
    using System.Threading;

    /// <summary>
    /// http://www.codeproject.com/Articles/488207/ZeroMQ-via-Csharp-Introduction
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            using (var publisher = ctx.CreatePublisherSocket())
            {
                publisher.Bind("tcp://*:5555");

                while (true)
                {
                    var ticks = DateTime.Now.Ticks.ToString();
                    publisher.SendMore("ticks").Send(ticks);
                    Console.WriteLine(ticks);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
        }
    }
}
