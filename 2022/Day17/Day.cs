using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day17 : Day
    {
        public override int _iDay { get { return 17; } }

        int iBoulder = 0;
        clsBoulder get(long iHighest)
        {
            clsBoulder result = null;
            switch (iBoulder)
            {
                case 0: result = new clsHorizontal(iHighest); break;
                case 1: result = new clsCross(iHighest); break;
                case 2: result = new clsL(iHighest); break;
                case 3: result = new clsVertical(iHighest); break;
                case 4: result = new clsSquare(iHighest); break;
            }
            iBoulder++;
            if (iBoulder > 4) iBoulder = 0;
            return result;
        }

        public override string Q1()
        {
            iBoulder = 0;
            string sMoving = Data[0];
            List<(long, long)> map = new();
            for (int i = 0; i < 8; i++) map.Add((i, 0)); //floor

            long iHighestPoint = 0;
            int cPos = 0;
            for (int i = 0; i < 2022; i++)
            {
                clsBoulder current = get(iHighestPoint);
                while (true)
                {
                    char c = sMoving[cPos];
                    cPos++;
                    cPos = cPos % sMoving.Length;
                    if (c.Equals('<')) current.Left(map);
                    else current.Right(map);
                    if (!current.Down(map))
                    {
                        map.AddRange(current.Rocks);
                        iHighestPoint = map.Select(x => x.Item2).Max();
                        break;
                    }
                }
                    
            }
            return $"{iHighestPoint}";
        }

        // works for 2022
        // for q2 test data is 1 lower, input data is 2 lower --> no idea why
        // code should be cleaned up
        public override string Q2()
        {
            long lBoulders = 1000000000000;
            //lBoulders = 2022;
            List<(int, int)> lDeltas = new();
            List<long> lHeight = new();

            iBoulder = 0;
            string sMoving = Data[0];
            List<(long, long)> map = new();
            for (int i = 0; i < 8; i++) map.Add((i, 0)); //floor

            long iHighestPoint = 0;
            int cPos = 0;

            while(true)
            {
                clsBoulder current = get(iHighestPoint);
                while (true)
                {
                    char c = sMoving[cPos];
                    cPos++;
                    cPos = cPos % sMoving.Length;
                    if (c.Equals('<')) current.Left(map);
                    else current.Right(map);
                    if (!current.Down(map))
                    {
                        map.AddRange(current.Rocks);
                        iHighestPoint = map.Select(x => x.Item2).Max();
                        lDeltas.Add((current.iDeltaX, current.iDeltaY));
                        lHeight.Add(iHighestPoint);
                        break;
                    }
                }

                if(lDeltas.Count >100)
                {
                    int iAmount = 20;
                    for(int i = lDeltas.Count - 2 * iAmount; i >= 0; i--)
                    {
                        bool xRepeat = true;
                        for(int j= 0; j < iAmount; j++)
                        {
                            int k = i + j;
                            int l = lDeltas.Count - iAmount + j;
                            if (lDeltas[i+j] != lDeltas[lDeltas.Count - iAmount+j])
                            {
                                xRepeat = false;
                                break;
                            }
                        }
                        if (xRepeat)
                        {
                            long k = lHeight[i+iAmount-1];
                            long l = lHeight[lDeltas.Count-1];
                            long delta = lDeltas.Count - iAmount - i;
                            long heightDifference = l-k;
                            long amountOfBoulders = lDeltas.Count;
                            while(amountOfBoulders < lBoulders - delta)
                            {
                                amountOfBoulders += delta;
                                iHighestPoint += heightDifference;
                            }
                            int z = 0;
                            while (amountOfBoulders < lBoulders)
                            {
                                amountOfBoulders++;
                                int index = i+iAmount+z;
                                z++;
                                iHighestPoint += lHeight[index + 1] - lHeight[index];
                            }
                            break;
                        }
                    }
                }

            }

            return $"{iHighestPoint}";
        }
        public string Q2_v0()
        {
            iBoulder = 0;
            string sMoving = Data[0];
            List<(long, long)> map = new();
            for (int i = 0; i < 8; i++) map.Add((i, 0)); //floor

            int iRepeat = sMoving.Length * 5; //5 boulder types;
            long iHighestPoint = 0;
            long lBoulders = 0;
            clsBoulder current = get(iHighestPoint);
            for (long i = 0; i < iRepeat; i++)
            {
                char c = sMoving[(int)(i % sMoving.Length)];
                if (c.Equals('<')) current.Left(map);
                else current.Right(map);
                if (!current.Down(map))
                {
                    map.AddRange(current.Rocks);
                    iHighestPoint = map.Select(x => x.Item2).Max();
                    current = get(iHighestPoint);
                    lBoulders++;
                }
            }
            //from now on, this structure will repeat --> see as 1 type of boulder
            List<(long, long)> copy = new(map);
            int cPos = 0;
            long lTotalBoulders = lBoulders;
            while (lTotalBoulders < 1000000000000)
            {
                current.Rocks = new();
                foreach (var c in copy)
                    current.Rocks.Add((c.Item1, c.Item2 + iHighestPoint + 4));
                while (true)
                {
                    char c = sMoving[cPos];
                    cPos++;
                    cPos = cPos % sMoving.Length;
                    if (!current.Down(map))
                    {
                        map.AddRange(current.Rocks);
                        iHighestPoint = map.Select(x => x.Item2).Max();
                        break;
                    }
                }
                lTotalBoulders += lBoulders;
            }
            return $"{iHighestPoint}";
        }

        class clsBoulder
        {
            public List<(long, long)> Rocks {  get; set; }
            public long mostLeft { get; set; }
            public long mostRight { get; set; }
            public long mostDown { get; set; }

            public int iStartX { get; set; }
            public int iDeltaX { get; set; }
            public int iDeltaY { get; set; }

            public clsBoulder(long iHighestPoint)
            {
                Rocks = new();
                mostDown = iHighestPoint + 4;
            }
            //public abstract void Left();
            //public abstract void Right();
            //public abstract bool Down(List<(int,int)> map); 
            public void Left(List<(long, long)> map)
            {
                if (mostLeft == 1) return;
                List<(long, long)> newLoc = new();
                foreach (var r in Rocks)
                {
                    if(map.Contains((r.Item1 - 1, r.Item2)))
                    {
                        //can't move;
                        return;
                    }
                    newLoc.Add((r.Item1 - 1, r.Item2));
                    //if (r.Item1 - 1 < mostLeft) mostLeft = r.Item1 - 1;
                }
                Rocks = newLoc;
                iDeltaX--;
                mostLeft = Rocks.Select(x => x.Item1).Min();
                mostRight = Rocks.Select(x => x.Item1).Max();

            }

            public void Right(List<(long, long)> map)
            {
                if (mostRight == 7) return;
                List<(long, long)> newLoc = new();
                foreach (var r in Rocks)
                {
                    newLoc.Add((r.Item1 + 1, r.Item2));
                    //if (r.Item1 + 1 > mostRight) mostRight = r.Item1 + 1;
                    if(map.Contains((r.Item1 + 1, r.Item2)))
                    {
                        //can't move;
                        return;
                    }
                }
                Rocks = newLoc;
                iDeltaX++;
                mostLeft = Rocks.Select(x => x.Item1).Min();
                mostRight = Rocks.Select(x => x.Item1).Max();
            }

            public bool Down(List<(long, long)> map)
            {
                foreach (var r in Rocks)
                {
                    if (map.Contains((r.Item1, r.Item2 - 1)))
                    {
                        return false;
                    }
                }
                List<(long, long)> newLoc = new();
                foreach (var r in Rocks) newLoc.Add((r.Item1, r.Item2 - 1));
                Rocks = newLoc;
                iDeltaY++;
                return true;
            }
        }
        class clsHorizontal:clsBoulder
        {
            public clsHorizontal(long iHighestPoint):base(iHighestPoint)
            {
                mostLeft = 3;
                Rocks.Add((3, mostDown));
                Rocks.Add((4, mostDown));
                Rocks.Add((5, mostDown));
                Rocks.Add((6, mostDown));
                mostRight = 6;
            }
        }
        /*
        .#.
        ###
        .#.
        */
        class clsCross : clsBoulder
        {
            public clsCross(long iHighestPoint) : base(iHighestPoint)
            {
                mostLeft = 3;
                Rocks.Add((4, mostDown));
                Rocks.Add((3, mostDown+1));
                Rocks.Add((4, mostDown+1));
                Rocks.Add((5, mostDown+1));
                Rocks.Add((4, mostDown+2));
                mostRight = 5;
            }

        }
        class clsL : clsBoulder
        {
            public clsL(long iHighestPoint) : base(iHighestPoint)
            {
                mostLeft = 3;
                Rocks.Add((3, mostDown));
                Rocks.Add((4, mostDown));
                Rocks.Add((5, mostDown));
                Rocks.Add((5, mostDown + 1));
                Rocks.Add((5, mostDown + 2));
                mostRight = 5;
            }

        }
        class clsVertical : clsBoulder
        {
            public clsVertical(long iHighestPoint) : base(iHighestPoint)
            {
                mostLeft = 3;
                Rocks.Add((3, mostDown));
                Rocks.Add((3, mostDown+1));
                Rocks.Add((3, mostDown+2));
                Rocks.Add((3, mostDown+3));
                mostRight = 3;
            }
        }
        class clsSquare : clsBoulder
        {
            public clsSquare(long iHighestPoint) : base(iHighestPoint)
            {
                mostLeft = 3;
                Rocks.Add((3, mostDown));
                Rocks.Add((3, mostDown + 1));
                Rocks.Add((4, mostDown));
                Rocks.Add((4, mostDown + 1));
                mostRight = 4;
            }
        }

    }
}
