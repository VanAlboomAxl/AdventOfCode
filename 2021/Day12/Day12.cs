using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day12 : Day
    {
        public override int _iDay { get { return 12; } }

        internal override List<string> _lsTest => _lsTest1;

        private List<string> _lsTest1 = new List<string>() {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"
        };
        private List<string> _lsTest2 = new List<string>() {
            "dc-end",
            "HN-start",
            "start-kj",
            "dc-start",
            "dc-HN",
            "LN-dc",
            "HN-end",
            "kj-sa",
            "kj-HN",
            "kj-dc"
        };
        private List<string> _lsTest3 = new List<string>() {
            "fs-end",
            "he-DX",
            "fs-he",
            "start-DX",
            "pj-DX",
            "end-zg",
            "zg-sl",
            "zg-pj",
            "pj-he",
            "RW-he",
            "fs-DX",
            "pj-RW",
            "zg-RW",
            "start-pj",
            "he-WI",
            "zg-he",
            "pj-fs",
            "start-RW"
        };
      
        public override void Q1()
        {
            var lsInput = Input;
            //var lsInput = Test;
            Dictionary<string, cls12_Cave> dicConvert = new();
            cls12_Map oMap = new();
            foreach(var sInput in lsInput)
            {
                var asSplit = sInput.Split('-');
                string sCave1 = asSplit[0];
                string sCave2 = asSplit[1];
                cls12_Cave oCave1 = null, oCave2 = null;

                if (dicConvert.ContainsKey(sCave1))
                    oCave1 = dicConvert[sCave1];
                else
                {
                    oCave1 = new(sCave1);
                    dicConvert.Add(sCave1, oCave1);
                }
                if (dicConvert.ContainsKey(sCave2))
                    oCave2 = dicConvert[sCave2];
                else
                {
                    oCave2 = new(sCave2);
                    dicConvert.Add(sCave2, oCave2);
                }

                oMap.Add(new(oCave1, oCave2));
            }

            cls12_Cave oStart = dicConvert["start"];
            cls12_Cave oEnd = dicConvert["end"];
            Logic(oMap, oEnd, new(), oStart);
            //List<cls12_Cave> StartEndPoints = oMap.GoToFrom(oStart);

            Console.WriteLine("Result: " + PossiblePaths.Count());

        }

        public List<List<cls12_Cave>> PossiblePaths = new();

        public void Logic(cls12_Map oMap, cls12_Cave oEnd, List<cls12_Cave> PreceedingPath, cls12_Cave oCurrentCave)
        {
            if (PreceedingPath.Contains(oCurrentCave) && !oCurrentCave.BigCave)
                return;

            var MyPreceedingPath = CopyList(PreceedingPath);
            MyPreceedingPath.Add(oCurrentCave);
            List<cls12_Cave> PossibleCaves = oMap.GoToFrom(oCurrentCave);
            foreach(var cave in PossibleCaves)
            {
                if (cave.Equals(oEnd))
                {
                    var PossiblePath = CopyList(MyPreceedingPath);
                    PossiblePath.Add(oEnd);
                    PossiblePaths.Add(PossiblePath);
                }
                else if (!cave.BigCave && PreceedingPath.Contains(cave))
                    continue;
                else
                    Logic(oMap, oEnd, MyPreceedingPath, cave);
            }
        }
        public List<cls12_Cave> CopyList(List<cls12_Cave> ListIn)
        {
            cls12_Cave[] ArrayOut = new cls12_Cave[ListIn.Count()];
            ListIn.CopyTo(ArrayOut);
            return ArrayOut.ToList();
        }

        public override void Q2()
        {
            PossiblePaths = new();
            var lsInput = Input;
            //var lsInput = Test;
            Dictionary<string, cls12_Cave> dicConvert = new();
            cls12_Map oMap = new();
            foreach (var sInput in lsInput)
            {
                var asSplit = sInput.Split('-');
                string sCave1 = asSplit[0];
                string sCave2 = asSplit[1];
                cls12_Cave oCave1 = null, oCave2 = null;

                if (dicConvert.ContainsKey(sCave1))
                    oCave1 = dicConvert[sCave1];
                else
                {
                    oCave1 = new(sCave1);
                    dicConvert.Add(sCave1, oCave1);
                }
                if (dicConvert.ContainsKey(sCave2))
                    oCave2 = dicConvert[sCave2];
                else
                {
                    oCave2 = new(sCave2);
                    dicConvert.Add(sCave2, oCave2);
                }

                oMap.Add(new(oCave1, oCave2));
            }

            cls12_Cave oStart = dicConvert["start"];
            cls12_Cave oEnd = dicConvert["end"];
            Logic(oMap, oStart, oEnd, new(), oStart,null);

            foreach(var path in PossiblePaths)
            {
                //Console.WriteLine(cls12_Extenstion.ToString(path));
            }

            Console.WriteLine("Result: " + PossiblePaths.Count());

        }
        public void Logic(cls12_Map oMap, cls12_Cave oStart, cls12_Cave oEnd, List<cls12_Cave> PreceedingPath, cls12_Cave oCurrentCave, cls12_Cave oDubbleSmall)
        {
            if (PreceedingPath.Contains(oCurrentCave) && !oCurrentCave.BigCave)
            {
                if (oCurrentCave.Equals(oStart) || oCurrentCave.Equals(oEnd)) return;
                else
                {
                    if (oDubbleSmall == null)
                        oDubbleSmall = oCurrentCave;
                    else
                    {
                        if (oDubbleSmall.Equals(oCurrentCave))
                        {
                            int iCount = 0;
                            foreach(var cave in PreceedingPath)
                                if (cave.Equals(oCurrentCave))
                                    iCount++;
                            if (iCount >= 2)
                                return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            var MyPreceedingPath = CopyList(PreceedingPath);
            MyPreceedingPath.Add(oCurrentCave);
            List<cls12_Cave> PossibleCaves = oMap.GoToFrom(oCurrentCave);
            foreach (var cave in PossibleCaves)
            {
                if (cave.Equals(oEnd))
                {
                    var PossiblePath = CopyList(MyPreceedingPath);
                    PossiblePath.Add(oEnd);
                    PossiblePaths.Add(PossiblePath);
                }
                //else if (!cave.BigCave && PreceedingPath.Contains(cave))
                  //  continue;
                else
                    Logic(oMap, oStart, oEnd, MyPreceedingPath, cave, oDubbleSmall);
            }
        }

        

    }

    public class cls12_Map
    {
        public List<cls12_Path> Paths { get; private set; }
        public cls12_Map()
        {
            Paths = new();
        }
        public void Add(cls12_Path p)
        {
            Paths.Add(p);
        }
        public List<cls12_Cave> GoToFrom(cls12_Cave oCave)
        {
            List<cls12_Cave> Result = new();

            foreach(var path in Paths)
            {
                cls12_Cave To = path.MoveFrom(oCave);
                if (To != null)
                    Result.Add(To);
            }

            return Result;
        }
    }
    public class cls12_Path
    {
        public cls12_Cave Cave1 { get; private set; }
        public cls12_Cave Cave2 { get; private set; }
        public cls12_Path(cls12_Cave oCave1, cls12_Cave oCave2)
        {
            Cave1 = oCave1;
            Cave2 = oCave2;
        }
        public cls12_Cave MoveFrom(cls12_Cave oCave)
        {
            if (oCave.Equals(Cave1)) return Cave2;
            if (oCave.Equals(Cave2)) return Cave1;
            return null;
        }
        public override string ToString()
        {
            return Cave1 + " <--> "+Cave2;
        }
    }
    public class cls12_Cave
    {
        public string Name { get; private set; }
        public bool BigCave { get; private set; }
        public cls12_Cave(string sName)
        {
            Name = sName;
            if (sName.ToUpper().Equals(Name))
                BigCave = true;        
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public static class cls12_Extenstion
    {
        public static string ToString(List<cls12_Cave> me)
        {
            string sResult = "";
            for (int i = 0; i < me.Count(); i++)
            {
                sResult += me[i];
                if (i < me.Count() - 1)
                    sResult += "->";
            }
            return sResult;
        }
    }

}
