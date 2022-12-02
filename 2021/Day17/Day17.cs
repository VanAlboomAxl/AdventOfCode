using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day17 : Day<string>
    {
        public override int _iDay { get { return 17; } }

        internal override List<string> _lsTest => new List<string> {
            "target area: x=20..30, y=-10..-5"
        };

        public override string Convert(List<string> Input)
        {
            return Input[0];
        }

        public override void Q1()
        {
            //input target area: x=70..96, y=-179..-124
           
            //Logic((20, 30), (-10, -5));
            Logic((70, 96), (-179, -124));
            //string sInput = Input;
            //PossibleX((20, 30));
            //Console.WriteLine(Logic((7, 2), (20, 30), (-10, -5)));
            //Console.WriteLine(Logic((6, 3), (20, 30), (-10, -5)));
            //Console.WriteLine(Logic((9, 0), (20, 30), (-10, -5)));
            //Console.WriteLine(Logic((17, -4), (20, 30), (-10, -5)));
        }

        private (int,int) Logic((int,int) iiXrange, (int,int) iiYrange)
        {
            int iMaxHeight = int.MinValue;
            (int, int) iiMaxHeight = (0, 0);
            foreach (var x in PossibleX(iiXrange))
            {
                for(int y = 0; y < 1000;y++)
                {
                    int iResult = Logic((x, y), iiXrange, iiYrange);
                    if (iResult > iMaxHeight)
                    {
                        iMaxHeight = iResult;
                        iiMaxHeight = (x, y);
                    }
                }
            }

            return iiMaxHeight;
        }
        
        private int Logic((int,int) Velocity, (int, int) iiXrange, (int, int) iiYrange)
        {
            int iMaxHeight = int.MinValue;
            int iX = 0, iY = 0;
            while (true)
            {
                iX += Velocity.Item1;
                iY += Velocity.Item2;
                Velocity = StepLogic(Velocity.Item1, Velocity.Item2);
                if (iY > iMaxHeight)
                    iMaxHeight = iY;
                if (iiXrange.Item1<=iX && iX<= iiXrange.Item2 &&
                    iiYrange.Item1<=iY && iY<= iiYrange.Item2)
                    return iMaxHeight;
                
                if (iY < iiYrange.Item1)
                    // not gone work
                    return int.MinValue;
            }

            return int.MinValue;
        }
        
        private (int,int) StepLogic(int x, int y)
        {
            if (x > 0) x--;
            else if (x < 0) x++;
            y--;
            return (x, y);
        }

        private List<int> PossibleX((int, int) iiXrange)
        {
            List<int> liRange = new();

            int i = 0;
            while(i <= iiXrange.Item2)
            {
                int x = 0;
                int j = i;
                while (j > 0)
                {
                    x += j;
                    if (iiXrange.Item1 <= x && x <= iiXrange.Item2)
                    {
                        liRange.Add(i);
                        j = -1;
                    }
                    j--;
                }
                i++;
            }

            return liRange;
        }
        private List<int> PossibleY((int, int) iiYrange)
        {
            // from lower bound to 0 --> easy
            // 7 up means start -8 down --> needed
            // 0 to -(lowerbound) --> easy
            List<int> liRange = new();
            int velocity = 0;

            while (true)
            {

                int iMaxHeight = 0;
                for(int i=0;i<velocity;i++)
                {
                    iMaxHeight += (velocity - i);
                }


                velocity++;
            }

            return liRange;
        }


        public override void Q2()
        {
            //Console.WriteLine(Logic_Q2((20, 30), (-10, -5)));
            Console.WriteLine(Logic_Q2((70, 96), (-179, -124)));
        }
        private int Logic_Q2((int, int) iiXrange, (int, int) iiYrange)
        {
            List<(int, int)> liiResults = new();
            foreach (var x in PossibleX(iiXrange))
            {
                for (int y = -1000; y < 1000; y++)
                {
                    int iResult = Logic((x, y), iiXrange, iiYrange);
                    if (iResult > int.MinValue)
                    {
                        liiResults.Add((x, y));
                    }
                }
            }

            return liiResults.Count();
        }
        

    }

}
