using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day01 : Day
    {
        public override int _iDay { get { return 1; } }

        public override string Q1()
        {
            var lsInput = Data;
            int iFloor = 0;
            foreach(char c in lsInput[0])
            {
                if (c.Equals('(')) iFloor++;
                else if(c.Equals(')')) iFloor--;
                else
                {

                }
            }
            return iFloor.ToString();
        }

        public override string Q2()
        {
            var lsInput = Data;
            int iFloor = 0;
            int i = 0;
            for(i = 0; i < lsInput[0].Length;i++)
            {
                char c = lsInput[0][i];
                if (c.Equals('(')) iFloor++;
                else if (c.Equals(')')) iFloor--;
                else
                {

                }
                if (iFloor < 0)
                {
                    break;
                }
            }
            return (i+1).ToString();
        }
    }
}
