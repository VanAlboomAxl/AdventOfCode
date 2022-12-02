using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
namespace AdventOfCode_2021
{

    public class Day24 : Day
    {

        public override int _iDay { get { return 24; } }

        internal override List<string> _lsTest => new List<string>
                {
                    "inp w",
                    "add z w",
                    "mod z 2",
                    "div w 2",
                    "add y w",
                    "mod y 2",
                    "div w 2",
                    "add x w",
                    "mod x 2",
                    "div w 2",
                    "mod w 2"
                };

        List<clsInstruction> loInstructions;
        List<string> ValidModelNumbers;

        public Day24()
        {
            var lines = Input;

            loInstructions = new();

            for (int i = 0; i < 14; i++)
            {
                long divZ = int.Parse(lines[(18 * i) + 4].Split()[2]);
                long addX = int.Parse(lines[(18 * i) + 5].Split()[2]);
                long addY = int.Parse(lines[(18 * i) + 15].Split()[2]);
                loInstructions.Add(new (addX, divZ, addY));
            }

            //impossible to go back to 0 if value is bigger than max divide
            for (int i = 0; i < loInstructions.Count; i++)
                loInstructions[i].SetMaxZ(loInstructions.Skip(i).Aggregate(1L, (a, b) => a * b.divZ));
            
            ValidModelNumbers = RecursiveSearch(0, 0).ToList();
            ValidModelNumbers.Sort();
        }

        public override void Q1()
        {
            Console.WriteLine($"Total Valid IDs: {ValidModelNumbers.Count}");
            Console.WriteLine($"Biggest: {ValidModelNumbers.Last()}");
        }

        public override void Q2()
        {
            Console.WriteLine($"Smallest: {ValidModelNumbers.First()}");
        }

        Dictionary<(int groupNum, long prevZ), List<string>> cacheDic = new();

        private List<string> RecursiveSearch(int groupNum, long prevZ)
        {
            //We've Been here before...
            if (cacheDic.ContainsKey((groupNum, prevZ))) return cacheDic[(groupNum, prevZ)];
            
            if (groupNum >= 14)
            {
                if (prevZ == 0) 
                    return new() { "" };
                return null;
            }

            if (prevZ > loInstructions[groupNum].MaxZ)
                //impossible to go back to 0
                return null;
            
            List<string> res = new();

            foreach (int i in Enumerable.Range(1, 9))
            {
                long nextZ = loInstructions[groupNum].Execute(prevZ, i);
                List<string> nextStrings = RecursiveSearch(groupNum + 1, nextZ);

                if (nextStrings != null)
                {
                    foreach (var s in nextStrings)
                    {
                        res.Add($"{i}{s}");
                    }
                }
            }

            cacheDic[(groupNum, prevZ)] = res;
            return res;

        }

        public class clsInstruction
        {
            public long addX { get; private set; }
            public long divZ { get; private set; }
            public long addY { get; private set; }
            public long MaxZ { get; private set; }

            public clsInstruction(long lAddX, long lDivZ, long lAddY)
            {
                addX = lAddX;
                divZ = lDivZ;
                addY = lAddY;
            }

            public void SetMaxZ(long max)
            {
                MaxZ = max;
            }

            public long Execute(long prevZ, long input)
            {
                long z = prevZ;
                long x = addX + z % 26;
                z /= divZ;
                if (x != input)
                {
                    z *= 26;
                    z += input + addY;
                }

                return z;
            }

        }

    }
}
