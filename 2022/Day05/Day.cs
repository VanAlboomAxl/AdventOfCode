using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day5 : Day
    {
        public override int _iDay { get { return 5; } }
      
        private void logic(string s, List<Stack<char>> alcCrates)
        {
            string sRegexString = @"move (\d+) from (\d+) to (\d+)";
            Regex rg = new Regex(sRegexString);
            GroupCollection matches = rg.Matches(s)[0].Groups;

            int l0 = int.Parse(matches[1].Value);
            int l1 = int.Parse(matches[2].Value)-1;
            int l2 = int.Parse(matches[3].Value)-1;
            for (int i=0; i < l0; i++)
                alcCrates[l2].Push(alcCrates[l1].Pop());
        }

        public override void Q1()
        {
            List<string> lsInput = Data;
            int i = 0;
            for(i=0;i< lsInput.Count;i++)
            {
                var sD = Data[i];
                if (string.IsNullOrEmpty(sD)) break;
            }
            List<Stack<char>> llcCrates = new();
            int iLength = lsInput[i - 2].Length;
            for(int k=1; k < iLength - 1; k += 4)
            {
                Stack<char> crateStack = new();
                for (int j = i - 2; j >= 0; j--)
                {
                    char c = lsInput[j][k];
                    if (c == ' ') break;
                    crateStack.Push(c);
                }
                llcCrates.Add(crateStack);
            }


            for(int j=i+1; j < lsInput.Count; j++)
            {
                logic(lsInput[j], llcCrates);
            }

            string sResult = "";
            foreach (var scCrates in llcCrates) sResult += scCrates.Peek();
            Console.WriteLine(sResult);

        }
      
        public override void Q2()
        {
            List<string> lsInput = Data;
            int i = 0;
            for (i = 0; i < lsInput.Count; i++)
            {
                var sD = Data[i];
                if (string.IsNullOrEmpty(sD)) break;
            }
            List<Stack<char>> llcCrates = new();
            int iLength = lsInput[i - 2].Length;
            for (int k = 1; k < iLength - 1; k += 4)
            {
                Stack<char> crateStack = new();
                for (int j = i - 2; j >= 0; j--)
                {
                    char c = lsInput[j][k];
                    if (c == ' ') break;
                    crateStack.Push(c);
                }
                llcCrates.Add(crateStack);
            }

            for (int j = i + 1; j < lsInput.Count; j++)
            {
                logic2(lsInput[j], llcCrates);
            }

            string sResult = "";
            foreach (var scCrates in llcCrates) sResult += scCrates.Peek();
            Console.WriteLine(sResult);
        }
        private void logic2(string s, List<Stack<char>> alcCrates)
        {
            string sRegexString = @"move (\d+) from (\d+) to (\d+)";
            Regex rg = new Regex(sRegexString);
            GroupCollection matches = rg.Matches(s)[0].Groups;

            int l0 = int.Parse(matches[1].Value);
            int l1 = int.Parse(matches[2].Value) - 1;
            int l2 = int.Parse(matches[3].Value) - 1;
            List<char> tussen = new(); 
            for (int i = 0; i < l0; i++)
                tussen.Add(alcCrates[l1].Pop());
            while (tussen.Count > 0) 
            { 
                alcCrates[l2].Push(tussen.Last());
                tussen.RemoveAt(tussen.Count - 1);
            }
        }
    }
}
