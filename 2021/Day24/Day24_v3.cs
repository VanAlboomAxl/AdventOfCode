//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdventOfCode_2021
//{

//    public class Day24 : Day
//    {
//        public override int _iDay { get { return 24; } }

//        internal override List<string> _lsTest => new List<string>
//        {
//            "inp w",
//            "add z w",
//            "mod z 2",
//            "div w 2",
//            "add y w",
//            "mod y 2",
//            "div w 2",
//            "add x w",
//            "mod x 2",
//            "div w 2",
//            "mod w 2"
//        };
//        public override void Q1()
//        {
//            var lsInput = Input;

//            Logic(lsInput);

//        }

//        public override void Q2()
//        {


//        }

//        public void Logic(List<string> lsInput)
//        {
//            List<(Int64, Int64, Int64)> Params = new();
//            for (int i = 0; i < 18 * 14; i += 18)
//            {
//                Params.Add((
//                    Int64.Parse(lsInput[i + 4].Split()[2]),
//                    Int64.Parse(lsInput[i + 5].Split()[2]),
//                    Int64.Parse(lsInput[i + 15].Split()[2])
//                    ));
//            }

//            Dictionary<Int64, (Int64, Int64)> zs = new();
//            zs.Add(0, (0, 0));
//            for (int i = 0; i < Params.Count; i++)
//            {
//                var p = Params[i];
//                Dictionary<Int64, (Int64, Int64)> new_zs = new();
//                foreach ((var z, var inp) in zs)
//                    for (int w = 1; w < 10; w++)
//                    {
//                        Int64 new_z = f(p, z, w);

//                        //don't bother recording if it's a 'contraction' and we don't contract!
//                        if (p.Item1 == 1 || (p.Item1 == 26 && new_z < z))
//                        {
//                            if (!new_zs.ContainsKey(new_z))
//                                new_zs.Add(new_z, (inp.Item1 * 10 + w, inp.Item2 * 10 + w));
//                            else
//                            {
//                                var result = new_zs[new_z];
//                                result.Item1 = Math.Min(result.Item1, inp.Item1 * 10 + w);
//                                result.Item2 = Math.Max(result.Item2, inp.Item2 * 10 + w);
//                                new_zs[new_z] = result;
//                            }
//                        }

//                    }




//                Console.WriteLine("Digit:" + (i + 1).ToString() + "Tracked values of z:" + new_zs.Count());
//                zs = new_zs;
//            }



//        }
//        private Int64 f((Int64, Int64, Int64) Params, Int64 z, int w)
//        {

//            if ((z % 26 + Params.Item2) != w)
//                z = (Int64)Math.Round((decimal)z / Params.Item1 * 26 + w + Params.Item2);
//            else
//                z = (Int64)Math.Round((decimal)z / Params.Item1);
//            return z;
//        }


//    }
//}
