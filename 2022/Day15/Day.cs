using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Helper.BEC;
using Helper;
namespace AdventOfCode
{
    public class Day15 : Day
    {
        public override int _iDay { get { return 15; } }

        private (long, long, long, long) logic(string s)
        {
            string sRegexString = @"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)";
            Regex rg = new Regex(sRegexString);
            GroupCollection matches = rg.Matches(s)[0].Groups;

            return (
                long.Parse(matches[1].Value),
                long.Parse(matches[2].Value),
                long.Parse(matches[3].Value),
                long.Parse(matches[4].Value)
            );
        }

        public override string Q1()
        {
            long row = 10;
            if (!Testing) row = 2000000;
            var lsInput = Data;
            HashSet<(long, long)> map = new();
            HashSet<(long, long)> beacons = new();
            List<clsInterval> lRanges = new();
            foreach (var s in lsInput)
            {
                (long sensorX, long sensorY, long beaconX, long beaconY) = logic(s);
                long deltaX = Math.Abs(sensorX - beaconX);
                long deltaY = Math.Abs(sensorY - beaconY);
                long manhatten = deltaX + deltaY;
                map.Add((sensorX, sensorY));
                beacons.Add((beaconX, beaconY));

                long dy = Math.Abs(row - sensorY);
                if (dy > manhatten)
                {
                    // can't reach --> skip
                }
                else
                {
                    long dx = manhatten - dy;
                    lRanges.Add(new( sensorX - dx, sensorX + dx ));
                }
            }

            lRanges = Classes.Merge(lRanges).ToList();
            long mapItems = 0;
            foreach(var range in lRanges) mapItems += range.end - range.start + 1;
            
            long beaconItems = beacons.Where(x => x.Item2 == row).ToList().Count();

            return (mapItems - beaconItems).ToString();
        }

        public override string Q2()
        {
            long max = 20;
            if (!Testing) max = 4000000;
            var lsInput = Data;
            Dictionary<long, List<clsInterval>> map = new();
            foreach (var s in lsInput)
            {
                (long sensorX, long sensorY, long beaconX, long beaconY) = logic(s);
                long deltaX = Math.Abs(sensorX - beaconX);
                long deltaY = Math.Abs(sensorY - beaconY);
                long manhatten = deltaX + deltaY;

                for (long y = -manhatten; y <= manhatten; y++)
                {
                    long currentY = sensorY + y;
                    if (currentY < 0 || currentY > max) continue;
                    long dx = manhatten - Math.Abs(y);
                    long minX = sensorX - dx;
                    long maxX = sensorX + dx;
                    if (minX> max) continue;
                    if (maxX < 0) continue;

                    minX = Math.Max(0, minX);
                    maxX = Math.Min(max, maxX);

                    if (map.ContainsKey(sensorY+y))
                    {
                        map[sensorY+y].Add(new(minX, maxX));
                    }
                    else
                    {
                        map.Add(sensorY+y, new() { new(minX, maxX) });
                    }
                }

            }
            foreach (var key in map.Keys)
            {
                var result = Classes.Merge(map[key]).ToList();
                map[key] = result;
                if (result.Count > 1)
                {
                    // is dit de oplossing?
                    return (key + 4000000 * (result[1].start-1)).ToString();
                }
            }

            return null;
        }
      
    }

}
