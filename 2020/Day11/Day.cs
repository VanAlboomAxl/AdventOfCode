using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day11 : Day
    {
        public override int _iDay { get { return 11; } }

        private Func<List<string>, int, int, List<char>> SurroundingsFunction;
        int iAdjacentToMove;
        
        public override void Q1()
        {
            iAdjacentToMove = 4;
            SurroundingsFunction = Surroundings;
            var lsInput = Input;
            //lsInput = Test;

            var lsMap = Logic(lsInput);

            int iSeats = 0;
            foreach (var row in lsMap)
                foreach (var c in row)
                    if (c.Equals('#'))
                        iSeats++;

            Console.WriteLine(iSeats);
        }

        public override void Q2()
        {
            iAdjacentToMove = 5;
            SurroundingsFunction = Surroundings_2;

            var lsInput = Input;
            //lsInput = Test;

            var lsMap = Logic(lsInput);

            int iSeats = 0;
            foreach (var row in lsMap)
                foreach (var c in row)
                    if (c.Equals('#'))
                        iSeats++;

            Console.WriteLine(iSeats);

        }

        private List<string> Logic(List<string> lsMap)
        {
            int iChanges = 0;
            List<string> ResultingMap=null;
            do
            {
                ResultingMap = new();
                iChanges = 0;

                for (int row = 0; row < lsMap.Count; row++)
                {
                    string sResultRow = "";
                    for (int col = 0; col < lsMap[row].Length; col++)
                    {
                        char cResult = Change(lsMap, row, col);
                        if (!cResult.Equals(lsMap[row][col]))
                            iChanges++;
                        sResultRow += cResult;
                    }
                    ResultingMap.Add(sResultRow);
                }
                lsMap = ResultingMap;
            }
            while (iChanges > 0);
            return ResultingMap;
        }

        private char Change(List<string> lsMap, int row, int col)
        {
            if (lsMap[row][col].Equals('.')) return '.';
            else if (lsMap[row][col].Equals('L'))
            {
                List<char> lcSurroundings = SurroundingsFunction(lsMap,row,col);
                if (lcSurroundings.Contains('#')) return 'L';
                else return '#';
            }
            else
            {
                List<char> lcSurroundings = SurroundingsFunction(lsMap, row, col);
                if (lcSurroundings.Where(x => x.Equals('#')).Count()>= iAdjacentToMove) return 'L';
                else return '#';
            }

        }

        private List<char> Surroundings(List<string> lsMap, int row, int col)
        {
            List<char> results = new();
            if (col > 0)
            {
                if (row > 0)
                    results.Add(lsMap[row-1][col-1]);
                results.Add(lsMap[row][col-1]);
                if (row < lsMap.Count() - 1)
                    results.Add(lsMap[row+1][col - 1]);
            }

            if (row > 0)
                results.Add(lsMap[row - 1][col]);
            if (row < lsMap.Count() - 1)
                results.Add(lsMap[row + 1][col]);

            if (col < lsMap[row].Count() - 1)
            {
                if (row > 0)
                    results.Add(lsMap[row - 1][col + 1]);
                results.Add(lsMap[row][col + 1]);
                if (row < lsMap.Count() - 1)
                    results.Add(lsMap[row + 1][col + 1]);
            }

            return results;
        }   
        
        private List<char> Surroundings_2(List<string> lsMap, int row, int col)
        {
            List<char> results = new();
            var c = GetSeat(lsMap, row, col, 0, 1); if (!c.Equals('.')) results.Add(c);
            c = GetSeat(lsMap, row, col,  0, -1);   if (!c.Equals('.')) results.Add(c);
            c = GetSeat(lsMap, row, col,  1,  0);   if (!c.Equals('.')) results.Add(c);
            c = GetSeat(lsMap, row, col, -1,  0);   if (!c.Equals('.')) results.Add(c);
            c = GetSeat(lsMap, row, col,  1,  1);   if (!c.Equals('.')) results.Add(c);
            c = GetSeat(lsMap, row, col,  1, -1);   if (!c.Equals('.')) results.Add(c);
            c = GetSeat(lsMap, row, col, -1,  1);   if (!c.Equals('.')) results.Add(c);
            c = GetSeat(lsMap, row, col, -1, -1);   if (!c.Equals('.')) results.Add(c);
            return results;
        }

        private char GetSeat(List<string> lsMap, int row, int col, int dr, int dc)
        {
            char cResult = '.';
            int dR = dr, dC = dc;
            do
            {
                if (row + dR < 0 || col + dC < 0) break;
                if (row + dR >= lsMap.Count() || col + dC >= lsMap[row].Count()) break;

                cResult = lsMap[row + dR][col + dC];

                dR += dr;
                dC += dc;
            }
            while (cResult.Equals('.'));
            return cResult;
        }

    }
}
