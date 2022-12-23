using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day23 : Day
    {
        public override int _iDay { get { return 23; } }

        public override string Q1()
        {
            var lsInput = Data;
            HashSet<(int, int)> hElves = new();
            for(int y=0;y<lsInput.Count;y++)
                for(int x = 0; x < lsInput[y].Length; x++)
                {
                    char c = lsInput[y][x];
                    if (c.Equals('#')) hElves.Add((x, y));
                }
            if (Testing)
            {
                Console.WriteLine($"initial");
                drawMap(hElves);
            }

            int iDirection = 0;
            List<Func<(int,int),HashSet<(int, int)>, (bool,(int,int))>> fChecks = new() { North,South,West,East};
            for(int i = 0; i < 10; i++)
            {
                Dictionary<(int, int), (int, int)> dMoves = new();
                foreach(var elf in hElves)
                {
                    if (wantToMove(elf,hElves))
                        for (int j = iDirection; j < iDirection + 4; j++)  
                        {
                            (bool xMove,(int,int) newLocation) = fChecks[j%4](elf, hElves);
                            if (xMove)
                            {
                                dMoves.Add(elf, newLocation);
                                break;
                            }
                        }
                }          
                foreach(var oldLocation in dMoves.Keys)
                {
                    var newLocation = dMoves[oldLocation];
                    if (dMoves.Values.
                        Where(x=>x.Item1.Equals(newLocation.Item1) && x.Item2.Equals(newLocation.Item2)).ToList().Count == 1)
                    {
                        //only one to move to this location
                        hElves.Remove(oldLocation);
                        hElves.Add(newLocation);
                    }
                }

                if (Testing)
                {
                    Console.WriteLine($"after round {i}");
                    drawMap(hElves);
                }
                
                iDirection++;
                //if (iDirection > 3) iDirection = 0;
            }

            int dx = hElves.Select(x => x.Item1).Max()- hElves.Select(x => x.Item1).Min()+1;
            int dy = hElves.Select(x => x.Item2).Max()- hElves.Select(x => x.Item2).Min()+1;
            int maxArea = dx * dy;
            int result = maxArea - hElves.Count();
            return $"{result}";
        }
        bool wantToMove((int, int) elf, HashSet<(int, int)> hElves)
        {
            int x = elf.Item1;
            int y = elf.Item2;
            if (hElves.Contains((x - 1 , y - 1)) || hElves.Contains((x , y-1 )) ||  hElves.Contains((x +1, y - 1)) ||
                hElves.Contains((x -1, y ))      ||                                 hElves.Contains((x +1, y))     ||
                hElves.Contains((x - 1, y + 1))  || hElves.Contains((x, y + 1)) ||  hElves.Contains((x + 1, y + 1)) 
                ) return true;
            return false;
        }
        (bool,(int,int)) North((int,int) elf, HashSet<(int,int)> hElves)
        {
            int x = elf.Item1;
            int y = elf.Item2;
            if (!hElves.Contains((x, y - 1)) &&
                !hElves.Contains((x - 1, y - 1)) &&
                !hElves.Contains((x + 1, y - 1))
                ) return (true,(x, y - 1));
            return (false,(x,y));
        }
        (bool, (int, int)) South((int, int) elf, HashSet<(int, int)> hElves)
        {
            int x = elf.Item1;
            int y = elf.Item2;
            if (!hElves.Contains((x, y + 1)) &&
                !hElves.Contains((x - 1, y + 1)) &&
                !hElves.Contains((x + 1, y + 1))
                ) return (true,(x, y + 1));
            return (false, (x, y));
        }
        (bool, (int, int)) East((int, int) elf, HashSet<(int, int)> hElves)
        {
            int x = elf.Item1;
            int y = elf.Item2;
            if (!hElves.Contains((x + 1, y )) &&
                !hElves.Contains((x + 1, y - 1)) &&
                !hElves.Contains((x + 1, y + 1))
                ) return (true, (x+1, y ));
            return (false, (x, y));
        }
        (bool, (int, int)) West((int, int) elf, HashSet<(int, int)> hElves)
        {
            int x = elf.Item1;
            int y = elf.Item2;
            if (!hElves.Contains((x - 1, y)) &&
                !hElves.Contains((x - 1, y - 1)) &&
                !hElves.Contains((x - 1, y + 1))
                ) return (true, (x-1, y));
            return (false, (x, y));
        }

        void drawMap(HashSet<(int, int)> hElves)
        {
            int minX = hElves.Select(x => x.Item1).Min();
            int maxX = hElves.Select(x => x.Item1).Max();
            int minY = hElves.Select(x => x.Item2).Min();
            int maxY = hElves.Select(x => x.Item2).Max();
            for (int y = minY; y <= maxY; y++) 
            { 
                for (int x = minX; x <= maxX; x++)
                {
                    if (hElves.Contains((x, y))) Console.Write("#");
                    else Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public override string Q2()
        {
            var lsInput = Data;
            HashSet<(int, int)> hElves = new();
            for (int y = 0; y < lsInput.Count; y++)
                for (int x = 0; x < lsInput[y].Length; x++)
                {
                    char c = lsInput[y][x];
                    if (c.Equals('#')) hElves.Add((x, y));
                }

            int iDirection = 0;
            List<Func<(int, int), HashSet<(int, int)>, (bool, (int, int))>> fChecks = new() { North, South, West, East };
            int i = 1;
            while(true)
            {
                Dictionary<(int, int), (int, int)> dMoves = new();
                foreach (var elf in hElves)
                {
                    if (wantToMove(elf, hElves))
                        for (int j = iDirection; j < iDirection + 4; j++)
                        {
                            (bool xMove, (int, int) newLocation) = fChecks[j % 4](elf, hElves);
                            if (xMove)
                            {
                                dMoves.Add(elf, newLocation);
                                break;
                            }
                        }
                }
                if(dMoves.Count == 0)
                {
                    return i.ToString();
                }
                else
                    foreach (var oldLocation in dMoves.Keys)
                {
                    var newLocation = dMoves[oldLocation];
                    if (dMoves.Values.
                        Where(x => x.Item1.Equals(newLocation.Item1) && x.Item2.Equals(newLocation.Item2)).ToList().Count == 1)
                    {
                        //only one to move to this location
                        hElves.Remove(oldLocation);
                        hElves.Add(newLocation);
                    }
                }

                i++;

                iDirection++;
            }

            int dx = hElves.Select(x => x.Item1).Max() - hElves.Select(x => x.Item1).Min() + 1;
            int dy = hElves.Select(x => x.Item2).Max() - hElves.Select(x => x.Item2).Min() + 1;
            int maxArea = dx * dy;
            int result = maxArea - hElves.Count();
            return $"{result}";
        }


    }
}
