using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day9 : Day
    {
        public override int _iDay { get { return 9; } }

        internal override List<string> _lsTest => new List<string>() {
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678"
        };
      
        public override void Q1()
        {
            var lsInput = Input;
            //var lsInput = Test;

            List<List<int>> lliInput = new();
            foreach(var s in lsInput)
            {
                List<int> li = new();
                foreach (var c in s)
                    li.Add(int.Parse(c.ToString()));  
                lliInput.Add(li);
            }


            int iRisk = 0;
            for (int r = 0; r < lliInput.Count(); r++) 
                for (int c = 0; c < lliInput[r].Count(); c++)
                {
                    int iMe = lliInput[r][c];
                    (int iUp, int iRight, int iDown, int iLeft) = Surrounding(lliInput, r, c);

                    if (iMe < iUp &&
                        iMe < iDown &&
                        iMe < iLeft &&
                        iMe < iRight)
                        iRisk += (1 + iMe);
                }

            Console.WriteLine("Risk: " + iRisk);
        }

        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;

            List<List<int>> lliInput = new();
            foreach (var s in lsInput)
            {
                List<int> li = new();
                foreach (var c in s)
                    li.Add(int.Parse(c.ToString()));
                lliInput.Add(li);
            }

            List<int> liBassins = new();
            for (int r = 0; r < lliInput.Count(); r++)
                for (int c = 0; c < lliInput[r].Count(); c++)
                {
                    int iMe = lliInput[r][c];
                    int iUp = 10;
                    int iDown = 10;
                    int iLeft = 10;
                    int iRight = 10;

                    if (c != 0)
                        iLeft = lliInput[r][c - 1];
                    if (c != lliInput[r].Count() - 1)
                        iRight = lliInput[r][c + 1];

                    if (r != 0)
                        iUp = lliInput[r - 1][c];
                    if (r != lliInput.Count() - 1)
                        iDown = lliInput[r + 1][c];

                    if (iMe < iUp &&
                        iMe < iDown &&
                        iMe < iLeft &&
                        iMe < iRight)
                        liBassins.Add(Bassin(lliInput, r, c));
                }

            int iMax = liBassins.Max(); liBassins.Remove(iMax);
            int iMax2 = liBassins.Max(); liBassins.Remove(iMax2);
            int iMax3 = liBassins.Max(); liBassins.Remove(iMax3);
            Console.WriteLine("1: " + iMax);
            Console.WriteLine("2: " + iMax2);
            Console.WriteLine("3: " + iMax3);
            Console.WriteLine("Result: " + iMax* iMax2*iMax3);
        }
        private int Bassin(List<List<int>> lliInput, int r, int c)
        {
            List<(int, int)> PointsOverview = new();
            PointsOverview.Add((r, c));
            List<(int, int)> ToBeEvaluated = new();

            int iMe = lliInput[r][c];
            (int iUp, int iRight, int iDown, int iLeft) = Surrounding(lliInput, r, c);

            if (iUp < 9) ToBeEvaluated.Add((r - 1, c));
            if (iRight < 9) ToBeEvaluated.Add((r , c+1));
            if (iDown < 9) ToBeEvaluated.Add((r + 1, c));
            if (iLeft < 9) ToBeEvaluated.Add((r, c-1));

            while (ToBeEvaluated.Count > 0)
            {
                var loc = ToBeEvaluated[0];
                r = loc.Item1;
                c = loc.Item2;
                ToBeEvaluated.RemoveAt(0);

                if (PointsOverview.Contains((r, c))) continue;

                (iUp, iRight, iDown, iLeft) = Surrounding(lliInput, r, c);

                if (iUp < 9 && !PointsOverview.Contains((r-1,c))) ToBeEvaluated.Add((r - 1, c));
                if (iRight < 9 && !PointsOverview.Contains((r , c+1))) ToBeEvaluated.Add((r, c + 1));
                if (iDown < 9 && !PointsOverview.Contains((r + 1, c))) ToBeEvaluated.Add((r + 1, c));
                if (iLeft < 9 && !PointsOverview.Contains((r, c-1))) ToBeEvaluated.Add((r, c - 1));
                if (iUp < 9 || iRight < 9|| iDown < 9||iLeft < 9)
                    PointsOverview.Add((r, c));
            }
          
            return PointsOverview.Count();
        }
        private (int,int,int,int) Surrounding(List<List<int>> lliInput, int r, int c)
        {
            int iMe = lliInput[r][c];
            int iUp = 10;
            int iDown = 10;
            int iLeft = 10;
            int iRight = 10;

            if (c != 0)
                iLeft = lliInput[r][c - 1];
            if (c != lliInput[r].Count() - 1)
                iRight = lliInput[r][c + 1];

            if (r != 0)
                iUp = lliInput[r - 1][c];
            if (r != lliInput.Count() - 1)
                iDown = lliInput[r + 1][c];

            return (iUp, iRight, iDown, iLeft);
        }
        
    }

    public class clsLocation
    {
        public int Me { get; private set; }
        public int Up { get; private set; }
        public int Right { get; private set; }
        public int Down { get; private set; }
        public int Left { get; private set; }
        public clsLocation(int iMe, int iUp, int iRight, int iDown, int iLeft)
        {
            Me = iMe;
            Up = iUp;
            Right = iRight;
            Down = iDown;
            Left = iLeft;
        }
    }

}
