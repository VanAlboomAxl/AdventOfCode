using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day18 : Day
    {
        public override int _iDay { get { return 18; } }



        public override void Q1()
        {
            string sTest1 = "1 + 2 * 3 + 4 * 5 + 6"; // 71
            string sTest2 = "1 + (2 * 3) + (4 * (5 + 6))"; // 51
            string sTest3 = "2 * 3 + (4 * 5)"; // 36
            string sTest4 = "5 + (8 * 3 + 9 + 3 * 4 * 3)"; // 437
            string sTest5 = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"; // 12240
            string sTest6 = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"; // 13632
            //var l = LineLogic(sTest6);

            long lResult = 0;
            foreach(var d in Data)
            {
                lResult += LineLogic(d);
            }
            Console.WriteLine(lResult);

        }

        public long LineLogic(string s)
        {
            s = s.Replace(" ", "");
            string sRegexString = @"\(((?:\d+(\+|\*))+\d+)\)";
            Regex rg = new Regex(sRegexString);

            MatchCollection matches = rg.Matches(s);
            while (matches.Count > 0)
            {
                foreach (Match m in matches)
                    s = s.Replace(m.Value, simple(m.Value).ToString());
                matches = rg.Matches(s);
            }
            return simple(s);
        }
        public long simple(string s)
        {
            Func<long, long, long> math = sum;
            long result = 0;
            string sNumber = "";
            foreach(var c in s)
            {
                if (c.Equals('(') || c.Equals(')')) continue;
                if (c.Equals('+'))
                {
                    result = math(result, long.Parse(sNumber));
                    math = sum;
                    sNumber = "";
                }
                else if (c.Equals('*'))
                {
                    result = math(result, long.Parse(sNumber));
                    math = product;
                    sNumber = "";
                }
                else 
                    sNumber += c;
            }
            return math(result, long.Parse(sNumber)); ;
        }

        long sum(long a, long b) { return a+b;}
        long product(long a, long b) { return a * b; }

        public override void Q2()
        {
            long LineLogic(string s)
            {
                s = s.Replace(" ", "");
                string sRegexString = @"\(((?:\d+(\+|\*))+\d+)\)";
                Regex rg = new Regex(sRegexString);

                MatchCollection matches = rg.Matches(s);
                while (matches.Count > 0)
                {
                    foreach (Match m in matches)
                        s = s.Replace(m.Value, simple(m.Value).ToString());
                    matches = rg.Matches(s);
                }
                return simple(s);
            }
            long simple(string s)
            {
                s = s.Replace("(", "").Replace(")", "");
                string sRegexString = @"(\d+)(\+)(\d+)";
                Regex rg = new Regex(sRegexString);

                MatchCollection matches = rg.Matches(s);
                while (matches.Count > 0)
                {
                    Match m = matches[0];
                    var x = m.Value.Split("+");
                    s = s.Replace(m.Value, (long.Parse(x[0]) + long.Parse(x[1])).ToString());
                    matches = rg.Matches(s);
                }

                long result = 1;
                string sNumber = "";
                foreach (var c in s)
                {
                    if (c.Equals('*'))
                    {
                        result = product(result, long.Parse(sNumber));
                        sNumber = "";
                    }
                    else  sNumber += c;
                }
                return product(result, long.Parse(sNumber));

            }

            string sTest1 = "1 + 2 * 3 + 4 * 5 + 6"; // 71
            string sTest2 = "1 + (2 * 3) + (4 * (5 + 6))"; // 51
            string sTest3 = "2 * 3 + (4 * 5)"; // 36
            string sTest4 = "5 + (8 * 3 + 9 + 3 * 4 * 3)"; // 437
            string sTest5 = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"; // 12240
            string sTest6 = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"; // 13632
            //var l = LineLogic(sTest6);

            long lResult = 0;
            foreach (var d in Data) lResult += LineLogic(d);
            Console.WriteLine(lResult);

        }
        

    }
}
