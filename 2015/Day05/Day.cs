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

        public override string Q1()
        {
            var lsInput = Data;
            int iNice = 0;
            List<string> cantContain = new() { "ab", "cd", "pq", "xy" };
            foreach (var s in lsInput)
            {
                bool xDouble = false;
                bool xGood = true;
                char cPre = s[0];
                int iVowels = 0;
                if (CharIsVowel(cPre)) iVowels++;
                for (int i=1; i<s.Length; i++)
                {
                    char cMe = s[i];
                    string sPre = "" + cPre+ cMe;
                    if (sPre.Distinct().Count() == 1) xDouble = true;
                    //if (cMe == cPre + 1)
                    if (cantContain.Contains(sPre))
                    {
                        xGood = false; break;
                    }
                    if (CharIsVowel(cMe)) 
                        iVowels++;
                    cPre = cMe;
                }
                if(iVowels >2 && xGood && xDouble) iNice++;
                
            }

            return iNice.ToString();
        }

        public bool CharIsVowel(char c)
        {
            return "aeiouAEIOU".IndexOf(c) >= 0;
        }

        public override string Q2()
        {
            var lsInput = Data;/*
            lsInput = new()
            {
                "qjhvhtzxzqqjkmpb","xxyxx","uurcxstgmygtbstg","ieodomkazucvgmuy"
            };*/
            int iNice = 0;
            List<string> cantContain = new() { "ab", "cd", "pq", "xy" };
            foreach (var s in lsInput)
            {
                bool xDouble = false;
                bool xSameWithSpace = false;
                char cPre = s[1];
                for (int i = 2; i < s.Length; i++)
                {
                    char cMe = s[i];
                    if (s[i-2] == cMe) xSameWithSpace = true;
                    string sPre = "" + cPre + cMe;
                    string sub = s.Substring(0, i - 1);
                    if (sub.Contains(sPre))
                        xDouble = true;
                    cPre = cMe;
                }

                if (xDouble && xSameWithSpace ) iNice++;
            }

            return iNice.ToString();
        }
    }
}
