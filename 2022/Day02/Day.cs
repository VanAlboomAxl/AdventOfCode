using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day2 : Day
    {
        public override int _iDay { get { return 2; } }
        
        private List<(string,string)> convert(List<string> lsInput)
        {
            List<(string,string)> result = new();
            foreach (string s in lsInput)
            {
                var l = s.Split(' ');
                result.Add((l[0],l[1]));
            }    
            return result;
        }

        public override string Q1()
        {
            var lsInput = Input;
            //lsInput = Test;
            var data = convert(lsInput);
            long l = 0;
            foreach (var d in data) l += play(d.Item1, d.Item2);
            Console.WriteLine(l);
            return l.ToString();
        }
        private int play(string opponent, string me)
        {
            switch (opponent)
            {
                case "A":
                    switch (me)
                    {
                        case "X": return 1 + 3;
                        case "Y": return 2 + 6;
                        case "Z": return 3 + 0;
                    }
                    break;
                case "B":
                    switch (me)
                    {
                        case "X": return 1 + 0;
                        case "Y": return 2 + 3;
                        case "Z": return 3 + 6;
                    }
                    break;
                case "C":
                    switch (me)
                    {
                        case "X": return 1 + 6;
                        case "Y": return 2 + 0;
                        case "Z": return 3 + 3;
                    }
                    break;

            }
            return 0;
        }

        public override string Q2()
        {
            var lsInput = Input;
            //lsInput = Test;
            var data = convert(lsInput);
            long l = 0;
            foreach (var d in data) l += play2(d.Item1, d.Item2);
            return l.ToString();
        }
        private int play2(string opponent, string me)
        {
            switch (opponent)
            {
                case "A":
                    switch (me)
                    {
                        case "X": return 3 + 0;
                        case "Y": return 1 + 3;
                        case "Z": return 2 + 6;
                    }
                    break;
                case "B":
                    switch (me)
                    {
                        case "X": return 1 + 0;
                        case "Y": return 2 + 3;
                        case "Z": return 3 + 6;
                    }
                    break;
                case "C":
                    switch (me)
                    {
                        case "X": return 2 + 0;
                        case "Y": return 3 + 3;
                        case "Z": return 1 + 6;
                    }
                    break;

            }
            return 0;
        }

    }

    public class Day2_v2 : Day
    {
        public override int _iDay { get { return 2; } }

        enum hand
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
        Dictionary<hand, Dictionary<hand, int>> result = new()
        {
            { hand.Rock,new(){{hand.Rock,3 },{hand.Paper,6 },{hand.Scissors,0 }} },
            { hand.Paper,new(){{hand.Rock,0},{hand.Paper,3 },{hand.Scissors, 6 }} },
            { hand.Scissors,new(){{hand.Rock,6},{hand.Paper,0 },{hand.Scissors, 3 }} }
        };
        private hand Convert(char value)
        {
            if (new List<char>() { 'A', 'X' }.Contains(value)) return hand.Rock;
            if (new List<char>() { 'B', 'Y' }.Contains(value)) return hand.Paper;
            if (new List<char>() { 'C', 'Z' }.Contains(value)) return hand.Scissors;
            throw new Exception();
        }
        
        public override string Q1()
        {
            var lsInput = Input;
            //lsInput = Test;

            long l = 0;
            foreach (var s in lsInput)
            {
                var ca = s.ToCharArray();
                var me = Convert(ca[2]);
                l += result[Convert(ca[0])][me] +(int)me;
            }

            //Console.WriteLine(l);
            return l.ToString();
        }

        Dictionary<hand, Dictionary<char, hand>> q2convert = new()
        {
            { hand.Rock,new(){{'X',hand.Scissors }, { 'Y', hand.Rock }, {'Z', hand.Paper } } },
            { hand.Paper,new(){{'X',hand.Rock }, { 'Y', hand.Paper }, {'Z', hand.Scissors } } },
            { hand.Scissors,new(){{'X',hand.Paper }, { 'Y', hand.Scissors }, {'Z', hand.Rock } } },
        };
        public override string Q2()
        {
            var lsInput = Input;
            //lsInput = Test;

            long l = 0;
            foreach (var s in lsInput)
            {
                var ca = s.ToCharArray();
                var other = Convert(ca[0]);
                var me = q2convert[other][ca[2]];
                l += result[Convert(ca[0])][me] + (int)me;
            }

            //Console.WriteLine(l);
            return l.ToString();
        }

    }

}