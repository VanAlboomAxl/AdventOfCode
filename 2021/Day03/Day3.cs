using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day3 : Day
    {

        public override int _iDay { get { return 3; } }

        internal override List<string> _lsTest => new List<string>() {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        public override void Q1()
        {
            var lsInput = Input;
            //var lsInput = Test;

            int iGamma = 0, iEpsilon = 0;
            string sGamma = "";
            string sEpsilon = "";
            for(int i = 0; i < lsInput[0].Length; i++)
            {
                var lsResult = lsInput.Select(x => x[i]).Where(x => x == '1').ToList();
                if (lsResult.Count() > (lsInput.Count() / 2))
                {
                    sGamma += "1";// + sGamma;
                    sEpsilon += "0";
                }
                else
                {
                    sGamma += "0";// + sGamma;
                    sEpsilon += "1";
                }
            }
            iGamma = Convert.ToInt32(sGamma, 2);
            iEpsilon = Convert.ToInt32(sEpsilon, 2);


            Console.WriteLine("Gamma: "+iGamma);
            Console.WriteLine("Epsilon: "+ iEpsilon);
            Console.WriteLine("Power: " +iGamma * iEpsilon);
        }

        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;

            int iOxygen = 0, iCO2 = 0;

            for (int i = 0; i < lsInput[0].Length; i++)
            {
                var lsResult = lsInput.Where(x => x[i] == '1').ToList();

                double d1 = lsResult.Count() / 1.0;
                double d2 = lsInput.Count() / 2.0;
                  
                if (d1 >= d2)
                    lsInput = lsResult;         
                else
                    lsInput = lsInput.Where(x => x[i] == '0').ToList();
                
                if (lsResult.Count() == 1)
                    break;
                

            }
            iOxygen = Convert.ToInt32(lsInput[0], 2);

            lsInput = Input;
            //lsInput = Test;

            for (int i = 0; i < lsInput[0].Length; i++)
            {
                var lsResult = lsInput.Where(x => x[i] == '1').ToList();

                double d1 = lsResult.Count() / 1.0;
                double d2 = lsInput.Count() / 2.0;

                if (d1 < d2)
                    lsInput = lsResult;
                else lsInput = lsInput.Where(x => x[i] == '0').ToList();
                if (lsResult.Count() == 1)
                {
                    break;
                }

            }
            iCO2 = Convert.ToInt32(lsInput[0], 2);


            Console.WriteLine("Oxygen: " + iOxygen);
            Console.WriteLine("CO2: " + iCO2);
            Console.WriteLine("Life support: " + iOxygen * iCO2);
        }

    }
}
