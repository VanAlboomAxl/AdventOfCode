using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day10: Day<List<long>>
    {
        public override int _iDay { get { return 10; } }

        List<long> _lsTest2 = new()
        {
            28,
            33,
            18,
            42,
            31,
            14,
            46,
            20,
            48,
            47,
            24,
            23,
            49,
            45,
            19,
            38,
            39,
            11,
            1 ,
            32,
            25,
            35,
            8 ,
            17,
            7 ,
            9 ,
            4 ,
            2 ,
            34,
            10,
            3 
        };

        public override List<long> Convert(List<string> Input)
        {
            return Input.Select(s => Int64.Parse(s)).ToList();
        }

        public override void Q1()
        {
            var liInput = Input;
            //liInput = Test;
            //liInput = _lsTest2;
            liInput.Add(0);
            liInput.Sort();

            int i1 = 0, i2 = 0, i3 = 0;
            for(int i = 1; i < liInput.Count; i++)
            {
                switch (liInput[i] - liInput[i - 1])
                {
                    case 1: i1++;break;
                    case 2: i2++;break;
                    case 3: i3++;break;
                }
            }
            i3++; //always 3 higher

            Console.WriteLine($"1: {i1}");
            Console.WriteLine($"2: {i2}");
            Console.WriteLine($"3: {i3}");
            Console.WriteLine($"1*3: {i1*i3}");

        }

        public override void Q2()
        {
            var liInput = Input;
            //liInput = Test;
            //liInput = _lsTest2;

            liInput.Add(0);
            liInput.Sort();
            liInput.Add(liInput.Max() + 3);

            Console.WriteLine($"Amount: {Logic(liInput)}");






            //List<long> lAmounts = new() {1,1,1};
            //Dictionary<long, long> dllAmounts = new();
            //dllAmounts.Add(liInput.Max(), 1);

            //for(int i = liInput.Count-2; i > -1; i--)
            //{
            //    long result = dllAmounts[liInput[i + 1]];
            //    if (i + 2 < liInput.Count) result += dllAmounts[liInput[i + 2]];
            //    if (i + 3 < liInput.Count) result += dllAmounts[liInput[i + 3]];
            //    dllAmounts.Add(liInput[i],result);
            //}

            ////for (int i = 3; i < liInput.Count; i++)
            ////{
            ////    //if (liInput[i] - liInput[i - 3] < 4) lAmount+=2;
            ////    //else if (liInput[i] - liInput[i - 2] < 4) lAmount++;
            ////    if (liInput[i] - liInput[i - 3] < 4) lAmount *= 3;
            ////    else if (liInput[i] - liInput[i - 2] < 4) lAmount *=2;
            ////}

            ////lAmount++; //always 3 higher

            //Console.WriteLine($"Amount: {dllAmounts[0]}");
        }

        //credit to matthias


        public static Func<long, long> Tribonnaci()
        {
            return n =>
            {
                if (n < 0)
                    return 0;

                if (n == 0)
                    return 1;
                else
                {
                    Func<long, long> Trib = Tribonnaci();
                    return Trib(n - 1) +
                           Trib(n - 2) +
                           Trib(n - 3);
                }
            };
        }
        Func<long, long> Trib = Tribonnaci();


        private long Logic(List<long> input)
        {
            Dictionary<int, int> _values = new();
            long last = 0;
            int OneCounter = 0;
            for (int i = 0; i < input.Count(); i++)
            {
                if (input[i] - last == 1)
                {
                    OneCounter++;
                }
                else
                {
                    if (_values.ContainsKey(OneCounter))
                    {
                        _values[OneCounter]++;
                    }
                    else
                    {
                        _values[OneCounter] = 1;
                    }
                    OneCounter = 0;
                }
                last = input[i];
            }

            if (_values.ContainsKey(OneCounter))
            {
                _values[OneCounter]++;
            }
            else
            {
                _values[OneCounter] = 1;
            }

            long product = 1;
            foreach (KeyValuePair<int, int> item in _values)
            {
                product *= (long)(Math.Pow(Trib(item.Key), item.Value));
            }
            return product;
        }

    }
}
