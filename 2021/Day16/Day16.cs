using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day16 : Day<string>
    {
        public override int _iDay { get { return 16; } }

        internal override List<string> _lsTest => new List<string> {
            "8A004A801A8002F478",
            "620080001611562C8802118E34",
            "C0015000016115A2E0802F182340",
            "A0016C880162017C3686B18A3D4780",
            "C200B40A82",
            "04005AC33890",
            "880086C3E88112",
            "CE00C43D881120",
            "D8005AC2A8F0",
            "F600BC2D8F",
            "9C005AC2F8F0",
            "9C0141080250320F1802104A08"
        };

        private string LiteralTest = "110100101111111000101000";
        private string OperatorTest_0_2 = "38006F45291200";
        private string OperatorTest_1_3 = "EE00D40C823060";

        public override string Convert(List<string> Input)
        {
            return Input[0];
        }

        public override void Q1()
        {
            //Console.WriteLine(Logic(InputConvertor(Input)));
            //Console.WriteLine(Logic(LiteralTest));
            //Console.WriteLine(Logic(InputConvertor(OperatorTest_0_2)));
            //Console.WriteLine(Logic(InputConvertor(OperatorTest_1_3)));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[0])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[1])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[2])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[3])));
        }

        public override void Q2()
        {
            Console.WriteLine(Logic(InputConvertor(Input)));
            //Console.WriteLine(Logic(LiteralTest));
            //Console.WriteLine(Logic(InputConvertor(OperatorTest_0_2)));
            //Console.WriteLine(Logic(InputConvertor(OperatorTest_1_3)));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[4])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[5])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[6])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[7])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[8])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[9])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[10])));
            //Console.WriteLine(Logic(InputConvertor(_lsTest[11])));
        }

        private long Logic(string sInput)
        {
            _iVersion = 0;
            int iIndex = 0;        
            long result = StringAnalyzer(sInput,ref iIndex);
            return result;
        }
        private int _iVersion;
        private long StringAnalyzer(string sInput, ref int iIndex, bool xTrailingZeros = true)
        {
            _iVersion += Subint(sInput, ref iIndex, 3); 
            int typeID = Subint(sInput, ref iIndex, 3);
            if (typeID == 4) //literal value
            {
                return LiteralString(sInput, ref iIndex, xTrailingZeros);
            }
            else
            {
                return OperatorString(sInput, ref iIndex, typeID);
            }
        }

        private long LiteralString(string sInput, ref int iIndex, bool xTrailingZeros = true)
        {
            string sPrefix = Substring(sInput, ref iIndex, 1);
            string sCode = Substring(sInput, ref iIndex, 4);
            while (sPrefix == "1")
            {
                sPrefix = Substring(sInput, ref iIndex, 1);
                sCode += Substring(sInput, ref iIndex, 4);
            }
            if(xTrailingZeros)
                iIndex += (iIndex-6) % 4;
            return BitStringToLong(sCode);
        }

        private long OperatorString(string sInput, ref int iIndex, int typeID)
        {
            List<long> Results = new();
            int lengthTypeID = Subint(sInput, ref iIndex, 1);
            if (lengthTypeID.Equals(0))
            {
                int TotalBitLength = Subint(sInput,ref iIndex, 15);
                int iNewIndex = iIndex;
                while(iNewIndex < iIndex + TotalBitLength)
                {
                    Results.Add(StringAnalyzer(sInput, ref iNewIndex, false));
                }
                iIndex = iNewIndex;
            }
            else
            {
                int NumberOfPackets = Subint(sInput, ref iIndex, 11);
                for(int i = 0; i < NumberOfPackets; i++)
                {
                    Results.Add(StringAnalyzer(sInput, ref iIndex,false));
                }
            }

            switch (typeID)
            {
                case 0: return Results.Sum(); 
                case 1: return Results.Aggregate((total, next) => total * next);
                case 2: return Results.Min();
                case 3: return Results.Max();
                case 5:
                    if (Results[0] > Results[1]) return 1;
                    else return 0;
                case 6:
                    if (Results[0] < Results[1]) return 1;
                    else return 0;
                case 7:
                    if (Results[0] == Results[1]) return 1;
                    else return 0;
            }
            return -1;
        }

        private int Subint(string sInput, ref int iIndex, int length)
        {
            return BitStringToInt(Substring(sInput, ref iIndex, length));
        }
        private string Substring(string sInput, ref int iIndex, int length)
        {
            var result = sInput.Substring(iIndex, length); 
            iIndex += length;
            return result;
        }

        private int BitStringToInt(string s)
        {
            return System.Convert.ToInt32(s, 2);
        }
        private long BitStringToLong(string s)
        {
            return System.Convert.ToInt64(s, 2);
        }
        private string InputConvertor(string sInput)
        {
            string sResult = "";
            foreach (char c in sInput)
                sResult += HexConvertor(c);
            return sResult;
        }
        private string HexConvertor(char hex)
        {
            //return System.Convert.ToString(System.Convert.ToInt32(hex.ToString(),16),2);

            switch (hex)
            {
                case '0': return "0000";
                case '1': return "0001";
                case '2': return "0010";
                case '3': return "0011";
                case '4': return "0100";
                case '5': return "0101";
                case '6': return "0110";
                case '7': return "0111";
                case '8': return "1000";
                case '9': return "1001";
                case 'A': return "1010";
                case 'B': return "1011";
                case 'C': return "1100";
                case 'D': return "1101";
                case 'E': return "1110";
                case 'F': return "1111";
                default: return null;
            }
        }

    }
}
