//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdventOfCode_2021
//{

//    public class Day18 : Day
//    {
//        public override int _iDay { get { return 18; } }

//        internal override List<string> _lsTest => new List<string> {
//            "[[[[[9,8],1],2],3],4]", // test to explode
//            "[7,[6,[5,[4,[3,2]]]]]", // test to explode
//            "[[6,[5,[4,[3,2]]]],1]", // test to explode
//            "[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", // test to explode
//            "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", // test to explode
//            "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", // test to split
//            "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", // test to reduce
//            "[1,2]",
//            "[[1,2],3]",
//            "[9,[8,7]]",
//            "[[1,9],[8,5]]",
//            "[[[[1,2],[3,4]],[[5,6],[7,8]]],9]",
//            "[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]",
//            "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]"
//        };

//        internal List<string> _lsTest2 = new()
//        {
//            "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
//            "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
//            "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
//            "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
//            "[7,[5,[[3,8],[1,4]]]]",
//            "[[2,[2,2]],[8,[8,1]]]",
//            "[2,9]",
//            "[1,[[[9,3],9],[[9,0],[0,7]]]]",
//            "[[[5,[7,4]],7],1]",
//            "[[[[4,2],2],6],[8,7]]"
//        };

//        public List<Pair> Convert(List<string> lsInput)
//        {
//            Pair pParrent = null;
//            List<Pair> lpInput = new();
//            foreach (string s in lsInput)
//            {
//                foreach (char c in s)
//                {
//                    if (c.Equals('['))
//                    {
//                        if (pParrent == null)
//                            pParrent = new();
//                        else
//                        {
//                            Pair pNew = new();
//                            if (pParrent.N1 == null)
//                                pParrent.N1 = pNew;
//                            else
//                                pParrent.N2 = pNew;
//                            pNew.Parrent = pParrent;
//                            pParrent = pNew;
//                        }
//                    }
//                    else if (c.Equals(']'))
//                    {
//                        if (pParrent.Parrent != null)
//                            pParrent = pParrent.Parrent;
//                    }
//                    else if (!c.Equals(','))
//                    {
//                        // getal!
//                        int i = int.Parse(c.ToString());
//                        if (pParrent.N1 == null)
//                            pParrent.N1 = new Number(i);
//                        else
//                            pParrent.N2 = new Number(i);
//                    }
//                }

//                lpInput.Add(pParrent);
//                pParrent = null;
//            }
//            return lpInput;
//        }

//        public override void Q1()
//        {
//            var lsInput = Input;
//            lsInput = Test;

//            List<Pair> lpInput = Convert(_lsTest2);

//            //Console.WriteLine(Explode(lpInput[0]));
//            //Console.WriteLine(Explode(lpInput[1]));
//            //Console.WriteLine(Explode(lpInput[2]));
//            //Console.WriteLine(Explode(lpInput[3]));
//            //Console.WriteLine(Explode(lpInput[4]));

//            /*
//            (var p, var x) = Explode(lpInput[5]);
//            Console.WriteLine(p);
//            ( p,  x) = Split(p);
//            Console.WriteLine(p);
//            ( p,  x) = Split(p);
//            Console.WriteLine(p);*/
//            //Reduce(lpInput[6]);


//            Logic(lpInput);

//        }

//        private Pair Logic(List<Pair> lps)
//        {
//            Pair current = lps[0];
//            for(int i = 1; i < lps.Count; i++)
//            {
//                Console.WriteLine("-----------------------------");
//                current = Reduce(Addition(current, lps[i]));
//                Console.WriteLine("-----------------------------");
//            }
//            return current;
//        }

//        private Pair Addition(Pair p1, Pair p2)
//        {
//            return new Pair()
//            {
//                N1 = p1,
//                N2 = p2
//            };
//        }

//        private Pair Reduce(Pair p)
//        {
//            bool xAction = true;
//            while (xAction)
//            {
//                (p, xAction) = Split(p);
//                if (!xAction)
//                    (p, xAction) = Explode(p);
//                if (xAction)
//                    Console.WriteLine(p);
//            }
//            return p;
//        }

//        private (Pair,bool) Explode(Pair p)
//        {
//            bool xExplode = false;

//            int iDepth = 0;
//            Pair current = p;
//            Pair Parent = null;

//            List<Pair> Analised = new();

//            while (true)
//            {
//                Analised.Add(current);
//                if (current.N1.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N1))
//                {
//                    iDepth++;
//                    Parent = current;
//                    current = (Pair)current.N1;
//                }
//                else if (current.N2.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N2))
//                {
//                    iDepth++;
//                    Parent = current;
//                    current = (Pair)current.N2;
//                }
//                else
//                {
//                    iDepth--;
//                    if (current.Parrent == null)
//                    {
//                        if (current.Equals(current.Parrent.N1) && current.Parrent.N2.GetType() == typeof(Pair))
//                            current = (Pair)current.Parrent.N2;
//                        else
//                            break;
//                    }
//                    else
//                    {
//                        current = current.Parrent;
//                        Parent = current.Parrent;
//                    }
//                }
//                if (iDepth == 4) // explode
//                {

//                    if (current.Equals(Parent.N1))
//                    {
//                        Parent.N1 = new Number(0);

//                        if (Parent.N2.GetType() == typeof(Pair))
//                        {

//                        }


//                        Parent.N2 = new Number(((Number)Parent.N2).Value + ((Number)current.N2).Value);
//                        //pNew.Parrent = Parent.Parrent;
//                        //Parent = pNew;

//                        Pair currentParent = Parent.Parrent;
//                        while (currentParent != null)
//                        {
//                            if (currentParent.N1.GetType() == typeof(Number))
//                            {
//                                ((Number)currentParent.N1).Value = ((Number)currentParent.N1).Value + ((Number)current.N1).Value;
//                                break;
//                            }
//                            else
//                            {
//                                currentParent = currentParent.Parrent;
//                            }
//                        }

//                    }
//                    else
//                    {
//                        Parent.N1 = new Number(((Number)Parent.N1).Value + ((Number)current.N1).Value);
//                        Parent.N2 = new Number(0);

//                        Pair currentParent = Parent.Parrent;
//                        while (currentParent != null)
//                        {
//                            if (currentParent.N2.GetType() == typeof(Number))
//                            {
//                                ((Number)currentParent.N2).Value = ((Number)currentParent.N2).Value + ((Number)current.N2).Value;
//                                break;
//                            }
//                            else
//                            {

//                                // its a pair
//                                Pair sub = (Pair)currentParent.N2;
//                                while (sub !=null)
//                                {
//                                    if (sub.N1.GetType() == typeof(Number))
//                                    {
//                                        ((Number)sub.N1).Value = ((Number)sub.N1).Value + ((Number)current.N2).Value;
//                                        break;
//                                    }
//                                    else
//                                    {
//                                        sub = (Pair)sub.N2;
//                                    }
//                                }
//                                if (sub != null)
//                                    break;

//                                if (currentParent.Parrent == null)
//                                {
//                                    if (((Pair)currentParent.N2).N1.GetType()== typeof(Number) && !Analised.Contains((Pair)currentParent.N2))
//                                        ((Number)((Pair)currentParent.N2).N1).Value = ((Number)((Pair)currentParent.N2).N1).Value + ((Number)current.N2).Value;

//                                }

//                                currentParent = currentParent.Parrent;
//                            }
//                        }
//                    }


//                    xExplode = true;
//                    break;
//                }
//            }


//            return (p, xExplode);
//        }

//        private Number Exploding(Pair p)
//        {

//            int iN1 = ((Number)p.N1).Value;
//            int iN2 = ((Number)p.N2).Value;

//            Pair parrent = p.Parrent;

//            //find first NUmber to the left --> + iN1
//            while(parrent != null)
//            {
//                if (parrent.N1.GetType() == typeof(Number))
//                {
//                    ((Number)parrent.N1).Value = ((Number)parrent.N1).Value + iN1;
//                    break;
//                }
//                else
//                {

//                }
//            }

//            return new Number(0);

//        }

//        private (Pair,bool) Split(Pair p)
//        {
//            var xSplit = false;

//            List<Pair> Analised = new();
//            Pair current = p;
//            Pair Parent = null;

//            while (true)
//            {
//                Analised.Add(current);

//                if (current.N1.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N1))
//                {
//                    Parent = current;
//                    current = (Pair)current.N1;
//                }
//                else if (current.N1.GetType() == typeof(Number) && ((Number)current.N1).Value > 10)
//                {
//                    current.N1 = Split(((Number)current.N1).Value);
//                    current.N1.Parrent = current;
//                    xSplit = true;
//                    break;
//                }
//                else if (current.N2.GetType() == typeof(Pair) && !Analised.Contains((Pair)current.N2))
//                {
//                    Parent = current;
//                    current = (Pair)current.N2;
//                }
//                else if (current.N2.GetType() == typeof(Number) && ((Number)current.N2).Value > 10)
//                {
//                    current.N2 = Split(((Number)current.N2).Value);
//                    current.N2.Parrent = current;
//                    xSplit = true;
//                    break;
//                }
//                else
//                {
//                    if (current.Parrent == null)
//                    {
//                        break;
//                    }
//                    current = current.Parrent;
//                    Parent = current.Parrent;
//                }


//            }


//            return (p, xSplit);
//        } 

//        private Pair Split(int i)
//        {

//            int i1 = (int)Math.Floor(i / 2.0);
//            int i2 = (int)Math.Ceiling(i / 2.0);

//            return new Pair() { N1 = new Number(i1), N2 = new Number(i2) };
//        }

//        public override void Q2()
//        {
//            var lsInput = Input;
//            lsInput = Test;
//        }
     
        
//        public abstract class aNumber
//        {
//            public Pair Parrent { get; set; }
//        }

//        public class Number: aNumber
//        {
//            public int Value { get; set; }
//            public Number(int i)
//            {
//                Value = i;
//            }
//            public void Add(int i)
//            {
//                Value += i;
//            }
//            public override string ToString()
//            {
//                return Value.ToString();
//            }
//        }

//        public class Pair : aNumber
//        {
//            public aNumber N1 { get; set; }
//            public aNumber N2 { get; set; }
//            public Pair()
//            {

//            }
//            public override string ToString()
//            {
//                return $"[{N1},{N2}]";
//            }
//        }

//    }
//}
