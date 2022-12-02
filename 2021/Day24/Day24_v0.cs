//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdventOfCode_2021
//{

//    public class Day24_v0 : Day
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

//            //Int64 i = 99999999999999;
//            Int64 i = 13191913571211; //answer

//            alu = new(i, lsInput);

//            while (!alu.Valid)
//            {
//                i--;
//                string s = i.ToString();
//                while (s.Contains("0"))
//                {
//                    i--;
//                    s = i.ToString();
//                }

//                alu = new(i, lsInput);
//            }

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
//            public int Index { get; private set; }

//            public ALU(Int64 iInput, List<string> lsInstructions)
//            {
//                Input = new();
//                foreach (var s in iInput.ToString()) // better way?
//                    Input.Add(int.Parse(s.ToString()));

//                Instructions = new();
//                Instructions.Add("inp", inp);
//                Instructions.Add("add", add);
//                Instructions.Add("mul", mul);
//                Instructions.Add("div", div);
//                Instructions.Add("mod", mod);
//                Instructions.Add("eql", eql);

//                Execute(lsInstructions);
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

//            public void Execute(List<string> Instructions)
//            {
//                foreach (var ins in Instructions)
//                    Instruction(ins);
//                if (z == 0)
//                    Valid = true;
//            }

//            public void Instruction(string s)
//            {
//                string sIns = s.Split(" ")[0];
//                Instructions[sIns](s);
//            }

//            private void inp(string s)
//            {
//                string[] asSplit = s.Split(" ");

//                int iValue = Input[Index];
//                Index++;

//                SetProperty(asSplit[1], iValue);
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


//    }
//}
