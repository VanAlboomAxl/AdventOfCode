using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day8 : Day
    {
        public override int _iDay { get { return 8; } }

        internal override List<string> _lsTest => new List<string>() {
            "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
        };

        
        public override void Q1()
        {
            //var lsInput = Input;
            var lsInput = Test;

            int iCount = 0;
            foreach(var sInput in lsInput)
            {
                var asOutput = sInput.Split("|")[1].Trim().Split(" ");
                foreach (var sOutput in asOutput)
                    if (GetSegment(sOutput) > 0)
                        iCount++;
                
            }

            Console.WriteLine("Count: " + iCount);

        }
        private int GetSegment(string s)
        {
            if (s.Length == 3)
                return 7;
            else if (s.Length == 4)
                return 4;
            else if (s.Length == 7)
                return 8;
            else if (s.Length == 2)
                return 1;

            return -1;
        }

        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;

            int iCount = 0;
            foreach (var sInput in lsInput)
            {
                var iResult = Q2_logic(sInput);
                Console.WriteLine(iResult);
                iCount += iResult;
            }

            Console.WriteLine("Count: " + iCount);

        }
        

        private int Q2_logic(string sIn)
        {
            var ars = sIn.Split("|");
            var Input = ars[0].Split(" ");
            var Output = ars[1].Trim().Split(" ");

            char T = 'z';
            char M = 'z';
            char B = 'z';
            char TL = 'z';
            char BL = 'z';
            char TR = 'z';
            char BR = 'z';

            string s1 = Input.Where(x => x.Length == 2).First();
            string s7 = Input.Where(x => x.Length == 3).First();
            string s4 = Input.Where(x => x.Length == 4).First();
            string s6 = null;
            List<string> ls235 = Input.Where(x => x.Length == 5).ToList();
            List<string> ls069 = Input.Where(x => x.Length == 6).ToList();

            T = s7.Where(c => !s1.Contains(c)).First();
            foreach (string s in ls069)
                if (s.Where(x => s1.Contains(x)).ToList().Count != 2)
                {
                    ls069.Remove(s);
                    s6 = s;
                    if (s.Contains(s1[0]))
                    {
                        BR = s1[0];
                        TR = s1[1];
                    }
                    else
                    {
                        BR = s1[1];
                        TR = s1[0];
                    }

                    break;
                }

            char c09_1 = 'z';
            char c09_2 = 'z';
            var AllChars = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            foreach(var c in ls069[0])
                AllChars.Remove(c);
            c09_1 = AllChars[0];
            AllChars = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            foreach (var c in ls069[1])
                AllChars.Remove(c);
            c09_2 = AllChars[0];

            if (s4.Contains(c09_1))
            {
                M = c09_1;
                BL = c09_2;
            }
            else
            {
                M = c09_2;
                BL = c09_1;
            }

            var Chars4 = s4.ToCharArray().ToList();
            Chars4.Remove(M);
            Chars4.Remove(TR);
            Chars4.Remove(BR);
            TL = Chars4[0];

            AllChars = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            AllChars.Remove(T);
            AllChars.Remove(M);
            AllChars.Remove(TL);
            AllChars.Remove(TR);
            AllChars.Remove(BR);
            AllChars.Remove(BL);
            B = AllChars[0];

            //Console.WriteLine($"{T}{T}{T}{T}");
            //Console.WriteLine($"{TL}  {TR}");
            //Console.WriteLine($"{TL}  {TR}");
            //Console.WriteLine($"{M}{M}{M}{M}");
            //Console.WriteLine($"{BL}  {BR}");
            //Console.WriteLine($"{BL}  {BR}");
            //Console.WriteLine($"{B}{B}{B}{B}");

            
            string sO = "";
            foreach (var o in Output)
                sO += GetSegment2(o, T, M, B, TL, TR, BL, BR);
                    
            

            return int.Parse(sO);
        }

        private int GetSegment2(string s, char T, char M, char B, char TL, char TR, char BL, char BR)
        {
            if (s.Length == 2)
                return 1;
            else if (s.Length == 3)
                return 7;
            else if (s.Length == 7)
                return 8;
            else if (s.Length == 4)
                return 4;
            else if (s.Length == 5)
            {
                var s5 = new List<char>() { T,TL,M, BR,B };
                var s2 = new List<char>() { T, TR, M, BL, B };
                var s3 = new List<char>() { T, TR, M, BR, B };
                var invalidChars = s5.Where(validChar => s.Count(inputChar => inputChar == validChar) > 1);
                //if (s.Where(x => s5.All(c => x.Equals(c))).ToList().Count > 0)
                if (s.Where(x => s5.Contains(x)).ToList().Count == 5)
                    return 5;
                else if (s.Where(x => s2.Contains(x)).ToList().Count == 5)
                    return 2;
                else if (s.Where(x => s3.Contains(x)).ToList().Count == 5)
                    return 3;
            }
            else
            {
                var c9 = BL;
                var c0 = M;
                var c6 = TR;
                if (!s.Contains(c9)) return 9;
                if (!s.Contains(c0)) return 0;
                if (!s.Contains(c6)) return 6;
            }


            return -1;
        }

    }
}
