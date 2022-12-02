//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdventOfCode_2021
//{

//    public class Day24 : Day
//    {
//        public override int _iDay { get { return 24; } }

//        internal override List<string> _lsTest => new List<string>
//        {
//            "inp w",
//            "add z w",
//            "mod z 2",
//            "div w 2",
//            "add y w",
//            "mod y 2",
//            "div w 2",
//            "add x w",
//            "mod x 2",
//            "div w 2",
//            "mod w 2"
//        };
//        public override void Q1()
//        {
//            var lsInput = Input;

//            ALU alu = new(lsInput);

//        }

//        public override void Q2()
//        {


//        }

//        public class ALU
//        {
//            public bool Valid { get; private set; }
//            public int w { get; private set; }
//            public int x { get; private set; }
//            public int y { get; private set; }
//            public int z { get; private set; }
//            public List<int> Input { get; private set; }

//            private Dictionary<string, Action<string>> Instructions;

//            public ALU(List<string> lsInstructions)
//            {

//                Instructions = new();
//                Instructions.Add("inp", inp);
//                Instructions.Add("add", add);
//                Instructions.Add("mul", mul);
//                Instructions.Add("div", div);
//                Instructions.Add("mod", mod);
//                Instructions.Add("eql", eql);

//                llsInstructions = new();
//                for (int i = 1; i < lsInstructions.Count; i++)
//                {
//                    List<string> instr = new();
//                    do
//                    {
//                        instr.Add(lsInstructions[i]);
//                        i++;
//                    }
//                    while (i < lsInstructions.Count && !lsInstructions[i].StartsWith("inp"));
//                    llsInstructions.Add(instr);
//                }

//                //Logic(llsInstructions);
//                RecusiveSearch(0, 13,0,1);
//            }

//            private List<List<string>> llsInstructions;

//            List<Int64> PossibleAnswers = new();
//            private void RecusiveSearch(int iZ, int iIndex, Int64 iTw, int iBase)
//            {
//                if (iTw % 10 == 5)
//                {

//                }
//                if (iIndex < 0)
//                {
//                    PossibleAnswers.Add(iTw);
//                    return;
//                }

//                for (int iW = 9; iW > 0; iW--)
//                    for (int iz = 0; iz < 100; iz++)
//                    {
//                        x = 0;
//                        y = 0;
//                        w = iW;
//                        z = iz;
//                        Execute(llsInstructions[iIndex]);
//                        if (z == iZ)
//                        { 
//                            RecusiveSearch(iz, iIndex - 1, iW * iBase + iTw, iBase*10);
//                        }

//                    }

//            }

//            private void Logic(List<List<string>> liiInstructions)
//            {
//                Dictionary<clsPossibility, clsPossibility> Combis = new();
//                List<int> liPossibleZ = new() { 0 };
//                List<clsPossibility> liPossibilities = new() { null };
//                Int64 iBase = 1;

//                for(int i = liiInstructions.Count - 1; i >= 0; i--)
//                {
//                    var instructions = liiInstructions[i];

//                    //x = 0;
//                    //y = 0;

//                    //Dictionary<clsPossibility, clsPossibility> combinations = new();

//                    List<int> liNewPossibleZ = new() { };
//                    List<clsPossibility> liNewPossibilities = new() { };

//                    for (int iW = 9; iW > 0; iW--)
//                        for(int iZ =0;iZ <100;iZ++)
//                        {
//                            x = 0;
//                            y = 0;
//                            w = iW;
//                            z = iZ;
//                            Execute(instructions);
//                            if (liPossibleZ.Contains(z))
//                            {
//                                int iIndex = liPossibleZ.IndexOf(z);
//                                clsPossibility Pos = liPossibilities[iIndex];
//                                Int64 iPrevTw = 0;
//                                if (Pos != null)
//                                    iPrevTw = Pos.tW;
                                
//                                Int64 iTW = iW * iBase + iPrevTw;
//                                clsPossibility NewPos = new(iZ,iW, iTW);
                                
//                                liNewPossibleZ.Add(iZ);
//                                liNewPossibilities.Add(NewPos);

//                                Combis.Add(NewPos, Pos);
//                                //combinat

//                                //if (Combis.Count() == 0)
//                                //{
//                                //    combinations.Add(new(iZ,iW), null);
//                                //}
//                                //else if (Combis.ContainsKey(z))
//                                //{
//                                //    combinations.Add(iZ, iW * iBase + Combis[z]);
//                                //}
//                                //break;
//                            }

//                        }
                    
//                    liPossibleZ = liNewPossibleZ;
//                    liPossibilities = liNewPossibilities;
//                    iBase *= 10;
//                }

//                Int64 iMax = Int64.MinValue;
//                //foreach((clsPossibility key, clsPossibility Val) in Combis)
//                foreach(clsPossibility key in Combis.Keys)
//                {
//                    if (key.tW > 10000000000000)
//                    {
//                        Console.WriteLine(key.tW);
//                        if (key.tW > iMax)
//                            iMax = key.tW;
//                    }
//                }
//                Console.WriteLine(iMax);



//            }
//            private void Logic0(List<List<string>> liiInstructions)
//            {
//                Dictionary<int, Int64> Combis = new();

//                List<int> liPossibleZ = new() { 0 };

//                Int64 iBase = 1;

//                for (int i = liiInstructions.Count - 1; i >= 0; i--)
//                {
//                    var instructions = liiInstructions[i];

//                    x = 0;
//                    y = 0;

//                    Dictionary<int, Int64> combinations = new();

//                    List<int> liNewPossibleZ = new() { };

//                    for (int iW = 9; iW > 0; iW--)
//                        for (int iZ = 0; iZ < 100; iZ++)
//                        {
//                            w = iW;
//                            z = iZ;
//                            Execute(instructions);
//                            if (liPossibleZ.Contains(z))
//                            {
//                                liNewPossibleZ.Add(iZ);
//                            }
//                            if (Combis.Count() == 0)
//                            {
//                                if (z == 0)
//                                    combinations.Add(iZ, iW);
//                            }
//                            else
//                            {
//                                if (Combis.ContainsKey(z))
//                                {
//                                    combinations.Add(z, iW * iBase + Combis[z]);
//                                    break;
//                                }
//                            }
//                        }
//                    liPossibleZ = liNewPossibleZ;
//                    Combis = combinations;
//                    iBase *= 10;
//                }
//            }

//            public void Execute(List<string> Instructions)
//            {
//                foreach (var ins in Instructions)
//                    Instruction(ins);

//            }

//            public void Instruction(string s)
//            {
//                string sIns = s.Split(" ")[0];
//                Instructions[sIns](s);
//            }

//            private void inp(string s)
//            {
//                //string[] asSplit = s.Split(" ");

//                //int iValue = Input[Index];
//                //Index++;

//                //SetProperty(asSplit[1], iValue);
//            }
//            private void add(string s)
//            {
//                (string prop, int iCurrent, int iExtra) = Info(s);
//                SetProperty(prop, iCurrent + iExtra);
//            }
//            private void mul(string s)
//            {
//                (string prop, int iCurrent, int iExtra) = Info(s);
//                SetProperty(prop, iCurrent * iExtra);
//            }
//            private void div(string s)
//            {
//                (string prop, int iCurrent, int iExtra) = Info(s);
//                if (iExtra == 0)
//                {

//                }
//                SetProperty(prop, iCurrent / iExtra);
//            }
//            private void mod(string s)
//            {
//                (string prop, int iCurrent, int iExtra) = Info(s);
//                SetProperty(prop, iCurrent % iExtra);
//            }
//            private void eql(string s)
//            {
//                (string prop, int iCurrent, int iExtra) = Info(s);

//                int iResult = 0;
//                if (iCurrent == iExtra)
//                    iResult = 1;

//                SetProperty(prop, iResult);
//            }

//            private (string, int, int) Info(string s)
//            {
//                string[] asSplit = s.Split(" ");
//                int iVal = GetProperty(asSplit[1]);

//                int iAddedVal = 0;
//                if (!int.TryParse(asSplit[2], out iAddedVal))
//                    iAddedVal = GetProperty(asSplit[2]);

//                return (asSplit[1], iVal, iAddedVal);
//            }

//            private int GetProperty(string s)
//            {
//                return (int)typeof(ALU).GetProperty(s).GetValue(this);
//            }
//            private void SetProperty(string s, int i)
//            {
//                typeof(ALU).GetProperty(s).SetValue(this, i);
//            }

//        }

//        public class clsPossibility
//        {
//            public int Z { get; set; }
//            public int W { get; set; }
//            public Int64 tW { get; set; }
//            public clsPossibility(int iZ, int iW, Int64 iTW)
//            {
//                Z = iZ;
//                tW = iTW;
//                W = iW;
//            }
//            public override string ToString()
//            {
//                return $"[{Z},{W},{tW}]";
//            }
//        }



//    }
//}
