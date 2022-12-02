//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdventOfCode_2021
//{
//    public class Day5_V0 : Day
//    {
//        public override int _iDay { get { return 5; } }

//        private List<string> _lsTest = new List<string>() {
//            "0,9 -> 5,9",
//            "8,0 -> 0,8",
//            "9,4 -> 3,4",
//            "2,2 -> 2,1",
//            "7,0 -> 7,4",
//            "6,4 -> 2,0",
//            "0,9 -> 2,9",
//            "3,4 -> 1,4",
//            "0,0 -> 8,8",
//            "5,5 -> 8,2"
//        };
//        public override List<string> Test { get { return _lsTest; } }

//        private List<string> Map = new()
//        {
//            "0000000000",
//            "0000000000",
//            "0000000000",
//            "0000000000",
//            "0000000000",
//            "0000000000",
//            "0000000000",
//            "0000000000",
//            "0000000000",
//            "0000000000"
//        };

//        public override void Q1()
//        {
//            //var lsInput = Input;
//            var lsInput = Test;
//            foreach(var sInput in lsInput)
//            {
//                var split = sInput.Split("->");
                
//                var Pre = split[0].Trim().Split(",");
//                var x1 = int.Parse(Pre[0]);
//                var y1 = int.Parse(Pre[1]);
                
//                var Post = split[1].Trim().Split(",");
//                var x2 = int.Parse(Post[0]);
//                var y2 = int.Parse(Post[1]);


//                if (x1 == x2)
//                {
//                    int low = y1, high = y2;
//                    if (y1 > y2)
//                    {
//                        low = y2; high = y1;
//                    }

//                    for(int i = low; i <= high; i++)
//                    {
//                        char iCount = Map[x1][i];
//                    }

//                }
//                else
//                {

//                }

//            }

//        }

//        public override void Q2()
//        {
//            var lsInput = Input;
//            //var lsInput = Test;

//            int iOxygen = 0, iCO2 = 0;

//            for (int i = 0; i < lsInput[0].Length; i++)
//            {
//                var lsResult = lsInput.Where(x => x[i] == '1').ToList();

//                double d1 = lsResult.Count() / 1.0;
//                double d2 = lsInput.Count() / 2.0;
                  
//                if (d1 >= d2)
//                    lsInput = lsResult;         
//                else
//                    lsInput = lsInput.Where(x => x[i] == '0').ToList();
                
//                if (lsResult.Count() == 1)
//                    break;
                

//            }
//            iOxygen = Convert.ToInt32(lsInput[0], 2);

//            lsInput = Input;
//            //lsInput = Test;

//            for (int i = 0; i < lsInput[0].Length; i++)
//            {
//                var lsResult = lsInput.Where(x => x[i] == '1').ToList();

//                double d1 = lsResult.Count() / 1.0;
//                double d2 = lsInput.Count() / 2.0;

//                if (d1 < d2)
//                    lsInput = lsResult;
//                else lsInput = lsInput.Where(x => x[i] == '0').ToList();
//                if (lsResult.Count() == 1)
//                {
//                    break;
//                }

//            }
//            iCO2 = Convert.ToInt32(lsInput[0], 2);


//            Console.WriteLine("Oxygen: " + iOxygen);
//            Console.WriteLine("CO2: " + iCO2);
//            Console.WriteLine("Life support: " + iOxygen * iCO2);
//        }

//    }
//}
