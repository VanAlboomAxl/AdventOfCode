using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day12 : Day
    {
        public override int _iDay { get { return 12; } }

        public override void Q1()
        {
            var lsInput = Input;
            //lsInput = Test;

            //facing
            int fx = 1;
            int fy = 0;

            int x = 0;
            int y = 0;

            foreach(var sInput in lsInput)
            {
                string sDirection = sInput.Substring(0, 1);
                int iLength = int.Parse(sInput.Substring(1));

                switch(sDirection)
                {
                    case "N": y -=  iLength; break;
                    case "S": y += iLength; break;
                    case "E": x += iLength; break;
                    case "W": x -= iLength; break;
                    case "L": 
                        for(int i=0;i<iLength/90;i++)
                        {
                            if (fx == 0)
                            {
                                fx = fy;
                                fy = 0;
                            }
                            else
                            {
                                if (fx == 1) fy = -1;
                                else fy = 1;
                                fx = 0;
                            }
                        }                     
                        break;
                    case "R":
                        for (int i = 0; i < iLength / 90; i++)
                        {
                            if (fx == 0)
                            {
                                if (fy == 1) fx = -1;
                                else fx = 1;
                                fy = 0;
                            }
                            else
                            {
                                fy = fx;
                                fx = 0;
                            }
                        }
                        break;
                    case "F":
                        x += fx * iLength;
                        y += fy * iLength;
                        break;
                }

            }
            Console.WriteLine($"x: {x}");
            Console.WriteLine($"y: {y}");
            Console.WriteLine($"t: {Math.Abs(x)+ Math.Abs(y)}");

        }

        public override void Q2()
        {
            var lsInput = Input;
            //lsInput = Test;

            //waypoint
            int wx = 10;
            int wy = -1;

            int x = 0;
            int y = 0;

            foreach (var sInput in lsInput)
            {
                string sDirection = sInput.Substring(0, 1);
                int iLength = int.Parse(sInput.Substring(1));

                switch (sDirection)
                {
                    case "N": wy -= iLength; break;
                    case "S": wy += iLength; break;
                    case "E": wx += iLength; break;
                    case "W": wx -= iLength; break;
                    case "L":
                        for (int i = 0; i < iLength%360 / 90; i++)
                        {
                            int wx2 = wy;
                            int wy2 = -wx;
                            wx = wx2;
                            wy = wy2;
                        }
                        break;
                    case "R":
                        for (int i = 0; i < iLength%360 / 90; i++)
                        {
                            int wx2 = -wy;
                            int wy2 = wx;
                            wx = wx2;
                            wy = wy2;
                        }
                        break;
                    case "F":
                        x += wx * iLength;
                        y += wy * iLength;
                        break;
                }

            }
            
            Console.WriteLine($"x: {x}");
            Console.WriteLine($"y: {y}");
            Console.WriteLine($"t: {Math.Abs(x) + Math.Abs(y)}");

        }


    }
}

/*
             int dx = 1;
            int dy = 0;

            int x = 0;
            int y = 0;

            foreach(var sInput in lsInput)
            {
                string sDirection = sInput.Substring(0, 1);
                int iLength = int.Parse(sInput.Substring(1));

                switch(sDirection)
                {
                    case "N": dx = 0; dy = -1; break;
                    case "S": dx = 0; dy = 1; break;
                    case "E": dx = 1; dy = 0; break;
                    case "W": dx = 0-1; dy = 0; break;
                    case "L": 
                        if (dx == 0)
                        {
                            dx = dy;
                            dy = 0;
                        }
                        else
                        {
                            if (dx == 1) dy = -1;
                            else dy = 1;
                            dx = 0;
                        }
                        break;
                    case "R":
                        if (dx == 0)
                        {
                            if (dy == 1) dx = -1;
                            else dx = 1;
                            dy = 0;
                        }
                        else
                        {
                            dy = dx;
                            dx = 0;
                        }
                        break;
                    case "F": 
                        //do nothing here
                        break;
                }
                x += dx * iLength; 
                y += dy * iLength; 

            }
            Console.WriteLine($"x: {x}");
            Console.WriteLine($"y: {y}");
            Console.WriteLine($"t: {x+y}");

 
 
 */