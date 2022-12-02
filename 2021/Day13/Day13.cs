using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day13 : Day
    {
        public override int _iDay { get { return 13; } }

        internal override List<string> _lsTest =>  new List<string>() {
            "6,10",
            "0,14",
            "9,10",
            "0,3",
            "10,4",
            "4,11",
            "6,0",
            "6,12",
            "4,1",
            "0,13",
            "10,12",
            "3,4",
            "3,0",
            "8,4",
            "1,10",
            "2,14",
            "8,10",
            "9,0",
            "",
            "fold along y=7",
            "fold along x=5"
        };

        
        public override void Q1()
        {
            var lsInput = Input;
            //var lsInput = Test;

            int iMaxX = -1,iMaxY = -1;
            List<(int,int)> lsMapping = new();
            List<string> Folding = new();
            bool xFolding = false;

            foreach(var sInput in lsInput)
            {
                if (sInput.Equals("")) xFolding = true;
                else if (xFolding) Folding.Add(sInput);
                else
                {
                    var asSplit = sInput.Trim().Split(',');
                    int x = int.Parse(asSplit[0]);
                    int y = int.Parse(asSplit[1]);
                    lsMapping.Add((x,y));
                    if (x > iMaxX) iMaxX = x;
                    if (y > iMaxY) iMaxY = y;
                }
            }

            List<List<int>> lliMap = new();
            for (int i = 0; i <= iMaxY; i++)
            {
                List<int> row = new();
                for(int j = 0; j <= iMaxX; j++)
                    row.Add(0);
                lliMap.Add(row);
            }

            foreach((int x, int y) in lsMapping)
                lliMap[y][x] = 1;

            //WriteMap(lliMap);

            string sFold = Folding[0];
            Fold(lliMap, sFold);
            //Fold(lliMap, Folding[1]);

            int iCount = 0;
            foreach (var liMap in lliMap)
            {
                foreach (var i in liMap)
                {
                    //if (i.Equals(0))
                    if (i>0)
                    {
                        //Console.Write("#");
                        iCount++;
                    }
                    //else
                        //Console.Write(".");
                }
                //Console.WriteLine("");
            }
            Console.WriteLine("Amount of dots: "+ iCount);

        }

        public override void Q2()
        {
            var lsInput = Input;
            //var lsInput = Test;

            int iMaxX = -1, iMaxY = -1;
            List<(int, int)> lsMapping = new();
            List<string> Folding = new();
            bool xFolding = false;

            foreach (var sInput in lsInput)
            {
                if (sInput.Equals("")) xFolding = true;
                else if (xFolding) Folding.Add(sInput);
                else
                {
                    var asSplit = sInput.Trim().Split(',');
                    int x = int.Parse(asSplit[0]);
                    int y = int.Parse(asSplit[1]);
                    lsMapping.Add((x, y));
                    if (x > iMaxX) iMaxX = x;
                    if (y > iMaxY) iMaxY = y;
                }
            }

            List<List<int>> lliMap = new();
            for (int i = 0; i <= iMaxY; i++)
            {
                List<int> row = new();
                for (int j = 0; j <= iMaxX; j++)
                    row.Add(0);
                lliMap.Add(row);
            }

            foreach ((int x, int y) in lsMapping)
                lliMap[y][x] = 1;

            //WriteMap(lliMap);

            foreach(var sFold in Folding)
                Fold(lliMap, sFold);
            //Fold(lliMap, Folding[1]);

            int iCount = 0;
            foreach (var liMap in lliMap)
            {
                foreach (var i in liMap)
                {
                    //if (i.Equals(0))
                    if (i > 0)
                    {
                        Console.Write("#");
                        iCount++;
                    }
                    else
                    Console.Write(".");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Amount of dots: " + iCount);
            //map reads LGHEGUEJ
        }


        private void WriteMap(List<List<int>> lliMap) 
        {
            Console.WriteLine("---------------");
            foreach (var liMap in lliMap)
            {
                foreach (var i in liMap)
                {
                    if (i > 0) Console.Write("#");              
                    else Console.Write(".");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("---------------");
        }

        private void Fold(List<List<int>> lliMap, string sCMD)
        {
            var asFold = sCMD.Split(" ")[2].Split("="); 
            int iXY = int.Parse(asFold[1]);
            if (asFold[0].Equals("x")) Fold_x(lliMap, iXY);
            else Fold_y(lliMap, iXY);
        }
        private void Fold_x(List<List<int>> lliMap, int x)
        {
            for(int y=0;y<lliMap.Count;y++)
            {
                for (int i = 0; i < x; i++)
                {
                    lliMap[y][i] = lliMap[y][i] + lliMap[y][lliMap[y].Count()-1 - i];
                }
                int iMax = lliMap[y].Count;
                for (int i = x; i < iMax; i++)
                {
                    lliMap[y].RemoveAt(x);
                }
            }                    
        }
        private void Fold_y(List<List<int>> lliMap, int y)
        {
            for (int i = 0; i < y; i++)
                for (int x = 0; x < lliMap[i].Count; x++)
                    lliMap[i][x] = lliMap[i][x] + lliMap[lliMap.Count() - 1 - i][x];
                   
                
            int iMax = lliMap.Count();
            for (int i = y; i < iMax; i++)
            {
                lliMap.RemoveAt(y);
            }
        }



    }

}
