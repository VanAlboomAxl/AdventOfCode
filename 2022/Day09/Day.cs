using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day9 : Day
    {
        public override int _iDay { get { return 9; } }

        
        public override string Q1()
        {
            Point H = new Point(0,0);
            Point T = new Point(0,0);
            HashSet<(int, int)> visits = new() { (0, 0) };
            foreach(var d in Data)
            {
                string[] instruction = d.Split(" ");
                int iLoop = int.Parse(instruction[1]);
                switch (instruction[0]) 
                {
                    case "R": 
                        for(int i = 0; i < iLoop; i++)
                        {
                            Right(ref H); MoveTail(ref H, ref T, visits);
                        }
                        break;
                    case "L":
                        for (int i = 0; i < iLoop; i++)
                        {
                            Left(ref H); MoveTail(ref H, ref T, visits);
                        }
                        break;
                    case "U":
                        for (int i = 0; i < iLoop; i++)
                        {
                            Up(ref H); MoveTail(ref H, ref T, visits);
                        }
                        break;
                    case "D":
                        for (int i = 0; i < iLoop; i++)
                        {
                            Down(ref H); MoveTail(ref H, ref T, visits);
                        }
                        break;
                }

            }
            return visits.Count().ToString();
        }
        private void Right(ref Point H)
        {
            H.X += 1;
        }
        private void Left(ref Point H)
        {
            H.X -= 1;
        }
        private void Up(ref Point H)
        {
            H.Y += 1;
        }
        private void Down(ref Point H)
        {
            H.Y -= 1;
        }

        private void MoveTail(ref Point H, ref Point T, HashSet<(int, int)> visits)
        {
            bool xDiagonal=false;
            if ((Math.Abs(H.X-T.X)>1 || Math.Abs(H.Y-T.Y)>1) && H.X != T.X && H.Y != T.Y)
                xDiagonal = true;

            int dX = (H.X == T.X) ? 0 : (H.X-T.X>1 || (xDiagonal && (H.X-T.X ==1))) ? 1 : (T.X -H.X >1 || (xDiagonal && (T.X - H.X == 1))) ? - 1 :0;
            int dY = (H.Y == T.Y) ? 0 : (H.Y-T.Y>1 || (xDiagonal && (H.Y - T.Y == 1))) ? 1 : (T.Y - H.Y >1 || (xDiagonal && (T.Y - H.Y == 1))) ? -1 : 0;
            T.X+= dX;
            T.Y += dY;
            visits.Add((T.X, T.Y));
        }

        public override string Q2()
        {
            Point H = new Point(0, 0);
            List<Point> Tails = new();
            for (int i = 0; i < 9; i++) Tails.Add(new(0, 0));
            HashSet<(int, int)> visits = new() { (0, 0) };
            HashSet<(int, int)> visits9 = new() { (0, 0) };
            foreach (var d in Data)
            {
                string[] instruction = d.Split(" ");
                int iLoop = int.Parse(instruction[1]);
                switch (instruction[0])
                {
                    case "R":
                        for (int i = 0; i < iLoop; i++)
                        {
                            Right(ref H);
                            Point T = Tails[0];
                            MoveTail(ref H, ref T, visits);
                            Tails[0] = T;
                            for (int j = 0; j < Tails.Count; j++)
                            {
                                Point me = Tails[j];
                                MoveTail(ref T, ref me, visits);
                                Tails[j] = me;
                                T = me;
                            }

                            visits9.Add((Tails[8].X, Tails[8].Y));
                        }
                        break;
                    case "L":
                        for (int i = 0; i < iLoop; i++)
                        {
                            Left(ref H);
                            Point T = Tails[0];
                            MoveTail(ref H, ref T, visits);
                            Tails[0] = T;
                            for (int j = 0; j < Tails.Count; j++)
                            {
                                Point me = Tails[j];
                                MoveTail(ref T, ref me, visits);
                                Tails[j] = me;
                                T = me;
                            }
                            visits9.Add((Tails[8].X, Tails[8].Y));
                        }
                        break;
                    case "U":
                        for (int i = 0; i < iLoop; i++)
                        {
                            Up(ref H);
                            Point T = Tails[0];
                            MoveTail(ref H, ref T, visits);
                            Tails[0] = T;
                            for (int j = 0; j < Tails.Count; j++)
                            {
                                Point me = Tails[j];
                                MoveTail(ref T, ref me, visits);
                                Tails[j] = me;
                                T = me;
                            }
                            visits9.Add((Tails[8].X, Tails[8].Y));
                        }
                        break;
                    case "D":
                        for (int i = 0; i < iLoop; i++)
                        {
                            Down(ref H);
                            Point T = Tails[0];
                            MoveTail(ref H, ref T, visits);
                            Tails[0] = T;
                            for (int j = 0; j < Tails.Count; j++)
                            {
                                Point me = Tails[j];
                                MoveTail(ref T, ref me, visits);
                                Tails[j] = me;
                                T = me;
                            }
                            visits9.Add((Tails[8].X, Tails[8].Y));
                        }
                        break;
                }
            }
            return visits9.Count().ToString();
        }

    }
}
