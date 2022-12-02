using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day25 : Day
    {
        public override int _iDay { get { return 25; } }

        internal override List<string> _lsTest => new List<string>
        {
            "v...>>.vv>",
            ".vv>>.vv..",
            ">>.>v>...v",
            ">>v>>.>.v.",
            "v>v.vv.v..",
            ">.>>..v...",
            ".vv..>.>v.",
            "v.v..>>v.v",
            "....v..v.>"
        };

        public override void Q1()
        {
            Logic(_lsTest);
            //Logic(Input);

        }

        private void Logic(List<string> lsInput)
        {
            bool xPrint = false;

            int i = 1;
            Moved = true;
            if (xPrint)
            {
                Console.WriteLine($"Initial state");
                foreach (var s in lsInput)
                    Console.WriteLine(s);
                Console.WriteLine();
            }

            while (Moved)
            {
                Moved = false;
                lsInput = MoveDown(MoveRight(lsInput));
                if (xPrint)
                {
                    Console.WriteLine($"After {i} step:");
                    foreach (var s in lsInput)
                        Console.WriteLine(s);
                    Console.WriteLine();
                }
                if (Moved)
                    i++;
            }
            Console.WriteLine(i);
        }

        private bool Moved;

        private List<string> MoveRight(List<string> lsInput)
        {
            return Move(lsInput, '>');
        }

        private List<string> MoveDown(List<string> lsInput)
        {

            List<string> lsRotate = new();

            for (int i = 0; i < lsInput[0].Length; i++)
            {
                string sRotate = "";
                foreach (var s in lsInput)
                    sRotate += s[i];
                lsRotate.Add(sRotate);
            }

            lsRotate = Move(lsRotate, 'v');

            List<string> lsResult = new();
            for (int i = 0; i < lsRotate[0].Length; i++)
                lsResult.Add("");
            foreach (var s in lsRotate)
                for (int i = 0; i < lsRotate[0].Length; i++)
                {
                    lsResult[i] += s[i];

                }
            return lsResult;

        }
        private List<string> Move(List<string> lsInput, char c)
        {
            int i = 0;
            while (i < lsInput.Count)
            {
                string s = lsInput[i];
                if (s.First().Equals('.') && s.Last().Equals(c))
                {
                    s = 'x' + s.Substring(1, s.Length - 2) + 'y';
                    Moved = true;
                }
                if (s.Contains($"{c}."))
                {
                    s = s.Replace($"{c}.", $".{c}");
                    Moved = true;
                }
                s = s.Replace($"x", $"{c}");
                s = s.Replace($"y", $".");

                lsInput[i] = s;
                i++;
            }

            return lsInput;
        }



        public override void Q2()
        {


        }

    }
}
