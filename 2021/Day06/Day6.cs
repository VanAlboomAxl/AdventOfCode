using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day6 : Day
    {
        public override int _iDay { get { return 6; } }

        internal override List<string> _lsTest => new List<string>() {
            "3,4,3,1,2"
        };

        public override void Q1()
        {
            var lsInput = Input[0];
            //var lsInput = Test[0];
            List<int> Fish = new();
            foreach (var sInput in lsInput.Split(","))
                Fish.Add(int.Parse(sInput));

            for(int i = 0; i < 80; i++)
            {
                int ToBeAdded = 0;
                for(int j = 0; j < Fish.Count; j++)
                {
                    int k = Fish[j] - 1;
                    if (k < 0)
                    {
                        k = 6;
                        ToBeAdded++;
                    }
                    Fish[j] = k;
                }
                for(int j=0;j< ToBeAdded; j++)
                    Fish.Add(8);
                
            }

            Console.WriteLine(Fish.Count());
        }

        public override void Q2()
        {
            var lsInput = Input[0];
            //var lsInput = Test[0];
            long[] Fish = new long[9];
            foreach (var sInput in lsInput.Split(","))
                Fish[int.Parse(sInput)]++;

            int iDays = 256;

            while (iDays > 0)
            {
                long AmountThatProduceABaby = Fish[0];
                for(int i=1;i< Fish.Length; i++)
                    Fish[i - 1] = Fish[i];
                
                Fish[6] += AmountThatProduceABaby;
                Fish[8] = AmountThatProduceABaby;

                iDays--;
            }

           

            Console.WriteLine(Fish.Sum());
        }

    }
}
