using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day7 : Day
    {
        public override int _iDay { get { return 7; } }

        internal override List<string> _lsTest => new List<string>() {
            "16,1,2,0,4,2,7,1,2,14"
        };

        public override void Q1()
        {
            var lsInput = Input[0];
            //var lsInput = Test[0];
            List<int> InputList = new();
            foreach (var sInput in lsInput.Split(","))
                InputList.Add(int.Parse(sInput));

            int iMin = InputList.Min();
            int iMax = InputList.Max();

            int iBest = -1;
            int iMinFuel = int.MaxValue;
            for(int i = iMin; i <= iMax; i++)
            {
                int iFuel = 0;
                foreach(var j in InputList)
                    iFuel += Math.Abs(j - i);
                if (iFuel<iMinFuel)
                {
                    iMinFuel = iFuel;
                    iBest = i;
                }
            }

            Console.WriteLine("Best positiion: " + iBest);
            Console.WriteLine("Minimal fuel: " + iMinFuel);

        }

        public override void Q2()
        {
            var lsInput = Input[0];
            //var lsInput = Test[0];
            List<int> InputList = new();
            foreach (var sInput in lsInput.Split(","))
                InputList.Add(int.Parse(sInput));

            int iMin = InputList.Min();
            int iMax = InputList.Max();

            int iBest = -1;
            int iMinFuel = int.MaxValue;
            for (int i = iMin; i <= iMax; i++)
            {
                int iFuel = 0;
                foreach (var j in InputList)
                    for(int k=1;k<= Math.Abs(j - i);k++)
                        iFuel += k;
                if (iFuel < iMinFuel)
                {
                    iMinFuel = iFuel;
                    iBest = i;
                }
            }

            Console.WriteLine("Best positiion: " + iBest);
            Console.WriteLine("Minimal fuel: " + iMinFuel);

        }

    }
}
