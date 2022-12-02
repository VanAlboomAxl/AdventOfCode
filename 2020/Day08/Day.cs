using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day8 : Day
    {
        public override int _iDay { get { return 8; } }
      
        public override void Q1()
        {
            var lsInput = Input;
            //var lsInput = Test;

            int accumulator = 0;
            List<int> liRunned = new();
            for(int i=0;i< lsInput.Count;i++)
            {
                if (liRunned.Contains(i)) 
                    break;
                liRunned.Add(i);
                
                string[] asSplit = lsInput[i].Split(" ");
                if (asSplit[0].Equals("acc"))
                    accumulator = Operation(accumulator, asSplit[1]);
                else if (asSplit[0].Equals("jmp"))
                {
                    i += Operation(0, asSplit[1])-1;
                }
            }

            Console.WriteLine("accumulator: " + accumulator);

        }
      
        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;

            int iIndex = 1;
            int accumulator = -1;
            while(accumulator < 0)
            {
                accumulator = Q2_logic(lsInput, iIndex);
                iIndex++;
            }

        }

        private int Q2_logic(List<string> lsInput, int iIndex)
        {
            int accumulator = 0;
            List<int> liRunned = new();

            int iPosFault = 0;

            for (int i = 0; i < lsInput.Count; i++)
            {
                if (liRunned.Contains(i))
                    return -1;
                liRunned.Add(i);

                string[] asSplit = lsInput[i].Split(" ");
                if (asSplit[0].Equals("acc"))
                    accumulator = Operation(accumulator, asSplit[1]);
                else if (asSplit[0].Equals("jmp"))
                {
                    iPosFault++;
                    if (iPosFault == iIndex)
                    {
                        //lsInput[i] = $"nop {asSplit[1]}";
                        //i--;
                    }
                    else 
                        i += Operation(0, asSplit[1]) - 1;
                }
                else
                {
                    iPosFault++;
                    if (iPosFault == iIndex)
                    {
                        i += Operation(0, asSplit[1]) - 1;
                        //lsInput[i] = $"jmp {asSplit[1]}";
                        //i--;
                    }
                }
            }
            return accumulator;
        }

        private int Operation(int iCurrentValue, string sArgument)
        {
            string sOperation = sArgument.Substring(0, 1);
            int i = int.Parse(sArgument.Substring(1));
            if (sOperation.Equals("+")) iCurrentValue += i;
            if (sOperation.Equals("-")) iCurrentValue -= i;
            return iCurrentValue;
        }

    }
}
