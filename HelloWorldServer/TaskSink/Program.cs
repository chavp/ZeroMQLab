using NetMQ;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSink
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            using (var receiver = ctx.CreatePullSocket())
            {
                receiver.Bind("tcp://*:5558");


                // Wait for start of batch
                Console.WriteLine("Wait for start of batch");
                var begin = receiver.ReceiveString();

                var timer = Stopwatch.StartNew();
                Console.WriteLine("Start our clock now");

                int task_nbr;
                for (task_nbr = 0; task_nbr < 100; task_nbr++)
                {
                    var workload = receiver.ReceiveString();
                    if ((task_nbr / 10) * 10 == task_nbr)
                        Console.Write(":");
                    else
                        Console.Write(".");
                }

                Console.WriteLine();
                // Calculate and report duration of batch
                Console.WriteLine(
                    string.Format("Total elapsed time: {0} msec", 
                    timer.ElapsedMilliseconds)
                    );

                Console.ReadLine();
            }
        }
    }
}
