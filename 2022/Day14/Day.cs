using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day14 : Day
    {
        public override int _iDay { get { return 14; } }

        public override string Q1()
        {
            var lsInput = Data;
            HashSet<(int, int)> map = new();
            int maxY = 0;
            foreach(var s in lsInput)
            {
                string[] split = s.Replace(" ", "").Split("->");
                int x = 0;
                int y = 0;
                string[] location = split[0].Split(",");
                x = int.Parse(location[0]);
                y = int.Parse(location[1]);
                map.Add((x, y));
                if (y > maxY) maxY = y;
                for(int i = 1; i < split.Length; i++)
                {
                    int x2 = 0;
                    int y2 = 0;
                    location = split[i].Split(",");
                    x2 = int.Parse(location[0]);
                    y2 = int.Parse(location[1]);
                    if (y2 > maxY) maxY = y2;
                    if (x==x2)
                    {
                        if (y < y2)
                        {
                            for (int j = y; j <= y2; j++)
                                map.Add((x, j));
                        }
                        else
                        {
                            for (int j = y2; j <= y; j++)
                                map.Add((x, j));

                        }
                    }
                    else if (x < x2)
                        for (int j = x; j <= x2; j++)
                            map.Add((j, y));
                    else
                        for(int j = x2; j <= x; j++)
                            map.Add((j, y));
                    x = x2; y = y2;
                }
            }
            bool xLoop = true;
            int iCycle = 0;
            while (xLoop)
            {
                bool xInfiniteFall = false;
                bool xRest = false;
                int sandX = 500;
                int sandY = 0;
                while (!xRest && !xInfiniteFall)
                {
                    if (sandY >= maxY)
                    {
                        xInfiniteFall = true;
                    }
                    else if (!map.Contains((sandX, sandY + 1)))
                        sandY++;
                    else if(!map.Contains((sandX-1, sandY + 1)))
                    {
                        sandX--;
                        sandY++;
                    }
                    else if (!map.Contains((sandX + 1, sandY + 1)))
                    {
                        sandX++;
                        sandY++;
                    }
                    else
                    {
                        xRest = true;
                    }
                }
                if (xInfiniteFall)
                {
                    xLoop = false;
                }
                else
                {
                    map.Add((sandX, sandY));
                    iCycle++;
                }
            }
            return "";
        }

        public override string Q2()
        {
            var lsInput = Data;
            HashSet<(int, int)> map = new();
            int maxY = 0;
            foreach (var s in lsInput)
            {
                string[] split = s.Replace(" ", "").Split("->");
                int x = 0;
                int y = 0;
                string[] location = split[0].Split(",");
                x = int.Parse(location[0]);
                y = int.Parse(location[1]);
                map.Add((x, y));
                if (y > maxY) maxY = y;
                for (int i = 1; i < split.Length; i++)
                {
                    int x2 = 0;
                    int y2 = 0;
                    location = split[i].Split(",");
                    x2 = int.Parse(location[0]);
                    y2 = int.Parse(location[1]);
                    if (y2 > maxY) maxY = y2;
                    if (x == x2)
                    {
                        if (y < y2)
                        {
                            for (int j = y; j <= y2; j++)
                                map.Add((x, j));
                        }
                        else
                        {
                            for (int j = y2; j <= y; j++)
                                map.Add((x, j));

                        }
                    }
                    else if (x < x2)
                        for (int j = x; j <= x2; j++)
                            map.Add((j, y));
                    else
                        for (int j = x2; j <= x; j++)
                            map.Add((j, y));
                    x = x2; y = y2;
                }
            }

            int floorY = maxY+2;
            for (int i = -10000; i < 10000; i++) map.Add((i, floorY));
            bool xLoop = true;
            int iCycle = 0;
            while (xLoop)
            {
                bool xInfiniteFall = false;
                bool xRest = false;
                int sandX = 500;
                int sandY = 0;
                while (!xRest && !xInfiniteFall)
                {
                    if (!map.Contains((sandX, sandY + 1)))
                        sandY++;
                    else if (!map.Contains((sandX - 1, sandY + 1)))
                    {
                        sandX--;
                        sandY++;
                    }
                    else if (!map.Contains((sandX + 1, sandY + 1)))
                    {
                        sandX++;
                        sandY++;
                    }
                    else
                    {
                        xRest = true;
                    }
                }
                
                map.Add((sandX, sandY));
                iCycle++;
                
                if (map.Contains((500, 0)))
                {
                    xLoop = false;
                }
            }
            return null;
        }


    }
}
