using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day15 : Day
    {
        public override int _iDay { get { return 15; } }

        string _sInput = "1,20,11,6,12,0";
        string _sTest1 = "0,3,6";
        string _sTest2 = "1,3,2";
        string _sTest3 = "2,1,3";
        string _sTest4 = "1,2,3";
        string _sTest5 = "2,3,1";
        string _sTest6 = "3,2,1";
        string _sTest7 = "3,1,2";

        public override void Q1()
        {
            string[] sSplit = _sInput.Split(',');
            List<long> liGame = new();
            foreach (var s in sSplit)
                liGame.Add(Int64.Parse(s));

            int iTotalRounds = 2020;
            while(liGame.Count < 2020)
            {
                long lLast = liGame.Last();
                int iLastIndex = liGame.GetRange(0, liGame.Count - 1).LastIndexOf(lLast);
                if (iLastIndex.Equals(-1))
                    liGame.Add(0);
                else
                    liGame.Add((liGame.Count - 1)-iLastIndex);
            }
            long last = liGame.Last();
            Console.WriteLine(last);
        }

        public override void Q2()
        { 
            string[] sSplit = _sInput.Split(',');

            Dictionary<long,long> dGame = new();
            for (int i = 0; i < sSplit.Length; i++)
                dGame.Add(Int64.Parse(sSplit[i]), i);
            long lCount = sSplit.Length;
            long last = Int64.Parse(sSplit[sSplit.Length-1]);
            long lNew = 0;
            while (lCount < (30000000 - 1))
            {
                if (dGame.ContainsKey(lNew))
                {
                    long lDelta = lCount-dGame[lNew];
                    dGame[lNew] = lCount;
                    lNew = lDelta;
                }
                else
                {
                    dGame.Add(lNew, lCount);
                    lNew = 0;
                }
                last = lNew;

                lCount++;
            }

            Console.WriteLine(last);
        }

    }
}
