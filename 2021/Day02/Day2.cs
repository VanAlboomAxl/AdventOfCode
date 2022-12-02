using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day2
    {
        public string InputLocation = @"C:\Users\Axl Van Alboom\Desktop\AdventOfCode21\AdventOfCode21\Day2\input.txt";

        public List<string> Test => new List<string>() {
            "forward 5",
            "down 5",
            "forward 8",
            "up 3",
            "down 8",
            "forward 2"
        };

        public void Q1()
        {
            var lsInput = Helper.ReadInput(InputLocation);

            int iHor = 0, iDepth = 0;
            
            foreach(var s in lsInput)
            {
                var split = s.Split(" ");

                string sCmd = split[0];
                int iDistance = int.Parse(split[1]);

                switch (sCmd)
                {
                    case "forward":
                        iHor += iDistance; break;
                    case "up":
                        iDepth -= iDistance; break;
                    case "down":
                        iDepth += iDistance; break;
                }
            }

            Console.WriteLine(iHor*iDepth);
        }

        public void Q2()
        {
            var lsInput = Helper.ReadInput(InputLocation);
            //var lsInput = Test;
            
            int iHor = 0, iDepth = 0, iAim=0;

            foreach (var s in lsInput)
            {
                var split = s.Split(" ");

                string sCmd = split[0];
                int iDistance = int.Parse(split[1]);

                switch (sCmd)
                {
                    case "forward":
                        iHor += iDistance;
                        iDepth += iAim * iDistance;
                        break;
                    case "up":
                        iAim -= iDistance; break;
                    case "down":
                        iAim += iDistance; break;
                }
            }

            Console.WriteLine(iHor );
            Console.WriteLine(iDepth);
            Console.WriteLine(iHor * iDepth);
        }


    }
}
