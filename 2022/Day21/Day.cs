using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day21 : Day
    {
        public override int _iDay { get { return 21; } }

        public override string Q1()
        {
            var lsInput = Data;
            Dictionary<string, long> monkeyNumbers = new();
            Dictionary<string, (string,Func<long,long,long>,string)> monkeyMath = new();
            foreach(var s in lsInput)
            {
                string[] strings = s.Split(' ');
                string monkey = strings[0].Split(":")[0];
                if (strings.Length == 2)
                {
                    long num = long.Parse(strings[1]);
                    monkeyNumbers.Add(monkey, num);
                    update(monkeyNumbers, monkeyMath);
                    if (monkeyNumbers.ContainsKey("root")) 
                        return monkeyNumbers["root"].ToString();
                }
                else
                {
                    Func<long, long, long> f = plus;
                    if (strings[2] == "-") f = min;
                    else if (strings[2] == "*") f = maal;
                    else if (strings[2] == "/") f = delen;
                    monkeyMath.Add(monkey,(strings[1],f, strings[3]));
                }
            }
            return "";
        }
        long plus(long a, long b) => a + b;
        long min(long a, long b) => a - b;
        long maal(long a, long b) => a * b;
        long delen(long a, long b) => a / b;
        void update(Dictionary<string,long> monkeyNumbers, Dictionary<string, (string, Func<long, long, long>, string)> monkeyMath)
        {
            bool xUpdated = false;
            foreach(var m in monkeyMath)
            {
                var val = m.Value;
                if (monkeyNumbers.ContainsKey(val.Item1) && monkeyNumbers.ContainsKey(val.Item3))
                {
                    monkeyNumbers.Add(m.Key, m.Value.Item2(monkeyNumbers[val.Item1], monkeyNumbers[val.Item3]));
                    monkeyMath.Remove(m.Key);
                    xUpdated = true;
                }
            }
            if(xUpdated) update(monkeyNumbers, monkeyMath);
        }

        public override string Q2()
        {
            var lsInput = Data;
            Dictionary<string, long> monkeyNumbers = new();
            Dictionary<string, (string, Func<long, long, long>, string)> monkeyMath = new();
            string sRootMonkey1 = null, sRootMonkey2 = null;
            foreach (var s in lsInput)
            {
                string[] strings = s.Split(' ');
                string monkey = strings[0].Split(":")[0];
                if (monkey == "humn")
                {
                    //pass
                }
                else if (monkey == "root")
                {
                    sRootMonkey1 = strings[1];
                    sRootMonkey2 = strings[3];
                }
                else if (strings.Length == 2)
                {
                    long num = long.Parse(strings[1]);
                    monkeyNumbers.Add(monkey, num);
                    update(monkeyNumbers, monkeyMath);
                    //if (monkeyNumbers.ContainsKey("root"))
                    //    return monkeyNumbers["root"].ToString();
                }
                //else if (strings[1] == "humn" )
                //{
                //    Func<long, long, long> f = min;
                //    if (strings[2] == "-") f = plus;
                //    else if (strings[2] == "*") f = delen;
                //    else if (strings[2] == "/") f = maal;
                //    monkeyMath.Add(strings[1], (monkey, f, strings[3]));
                //}
                //else if (strings[3] == "humn")
                //{
                //    Func<long, long, long> f = min;
                //    if (strings[2] == "+") monkeyMath.Add(monkey, (strings[1], f, strings[3])); 
                //    else if (strings[2] == "-") f = min;
                //    else if (strings[2] == "*") f = delen;
                //    else if (strings[2] == "/") f = delen;


                //}
                else
                {
                    Func<long, long, long> f = plus;
                    if (strings[2] == "-") f = min;
                    else if (strings[2] == "*") f = maal;
                    else if (strings[2] == "/") f = delen;
                    monkeyMath.Add(monkey, (strings[1], f, strings[3]));
                }
            }
            long eq = 0;
            string otherMonkey = null;
            if (monkeyNumbers.ContainsKey(sRootMonkey1)) 
            { 
                eq = monkeyNumbers[sRootMonkey1];
                otherMonkey = sRootMonkey2;
                monkeyNumbers.Add(otherMonkey, eq);
            }
            if (monkeyNumbers.ContainsKey(sRootMonkey2)) 
            {
                eq = monkeyNumbers[sRootMonkey2];
                otherMonkey = sRootMonkey1;
                monkeyNumbers.Add(otherMonkey, eq);
            }

            while (monkeyMath.Count > 0)
            {
                string newMonkey = null;
                var math = monkeyMath[otherMonkey];
                if (monkeyNumbers.ContainsKey(math.Item1))
                {
                    //long1 = long2 func ? --> hieruit ? afleiden
                    // ? = long1 func* long2
                    long lResult = 0;
                    if (math.Item2 == plus)
                    {
                        //long 1 = long 2 + ?
                        //? = long 1 - long 2
                        lResult = min(monkeyNumbers[otherMonkey], monkeyNumbers[math.Item1]);
                        monkeyNumbers.Add(math.Item3, lResult);
                    }
                    else if (math.Item2 == min)
                    {
                        //long 1 = long 2 - ?
                        //? = long 2 - long 1
                        lResult = min(monkeyNumbers[math.Item1], monkeyNumbers[otherMonkey]);
                        monkeyNumbers.Add(math.Item3, lResult);
                    }
                    else if (math.Item2 == maal)
                    {
                        //long 1 = long 2 * ?
                        //? = long 1 / long 2
                        lResult = delen(monkeyNumbers[otherMonkey], monkeyNumbers[math.Item1]);
                        monkeyNumbers.Add(math.Item3, lResult);
                    }
                    else if (math.Item2 == delen)
                    {
                        //long 1 = long 2 /?
                        //? = long 2 / long 1
                        lResult = delen(monkeyNumbers[math.Item1], monkeyNumbers[otherMonkey]);
                        monkeyNumbers.Add(math.Item3, lResult);
                    }
                    newMonkey = math.Item3;
                }
                else
                {
                    //long1 = ? func long2 --> hieruit ? afleiden
                    // ? = long1 func* long2
                    long lResult = 0;
                    if (math.Item2 == plus)
                    {
                        //long 1 = ? + long 2 
                        //? = long 1 - long 2
                        lResult = min(monkeyNumbers[otherMonkey], monkeyNumbers[math.Item3]);
                        monkeyNumbers.Add(math.Item1, lResult);
                    }
                    else if (math.Item2 == min)
                    {
                        //long 1 = ? - long 2
                        //? = long 1 + long 2
                        lResult = plus(monkeyNumbers[math.Item3], monkeyNumbers[otherMonkey]);
                        monkeyNumbers.Add(math.Item1, lResult);
                    }
                    else if (math.Item2 == maal)
                    {
                        //long 1 = ? * long 2 
                        //? = long 1 / long 2
                        lResult = delen(monkeyNumbers[otherMonkey], monkeyNumbers[math.Item3]);
                        monkeyNumbers.Add(math.Item1, lResult);
                    }
                    else if (math.Item2 == delen)
                    {
                        //long 1 = ? * long2
                        //? = long 2 / long 1
                        lResult = maal(monkeyNumbers[math.Item3], monkeyNumbers[otherMonkey]);
                        monkeyNumbers.Add(math.Item1, lResult);
                    }
                    newMonkey = math.Item1;
                }
                monkeyMath.Remove(otherMonkey);
                otherMonkey = newMonkey;
            }

            return monkeyNumbers["humn"].ToString();
        }

    }
}
