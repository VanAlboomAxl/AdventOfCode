using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day11 oDay = new();
            //oDay.Testing = true;
            Console.WriteLine("Day1:");
            Console.WriteLine(oDay.Q1());
            Console.WriteLine("Day2:");
            Console.WriteLine(oDay.Q2());
            
        }
    }
}
