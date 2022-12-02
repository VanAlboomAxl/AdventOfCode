using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day10 : Day
    {
        public override int _iDay { get { return 10; } }

        internal override List<string> _lsTest => new List<string>() {
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]"
        };
      
        public override void Q1()
        {
            var lsInput = Input;
            //var lsInput = Test;

            Dictionary<char, char> ClosingChars = new()
            {
                { '>', '<' },
                { ']', '[' },
                { ')', '(' },
                { '}', '{' }
            };
            Dictionary<char, int> CorruptionScore = new()
            {
                { '>', 25137 },
                { ']', 57 },
                { ')', 3 },
                { '}', 1197 }
            };
            int iCorruptCount=0;
            foreach(var sInput in lsInput)
            {
                List<char> CharList = new();
                foreach(char c in sInput)
                {
                    if (ClosingChars.ContainsKey(c))
                    {
                        if (CharList.Last().Equals(ClosingChars[c]))
                        {
                            CharList.RemoveAt(CharList.Count() - 1);
                        }
                        else
                        {
                            iCorruptCount += CorruptionScore[c];
                            break;
                        }
                    }
                    else
                    {
                        CharList.Add(c);
                    }
                }
            }

            Console.WriteLine("CorruptCount: " + iCorruptCount);
        }

        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;

            Dictionary<char, char> ClosingChars = new()
            {
                { '>', '<' },
                { ']', '[' },
                { ')', '(' },
                { '}', '{' }
            };
            Dictionary<char, int> CorruptionScore = new()
            {
                { '>', 25137 },
                { ']', 57 },
                { ')', 3 },
                { '}', 1197 }
            };
            Dictionary<char, char> RequiredClosingChar = new()
            {
                { '<','>' },
                { '[',']' },
                { '(',')' },
                { '{','}' }
            };
            Dictionary<char, int> ClosingScore = new()
            {
                { '>', 4 },
                { ']', 2 },
                { ')', 1 },
                { '}', 3 }
            };
            List<Int64> liScores = new();
            foreach (var sInput in lsInput)
            {
                List<char> CharList = new();
                bool xCorrupt = false;
                foreach (char c in sInput)
                {
                    if (ClosingChars.ContainsKey(c))
                        if (CharList.Last().Equals(ClosingChars[c]))
                            CharList.RemoveAt(CharList.Count() - 1);    
                        else
                        {
                            xCorrupt = true;
                            break;
                        }                    
                    else
                        CharList.Add(c);                 
                }

                if (!xCorrupt && CharList.Count > 0)
                {
                    Int64 iScore = 0;
                    for (int i = CharList.Count() - 1; i > -1; i--)
                    {
                        iScore *= 5;
                        iScore += ClosingScore[RequiredClosingChar[CharList[i]]];
                        CharList.RemoveAt(i);
                    }
                    liScores.Add(iScore);
                }
                    
                
            }
            liScores.Sort();
            //3662008566
            Int64 iResult = liScores[(int)Math.Round(liScores.Count / 2.0)-1];
            Console.WriteLine("Result: " + iResult);
        }
        
    }
  
}
