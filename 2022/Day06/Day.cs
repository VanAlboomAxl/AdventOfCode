using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day6 : Day
    {
        public override int _iDay { get { return 6; } }
      
        public long logic(string s, int length)
        {
            for(int i = 0; i < s.Length - length-1; i++)
                if (s.Substring(i, length).Distinct().Count() == length)
                    return i + length;
            return -1;
        }

        public override string Q1()
        {
            foreach(var s in Test) Console.WriteLine(logic(s,4));
            return logic(Input[0],4).ToString();
        }

        public override string Q2()
        {
            foreach (var s in Test) Console.WriteLine(logic(s, 14));
            return logic(Input[0], 14).ToString();
        }

    }
}
