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

//            public bool CheckNumber(Int64 input)
//            {
//                List<int> Input = new();
//                foreach (var s in input.ToString()) // better way?
//                    Input.Add(int.Parse(s.ToString()));

//                int z = ((Input[0] + 7) * 26 + Input[1] + 8) * 26 + Input[2] + 10;
//                int x = z;
//                z /= 26;



//                return false;

//            }

//        }

//    }
//}
