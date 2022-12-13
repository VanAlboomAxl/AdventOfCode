using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day11 : Day
    {
        public override int _iDay { get { return 11; } }

        public override string Q1()
        {
            var lsInput = Data;
            //lsInput = Test;
            List<clsMonkey> monkeys = new();
            long maxWorry = 1;
            for (int i = 0; i < lsInput.Count; i += 7)
            {
                clsMonkey m = new(lsInput.GetRange(i, 6), 3);
                monkeys.Add(m);
                maxWorry *= m.Test;
            }
            foreach(var m in monkeys)
            {
                m.maxWorry = maxWorry;
                m.TrueFriend = monkeys.Where(x=>x.Name.Equals(m.trueFriend)).FirstOrDefault();
                m.FalseFriend = monkeys.Where(x=>x.Name.Equals(m.falseFriend)).FirstOrDefault();
            }

            logic(20, monkeys, l=>l/3);
            monkeys = monkeys.OrderByDescending(x => x.inspected).ToList();
            return (monkeys[0].inspected * monkeys[1].inspected).ToString();
        }

        public void logic(int iRounds, List<clsMonkey> Monkeys, Func<long,long> reduceWorry)
        {
            for (int i = 0; i < iRounds; i++)
            {
                for (int j = 0; j < Monkeys.Count; j++)
                {
                    clsMonkey m = Monkeys[j];
                    m.Round(reduceWorry);
                }

            }
        }
        public override string Q2()
        {
            var lsInput = Data;
            //lsInput = Test;
            List<clsMonkey> monkeys = new();
            long maxWorry = 1;
            for (int i = 0; i < lsInput.Count; i += 7)
            {
                clsMonkey m = new(lsInput.GetRange(i, 6), 1);
                monkeys.Add(m);
                maxWorry *= m.Test;
            }
            foreach (var m in monkeys)
            {
                m.maxWorry = maxWorry;
                m.TrueFriend = monkeys.Where(x => x.Name.Equals(m.trueFriend)).FirstOrDefault();
                m.FalseFriend = monkeys.Where(x => x.Name.Equals(m.falseFriend)).FirstOrDefault();
            }

            logic(10000, monkeys, l => l % maxWorry);
            monkeys = monkeys.OrderByDescending(x => x.inspected).ToList();
            return (monkeys[0].inspected * monkeys[1].inspected).ToString();
        }

        public class clsMonkey
        {
            public int Bored { get; set; }
            public long inspected { get; set; }
            public long maxWorry { get; set; }
            public string Name { get; private set; }
            public List<long> Items { get; private set; }
            public int Test { get; private set; }
            public string trueFriend { get; private set; }
            public string falseFriend { get; private set; }
            public Func<long, long> fOperation { get; private set; }
            //public delegate long fOperation(long l);
            public clsMonkey TrueFriend { get; set; }
            public clsMonkey FalseFriend { get; set; }
            public clsMonkey(List<string> data, int iBored)
            {
                Bored = iBored;
                Name = data[0].Split(":")[0].Split(" ")[1];
                Items = new();
                foreach (var s in data[1].Split(":")[1].Replace(" ", "").Split(","))
                {
                    Items.Add(long.Parse(s));
                }
                string[] operationData = data[2].Split("=")[1].Split(" ");
                int iOperation = -1;
                if (operationData[3] != "old") iOperation = int.Parse(operationData[3]);
                if (operationData[2] == "+")
                {
                    if (iOperation > 0) fOperation = l => l + iOperation;
                    else fOperation = l => l + l;
                }
                else
                {
                    if (iOperation > 0) fOperation = l => l * iOperation;
                    else fOperation = l => l * l;
                }

                Test = int.Parse(data[3].Trim().Split(" ")[3]);
                trueFriend = data[4].Replace(" ", "").Split("monkey")[1];
                falseFriend = data[5].Replace(" ", "").Split("monkey")[1];
            }

            public void Round(Func<long,long> reduceWorryLevel)
            {
                while (Items.Count > 0)
                {
                    var item = Items[0];
                    Items.RemoveAt(0);
                    //item = (long)Math.Floor((double)fOperation(item) / Bored);
                    item = fOperation(item);// / Bored;
                    //item = item % maxWorry;
                    item = reduceWorryLevel(item);
                    if (item % Test == 0) TrueFriend.Items.Add(item);
                    else FalseFriend.Items.Add(item );
                    inspected++;
                }
            }

            public override string ToString()
            {
                return $"{Name}: "+String.Join(',',Items);
            }
        }
    }
}
