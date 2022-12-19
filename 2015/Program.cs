using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day5 oDay = new();
            //oDay.Testing = true;
            Stopwatch watch = new();
            watch.Reset();
            Console.WriteLine($"{oDay.GetType()}:");
            watch.Start();
            Console.WriteLine(watch.Elapsed + " - " + oDay.Q1());
            watch.Restart();
            Console.WriteLine(watch.Elapsed + " - " + oDay.Q2());
            watch.Stop();
            Console.WriteLine("__________________________________");
        }
    }
}
