using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day22 : Day
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
            "on x=-54112..-39298,y=-85059..-49293,z=-27449..7877",
            "on x=967..23432,y=45373..81175,z=27513..53682"
        };

        List<string> _lsTest2 = new()
        {
            "on x=10..12,y=10..12,z=10..12",
            "on x=11..13,y=11..13,z=11..13",
            "off x=9..11,y=9..11,z=9..11",
            "on x=10..10,y=10..10,z=10..10"
        };
        List<string> _lsTest3 
        {
            get
            {
                return Helper.ReadInput(@"Y:\Repositories\AdventOfCode\2021\Day22\Test2.txt");
            }
        }

        List<clsInstruction> _loInstructions;

        public Day22()
        {
            List<string> lsInput = Input;
            //lsInput = Test;
            //lsInput = _lsTest2;
            //lsInput = _lsTest3;
            //lsInput = _lsTest4;
            //lsInput = _lsTest5;

            _loInstructions = new();
            foreach (var s in lsInput)
                _loInstructions.Add(new(s));

        }

        public override void Q1()
        {
            List<clsZone> zones = new();
            foreach (clsInstruction i in _loInstructions)
                if (i.zone.X.Item1 >= -50 && i.zone.X.Item2 <= 50)
                    zones = ExecuteInstruction(zones, i);

            Int64 iOn = 0;
            foreach(clsZone z in zones)
            {
                int iFactor = 1;
                if (!z.On) iFactor = -1;
                iOn += z.Size() * iFactor;
            }
            Console.WriteLine(iOn);
        }

        public List<clsZone> ExecuteInstruction(List<clsZone> zones, clsInstruction i)
        {
            //see picture for explanation
            
            clsZone zone = i.zone;
            zone.On = i.Value;
            List<clsZone> Intersections = new();
            foreach (var z in zones)
            {
                //if (z.Overlap(zone))
                
                    clsZone intersection = z.Intersection(zone);
                    if(intersection != null)
                    {
                        intersection.On = !z.On;
                        Intersections.Add(intersection);
                    }
                
            }
            foreach (clsZone intersection in Intersections)
                zones.Add(intersection);
            if(zone.On)
                zones.Add(zone);

            return zones;
        }
        
        public override void Q2()
        {
            List<clsZone> zones = new();
            foreach (clsInstruction i in _loInstructions)
                    zones = ExecuteInstruction(zones, i);

            Int64 iOn = 0;
            foreach (clsZone z in zones)
            {
                int iFactor = 1;
                if (!z.On) iFactor = -1;
                iOn += z.Size() * iFactor;
                //Console.WriteLine(iOn);
            }
            Console.WriteLine(iOn);

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
            public bool On { get; set; }
            public (int, int) X { get; set; }
            public (int, int) Y { get; set; }
            public (int, int) Z { get; set; }

            public clsZone()
            {
                //On = true;
            }

            public bool Overlap(clsZone zone)
            {
                if (((zone.X.Item1 >= X.Item1 && zone.X.Item1 <= X.Item2) || (zone.X.Item2 >= X.Item1 && zone.X.Item2 <= X.Item2)) &&
                    ((zone.Y.Item1 >= Y.Item1 && zone.Y.Item1 <= Y.Item2) || (zone.Y.Item2 >= Y.Item1 && zone.Y.Item2 <= Y.Item2)) &&
                    ((zone.Z.Item1 >= Z.Item1 && zone.Z.Item1 <= Z.Item2) || (zone.Z.Item2 >= Z.Item1 && zone.Z.Item2 <= Z.Item2)))
                    return true;

                return false;
            }

            public bool Inside(clsZone zone)
            {
                if (((zone.X.Item1 >= X.Item1 && zone.X.Item1 <= X.Item2) && (zone.X.Item2 >= X.Item1 && zone.X.Item2 <= X.Item2)) &&
                    ((zone.Y.Item1 >= Y.Item1 && zone.Y.Item1 <= Y.Item2) && (zone.Y.Item2 >= Y.Item1 && zone.Y.Item2 <= Y.Item2)) &&
                    ((zone.Z.Item1 >= Z.Item1 && zone.Z.Item1 <= Z.Item2) && (zone.Z.Item2 >= Z.Item1 && zone.Z.Item2 <= Z.Item2)))
                    return true;

                return false;
            }

            public clsZone Intersection(clsZone zone)
            {
                clsZone result = new();
                result.On = !On;

                result.X = (Math.Max(X.Item1, zone.X.Item1), Math.Min(X.Item2, zone.X.Item2));
                result.Y = (Math.Max(Y.Item1, zone.Y.Item1), Math.Min(Y.Item2, zone.Y.Item2));
                result.Z = (Math.Max(Z.Item1, zone.Z.Item1), Math.Min(Z.Item2, zone.Z.Item2));

                if (result.X.Item1 > result.X.Item2)
                {
                    return null;
                }
                if (result.Y.Item1 > result.Y.Item2)
                {
                    return null;

                }
                if (result.Z.Item1 > result.Z.Item2)
                {

                    return null;
                }

                return result;
            }

            public Int64 Size()
            {
                Int64 x = (1 + X.Item2 - X.Item1);
                Int64 y = (1 + Y.Item2 - Y.Item1);
                Int64 z = (1 + Z.Item2 - Z.Item1);
                return  x*y*z;
            }

            public override string ToString()
            {
                return $"{{{On}({X.Item1},{X.Item2});({Y.Item1},{Y.Item2});({Z.Item1},{Z.Item2})}}";
            }
        }

    }
}
