using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day3 : Day
    {
        public override int _iDay { get { return 3; } }
      
        public override string Q1()
        {
            long iResult= 0;
            foreach(var s in Data)
            {
                char res = 'a';
                var a = s.Substring(0, s.Length / 2);
                var b = s.Substring(s.Length / 2, s.Length / 2);
                foreach(var c in a)
                    if (b.Contains(c))
                    {
                        res = c;
                        break;
                    }
                iResult += charNumber(res);
            }
            //Console.WriteLine(iResult);
            return iResult.ToString();
        }
      
        public int charNumber(char c)
        {
            int result = 0;
            if (Char.IsUpper(c)) result += 26;
            c = Char.ToLower(c);
            result += ((int)c - 96);
            return result;
        }

        public override string Q2()
        {
            long iResult = 0;
            var lsInput = Data;
            for(int i = 0; i < lsInput.Count - 2; i+=3)
            {
                char res = 'a';
                var s1 = lsInput[i];
                var s2 = lsInput[i + 1];
                var s3 = lsInput[i + 2];
                foreach (var c in s1)
                    if (s2.Contains(c) && s3.Contains(c))
                    {
                        res = c;
                        break;
                    }
                iResult += charNumber(res);
            }
            //Console.WriteLine(iResult);
            return iResult.ToString();
        }

    }
}
