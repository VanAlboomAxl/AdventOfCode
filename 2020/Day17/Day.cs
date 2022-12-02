using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day17 : Day
    {
        public override int _iDay { get { return 17; } }

        
        public override void Q1()
        {
            HashSet<(int x, int y, int z)> convert(List<string> data)
            {
                HashSet<(int x, int y, int z)> CubesOn = new();
                for (int x = 0; x < data.Count; x++)
                    for (int y = 0; y < data[x].Length; y++)
                        if (data[x][y] == '#')
                            CubesOn.Add((x, y, 0));
                return CubesOn;
            }

            void GetNeightbours((int x, int y, int z) location, Dictionary<(int x, int y, int z), int> Neightbours)
            {
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        for (int dz = -1; dz <= 1; dz++)
                            if (dx != 0 || dy != 0 || dz != 0)
                            {
                                (int x, int y, int z) Nposition = (location.x + dx, location.y + dy, location.z + dz);
                                Neightbours.TryGetValue(Nposition, out int Current);
                                Neightbours[Nposition] = Current + 1;
                            }
            }

            HashSet<(int x, int y, int z)> logic(HashSet<(int x, int y, int z)> CubesOn)
            {
                Dictionary<(int x, int y, int z), int> OnCounter = new();
                foreach ((int x, int y, int z) cube in CubesOn)
                    GetNeightbours(cube, OnCounter);

                HashSet<(int x, int y, int z)> NewCubesOn = new();
                foreach (KeyValuePair<(int x, int y, int z), int> item in OnCounter)
                    if (item.Value == 3 || (item.Value == 2 && CubesOn.Contains(item.Key)))
                        NewCubesOn.Add(item.Key);

                return NewCubesOn;
            }

            HashSet<(int x, int y, int z)> CubesOn = convert(Data);
            for (int i = 0; i < 6; i++) CubesOn = logic(CubesOn);
            Console.WriteLine(CubesOn.Count);
        }
        
        public override void Q2()
        {

            HashSet<(int x, int y, int z,int h)> convert(List<string> data)
            {
                HashSet<(int x, int y, int z,int h)> CubesOn = new();
                for (int x = 0; x < data.Count; x++)
                    for (int y = 0; y < data[x].Length; y++)
                        if (data[x][y] == '#')
                            CubesOn.Add((x, y, 0, 0));
                return CubesOn;
            }

            void GetNeightbours((int x, int y, int z, int h) location, Dictionary<(int x, int y, int z, int h), int> Neightbours)
            {
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        for (int dz = -1; dz <= 1; dz++)
                            for (int dh = -1; dh <= 1; dh++)
                                if (dx != 0 || dy != 0 || dz != 0 || dh != 0)
                                {
                                    (int x, int y, int z, int h) Nposition = (location.x + dx, location.y + dy, location.z + dz, location.h + dh);
                                    Neightbours.TryGetValue(Nposition, out int Current);
                                    Neightbours[Nposition] = Current + 1;
                                }
            }

            HashSet<(int x, int y, int z, int h)> logic(HashSet<(int x, int y, int z, int h)> CubesOn)
            {
                Dictionary<(int x, int y, int z, int h), int> OnCounter = new();
                foreach ((int x, int y, int z, int h) cube in CubesOn)
                    GetNeightbours(cube, OnCounter);

                HashSet<(int x, int y, int z, int h)> NewCubesOn = new();
                foreach (KeyValuePair<(int x, int y, int z, int h), int> item in OnCounter)
                    if (item.Value == 3 || (item.Value == 2 && CubesOn.Contains(item.Key)))
                        NewCubesOn.Add(item.Key);

                return NewCubesOn;
            }

            HashSet<(int x, int y, int z, int h)> CubesOn = convert(Data);
            for (int i = 0; i < 6; i++) CubesOn = logic(CubesOn);
            Console.WriteLine(CubesOn.Count);
        }

    }
}
