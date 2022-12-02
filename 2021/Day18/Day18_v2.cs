using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day18 : Day
    {
        public override int _iDay { get { return 18; } }

        internal override List<string> _lsTest => new List<string> {
            "[[[[[9,8],1],2],3],4]", // test to explode
            "[7,[6,[5,[4,[3,2]]]]]", // test to explode
            "[[6,[5,[4,[3,2]]]],1]", // test to explode
            "[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", // test to explode
            "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", // test to explode
            "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", // test to split
            "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", // test to reduce
            "[1,2]",
            "[[1,2],3]",
            "[9,[8,7]]",
            "[[1,9],[8,5]]",
            "[[[[1,2],[3,4]],[[5,6],[7,8]]],9]",
            "[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]",
            "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]"
        };

        internal List<string> _lsTest2 = new()
        {
            "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
            "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
            "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
            "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
            "[7,[5,[[3,8],[1,4]]]]",
            "[[2,[2,2]],[8,[8,1]]]",
            "[2,9]",
            "[1,[[[9,3],9],[[9,0],[0,7]]]]",
            "[[[5,[7,4]],7],1]",
            "[[[[4,2],2],6],[8,7]]"
        };

        private List<string> _lsTest3 = new()
        {
            "[1,1]",
            "[2,2]",
            "[3,3]",
            "[4,4]",
            "[5,5]"
        };

        private List<string> _lsTest4 = new()
        {
            "[[1,2],[[3,4],5]]",
            "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]",
            "[[[[1,1],[2,2]],[3,3]],[4,4]]",
            "[[[[3,0],[5,3]],[4,4]],[5,5]]",
            "[[[[5,0],[7,4]],[5,5]],[6,6]]",
            "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]"
        };

        private List<string> _lsTest5 = new()
        {
            "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
            "[[[5,[2,8]],4],[5,[[9,9],0]]]",
            "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
            "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
            "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
            "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
            "[[[[5,4],[7,7]],8],[[8,3],8]]",
            "[[9,3],[[9,9],[6,[4,9]]]]",
            "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
            "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"
        };

        public List<Pair> Convert(List<string> lsInput)
        {
            Pair pParrent = null;
            List<Pair> lpInput = new();
            foreach (string s in lsInput)
            {
                Number Last = null;
                foreach (char c in s)
                {
                    if (c.Equals('['))
                    {
                        if (pParrent == null)
                            pParrent = new();
                        else
                        {
                            Pair pNew = new();
                            pNew.Depth = pParrent.Depth + 1;

                            if (pParrent.N1 == null)
                                pParrent.N1 = pNew;
                            else
                                pParrent.N2 = pNew;

                            pNew.Parrent = pParrent;
                            pParrent = pNew;
                        }
                    }
                    else if (c.Equals(']'))
                    {
                        if (pParrent.Parrent != null)
                            pParrent = pParrent.Parrent;
                    }
                    else if (!c.Equals(','))
                    {
                        // getal!

                        int i = int.Parse(c.ToString());
                        Number nNew = new(i);
                        nNew.Parrent = pParrent;
                        if (Last != null)
                        {
                            nNew.Left = Last;
                            Last.Right = nNew;
                        }
                        Last = nNew;

                        if (pParrent.N1 == null)
                            pParrent.N1 = nNew;
                        else
                            pParrent.N2 = nNew;
                    }
                }

                lpInput.Add(pParrent);
                pParrent = null;
            }
            return lpInput;
        }

        public override void Q1()
        {

            //List<Pair> lpInput = Convert(_lsTest2);
            //List<Pair> lpInput = Convert(_lsTest3);
            //List<Pair> lpInput = Convert(_lsTest4);
            //List<Pair> lpInput = Convert(_lsTest5);
            List<Pair> lpInput = Convert(Input);

            //Console.WriteLine(Explode(lpInput[0]));
            //Console.WriteLine(Explode(lpInput[1]));
            //Console.WriteLine(Explode(lpInput[2]));
            //Console.WriteLine(Explode(lpInput[3]));
            //Console.WriteLine(Explode(lpInput[4]));


            //(var p, var x) = Explode(lpInput[5]);
            //Console.WriteLine(p);
            //( p,  x) = Split(p);
            //Console.WriteLine(p);
            //( p,  x) = Split(p);
            //Console.WriteLine(p);
            //( p,  x) = Explode(p);
            //Console.WriteLine(p);
            //Reduce(lpInput[6]);


            Pair result = Logic(lpInput);

            Console.WriteLine(result + " : " + result.Magnitude());
            //foreach (var p in lpInput)
            //    Console.WriteLine(p + " : " +p.Magnitude());
        }

        private Pair Logic(List<Pair> lps)
        {
            //Pair current = Reduce(lps[0]);
            Pair current = lps[0];
            for(int i = 1; i < lps.Count; i++)
            {
                //Console.WriteLine("-----------------------------");
                //Pair next = Reduce(lps[i]);
                Pair next = lps[i];
                current = Reduce(Addition(current, next));
                
                //Console.WriteLine("-----------------------------");
            }
            return current;
        }

        private Pair Addition(Pair p1, Pair p2)
        {

            // update al pairs depth!
            Number n1 = LoopOverAllPairs(p1, UpdateDepth,true);
            Number n2 = LoopOverAllPairs(p2, UpdateDepth,false);

            n1.Right = n2;
            n2.Left = n1;

            Pair result = new Pair()
            {
                N1 = p1,
                N2 = p2
            };

            p1.Parrent = result;
            p2.Parrent = result;

            return result;

        }
        private void UpdateDepth(Pair p)
        {
            p.Depth = p.Depth + 1;
        }

        private Pair Reduce(Pair p)
        {
            bool xAction = true;
            Console.WriteLine(p);
            while (xAction)
            {
                (p, xAction) = Explode(p);
                if (!xAction)
                    (p, xAction) = Split(p);
                //if (xAction)
                //    Console.WriteLine(p);
            }
            Console.WriteLine(p);
            return p;
        }

        private (Pair,bool) Explode(Pair p)
        {
            bool xExplode = false;

            Pair current = p;
            Pair Parent = null;

            List<Pair> Analised = new();

            while (true)
            {
                Analised.Add(current);

                if(current.Depth >= 4 && current.AllNumbers)
                {
                    Exploding(current);
                    //if (current.Equals(Parent.N1))
                    //    Parent.N1 = Exploding(current);           
                    //else
                    //    Parent.N2 = Exploding(current);
                    
                    xExplode = true;
                    break;
                }
                else if (current.N1.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N1))
                {
                    Parent = current;
                    current = (Pair)current.N1;
                }
                else if (current.N2.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N2))
                {
                    Parent = current;
                    current = (Pair)current.N2;
                }
                else
                {
                    if (current.Parrent == null)
                    {
                        break;
                    }
                    else
                    {
                        current = current.Parrent;
                        Parent = current.Parrent;
                    }
                }
            }


            return (p, xExplode);
        }

        private Number Exploding(Pair p)
        {
            Number N1 = (Number)p.N1;
            Number N2 = (Number)p.N2;

            int iN1 = N1.Value;
            int iN2 = N2.Value;

            Number nResult = new Number(0);
            if (N1.Left != null)
            {
                Number nLeft = N1.Left;
                nResult.Left = nLeft;
                nLeft.Right = nResult;
                nLeft.Value = nLeft.Value + iN1; 
            }
            if (N2.Right != null)
            {
                Number nRight = N2.Right;
                nResult.Right = nRight;
                nRight.Left = nResult;
                nRight.Value = nRight.Value + iN2;
            }

            Pair parrent = p.Parrent;

            nResult.Parrent = parrent;

            if (nResult.Parrent == null)
            {
                var x = true;
            }


            if (parrent.N1.Equals(p))
                parrent.N1 = nResult;
            else
                parrent.N2 = nResult;


            return nResult;

        }

        private (Pair,bool) Split(Pair p)
        {
            var xSplit = false;

            Pair current = p;

            Number nFirst = null;

            while (nFirst == null)
            {
                if (current.N1.GetType() == typeof(Number))
                    nFirst = (Number)current.N1;
                else
                    current = (Pair)current.N1;
            }

            while(nFirst != null)
            {
                if (nFirst.Value >= 10)
                {
                    Pair result = Split(nFirst);
                    xSplit = true;
                    break;
                }
                else
                {
                    nFirst = nFirst.Right;
                }
            }



            return (p, xSplit);
        }
        private (Pair, bool) Split_0(Pair p)
        {
            var xSplit = false;

            List<Pair> Analised = new();
            Pair current = p;
            Pair Parent = null;

            while (true)
            {
                Analised.Add(current);

                if (current.N1.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N1))
                {
                    Parent = current;
                    current = (Pair)current.N1;
                }
                else if (current.N1.GetType() == typeof(Number) && ((Number)current.N1).Value > 10)
                {
                    Pair result = Split(((Number)current.N1));
                    current.N1 = result;
                    current.N1.Parrent = current;
                    result.Depth = current.Depth + 1;
                    xSplit = true;
                    break;
                }
                else if (current.N2.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N2))
                {
                    Parent = current;
                    current = (Pair)current.N2;
                }
                else if (current.N2.GetType() == typeof(Number) && ((Number)current.N2).Value > 10)
                {
                    Pair result = Split(((Number)current.N2));
                    current.N2 = result;
                    current.N2.Parrent = current;
                    result.Depth = current.Depth + 1;
                    xSplit = true;
                    break;
                }
                else
                {
                    if (current.Parrent == null)
                    {
                        break;
                    }
                    current = current.Parrent;
                    Parent = current.Parrent;
                }


            }


            return (p, xSplit);
        }

        private Pair Split(Number n)
        {
            int i = n.Value;
            int i1 = (int)Math.Floor(i / 2.0);
            int i2 = (int)Math.Ceiling(i / 2.0);
            Number N1 = new Number(i1);
            Number N2 = new Number(i2);
            if (n.Left != null)
            {
                Number nLeft = n.Left;
                N1.Left = nLeft;
                nLeft.Right = N1;
            }
            N1.Right = N2;
            if (n.Right != null)
            {
                Number nRight = n.Right;
                N2.Right = nRight;
                nRight.Left = N2;
            }
            N2.Left = N1;

            Pair parrent = n.Parrent;
            Pair Result = new Pair() { N1 = N1, N2 = N2 };
            N1.Parrent = Result;
            N2.Parrent = Result;
            Result.Parrent = parrent;
            if (Result.Parrent == null)
            {
                var x = true;
            }

            Result.Depth = parrent.Depth + 1;

            
            if (parrent.N1.Equals(n))
                parrent.N1 = Result;
            else if (parrent.N2.Equals(n))
                parrent.N2 = Result;
            else
                i = 0;

            return Result;
        }

        private Number LoopOverAllPairs(Pair p, Action<Pair> A, bool xLastNumber)
        {
            A(p);
            //List<Pair> Analised = new();
            List<aNumber> Analised = new();
            Pair current = p;
            Pair Parent = null;

            Number nResult = null;

            while (true)
            {
                if (!Analised.Contains(current))
                    Analised.Add(current);

                if (current.N1.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N1))
                {
                    //A(current);
                    Parent = current;
                    current = (Pair)current.N1;
                    A(current);
                }
                else if (current.N1.GetType() == typeof(Number) && !xLastNumber && nResult == null && !Analised.Contains(current.N1))
                {
                    nResult = ((Number)current.N1);
                    
                    if(current.N2.GetType() == typeof(Pair))
                    {
                        current = (Pair)current.N2;
                        A(current);
                    }
                    else
                    {
                        if (current.Parrent == null)
                            break;
                        current = current.Parrent;
                        Parent = current.Parrent;
                    }
                }
                else if (current.N2.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N2))
                {
                    //A(current);
                    Parent = current;
                    current = (Pair)current.N2;
                    A(current);
                }
                else if (current.N2.GetType() == typeof(Number) && xLastNumber && !Analised.Contains(current.N2))
                {
                    nResult = ((Number)current.N2);
                    if (current.Parrent == null)
                    {
                        break;
                    }
                    current = current.Parrent;
                    Parent = current.Parrent;
                }
                else
                {
                    if (current.Parrent == null)
                    {
                        
                        break;
                    }
                    current = current.Parrent;
                    Parent = current.Parrent;
                }

            }

            return nResult;
        }


        public override void Q2()
        {
            //not clean!
            // but objects get updated, so easiest way

            //var lsInput = _lsTest5;
            var lsInput = Input;

            int iCount = lsInput.Count();

            Int64 iMaxMagnitude = 0;
            for(int j = 0; j < iCount; j++)
                for (int k = 0; k < iCount; k++)
                {
                    if (j == k) continue;

                    List<Pair> lpInput = Convert(new() { lsInput[j],lsInput[k]});
                    Pair result = Logic(lpInput);
                    Int64 iMag = result.Magnitude();
                    if (iMag > iMaxMagnitude)
                        iMaxMagnitude = iMag;
                    Console.WriteLine(result + " : " + iMag);
                }

            Console.WriteLine("Max "+ iMaxMagnitude);
        }


        public abstract class aNumber: ICloneable
        {
            public Pair Parrent { get; set; }

            public Guid ID { get; set; }
            
            public aNumber()
            {
                ID = Guid.NewGuid();
            }
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        public class Number: aNumber, ICloneable
        {
            public int Value { get; set; }
            
            public Number Left { get; set; }
            public Number Right { get; set; }

            public Number(int i)
            {
                Value = i;
            }
            public void Add(int i)
            {
                Value += i;
            }
            public override string ToString()
            {
                return Value.ToString();
            }


            public Number Clone()
            {
                return (Number)this.MemberwiseClone();
            }
        }

        public class Pair : aNumber, ICloneable
        {
            public int Depth { get; set; }

            public aNumber N1 { get; set; }
            public aNumber N2 { get; set; }

            public override string ToString()
            {
                return $"[{N1},{N2}]";
            }

            public bool AllNumbers
            {
                get
                {
                    if (N1.GetType() == typeof(Number) && N2.GetType() == typeof(Number))
                        return true;
                    return false;
                }
            }

            public Int64 Magnitude()
            {
                Int64 i1 = 0;
                Int64 i2 = 0;
                if (N1.GetType() == typeof(Number))
                    i1 = ((Number)N1).Value;
                else
                    i1 = ((Pair)N1).Magnitude();
                if (N2.GetType() == typeof(Number))
                    i2 = ((Number)N2).Value;
                else
                    i2 = ((Pair)N2).Magnitude();
                return i1 * 3 + i2 * 2;
            }

            public Pair Clone()
            {
                return (Pair)this.MemberwiseClone();
            }
        }

    }
}
