using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day4 : Day
    {
        public override int _iDay { get { return 4; } }

        public override string Q1()
        {
            //Console.WriteLine(logic("abcdef"));// 609043
            //Console.WriteLine(logic("pqrstuv"));// 1048970
            Console.WriteLine(logic("yzbqklnj"));
            return "";
        }
        public long logic(string secretKey)
        {
            long l = 0;
            while (true)
            {
                if (CreateMD5(secretKey+l).StartsWith("000000")) return l;
                l++;
            }
            //return l;
        }
        public override string Q2()
        {
            var lsInput = Data;
            return "";
        }

        public string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }

    }
}
