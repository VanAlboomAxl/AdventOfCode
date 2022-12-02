using Syncfusion.Licensing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day14 : Day
    {
        public override int _iDay { get { return 14; } }

        public override void Q1()
        {
            string sMask = null;
            Dictionary<int, long> Register = new();

            foreach(var s in Input)
            //foreach (var s in Test)
            {
                if (s.StartsWith("mask"))
                    sMask = s.Split("=")[1].Trim();
                else
                {
                    Regex regex = new Regex(@"mem\[(\d+)\] = (\d+)");
                    Match match = regex.Match(s);
                    if (match.Success)
                    {
                        int iAdres = int.Parse(match.Groups[1].Value);
                        int iValue = int.Parse(match.Groups[2].Value);
                        Register[iAdres] = ApplyMask(iValue, sMask);
                    }
                }
            }

            Console.WriteLine(Register.Values.Sum());

        }

        private long ApplyMask(long iNumber, string sMask)
        {
            string BinValue = (string)Convert.ToString(iNumber, 2);
            string sResult = "";
            //for (int i = sMask.Length - 1; i > -1; i--)
            for (int i = 0; i <sMask.Length; i++)
            {
                char c = '0';
                if (i < BinValue.Length)
                    c = BinValue[BinValue.Length-1-i];

                if (sMask[sMask.Length - 1 - i].Equals('0'))
                    c = '0';
                else if (sMask[sMask.Length - 1 - i].Equals('1'))
                    c = '1';

                sResult = c + sResult;
            }

            return Convert.ToInt64(sResult, 2);

        }

        public override void Q2()
        {
            string sMask = null;
            Dictionary<long, long> Register = new();
            List<string> Test2 = Helper.ReadInput($"{FolderLocation}\\test2.txt");
            foreach (var s in Input)
            //foreach (var s in Test2)
            {
                if (s.StartsWith("mask"))
                    sMask = s.Split("=")[1].Trim();
                else
                {
                    Regex regex = new Regex(@"mem\[(\d+)\] = (\d+)");
                    Match match = regex.Match(s);
                    if (match.Success)
                    {
                        int iAdres = int.Parse(match.Groups[1].Value);
                        int iValue = int.Parse(match.Groups[2].Value);
                        long[] Addresses = GetAddresses(ApplyMask2(iAdres,sMask));
                        foreach (long addr in Addresses)
                        {
                            Register[addr] = iValue;
                        }
                    }
                }
            }

            Console.WriteLine(Register.Values.Sum());

        }
        private string ApplyMask2(long iNumber, string sMask)
        {
            string BinValue = (string)Convert.ToString(iNumber, 2);
            string sResult = "";
            for (int i = 0; i < sMask.Length; i++)
            {
                char c = '0';
                if (i < BinValue.Length)
                    c = BinValue[BinValue.Length - 1 - i];

                if (sMask[sMask.Length - 1 - i].Equals('X'))
                    c = 'X';
                else if (sMask[sMask.Length - 1 - i].Equals('1'))
                    c = '1';

                sResult = c + sResult;
            }


            return sResult;

        }

        private long[] GetAddresses(string Address)
        {
            List<long> addresses = new();
            foreach (var item in MakeCombinations(Address))
            {
                addresses.Add(Convert.ToInt64(item, 2));
            }
            return addresses.ToArray();
        }

        private string[] MakeCombinations(string Addresses)
        {
            if (!Addresses.Contains('X'))
            {
                return new[] { Addresses };
            }


            List<string> NewAddresses = new();
            int loc = Addresses.IndexOf("X");
            NewAddresses.AddRange(MakeCombinations(new StringBuilder(Addresses) { [loc] = '1' }.ToString()));
            NewAddresses.AddRange(MakeCombinations(new StringBuilder(Addresses) { [loc] = '0' }.ToString()));
            return NewAddresses.ToArray();
        }

    }
}
