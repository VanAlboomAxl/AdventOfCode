//using System;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;

//namespace AdventOfCode_2021
//{
//    public class Day19 : Day
//    {

//        public override int _iDay { get { return 19; } }

//        internal override List<string> _lsTest => new List<string> {
//            "--- scanner 0 ---",
//            "404,-588,-901",
//            "528,-643,409",
//            "-838,591,734",
//            "390,-675,-793",
//            "-537,-823,-458",
//            "-485,-357,347",
//            "-345,-311,381",
//            "-661,-816,-575",
//            "-876,649,763",
//            "-618,-824,-621",
//            "553,345,-567",
//            "474,580,667",
//            "-447,-329,318",
//            "-584,868,-557",
//            "544,-627,-890",
//            "564,392,-477",
//            "455,729,728",
//            "-892,524,684",
//            "-689,845,-530",
//            "423,-701,434",
//            "7,-33,-71",
//            "630,319,-379",
//            "443,580,662",
//            "-789,900,-551",
//            "459,-707,401",
//            "",
//            "--- scanner 1 ---",
//            "686,422,578",
//            "605,423,415",
//            "515,917,-361",
//            "-336,658,858",
//            "95,138,22",
//            "-476,619,847",
//            "-340,-569,-846",
//            "567,-361,727",
//            "-460,603,-452",
//            "669,-402,600",
//            "729,430,532",
//            "-500,-761,534",
//            "-322,571,750",
//            "-466,-666,-811",
//            "-429,-592,574",
//            "-355,545,-477",
//            "703,-491,-529",
//            "-328,-685,520",
//            "413,935,-424",
//            "-391,539,-444",
//            "586,-435,557",
//            "-364,-763,-893",
//            "807,-499,-711",
//            "755,-354,-619",
//            "553,889,-390",
//            "",
//            "--- scanner 2 ---",
//            "649,640,665",
//            "682,-795,504",
//            "-784,533,-524",
//            "-644,584,-595",
//            "-588,-843,648",
//            "-30,6,44",
//            "-674,560,763",
//            "500,723,-460",
//            "609,671,-379",
//            "-555,-800,653",
//            "-675,-892,-343",
//            "697,-426,-610",
//            "578,704,681",
//            "493,664,-388",
//            "-671,-858,530",
//            "-667,343,800",
//            "571,-461,-707",
//            "-138,-166,112",
//            "-889,563,-600",
//            "646,-828,498",
//            "640,759,510",
//            "-630,509,768",
//            "-681,-892,-333",
//            "673,-379,-804",
//            "-742,-814,-386",
//            "577,-820,562",
//            "",
//            "--- scanner 3 ---",
//            "-589,542,597",
//            "605,-692,669",
//            "-500,565,-823",
//            "-660,373,557",
//            "-458,-679,-417",
//            "-488,449,543",
//            "-626,468,-788",
//            "338,-750,-386",
//            "528,-832,-391",
//            "562,-778,733",
//            "-938,-730,414",
//            "543,643,-506",
//            "-524,371,-870",
//            "407,773,750",
//            "-104,29,83",
//            "378,-903,-323",
//            "-778,-728,485",
//            "426,699,580",
//            "-438,-605,-362",
//            "-469,-447,-387",
//            "509,732,623",
//            "647,635,-688",
//            "-868,-804,481",
//            "614,-800,639",
//            "595,780,-596",
//            "",
//            "--- scanner 4 ---",
//            "727,592,562",
//            "-293,-554,779",
//            "441,611,-461",
//            "-714,465,-776",
//            "-743,427,-804",
//            "-660,-479,-426",
//            "832,-632,460",
//            "927,-485,-438",
//            "408,393,-506",
//            "466,436,-512",
//            "110,16,151",
//            "-258,-428,682",
//            "-393,719,612",
//            "-211,-452,876",
//            "808,-476,-593",
//            "-575,615,604",
//            "-485,667,467",
//            "-680,325,-822",
//            "-627,-443,-432",
//            "872,-547,-609",
//            "833,512,582",
//            "807,604,487",
//            "839,-516,451",
//            "891,-625,532",
//            "-652,-548,-490",
//            "30,-46,-14",
//        };

//        public void Convert(List<string> lsInput)
//        {
//            List<List<Coord>> loResult = new();

//            int iScanner = -1;
//            int iScannerId = 0;
//            List<Coord> Beacons = new();

//            _readings = new();

//            foreach (var s in lsInput)
//            {
//                if (s == "")
//                {
//                    iScanner = -1;
//                }
//                else if (iScanner == -1)
//                {
//                    iScanner = 1;
//                    Beacons = new();
//                    _readings.Add(iScannerId,Beacons);
//                    iScannerId++;
//                }
//                else
//                {
//                    string[] asSplit = s.Split(",");
//                    Beacons.Add(new(
//                        int.Parse(asSplit[0].Trim()),
//                        int.Parse(asSplit[1].Trim()),
//                        int.Parse(asSplit[2].Trim())
//                    ));
//                }
//            }

//        }
        
//        public record Coord(int X, int Y, int Z)
//        {
//            public int Distance(Coord other) => Math.Abs(other.X - X) + Math.Abs(other.Y - Y) + Math.Abs(other.Z - Z);

//            public Coord Vector(Coord other) => new(other.X - X, other.Y - Y, other.Z - Z);

//            internal Coord Translate(Coord translation) => new(X + translation.X, Y + translation.Y, Z + translation.Z);
//        }

//        private Dictionary<int, List<Coord>> _readings;
//        private List<Coord> _beaconMap;
//        private Dictionary<int, Coord> _scanners;

//        public Day19()
//        {
//            var lsInput = Input;
//            //lsInput = Test;

//            Convert(lsInput);
//            _beaconMap = _readings[0];
//            _scanners = new()
//            {
//                [0] = new Coord(0, 0, 0)
//            };
//        }

//        public override void Q1()
//        {
//            MapSpace();
//            Console.WriteLine(_beaconMap.Count);
//        }
//        public override void Q2()
//        {
//            HashSet<(int, int)> tested = new();
//            int maxDistance = 0;

//            foreach ((int scannerId, Coord scannerFrom) in _scanners)
//            {
//                foreach ((int scannerToId, Coord scannerTo) in _scanners)
//                {
//                    if (scannerId == scannerToId)
//                    {
//                        continue;
//                    }

//                    var key1 = (scannerId, scannerToId);
//                    var key2 = (scannerToId, scannerId);

//                    if (tested.Contains(key1) || tested.Contains(key2))
//                    {
//                        continue;
//                    }

//                    int distance = scannerFrom.Distance(scannerTo);
//                    if (distance > maxDistance)
//                    {
//                        maxDistance = distance;
//                    }

//                    tested.Add(key1);
//                }
//            }

//            Console.WriteLine(maxDistance);
//        }



//        private void MapSpace()
//        {
//            Dictionary<Coord, Coord> vectors = ReadVectors(_beaconMap);

//            Queue<int> scannersToCheck = new();
//            for (int i = 1; i < _readings.Count; i++)
//            {
//                scannersToCheck.Enqueue(i);
//            }

//            while (scannersToCheck.Count > 0)
//            {
//                int scanner = scannersToCheck.Dequeue();
//                var readings = _readings[scanner];

//                Func<Coord, Coord> scannerRotation = null;
//                Coord translation = null;
//                foreach (var rotation in GetRotations())
//                {
//                    if (TestRotation(vectors, readings, rotation, out translation))
//                    {
//                        scannerRotation = rotation;
//                        break;
//                    }
//                }

//                if (scannerRotation != null)
//                {
//                    var rotated = RotateScannerReadings(readings, scannerRotation);
//                    var translated = TranslateScannerReadings(rotated, translation);

//                    foreach (Coord beacon in translated)
//                    {
//                        _beaconMap.Add(beacon);
//                    }

//                    vectors = ReadVectors(_beaconMap);

//                    _scanners.Add(scanner, translation);
//                }
//                else
//                {
//                    scannersToCheck.Enqueue(scanner);
//                }
//            }
//        }

//        private static Dictionary<Coord, Coord> ReadVectors(List<Coord> beaconMap)
//        {
//            Dictionary<Coord, Coord> vectors = new();
//            foreach (var p1 in beaconMap)
//            {
//                foreach (var p2 in beaconMap)
//                {
//                    if (p1 == p2) continue;
//                    Coord vector = p2.Vector(p1);
//                    if (!vectors.ContainsKey(vector))
//                    {
//                        vectors.Add(vector, p2);
//                    }
//                }
//            }
//            return vectors;
//        }

//        private static bool TestRotation(Dictionary<Coord, Coord> masterVectors, List<Coord> beacons, Func<Coord, Coord> rotation, out Coord translation)
//        {
//            int matchCount = 0;
//            foreach (var p1 in beacons)
//            {
//                Coord p1Rotated = rotation(p1);
//                foreach (var p2 in beacons)
//                {
//                    if (p1 == p2) continue;

//                    Coord p2Rotated = rotation(p2);
//                    Coord vector = p1Rotated.Vector(p2Rotated);

//                    if (masterVectors.ContainsKey(vector))
//                    {
//                        matchCount++;
//                        if (matchCount == 11)
//                        {
//                            translation = p1Rotated.Vector(masterVectors[vector]);
//                            return true;
//                        }
//                    }
//                }
//            }

//            translation = null;
//            return false;
//        }

//        private static HashSet<Coord> RotateScannerReadings(List<Coord> beacons, Func<Coord, Coord> scannerRotation)
//        {
//            HashSet<Coord> rotatedBeacons = new();
//            foreach (Coord beacon in beacons)
//            {
//                rotatedBeacons.Add(scannerRotation(beacon));
//            }
//            return rotatedBeacons;
//        }

//        private static HashSet<Coord> TranslateScannerReadings(HashSet<Coord> beacons, Coord translation)
//        {
//            HashSet<Coord> translatedBeacons = new();
//            foreach (Coord beacon in beacons)
//            {
//                translatedBeacons.Add(beacon.Translate(translation));
//            }
//            return translatedBeacons;
//        }

//        private static IEnumerable<Func<Coord, Coord>> GetRotations()
//        {
//            //yield return v => new(v.X, v.Y, v.Z);

//            yield return v => new(v.X, -v.Z, v.Y);
//            yield return v => new(v.X, -v.Y, -v.Z);
//            yield return v => new(v.X, v.Z, -v.Y);

//            yield return v => new(-v.Y, v.X, v.Z);
//            yield return v => new(v.Z, v.X, v.Y);
//            yield return v => new(v.Y, v.X, -v.Z);
//            yield return v => new(-v.Z, v.X, -v.Y);

//            yield return v => new(-v.X, -v.Y, v.Z);
//            yield return v => new(-v.X, -v.Z, -v.Y);
//            yield return v => new(-v.X, v.Y, -v.Z);
//            yield return v => new(-v.X, v.Z, v.Y);

//            yield return v => new(v.Y, -v.X, v.Z);
//            yield return v => new(v.Z, -v.X, -v.Y);
//            yield return v => new(-v.Y, -v.X, -v.Z);
//            yield return v => new(-v.Z, -v.X, v.Y);

//            yield return v => new(-v.Z, v.Y, v.X);
//            yield return v => new(v.Y, v.Z, v.X);
//            yield return v => new(v.Z, -v.Y, v.X);
//            yield return v => new(-v.Y, -v.Z, v.X);

//            yield return v => new(-v.Z, -v.Y, -v.X);
//            yield return v => new(-v.Y, v.Z, -v.X);
//            yield return v => new(v.Z, v.Y, -v.X);
//            yield return v => new(v.Y, -v.Z, -v.X);
//        }
//    }
//}

