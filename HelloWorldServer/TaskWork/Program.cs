using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWork
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            using (var receiver = ctx.CreatePullSocket())
            using (var sender = ctx.CreatePushSocket())
            {
                receiver.Connect("tcp://localhost:5557");
                sender.Connect("tcp://localhost:5558");

                while(true)
                {
                    var taskItem = receiver.ReceiveString();
                    var data = int.Parse(taskItem);

                    // Process
                    Thread.Sleep(data);

                    var process = "process: " + data;
                    // Send results to sink
                    sender.Send(process);

                    Console.WriteLine(process);
                }
            }
        }
    }
}
