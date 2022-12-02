using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day13 : Day
    {
        public override int _iDay { get { return 13; } }

        public override void Q1()
        {
            var lsInput = Input;
            //lsInput = Test;

            int iEarliestTime = int.Parse(lsInput[0]);

            var lsSplit = lsInput[1].Split(',');
            List<int> liBusses = new();
            List<int> liBusTimes = new();
            foreach (var s in lsSplit)
            {
                int iBus = 0;
                if (int.TryParse(s, out iBus))
                {
                    liBusses.Add(iBus);
                    int iTime = 0;
                    while (iTime < iEarliestTime)
                    {
                        iTime += iBus;
                    }
                    liBusTimes.Add(iTime);
                }
            }
            int nextBus = liBusTimes.Min();
            int index = liBusTimes.IndexOf(nextBus);
            int bus = liBusses[index];
            Console.WriteLine($"{bus}");
            Console.WriteLine($"{nextBus}");
            Console.WriteLine($"{(nextBus-iEarliestTime)*bus}");


        }

        public void Q2_own_Long()
        {
            var lsInput = Input;
            //lsInput = Test;

            int iEarliestTime = int.Parse(lsInput[0]);

            var lsSplit = lsInput[1].Split(',');
            List<int> liBusses = new();
            foreach (var s in lsSplit)
            {
                int iBus = 0;
                if (int.TryParse(s, out iBus)) liBusses.Add(iBus);
                else liBusses.Add(1);
            }

            bool xLoop = true;
            long iTime = liBusses[0];

            // for real data:
            iTime = 100000000000000;
            while (iTime % liBusses[0] != 0)
            {
                iTime++;
            }


            while (xLoop)
            {
                bool xGood = true;
                for(int i = 1; i < liBusses.Count; i++)
                {
                    long iTime2 = iTime + i;
                    if (iTime2 % liBusses[i] != 0)
                    {
                        xGood = false;
                        break;
                    }
                }
                if (xGood)
                {
                    xLoop = false;
                }
                else iTime += liBusses[0];
            }

            Console.WriteLine($"{iTime}");

        }

        public override void Q2()
        {
            var lsInput = Input;
            //lsInput = Test;
            var busses = lsInput[1].Split(',').ToList();
            /*solved by Chinese Remainder Theorem
             N is sum multiply of all modules
             in this case, multily all busses ids
             */
            long N = GetN(busses);
            long sum = 0;
            for (int i = 0; i < busses.Count; i++)
            {
                if (busses[i] == "x")
                {
                    continue;
                }

                int busNumber = int.Parse(busses[i]);
                long module = AbsoluteModulo(busNumber - i, busNumber);
                long Ni = N / busNumber;
                long inverse = GetInverse(Ni, busNumber);

                sum += module * Ni * inverse;
            }

            Console.WriteLine( sum % N);
        }

        private static long GetN(List<string> busses)
        {
            long N = 1;
            for (int i = 0; i < busses.Count; i++)
            {
                if (busses[i] == "x")
                {
                    continue;
                }

                N *= int.Parse(busses[i]);
            }

            return N;
        }

        private static long GetInverse(long nU, int cur)
        {
            var b = nU % cur;
            for (int i = 1; i < cur; i++)
            {
                if ((b * i) % cur == 1)
                {
                    return i;
                }
            }

            return 1;
        }

        private static long AbsoluteModulo(int v, int cur)
        {
            return ((v % cur) + cur) % cur;
        }

    }
}
