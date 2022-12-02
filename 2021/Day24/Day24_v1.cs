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
//        List<string> _lsTest2 => new List<string>
//        {
//            "inp z",
//            "inp x",
//            "mul z 3",
//            "eql z x"
//        };

//        public override void Q1()
//        {
//            var lsInput = Input;

//            ALU alu = null;

//            Int64 i = 99999999999999;

//            alu = new(lsInput);

//            while (!alu.Valid)
//            {
//                i--;
//                alu.CheckNumber(i);
//            }

//        }

//        public override void Q2()
//        {


//        }

//        public class ALU
//        {
//            public bool Valid { get; private set; }
//            public string w { get; private set; }
//            public List<clsInstruction> x { get; private set; }
//            public List<clsInstruction> y { get; private set; }
//            public List<clsInstruction> z { get; private set; }
//            public int Index { get; private set; }

//            public ALU(List<string> lsInstructions)
//            {
//                w = "0";
//                x = new();
//                y = new();
//                z = new();

//                Instructions = new();
//                Instructions.Add("inp", inp);
//                Instructions.Add("add", add);
//                Instructions.Add("mul", mul);
//                Instructions.Add("div", div);
//                Instructions.Add("mod", mod);
//                Instructions.Add("eql", eql);

//                AnalyzeInstructions(lsInstructions);
//            }

//            private Dictionary<string, Action<string>> Instructions;// = new();
//            //{
//            //    //{ "add", add => { add } },
//            //    { "add", (string s) => { ; }  },
//            //    { "add", (string s) => new Action(add(s))  },
//            //    { "mul", mul => { } },
//            //    { "div", div => { } },
//            //    { "mod", mod => { } },
//            //    { "eql", eql => { } },

//            //};

//            private void AnalyzeInstructions(List<string> Instructions)
//            {
//                foreach (var i in Instructions)
//                    Instruction(i);
//            }

//            public void Instruction(string s)
//            {
//                string sIns = s.Split(" ")[0];
//                Instructions[sIns](s);
//                Console.WriteLine($"w: {w}");
//                Console.WriteLine($"x: {x}");
//                Console.WriteLine($"y: {y}");
//                Console.WriteLine($"z: {z}");
//                Console.WriteLine($"----");
//            }

//            private void inp(string s)
//            {
//                //always for w 
//                w = $"p{Index}";
//                Index++;
//            }
//            private void add(string s)
//            {
//                string[] asSplit = s.Split(" ");
//                List<clsInstruction> iVal = GetProperty(asSplit[1]);

//                int iAddedVal = 0;
//                if (!int.TryParse(asSplit[2], out iAddedVal))
//                {
//                    List<clsInstruction> Vals = GetProperty(asSplit[2]);

//                }
//                else if (iAddedVal != 0)
//                    iVal.Add(new("add", iAddedVal));


//            }
//            private void mul(string s)
//            {
//                (string prop, string iCurrent, string iExtra) = Info(s);

//                if (iExtra == "0")
//                    SetProperty(prop, "0");
//                else if (iExtra != "1")
//                    SetProperty(prop, $"({iCurrent})*{iExtra}");

//            }
//            private void div(string s)
//            {
//                (string prop, string iCurrent, string iExtra) = Info(s);
//                if (iExtra != "1")
//                    SetProperty(prop, $"({iCurrent})/{iExtra}");
//            }
//            private void mod(string s)
//            {
//                (string prop, string iCurrent, string iExtra) = Info(s);
//                SetProperty(prop, $"({iCurrent})%{iExtra}");
//            }
//            private void eql(string s)
//            {
//                //(string prop, string iCurrent, string iExtra) = Info(s);

//                //int iResult = 0;
//                //if (iCurrent == iExtra)
//                //    iResult = 1;
                
//                //SetProperty(prop, iResult);
//            }

//            private (string, List<clsInstruction>, string) Info(string s)
//            {
//                string[] asSplit = s.Split(" ");
//                List<clsInstruction> iVal = GetProperty(asSplit[1]);

//                int iAddedVal = 0;
//                string sAddedVal = "";
//                if (!int.TryParse(asSplit[2], out iAddedVal))
//                    sAddedVal = GetProperty(asSplit[2]);            
//                else
//                    sAddedVal = iAddedVal.ToString();
                
//                return (asSplit[1], iVal, sAddedVal);
//            }

//            private List<clsInstruction> GetProperty(string s)
//            {
//                return (string)typeof(ALU).GetProperty(s).GetValue(this);
//            }
            
//            public void CheckNumber(Int64 input)
//            {
//                List<int> Input = new();
//                foreach (var s in input.ToString()) // better way?
//                    Input.Add(int.Parse(s.ToString()));
//            }

//        }

//        public class clsInstruction
//        {
//            Func<int, int, int> Instruction;

//            string sValue;
//            int iValue;

//            public clsInstruction(string s, string val)
//            {
//                sValue = val;
//                Constructor(s);
//            }
//            public clsInstruction(string s, int val)
//            {
//                iValue = val;
//                Constructor(s);
//            }
//            private void Constructor(string s)
//            {
//                Dictionary<string, Func<int, int, int>> Instructions = new();
//                Instructions.Add("add", add);
//                Instructions.Add("mul", mul);
//                Instructions.Add("div", div);
//                Instructions.Add("mod", mod);
//                Instructions.Add("eql", eql);
//                Instruction = Instructions[s];
//            }

//            public int execute(int i1)
//            {
//                return Instruction(i1, iValue);
//            }
//            public int execute(int i1,int i2)
//            {
//                return Instruction(i1, i2);
//            }

//            private int add(int i1, int i2)
//            {
//                return i1 + i2;
//            }
//            private int mul(int i1, int i2)
//            {
//                return i1 * i2;
//            }
//            private int div(int i1, int i2)
//            {
//                return i1 / i2;
//            }
//            private int mod(int i1, int i2)
//            {
//                return i1 % i2;
//            }
//            private int eql(int i1, int i2)
//            {
//                if (i1 == i2)
//                    return 1;
//                return 0;
//            }
        
//        }


//    }
//}
