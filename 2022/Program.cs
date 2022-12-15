using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day15 oDay = new();
            //oDay.Testing = true;
            Stopwatch watch = new();
            watch.Reset();
            Console.WriteLine($"{oDay.GetType()}:");
            watch.Start();
            Console.WriteLine(oDay.Q1() + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(oDay.Q2() + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");
        }
    }
}
