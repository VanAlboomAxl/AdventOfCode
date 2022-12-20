using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day20 oDay = new();
            //oDay.Testing = true;
            Stopwatch watch = new();
            watch.Reset();
            Console.WriteLine($"{oDay.GetType()}:");
            watch.Start();
            string q1 = oDay.Q1();
            Console.WriteLine(watch.Elapsed + " - " + q1);
            watch.Restart();
            string q2 = oDay.Q2();
            Console.WriteLine(watch.Elapsed + " - " + q2);
            watch.Stop();
            Console.WriteLine("__________________________________");
        }
    }
}
