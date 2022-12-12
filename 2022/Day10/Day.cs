using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day10: Day
    {
        public override int _iDay { get { return 10; } }

        public override string Q1()
        {
            //long lCylce = 1;
            //long loutput = 1;
            //List<long> Outcomes = new List<long>();
            //foreach(var d in Data)
            //{
            //    Outcomes.Add(loutput);
            //    string[] split = d.Split(" ");
            //    if (!split[0].Equals("noop"))
            //    {
            //        Outcomes.Add(loutput);
            //        loutput += long.Parse(split[0]);
            //        lCylce++;
            //    }
            //    lCylce++;
            //}
            //int[] iChecks = new int[] {20,60,100,140,180,220 };
            //for(int i = 0; i < iChecks)
            //{

            //}

            List<int> iChecks = new() { 20, 60, 100, 140, 180, 220 };
            int lCylce = 0;
            long lOutput = 1;
            long lResult = 0;
            foreach (var d in Data)
            {
                lCylce++;
                if (iChecks.Contains(lCylce))
                {
                    lResult += lOutput * lCylce;
                }
                string[] split = d.Split(" ");
                if (!split[0].Equals("noop"))
                {
                    lCylce++;
                    if (iChecks.Contains(lCylce))
                    {
                        lResult += lOutput * lCylce;
                    }
                    lOutput += long.Parse(split[1]);
                }
            }

            return lResult.ToString();
        }

        public override string Q2()
        {

            List<long> Outcomes = new List<long>();
            int lCylce = 0;
            long lOutput = 1;
            foreach (var d in Data)
            {
                lCylce++;
                Outcomes.Add(lOutput);
                string[] split = d.Split(" ");
                if (!split[0].Equals("noop"))
                {
                    lCylce++;
                    Outcomes.Add(lOutput);
                    lOutput += long.Parse(split[1]);
                }
            }

            for(int ij = 0; ij < 240; ij++)
            {
                if (ij % 40 == 0) Console.WriteLine();
                char c = '.';
                long iDrawing = ij % 40;
                long l = Outcomes[ij];
                List<long> active = new() { l, l - 1, l + 1 };
                if (active.Contains(iDrawing)) c = '#';
                Console.Write(c);
            }

            //for (int i = 1; i < 7;i++)
            //{
            //    for(int j=1; j < 41; j++)
            //    {
            //        char c = '.';
            //        int ij = i * j;
            //        long l = Outcomes[ij-1] % 40;
            //        List<long> active = new() { l, l - 1, l + 1 };
            //        long iCycle = ij%40-1;
            //        if (active.Contains(iCycle)) c = '#';
            //        Console.Write(c);
            //    }
            //    Console.WriteLine();
            //}

            return "";
        }
    }
}