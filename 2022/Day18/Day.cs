using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day18 : Day
    {
        public override int _iDay { get { return 18; } }

        public override string Q1()
        {
            var lsInput = Data;
            Dictionary<(int, int, int), int> freeSurfaces = new();
            foreach(var s in lsInput)
            {
                string[] sData = s.Split(",");
                int x = int.Parse(sData[0]);
                int y = int.Parse(sData[1]);
                int z = int.Parse(sData[2]);
                int iFreeSurfaces = 6;
                for(int i = 0; i < 6; i++)
                {
                    int iSign = 1;
                    if (i >= 3) iSign = -1;
                    int dx = 0; if (i % 3 == 0) dx = iSign * 1;
                    int dy = 0; if (i % 3 == 1) dy = iSign * 1;
                    int dz = 0; if (i % 3 == 2) dz = iSign * 1;
                    if (freeSurfaces.ContainsKey((x + dx, y + dy, z + dz)))
                    {
                        iFreeSurfaces--;
                        freeSurfaces[(x + dx, y + dy, z + dz)]--;
                    }
                }
                freeSurfaces.Add((x, y, z), iFreeSurfaces);
            }
            return freeSurfaces.Values.Sum().ToString();
        }

        public override string Q2()
        {

            var lsInput = Data;
            // load map --> as in q1
            Dictionary<(int, int, int), int> freeSurfaces = new();
            int maxX = 0, maxY = 0, maxZ = 0;
            foreach (var s in lsInput)
            {
                string[] sData = s.Split(",");
                int x = int.Parse(sData[0]); if (x > maxX) maxX = x;
                int y = int.Parse(sData[1]); if (y > maxY) maxY = y;
                int z = int.Parse(sData[2]); if (z > maxZ) maxZ = z;

                int iFreeSurfaces = 6;
                for (int i = 0; i < 6; i++)
                {
                    int iSign = 1;
                    if (i >= 3) iSign = -1;
                    int dx = 0; if (i % 3 == 0) dx = iSign * 1;
                    int dy = 0; if (i % 3 == 1) dy = iSign * 1;
                    int dz = 0; if (i % 3 == 2) dz = iSign * 1;
                    if (freeSurfaces.ContainsKey((x + dx, y + dy, z + dz)))
                    {
                        iFreeSurfaces--;
                        freeSurfaces[(x + dx, y + dy, z + dz)]--;
                    }
                }
                freeSurfaces.Add((x, y, z), iFreeSurfaces);
            }

            // build air map
            //go from 0,0,0 to maxX+1, maxY+1, maxZ+1
            // +1 to go fully around, and connect all the open air to eachother
            List<(int, int, int)> airmap = new();
            for (int z = 0;z <= maxZ+1;z++ )
                for (int y = 0;y <= maxY+1;y++ )
                    for (int x = 0;x <= maxX+1;x++)
                        if (!freeSurfaces.ContainsKey((x, y, z))) 
                            airmap.Add((x, y, z));
            
            // go over all linked air molecules starting from the outside
            // delete them from the air map
            // and next check their neighbours, and so on ...
            // resulting points in airmap are trapped
            Queue<(int, int, int)> ToCheck = new(); 
            ToCheck.Enqueue(airmap[0]);
            while (ToCheck.TryDequeue(out (int,int,int) current))
            {
                airmap.Remove(current);
                for (int i = 0; i < 6; i++)
                {
                    int iSign = 1;
                    if (i >= 3) iSign = -1;
                    int dx = current.Item1; if (i % 3 == 0) dx += iSign * 1;
                    int dy = current.Item2; if (i % 3 == 1) dy += iSign * 1;
                    int dz = current.Item3; if (i % 3 == 2) dz += iSign * 1;

                    if (airmap.Contains((dx, dy, dz)) && !ToCheck.Contains((dx,dy,dz))) //continue;
                        ToCheck.Enqueue((dx,dy,dz));
                }


            }

            //count the amount of surfaces the air molecules touch
            Dictionary<(int, int, int), int> airSurfaces = new();
            foreach ((int x,int y,int z) in airmap)
            {
                int iSurfaces = 0;
                for (int i = 0; i < 6; i++)
                {
                    int iSign = 1;
                    if (i >= 3) iSign = -1;
                    int dx = 0; if (i % 3 == 0) dx = iSign * 1;
                    int dy = 0; if (i % 3 == 1) dy = iSign * 1;
                    int dz = 0; if (i % 3 == 2) dz = iSign * 1;
                    if (freeSurfaces.ContainsKey((x + dx, y + dy, z + dz)))
                    {
                        iSurfaces++;
                    }

                }
                airSurfaces.Add((x, y, z), iSurfaces);
            }

            return (freeSurfaces.Values.Sum()- airSurfaces.Values.Sum()).ToString();
        }


    }
}
