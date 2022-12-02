using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day11 : Day
    {
        public override int _iDay { get { return 11; } }

        internal override List<string> _lsTest => new List<string>() {
            "5483143223",
            "2745854711",
            "5264556173",
            "6141336146",
            "6357385478",
            "4167524645",
            "2176841721",
            "6882881134",
            "4846848554",
            "5283751526"
        };
      
        public override void Q1()
        {
            var lsInput = Input;
            //var lsInput = Test;
            var lliInput = Helper.Convertor_LLI(lsInput);

            int iFlashed = 0;
            for(int i = 0; i < 100; i++)
            {
                //1: all + 1:
                List<(int,int)> Flashing = new();
                for(int r = 0; r < lliInput.Count(); r++)
                    for(int c=0;c<lliInput[r].Count(); c++)
                        if (IncreaseEnergy(lliInput, r, c) > 9)
                            Flashing.Add((r, c));               

                List<(int, int)> Flashed = new();
                //2:  
                while (Flashing.Count > 0)
                {
                    (int r, int c) = Flashing[0];
                    Flashing.RemoveAt(0);
                    if (Flashed.Contains((r, c))) continue;

                    Flashed.Add((r, c));

                    if (c > 0)
                    {
                        if (IncreaseEnergy(lliInput, r, c - 1) > 9)
                            Flashing.Add((r, c - 1));
                        if (r> 0)
                            if (IncreaseEnergy(lliInput, r-1, c - 1) > 9)
                                Flashing.Add((r-1, c - 1));
                        if (r < lliInput.Count()-1)
                            if (IncreaseEnergy(lliInput, r + 1, c - 1) > 9)
                                Flashing.Add((r + 1, c - 1));
                    }

                    if (r > 0)
                        if (IncreaseEnergy(lliInput, r - 1, c) > 9)
                            Flashing.Add((r - 1, c ));
                    if (r < lliInput.Count() - 1)
                        if (IncreaseEnergy(lliInput, r + 1, c) > 9)
                            Flashing.Add((r + 1, c));

                    if (c < lliInput[r].Count() - 1)
                    {
                        if (IncreaseEnergy(lliInput, r, c + 1) > 9)
                            Flashing.Add((r, c + 1));
                        if (r > 0)
                            if (IncreaseEnergy(lliInput, r - 1, c +1) > 9)
                                Flashing.Add((r - 1, c + 1));
                        if (r < lliInput.Count() - 1)
                            if (IncreaseEnergy(lliInput, r + 1, c + 1) > 9)
                                Flashing.Add((r + 1, c + 1));
                    }

                }
                iFlashed += Flashed.Count();

                foreach((int r, int c) in Flashed)
                    lliInput[r][c] = 0;
                

                //foreach(var r in lliInput)
                //{
                //    foreach(var c in r)
                //    {
                //        Console.Write(c);
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine("--------------------");

            }

            Console.WriteLine("Flashes: " + iFlashed);


        }

        private int IncreaseEnergy(List<List<int>> lliInput, int r, int c)
        {
            int iEnergy = lliInput[r][c] + 1;
            lliInput[r][c] = iEnergy;
            return iEnergy;
        }

        private void WriteMap(List<List<int>> lliInput, string sTitle=null)
        {
            if (sTitle != null)
                Console.WriteLine(sTitle);
            foreach (var r in lliInput)
            {
                foreach (var c in r)
                    Console.Write(c);               
                Console.WriteLine();
            }
            Console.WriteLine("--------------------");
        }

        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;
            var lliInput = Helper.Convertor_LLI(lsInput);

            int iFlashed = 0;
            int i = 1;
            while(i>0)
            {
                //1: all + 1:
                List<(int, int)> Flashing = new();
                for (int r = 0; r < lliInput.Count(); r++)
                    for (int c = 0; c < lliInput[r].Count(); c++)
                        if (IncreaseEnergy(lliInput, r, c) > 9)
                            Flashing.Add((r, c));

                List<(int, int)> Flashed = new();
                //2:  
                while (Flashing.Count > 0)
                {
                    (int r, int c) = Flashing[0];
                    Flashing.RemoveAt(0);
                    if (Flashed.Contains((r, c))) continue;

                    Flashed.Add((r, c));

                    if (c > 0)
                    {
                        if (IncreaseEnergy(lliInput, r, c - 1) > 9)
                            Flashing.Add((r, c - 1));
                        if (r > 0)
                            if (IncreaseEnergy(lliInput, r - 1, c - 1) > 9)
                                Flashing.Add((r - 1, c - 1));
                        if (r < lliInput.Count() - 1)
                            if (IncreaseEnergy(lliInput, r + 1, c - 1) > 9)
                                Flashing.Add((r + 1, c - 1));
                    }

                    if (r > 0)
                        if (IncreaseEnergy(lliInput, r - 1, c) > 9)
                            Flashing.Add((r - 1, c));
                    if (r < lliInput.Count() - 1)
                        if (IncreaseEnergy(lliInput, r + 1, c) > 9)
                            Flashing.Add((r + 1, c));

                    if (c < lliInput[r].Count() - 1)
                    {
                        if (IncreaseEnergy(lliInput, r, c + 1) > 9)
                            Flashing.Add((r, c + 1));
                        if (r > 0)
                            if (IncreaseEnergy(lliInput, r - 1, c + 1) > 9)
                                Flashing.Add((r - 1, c + 1));
                        if (r < lliInput.Count() - 1)
                            if (IncreaseEnergy(lliInput, r + 1, c + 1) > 9)
                                Flashing.Add((r + 1, c + 1));
                    }

                }
                iFlashed += Flashed.Count();

                foreach ((int r, int c) in Flashed)
                    lliInput[r][c] = 0;


               
                
                if (Flashed.Count == lliInput.Count() * lliInput[0].Count())
                {
                    break;
                }
                i++;

            }

            Console.WriteLine("step: " + i);


        }

    }

}
