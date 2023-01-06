using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day25 : Day
    {
        public override int _iDay { get { return 25; } }

        public override string Q1()
        {
            var lsInput = Data;
            long lCount = 0;
            foreach (var s in lsInput) lCount += convert(s);
            //Console.WriteLine(convert("1121-1110-1=0")); //314159265  
            
            //foreach (var s in lsInput) Console.WriteLine(convert(convert(s)) == s);
            //Console.WriteLine(convert(convert("1121-1110-1=0")) == "1121-1110-1=0");

            return convert(lCount);
        }
        Dictionary<char, int> map = new()
        {
            { '2',2 },
            { '1',1 },
            { '0',0 },
            { '-',-1 },
            { '=',-2 }
        };
        Dictionary<int, char> mapR = new()
        {
            //{ 4,'-' },
            //{ 3,'=' },
            { 2,'2' },
            { 1,'1' },
            { 0,'0' },
            { -1,'-' },
            { -2,'=' }
        };
        long convert(string s)
        {
            long result=0;
            long iFivePlace = 1;
            for(int i = s.Length - 1; i >= 0; i--)
            {
                char c = s[i];
                result += iFivePlace * map[c];
                iFivePlace *= 5;
            }
            return result;
        }
        string convert(long l)
        {
            string result = "";

            while(l> 0)
            {
                long i = l % 5;
                if (i > 2) i -= 5;
                result = mapR[(int)(i)] + result;
                //l -= 2;
                l += 2;
                l /= 5;
            }
            return result;
        }
        
        public override string Q2()
        {
            return "Congratulations :)";
        }


    }
}
