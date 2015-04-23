using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskVent
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            using (var sender = ctx.CreatePushSocket())
            using (var sink = ctx.CreatePushSocket())
            {
                sender.Bind("tcp://*:5557");

                sink.Connect("tcp://localhost:5558");

                Console.Write("Press Enter when the workers are ready:");
                string msg = Console.ReadLine();
                Console.WriteLine("Sending tasks to workers...");

                sink.Send("0");

                var random = new Random();
                int task_nbr, 
                    total_msec = 0;

                for (task_nbr = 0; task_nbr < 100; task_nbr++)
                {
                    var workload = random.Next(1, 100);

                    sender.Send(workload.ToString());

                    total_msec += workload;
                }

                Console.WriteLine("Total expected cost: " + total_msec.ToString());

                Console.ReadLine();
            }
        }
    }
}
