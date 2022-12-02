using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day5 : Day
    {
        public override int _iDay { get { return 5; } }

        internal override List<string> _lsTest => new List<string>() {
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2"
        };

        private List<List<int>> Map;

        private void MapCreator()
        {
            Map = new();
            for(int i=0;i<1000;i++)
            {
                List<int> Column = new();
                for (int j = 0; j < 1000; j++)
                    Column.Add(0);
                Map.Add(Column);
            }

        }

        public override void Q1()
        {
            MapCreator();
            var lsInput = Input;
            //var lsInput = Test;
            foreach(var sInput in lsInput)
                foreach(var (x,y) in MovePath(sInput))
                    Map[x][y] = Map[x][y]+1;
                
            int iResult = 0;
            foreach (var c in Map)
                foreach (var r in c)
                    if (r > 1)
                        iResult++;
            Console.WriteLine(iResult);

        }

        private List<(int,int)> MovePath(string sPath, bool xUseDiagonals = false)
        {
            var split = sPath.Split("->");

            var Pre = split[0].Trim().Split(",");
            var x1 = int.Parse(Pre[0]);
            var y1 = int.Parse(Pre[1]);

            var Post = split[1].Trim().Split(",");
            var x2 = int.Parse(Post[0]);
            var y2 = int.Parse(Post[1]);

            List<(int, int)> Result = new();

            if (x1 == x2)
            {
                int low = y1, high = y2;
                if (y1 > y2)
                {
                    low = y2; high = y1;
                }

                for (int i = low; i <= high; i++)
                    Result.Add((x1, i));

            }
            else if (y1 == y2)
            {
                int low = x1, high = x2;
                if (x1 > x2)
                {
                    low = x2; high = x1;
                }

                for (int i = low; i <= high; i++)
                    Result.Add((i, y1));
            }
            else if (xUseDiagonals)
            {
                int dx = 1, dy = 1;
                if (x1 > x2) dx = -1;
                if (y1 > y2) dy = -1;

                int x = x1;
                int y = y1;

                while(x !=x2 && y != y2)
                {
                    Result.Add((x, y));
                    x += dx;
                    y += dy;
                }
                Result.Add((x2, y2));
            }

            return Result;
        }

        public void Q1_v0()
        {
            //var lsInput = Input;
            var lsInput = Test;
            foreach (var sInput in lsInput)
            {
                var split = sInput.Split("->");

                var Pre = split[0].Trim().Split(",");
                var x1 = int.Parse(Pre[0]);
                var y1 = int.Parse(Pre[1]);

                var Post = split[1].Trim().Split(",");
                var x2 = int.Parse(Post[0]);
                var y2 = int.Parse(Post[1]);


                if (x1 == x2)
                {
                    int low = y1, high = y2;
                    if (y1 > y2)
                    {
                        low = y2; high = y1;
                    }

                    for (int i = low; i <= high; i++)
                    {
                        int iCount = Map[x1][i];
                        iCount++;
                        Map[x1][i] = iCount;
                    }

                }
                else if (y1 == y2)
                {
                    int low = x1, high = x2;
                    if (x1 > x2)
                    {
                        low = x2; high = x1;
                    }

                    for (int i = low; i <= high; i++)
                    {
                        int iCount = Map[i][y1];
                        iCount++;
                        Map[i][y1] = iCount;
                    }
                }

            }

            int iResult = 0;
            foreach (var c in Map)
                foreach (var r in c)
                    if (r > 1)
                        iResult++;
            Console.WriteLine(iResult);

        }

        public override void Q2()
        {
            MapCreator();
            var lsInput = Input;
            //var lsInput = Test;
            foreach (var sInput in lsInput)
                foreach (var (x, y) in MovePath(sInput,true))
                    Map[x][y] = Map[x][y] + 1;

            int iResult = 0;
            foreach (var c in Map)
                foreach (var r in c)
                    if (r > 1)
                        iResult++;

            Console.WriteLine(iResult);

        }
   
    }
}
