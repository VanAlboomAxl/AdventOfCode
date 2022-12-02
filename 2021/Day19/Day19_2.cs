//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

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

//        public List<clsScanner> Convert(List<string> lsInput)
//        {
//            List<clsScanner> loResult = new();

//            clsScanner current = null;
//            foreach (var s in lsInput)
//            {
//                if (s == "")
//                {
//                    current = null;
//                }
//                else if (current == null)
//                {
//                    current = new(s);
//                    loResult.Add(current);
//                }
//                else
//                {
//                    current.AddBeacon(new(s));
//                }
//            }


//            return loResult;
//        }


//        public override void Q1()
//        {
//            var lsInput = Input;
//            lsInput = Test;

//            List<clsScanner> loScanners = Convert(lsInput);
//            Logic(loScanners);

//            //int iCount = loScanners[0].Beacons.Count();
//            //for (int i = 1; i < loScanners.Count; i++)
//            //{
//            //    iCount += loScanners[i].Beacons.Count - 12;
//            //}



//        }


//        public void Logic(List<clsScanner> loScanners)
//        {
//            clsScanner S0 = loScanners[0];
//            List<clsBeacon> KnownBeacons = S0.Beacons;
//            loScanners.RemoveAt(0);

//            while (loScanners.Count > 0)
//            {
//                List<int> common = new();
//                foreach (var s in loScanners)
//                    common.Add(GetCommonPoints(KnownBeacons, s.Beacons));

//                int iScanner = common.IndexOf(common.Max());
//                clsScanner current = loScanners[iScanner];
//                loScanners.RemoveAt(iScanner);


//                foreach (var i in common)
//                    Console.Write(i + ",");
//                Console.Write(']');
//                Console.WriteLine();
//                Console.WriteLine(current.Name);

//                Dictionary<clsBeacon, clsBeacon> Mapping = new();
//                foreach (var known in KnownBeacons)
//                {
//                    foreach (var b in current.Beacons)
//                    {
//                        var intersect = known.Distances.Intersect(b.Distances);
//                        //Console.WriteLine("------");
//                        //known.Distances.ForEach(i => Console.Write(i + ","));
//                        //Console.WriteLine(); 
//                        //b.Distances.ForEach(i => Console.Write(i + ","));
//                        //Console.WriteLine();
//                        //intersect.ToList().ForEach(i => Console.Write(i + ","));
//                        //Console.WriteLine();
//                        if (intersect.Count() > 10)
//                        {
//                            Mapping.Add(known, b);
//                        }
//                    }
//                }

//                Console.WriteLine(Mapping.Count);

//                int cog_1_x = 0;
//                int cog_1_y = 0;
//                int cog_1_z = 0;
//                int cog_2_x = 0;
//                int cog_2_y = 0;
//                int cog_2_z = 0;

//                foreach ((var key, var value) in Mapping)
//                {
//                    cog_1_x += key.Position.X;
//                    cog_1_y += key.Position.Y;
//                    cog_1_z += key.Position.Z;

//                    cog_2_x += value.Position.X;
//                    cog_2_y += value.Position.Y;
//                    cog_2_z += value.Position.Z;
//                }

//                cog_1_x /= Mapping.Keys.Count();//Console.WriteLine(cog_1_x);                   
//                cog_1_y /= Mapping.Keys.Count();//Console.WriteLine(cog_1_y);
//                cog_1_z /= Mapping.Keys.Count();//Console.WriteLine(cog_1_z);
//                cog_2_x /= Mapping.Keys.Count();//Console.WriteLine(cog_2_x);
//                cog_2_y /= Mapping.Keys.Count();//Console.WriteLine(cog_2_y);
//                cog_2_z /= Mapping.Keys.Count();//Console.WriteLine(cog_2_z);


//                clsBeacon b1 = Mapping.Keys.ToList()[0];
//                clsBeacon b2 = Mapping[b1];

//                Coord p1_mod = new((int)Math.Floor((decimal)b1.Position.X - cog_1_x),
//                                    (int)Math.Floor((decimal)b1.Position.Y - cog_1_y),
//                                    (int)Math.Floor((decimal)b1.Position.Z - cog_1_z));

//                Coord p2_mod = new((int)Math.Floor((decimal)b2.Position.X - cog_2_x),
//                                    (int)Math.Floor((decimal)b2.Position.Y - cog_2_y),
//                                    (int)Math.Floor((decimal)b2.Position.Z - cog_2_z));


//                int iFactorX = (int)Math.Round((decimal)p1_mod.X / p2_mod.X);
//                int iFactorY = (int)Math.Round((decimal)p1_mod.Y / p2_mod.Y);
//                int iFactorZ = (int)Math.Round((decimal)p1_mod.Z / p2_mod.Z);


//                Func<Coord, Coord> Rotation = (Coord c) => new(c.X * iFactorX, c.Y * iFactorY, c.Z * iFactorZ);

//                Coord rotP2 = Rotation(b2.Position);

//                Coord Translation = new(
//                    rotP2.X - b1.Position.X,
//                    rotP2.Y - b1.Position.Y,
//                    rotP2.Z - b1.Position.Z
//                );

//                Func<Coord, Coord> Translated = (Coord c) => new(c.X - Translation.X, c.Y - Translation.Y, c.Z - Translation.Z);

//                foreach (var b in current.Beacons)
//                {

//                    var newPos = Translated(Rotation(b.Position));
//                    b.Position = newPos;
//                    bool xFound = false;
//                    foreach (var c in KnownBeacons.Select(x => x.Position))
//                    {
//                        if (c.Equals(newPos))
//                        {
//                            xFound = true;
//                            break;
//                        }
//                    }

//                    if (!xFound)
//                    {
//                        KnownBeacons.Add(b);
//                    }
//                    else
//                    {

//                    }

//                }



//                Console.WriteLine("-------------");

//            }




//            Console.WriteLine("Result: ");
//            Console.WriteLine(KnownBeacons.Count());
//            Console.WriteLine("-------------");
//            //var sortedList = KnownBeacons.OrderBy(x => x.Position.X);

//            //foreach(var b in sortedList)
//            //{
//            //    Console.WriteLine(b);
//            //}

//        }

//        public void Logic0(List<clsScanner> loScanners)
//        {
//            clsScanner S0 = loScanners[0];
//            List<clsBeacon> KnownBeacons = S0.Beacons;

//            Queue<clsScanner> scannersToCheck = new();
//            for (int i = 1; i < loScanners.Count; i++)
//                scannersToCheck.Enqueue(loScanners[i]);

//            while (scannersToCheck.Any())
//            {
//                clsScanner current = scannersToCheck.Dequeue();
//                Console.WriteLine(current.Name);

//                Dictionary<clsBeacon, clsBeacon> Mapping = new();
//                foreach (var known in KnownBeacons)
//                {
//                    foreach (var b in current.Beacons)
//                    {
//                        var intersect = known.Distances.Intersect(b.Distances);
//                        if (intersect.Count() > 10)
//                        {
//                            Mapping.Add(known, b);
//                        }
//                    }
//                }

//                if (Mapping.Count > 0)
//                {
//                    Console.WriteLine(Mapping.Count);

//                    int cog_1_x = 0;
//                    int cog_1_y = 0;
//                    int cog_1_z = 0;
//                    int cog_2_x = 0;
//                    int cog_2_y = 0;
//                    int cog_2_z = 0;

//                    foreach ((var key, var value) in Mapping)
//                    {
//                        //Console.WriteLine(key + "-->" + value);


//                        cog_1_x += key.Position.X;
//                        cog_1_y += key.Position.Y;
//                        cog_1_z += key.Position.Z;

//                        cog_2_x += value.Position.X;
//                        cog_2_y += value.Position.Y;
//                        cog_2_z += value.Position.Z;
//                    }

//                    cog_1_x /= Mapping.Keys.Count();//Console.WriteLine(cog_1_x);                   
//                    cog_1_y /= Mapping.Keys.Count();//Console.WriteLine(cog_1_y);
//                    cog_1_z /= Mapping.Keys.Count();//Console.WriteLine(cog_1_z);
//                    cog_2_x /= Mapping.Keys.Count();//Console.WriteLine(cog_2_x);
//                    cog_2_y /= Mapping.Keys.Count();//Console.WriteLine(cog_2_y);
//                    cog_2_z /= Mapping.Keys.Count();//Console.WriteLine(cog_2_z);


//                    clsBeacon b1 = Mapping.Keys.ToList()[0];
//                    clsBeacon b2 = Mapping[b1];



//                    Coord p1_mod = new((int)Math.Floor((decimal)b1.Position.X - cog_1_x),
//                                        (int)Math.Floor((decimal)b1.Position.Y - cog_1_y),
//                                        (int)Math.Floor((decimal)b1.Position.Z - cog_1_z));

//                    Coord p2_mod = new((int)Math.Floor((decimal)b2.Position.X - cog_2_x),
//                                        (int)Math.Floor((decimal)b2.Position.Y - cog_2_y),
//                                        (int)Math.Floor((decimal)b2.Position.Z - cog_2_z));


//                    int iFactorX = (int)Math.Round((decimal)p1_mod.X / p2_mod.X);
//                    int iFactorY = (int)Math.Round((decimal)p1_mod.Y / p2_mod.Y);
//                    int iFactorZ = (int)Math.Round((decimal)p1_mod.Z / p2_mod.Z);


//                    Func<Coord, Coord> Rotation = (Coord c) => new(c.X * iFactorX, c.Y * iFactorY, c.Z * iFactorZ);

//                    Coord rotP2 = Rotation(b2.Position);

//                    Coord Translation = new(
//                        rotP2.X - b1.Position.X,
//                        rotP2.Y - b1.Position.Y,
//                        rotP2.Z - b1.Position.Z
//                    );

//                    Func<Coord, Coord> Translated = (Coord c) => new(c.X - Translation.X, c.Y - Translation.Y, c.Z - Translation.Z);

//                    foreach (var b in current.Beacons)
//                    {

//                        var newPos = Translated(Rotation(b.Position));
//                        b.Position = newPos;
//                        bool xFound = false;
//                        foreach (var c in KnownBeacons.Select(x => x.Position))
//                        {
//                            if (c.Equals(newPos))
//                            {
//                                xFound = true;
//                                break;
//                            }
//                        }

//                        if (!xFound)
//                        {
//                            KnownBeacons.Add(b);
//                        }
//                        else
//                        {

//                        }

//                    }

//                }
//                else
//                {
//                    scannersToCheck.Enqueue(current);
//                }

//            }

//            Console.WriteLine(KnownBeacons.Count());
//            //var sortedList = KnownBeacons.OrderBy(x => x.Position.X);

//            //foreach(var b in sortedList)
//            //{
//            //    Console.WriteLine(b);
//            //}

//        }


//        public int GetCommonPoints(clsBeacon b1, clsBeacon b2)
//        {     
//            return  b1.Distances.Intersect(b2.Distances).Count();              
//        }
//        public int GetCommonPoints(List<clsBeacon> b1, List<clsBeacon> b2)
//        {
//            int iMax = int.MinValue;
//            foreach (var known in b1)
//            {
//                foreach (var b in b2)
//                {
//                    var intersect = known.Distances.Intersect(b.Distances).Count();
//                    if (intersect > iMax)
//                        iMax = intersect;
//                }
//            }
//            return iMax;
//        }

//        public override void Q2()
//        {
//            var lsInput = Input;
//            lsInput = Test;
//        }



//        public class clsScanner
//        {
//            public Coord Position { get; set; }
//            public string Name { get; private set; }
//            public List<clsBeacon> Beacons { get; private set; }
//            public clsScanner(string sName)
//            {
//                Name = sName;
//                Beacons = new();
//            }
//            public void AddBeacon(clsBeacon beacon)
//            {
//                foreach (var b in Beacons)
//                    beacon.AddNeighbour(b);
//                Beacons.Add(beacon);
//            }

//            public override string ToString()
//            {
//                return Name;
//            }
//        }

//        public class clsBeacon
//        {
//            public Coord Position { get; set; }

//            public List<int> Distances { get; set; }

//            public clsBeacon(string sBeacon)
//            {
//                Distances = new();
//                string[] asSplit = sBeacon.Split(",");
//                Position = new(
//                    int.Parse(asSplit[0].Trim()),
//                    int.Parse(asSplit[1].Trim()),
//                    int.Parse(asSplit[2].Trim())
//                );
//            }

//            public void AddNeighbour(clsBeacon neighbour, bool xAddMeToNeighbour = true)
//            {
//                Distances.Add(Position.Distance(neighbour.Position));
//                if (xAddMeToNeighbour)
//                    neighbour.AddNeighbour(this, false);
//                //Neighbours = Neighbours.OrderBy(x => x.Distance).ToList();
//            }

//            public override string ToString()
//            {
//                return $"{Position}";
//            }
//        }

//        public class Coord
//        {
//            public int X { get; set; }
//            public int Y { get; set; }
//            public int Z { get; set; }
//            public Coord(int x, int y, int z)
//            {
//                X = x;
//                Y = y;
//                Z = z;
//            }

//            public int ManhattenDistance(Coord other) => Math.Abs(other.X - X) + Math.Abs(other.Y - Y) + Math.Abs(other.Z - Z);
//            public int Distance(Coord other)
//            {
//                int dx = X - other.X;
//                int dy = Y - other.Y;
//                int dz = Z - other.Z;

//                return (int)Math.Floor(Math.Sqrt(dx * dx + dy * dy + dz * dz));
//            }

//            public Coord Vector(Coord other) => new(other.X - X, other.Y - Y, other.Z - Z);

//            internal Coord Translate(Coord translation) => new(X + translation.X, Y + translation.Y, Z + translation.Z);

//            public override string ToString()
//            {
//                return $"[{X},{Y},{Z}]";
//            }

//            public bool Equals(Coord c)
//            {
//                if (c.X == X && c.Y == Y && c.Z == Z) 
//                    return true;
//                return false;
//            }
//        }

//    }


//}
