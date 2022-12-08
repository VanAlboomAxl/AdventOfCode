using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day4 : Day
    {
        public override int _iDay { get { return 4; } }
      
        private bool check(string s)
        {
            string[] sRanges = s.Split(',');

            string[] s1 = sRanges[0].Split('-');
            int min1 = int.Parse(s1[0]);
            int max1 = int.Parse(s1[1]);

            string[] s2 = sRanges[1].Split('-');
            int min2 = int.Parse(s2[0]);
            int max2 = int.Parse(s2[1]);

            if (min1 <= min2 && max1 >=max2) return true;
            if (min2 <= min1 && max2 >=max1) return true;
            return false;
        }

        public override string Q1()
        {
            long lResult = 0;
            foreach (var s in Data) if (check(s)) lResult++;
            //Console.WriteLine(lResult);
            return lResult.ToString();
        }
      
        public override string Q2()
        {
            long lResult = 0;
            foreach (var s in Data) if (check2(s)) lResult++;
            //Console.WriteLine(lResult);
            return lResult.ToString();
        }
        private bool check2(string s)
        {
            string[] sRanges = s.Split(',');

            string[] s1 = sRanges[0].Split('-');
            int min1 = int.Parse(s1[0]);
            int max1 = int.Parse(s1[1]);
            List<int> l1 = new();
            for (int i = min1; i <= max1; i++)l1.Add(i); 

            string[] s2 = sRanges[1].Split('-');
            int min2 = int.Parse(s2[0]);
            int max2 = int.Parse(s2[1]);
            List<int> l2 = new();
            for (int i = min2; i <= max2; i++) l2.Add(i);

            List<int> common = l1.Intersect(l2).ToList();

            if (common.Count > 0) return true;
            return false;
        }
    }
}
