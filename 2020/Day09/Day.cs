using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day9 : Day<List<Int64>>
    {
        public override int _iDay { get { return 9; } }

        public override List<Int64> Convert(List<string> Input)
        {
            return Input.Select(s => Int64.Parse(s)).ToList();
        }

        public override void Q1()
        {
            var liInput = Input;
            //var liInput = Test;

            Console.WriteLine(Logic(liInput, 25));
        }
      
        public Int64 Logic(List<Int64> liInput, int iPreamble)
        {
            for(int i = iPreamble; i < liInput.Count; i++)
            {
                bool xFound = false;
                Int64 iValue = liInput[i];
                for (int j=i-iPreamble; j < i-1; j++)
                {
                    //bool xSubFound = false;
                    for (int k = j + 1; k < i; k++)
                    {
                        if (liInput[i] == liInput[j] + liInput[k])
                        {
                            xFound = true;
                            break;
                        }
                    }
                    if (xFound)
                        break;         
                }
                if (!xFound)
                    return iValue;
            }
            return -1;
        }

        public override void Q2()
        {
            var liInput = Input;
            //var liInput = Test;

            Console.WriteLine(Logic2(liInput ,Logic(liInput, 25)));

        }
        public Int64 Logic2(List<Int64> liInput, Int64 iNumber)
        {
            for(int i=0;i<liInput.Count-1;i++)
            {
                Int64 iValue = liInput[i];
                for (int j = i+1; j < liInput.Count; j++)
                {
                    iValue += liInput[j];
                    if (iValue == iNumber)
                    {
                        List<Int64> Set = new();
                        for(int k = i; k <= j; k++)
                            Set.Add(liInput[k]);    
                        return Set.Min()+Set.Max();
                    }
                    else if (iValue > iNumber)
                        break;
                }
            }
            return -1;
        }


    }
}
