using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day1
    {

        public string InputLocation = @"C:\Users\Axl Van Alboom\Desktop\AdventOfCode21\AdventOfCode21\Day1\input.txt";

        private int IntParsers(string s)
        {
            int iResult = 0;
            int.TryParse(s, out iResult);
            return iResult;
        }

        public void Q1()
        {
            var lsInput = Helper.ReadInput(InputLocation);

            int iCount = 0;

            int iPre = IntParsers(lsInput[0]);
            for(int i=1; i<lsInput.Count();i++)
            {
                int iNow = IntParsers(lsInput[i]);
                if (iNow > iPre) iCount++;
                iPre = iNow;
            }
            Console.WriteLine(iCount);
        }

        public void Q2()
        {
            var lsInput = Helper.ReadInput(InputLocation);

            int iCount = 0;

            int iPre = IntParsers(lsInput[0]) + IntParsers(lsInput[1]) + IntParsers(lsInput[2]); 

            for (int i = 3; i < lsInput.Count(); i++)
            {
                int iSum = IntParsers(lsInput[i-2]) + IntParsers(lsInput[i-1]) + IntParsers(lsInput[i]);
                if (iSum > iPre) iCount++;
                iPre = iSum;
            }
            Console.WriteLine(iCount);
        }

    }
}
