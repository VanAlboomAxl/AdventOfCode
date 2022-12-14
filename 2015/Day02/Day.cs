using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day2 : Day
    {
        public override int _iDay { get { return 2; } }
        
        public override string Q1()
        {
            var lsInput = Data;
            long lResult = 0;
            foreach(var s in lsInput)
            {
                string[] cube = s.Split('x');
                long w = long.Parse(cube[0]);
                long h = long.Parse(cube[1]);
                long b = long.Parse(cube[2]);
                List<long> lAreas = new();
                lAreas.Add( w * h);
                lAreas.Add( w * b);
                lAreas.Add( h * b);
                lAreas = lAreas.OrderBy(x => x).ToList();
                lResult += (3 * lAreas[0] + 2 * lAreas[1] + 2 * lAreas[2]);

            }
            return lResult.ToString();
        }

        public override string Q2()
        {
            var lsInput = Data;
            long lResult = 0;
            foreach (var s in lsInput)
            {
                string[] cube = s.Split('x');
                List<long> lAreas = new();
                lAreas.Add(long.Parse(cube[0]));
                lAreas.Add(long.Parse(cube[1]));
                lAreas.Add(long.Parse(cube[2]));
                lAreas = lAreas.OrderBy(x => x).ToList();
                lResult += (2 * lAreas[0] + 2 * lAreas[1] + lAreas[0] * lAreas[1] * lAreas[2]);

            }
            return lResult.ToString();
        }
    }

}