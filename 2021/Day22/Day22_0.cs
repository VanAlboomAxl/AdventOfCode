//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace AdventOfCode_2021
//{

//    public class Day22 : Day
//    {
//        public override int _iDay { get { return 22; } }

//        internal override List<string> _lsTest => new List<string> {
//            "on x=-20..26,y=-36..17,z=-47..7",
//            "on x=-20..33,y=-21..23,z=-26..28",
//            "on x=-22..28,y=-29..23,z=-38..16",
//            "on x=-46..7,y=-6..46,z=-50..-1",
//            "on x=-49..1,y=-3..46,z=-24..28",
//            "on x=2..47,y=-22..22,z=-23..27",
//            "on x=-27..23,y=-28..26,z=-21..29",
//            "on x=-39..5,y=-6..47,z=-3..44",
//            "on x=-30..21,y=-8..43,z=-13..34",
//            "on x=-22..26,y=-27..20,z=-29..19",
//            "off x=-48..-32,y=26..41,z=-47..-37",
//            "on x=-12..35,y=6..50,z=-50..-2",
//            "off x=-48..-32,y=-32..-16,z=-15..-5",
//            "on x=-18..26,y=-33..15,z=-7..46",
//            "off x=-40..-22,y=-38..-28,z=23..41",
//            "on x=-16..35,y=-41..10,z=-47..6",
//            "off x=-32..-23,y=11..30,z=-14..3",
//            "on x=-49..-5,y=-3..45,z=-29..18",
//            "off x=18..30,y=-20..-8,z=-3..13",
//            "on x=-41..9,y=-7..43,z=-33..15",
//            "on x=-54112..-39298,y=-85059..-49293,z=-27449..7877",
//            "on x=967..23432,y=45373..81175,z=27513..53682"
//        };

//        List<clsInstruction> _loInstructions;

//        public Day22()
//        {
//            List<string> lsInput = Input;
//            lsInput = Test;

//            _loInstructions = new();
//            foreach (var s in lsInput)
//                _loInstructions.Add(new(s));

//            Optimize(new() { new() { X = (0, 10), Y = (0, 10), Z = (0, 10) }, new() { X = (1, 9), Y = (1, 9), Z = (1, 9) },  new() { X = (11, 19), Y = (11, 19), Z = (11, 19) } });
//            Optimize(new() { new() { X = (1, 9), Y = (1, 9), Z = (1, 9) }, new() { X = (0, 10), Y = (0, 10), Z = (0, 10) } });

//        }

//        public override void Q1()
//        {
//            List<clsZone> zones = new();
//            foreach (clsInstruction i in _loInstructions)
//                if (i.Value)
//                    zones = SetOn(zones, i.zone);
//                else
//                    zones = SetOff(zones, i.zone);

//        }

//        public List<clsZone> SetOn(List<clsZone> zones, clsZone zone)
//        {

//            if (!zones.Any())
//                zones.Add(zone);
//            else
//            {
//                foreach (var z in zones)
//                {
//                    if (z.Overlap(zone))
//                    {

//                    }
//                }
//            }
//            return zones;
//        }

//        public List<clsZone> Optimize(List<clsZone> zones)
//        {
//            List<clsZone> result = new() { zones[0] };
//            for(int i = 1; i < zones.Count; i++)
//            {
//                clsZone zone = zones[i];;
//                bool xAdd = true;
//                for(int j = 0; j < result.Count(); j++)
//                {
//                    clsZone z = result[j];
//                    if (z.Inside(zone))
//                    {
//                        xAdd = false;
//                        break;
//                    }
//                    else if (zone.Inside(z))
//                    {
//                        xAdd = false;
//                        result[j] = zone;
//                        break;
//                    }
//                    else if (z.Overlap(zone))
//                    {
//                        // corner --> ambetant
//                        List<clsZone> newZones = Overlap(z, zone);
//                        result.RemoveAt(j);
//                        foreach (var nz in newZones)
//                            result.Add(nz);

//                        xAdd = false;
//                        break;
//                    }
//                }
//                if(xAdd)
//                    result.Add(zone);
//            }
//            return result;
//        }

//        private List<clsZone> Overlap(clsZone z1, clsZone z2)
//        {
//            List<clsZone> Result = new();





//            return Result;
//        }

//        public List<clsZone> SetOff(List<clsZone> zones, clsZone zone)
//        {
//            return null;
//        }

//        public override void Q2()
//        {


//        }

//        public class clsInstruction
//        {
//            private string RegexCmd = "x=(-?\\d+)..(-?\\d+),y=(-?\\d+)..(-?\\d+),z=(-?\\d+)..(-?\\d+)";


//            public bool Value { get; private set; }
//            //public (int,int) X { get; private set; }
//            //public (int,int) y { get; private set; }
//            //public (int,int) z { get; private set; }
//            public clsZone zone { get; private set; }
//            public clsInstruction(string s)
//            {
//                string[] asSplit = s.Split(" ");
//                if (asSplit[0].Equals("on"))
//                    Value = true;

//                Regex reg = new(RegexCmd);
//                var Match = reg.Match(asSplit[1]);

//                zone = new();
//                zone.X = (int.Parse(Match.Groups[1].Value), int.Parse(Match.Groups[2].Value));
//                zone.Y = (int.Parse(Match.Groups[3].Value), int.Parse(Match.Groups[4].Value));
//                zone.Z = (int.Parse(Match.Groups[5].Value), int.Parse(Match.Groups[6].Value));

//            }
//        }

//        public class clsZone
//        {
//            public bool On { get; set; }
//            public (int, int) X { get; set; }
//            public (int, int) Y { get; set; }
//            public (int, int) Z { get; set; }

//            public clsZone()
//            {
//                //On = true;
//            }

//            public bool Overlap(clsZone zone)
//            {
//                if (((zone.X.Item1 >= X.Item1 && zone.X.Item1 <= X.Item2) || (zone.X.Item2 >= X.Item1 && zone.X.Item2 <= X.Item2)) &&
//                    ((zone.Y.Item1 >= Y.Item1 && zone.Y.Item1 <= Y.Item2) || (zone.Y.Item2 >= Y.Item1 && zone.Y.Item2 <= Y.Item2)) &&
//                    ((zone.Z.Item1 >= Z.Item1 && zone.Z.Item1 <= Z.Item2) || (zone.Z.Item2 >= Z.Item1 && zone.Z.Item2 <= Z.Item2)))
//                    return true;

//                return false;
//            }

//            public bool Inside(clsZone zone)
//            {
//                if (((zone.X.Item1 >= X.Item1 && zone.X.Item1 <= X.Item2) && (zone.X.Item2 >= X.Item1 && zone.X.Item2 <= X.Item2)) &&
//                    ((zone.Y.Item1 >= Y.Item1 && zone.Y.Item1 <= Y.Item2) && (zone.Y.Item2 >= Y.Item1 && zone.Y.Item2 <= Y.Item2)) &&
//                    ((zone.Z.Item1 >= Z.Item1 && zone.Z.Item1 <= Z.Item2) && (zone.Z.Item2 >= Z.Item1 && zone.Z.Item2 <= Z.Item2)))
//                    return true;

//                return false;
//            }

//            public clsZone Intersection(clsZone zone)
//            {
//                clsZone result = new();
//                result.On = !On;

//                result.X = (Math.Max(X.Item1, zone.X.Item1), Math.Min(X.Item2, zone.X.Item2));
//                result.Y = (Math.Max(Y.Item1, zone.Y.Item1), Math.Min(Y.Item2, zone.Y.Item2));
//                result.Z = (Math.Max(Z.Item1, zone.Z.Item1), Math.Min(Z.Item2, zone.Z.Item2));

//                return result;
//            }

//            public Int64 Size()
//            {
//                return (X.Item2 - X.Item1) + (Y.Item2 - Y.Item1) + (Z.Item2 - Z.Item1);
//            }

//            public override string ToString()
//            {
//                return $"{{({X.Item1},{X.Item2});({Y.Item1},{Y.Item2});({Z.Item1},{Z.Item2})}}";
//            }
//        }

//    }
//}
