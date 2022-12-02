using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day14 : Day
    {
        public override int _iDay { get { return 14; } }

        internal override List<string> _lsTest =>  new List<string>() {
            "NNCB",
            "",
            "CH -> B",
            "HH -> N",
            "CB -> H",
            "NH -> C",
            "HB -> C",
            "HC -> B",
            "HN -> C",
            "NN -> C",
            "BH -> H",
            "NC -> B",
            "NB -> B",
            "BN -> B",
            "BB -> N",
            "BC -> B",
            "CC -> N",
            "CN -> C"
        };

        public override void Q1()
        {
            return;

            var lsInput = Input;
            //var lsInput = Test;
            string sPolymer = lsInput[0];
            Dictionary<string, string> Insertions = new();
            for(int i = 2; i < lsInput.Count; i++)
            {
                string[] asSplit = lsInput[i].Split("->");
                Insertions.Add(asSplit[0].Trim(), asSplit[1].Trim());
            }

            for (int i = 0; i < 10; i++)
            {
                string sNewVersion = sPolymer;
                int iInsertions = 0;
                for(int j = 0; j < sPolymer.Count()-1;j++)
                {
                    string sMatch = sPolymer.Substring(j, 2);
                    if (Insertions.ContainsKey(sMatch))
                    {
                        sNewVersion = sNewVersion.Insert(j+1+iInsertions, Insertions[sMatch]);
                        iInsertions++;
                    }
                }

                sPolymer = sNewVersion;

            }

            List<int> Amounts = new();
            foreach(var c in sPolymer) // to be update to unique chars!
                Amounts.Add(sPolymer.Count(x => x.Equals(c)));

            int iresult = Amounts.Max() - Amounts.Min();
            Console.WriteLine("result: " + iresult);
        }

        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;

            string sPolymer = lsInput[0];
            Dictionary<string, Int64> dPolymer = new();
            for(int i = 0; i < sPolymer.Length - 1; i++)
            {
                string sub = sPolymer.Substring(i, 2);
                if (dPolymer.ContainsKey(sub))
                    dPolymer[sub]++;
                else
                    dPolymer.Add(sub, 1);
            }

            Dictionary<string, (string,string)> Insertions = new();
            for (int i = 2; i < lsInput.Count; i++)
            {
                string[] asSplit = lsInput[i].Split("->");
                var sMatch = asSplit[0].Trim();
                var sInsert = asSplit[1].Trim();
                Insertions.Add(sMatch, (sMatch[0]+sInsert, sInsert+sMatch[1]));
            }
                   
            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, Int64> dNewPolymer = new();
                foreach ((string sMatch, Int64 iCount) in dPolymer)
                {
                    (var Insert1, var Insert2) = Insertions[sMatch];
                    if (dNewPolymer.ContainsKey(Insert1)) 
                        dNewPolymer[Insert1] += iCount;
                    else 
                        dNewPolymer.Add(Insert1, iCount);
                    if (dNewPolymer.ContainsKey(Insert2)) 
                        dNewPolymer[Insert2] += iCount;
                    else 
                        dNewPolymer.Add(Insert2, iCount);           
                }
                dPolymer = dNewPolymer;
            }

            Dictionary<char, Int64> dCounts = new();       
            foreach ((string key, Int64 iCount) in dPolymer)
            {
                //enkel 2de deel optelen, anders krijg je dubbele tellingen
                if (dCounts.ContainsKey(key[1]))
                    dCounts[key[1]] += iCount;
                else dCounts.Add(key[1], iCount);
            }
            dCounts[lsInput[0][0]]++; // eerste char van polymeer niet vergeten
            Int64 iresult = dCounts.Values.Max() - dCounts.Values.Min();
            Console.WriteLine("result: " + iresult);
        }
        public  void Q2_2()
        {
            //var lsInput = Input;
            var lsInput = Test;

            string sPolymer = lsInput[0];
            Dictionary<string, string> Insertions = new();
            for (int i = 2; i < lsInput.Count; i++)
            {
                string[] asSplit = lsInput[i].Split("->");
                Insertions.Add(asSplit[0].Trim(), asSplit[1].Trim());
            }


            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine("Step " + (i + 1));
                Int64 iInserts = 0;
                List<(Int64, string)> replaces = new();
                foreach ((var key, var value) in Insertions)
                {
                    sPolymer = sPolymer.Replace(key, $"{key[0]}{iInserts}{key[1]}");
                    replaces.Add((iInserts, value));
                    iInserts++;
                }
                foreach ((Int64 index, string value) in replaces)
                {
                    sPolymer = sPolymer.Replace(index.ToString(), value);
                }
            }

            List<Int64> Amounts = new();
            List<char> Checked = new();
            foreach (var c in sPolymer)
                if (!Checked.Contains(c))
                {
                    Checked.Add(c);
                    Int64 iCount = 0;
                    foreach (var c2 in sPolymer)
                        if (c.Equals(c2))
                            iCount++;
                    Amounts.Add(iCount);
                }

            Int64 iresult = Amounts.Max() - Amounts.Min();
            Console.WriteLine("result: " + iresult);
        }
        public void Q2_v1()
        {
            //var lsInput = Input;
            var lsInput = Test;

            string sPolymer = lsInput[0];
            Dictionary<string, string> Insertions = new();
            for (int i = 2; i < lsInput.Count; i++)
            {
                string[] asSplit = lsInput[i].Split("->");
                Insertions.Add(asSplit[0].Trim(), asSplit[1].Trim());
            }

            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine("Step " + (i + 1));
                foreach ((var key, var value) in Insertions)
                    sPolymer = sPolymer.Replace(key, $"{key[0]}1{value}1{key[1]}");

                sPolymer = sPolymer.Replace("1", "");
            }

            List<Int64> Amounts = new();
            List<char> Checked = new();
            foreach (var c in sPolymer)
                if (!Checked.Contains(c))
                {
                    Checked.Add(c);
                    Int64 iCount = 0;
                    foreach (var c2 in sPolymer)
                        if (c.Equals(c2))
                            iCount++;
                    Amounts.Add(iCount);
                }

            Int64 iresult = Amounts.Max() - Amounts.Min();
            Console.WriteLine("result: " + iresult);
        }
        public void Q2_0()
        {
            //var lsInput = Input;
            var lsInput = Test;

            string sPolymer = lsInput[0];
            Dictionary<string, string> Insertions = new();
            for (int i = 2; i < lsInput.Count; i++)
            {
                string[] asSplit = lsInput[i].Split("->");
                Insertions.Add(asSplit[0].Trim(), asSplit[1].Trim());
            }

            for (int i = 0; i < 40; i++)
            {
                string sNewVersion = sPolymer;
                int iInsertions = 0;
                for (int j = 0; j < sPolymer.Count() - 1; j++)
                {
                    string sMatch = sPolymer.Substring(j, 2);
                    if (Insertions.ContainsKey(sMatch))
                    {
                        sNewVersion = sNewVersion.Insert(j + 1 + iInsertions, Insertions[sMatch]);
                        iInsertions++;
                    }
                }

                sPolymer = sNewVersion;

            }

            List<Int64> Amounts = new();
            List<char> Checked = new();
            foreach (var c in sPolymer)
                if (!Checked.Contains(c))
                {
                    Checked.Add(c);
                    Int64 iCount = 0;
                    foreach (var c2 in sPolymer)
                        if (c.Equals(c2))
                            iCount++;
                    Amounts.Add(iCount);
                }

            Int64 iresult = Amounts.Max() - Amounts.Min();
            Console.WriteLine("result: " + iresult);
        }

    }
}
