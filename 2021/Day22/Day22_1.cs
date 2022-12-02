using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day22_2 : Day
    {
        public override int _iDay { get { return 22; } }

        internal override List<string> _lsTest => new List<string> {
            "on x=-20..26,y=-36..17,z=-47..7",
            "on x=-20..33,y=-21..23,z=-26..28",
            "on x=-22..28,y=-29..23,z=-38..16",
            "on x=-46..7,y=-6..46,z=-50..-1",
            "on x=-49..1,y=-3..46,z=-24..28",
            "on x=2..47,y=-22..22,z=-23..27",
            "on x=-27..23,y=-28..26,z=-21..29",
            "on x=-39..5,y=-6..47,z=-3..44",
            "on x=-30..21,y=-8..43,z=-13..34",
            "on x=-22..26,y=-27..20,z=-29..19",
            "off x=-48..-32,y=26..41,z=-47..-37",
            "on x=-12..35,y=6..50,z=-50..-2",
            "off x=-48..-32,y=-32..-16,z=-15..-5",
            "on x=-18..26,y=-33..15,z=-7..46",
            "off x=-40..-22,y=-38..-28,z=23..41",
            "on x=-16..35,y=-41..10,z=-47..6",
            "off x=-32..-23,y=11..30,z=-14..3",
            "on x=-49..-5,y=-3..45,z=-29..18",
            "off x=18..30,y=-20..-8,z=-3..13",
            "on x=-41..9,y=-7..43,z=-33..15",
            //"on x=-54112..-39298,y=-85059..-49293,z=-27449..7877",
            //"on x=967..23432,y=45373..81175,z=27513..53682"
        };

        List<clsInstruction> _loInstructions;

        public Day22_2()
        {
            List<string> lsInput = Input;
            lsInput = Test;

            _loInstructions = new();
            foreach (var s in lsInput)
                _loInstructions.Add(new(s));

        }

        public override void Q1()
        {
            List<clsPoint> zones = new();
            foreach (clsInstruction i in _loInstructions)
                if (i.Value)
                    zones = SetOn(zones, i.zone);
                else
                    zones = SetOff(zones, i.zone);

        }

        public List<clsPoint> SetOn(List<clsPoint> On, clsZone zone)
        {
            for (int x = zone.X.Item1; x < zone.X.Item2 + 1; x++)
                for (int y = zone.Y.Item1; y < zone.Y.Item2 + 1; y++)
                    for (int z = zone.Z.Item1; z < zone.Z.Item2 + 1; z++)
                    {
                        clsPoint Point = new(x, y, z);
                        if (!On.Contains(Point))
                            On.Add(Point);

                    }
            return On;
        }

        public List<clsPoint> SetOff(List<clsPoint> On, clsZone zone)
        {
            for (int x = zone.X.Item1; x < zone.X.Item2 + 1; x++)
                for (int y = zone.Y.Item1; y < zone.Y.Item2 + 1; y++)
                    for (int z = zone.Z.Item1; z < zone.Z.Item2 + 1; z++)
                    {
                        clsPoint Point = new(x, y, z);
                        if (!On.Contains(Point))
                            On.Remove(Point);

                    }
            return On;
        }

        public override void Q2()
        {


        }

        public class clsInstruction
        {
            private string RegexCmd = "x=(-?\\d+)..(-?\\d+),y=(-?\\d+)..(-?\\d+),z=(-?\\d+)..(-?\\d+)";


            public bool Value { get; private set; }
            //public (int,int) X { get; private set; }
            //public (int,int) y { get; private set; }
            //public (int,int) z { get; private set; }
            public clsZone zone { get; private set; }
            public clsInstruction(string s)
            {
                string[] asSplit = s.Split(" ");
                if (asSplit[0].Equals("on"))
                    Value = true;

                Regex reg = new(RegexCmd);
                var Match = reg.Match(asSplit[1]);

                zone = new();
                zone.X = (int.Parse(Match.Groups[1].Value), int.Parse(Match.Groups[2].Value));
                zone.Y = (int.Parse(Match.Groups[3].Value), int.Parse(Match.Groups[4].Value));
                zone.Z = (int.Parse(Match.Groups[5].Value), int.Parse(Match.Groups[6].Value));

            }
        }

        public class clsZone
        {
            public (int, int) X { get; set; }
            public (int, int) Y { get; set; }
            public (int, int) Z { get; set; }

            public bool Overlap(clsZone zone)
            {
                if (((zone.X.Item1 >= X.Item1 && zone.X.Item1 <= X.Item2) || (zone.X.Item2 >= X.Item1 && zone.X.Item2 <= X.Item2)) &&
                    ((zone.Y.Item1 >= Y.Item1 && zone.Y.Item1 <= Y.Item2) || (zone.Y.Item2 >= Y.Item1 && zone.Y.Item2 <= Y.Item2)) &&
                    ((zone.Z.Item1 >= Z.Item1 && zone.Z.Item1 <= Z.Item2) || (zone.Z.Item2 >= Z.Item1 && zone.Z.Item2 <= Z.Item2)))
                    return true;


                return false;
            }

            public override string ToString()
            {
                return $"{{({X.Item1},{X.Item2});({Y.Item1},{Y.Item2});({Z.Item1},{Z.Item2})}}";
            }
        }

        public class clsPoint
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public clsPoint()
            {

            }
            public clsPoint(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public bool Equals(clsPoint p)
            {
                if (p.X == X && p.Y == Y && p.Z == Z)
                    return true;
                return false;
            }



            public override string ToString()
            {
                return $"({X},{Y},{Z})";
            }
        }

    }
}
