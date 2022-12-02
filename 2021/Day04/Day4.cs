using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public class Day4 : Day
    {
        public override int _iDay { get { return 4; } }

        internal override List<string> _lsTest => new List<string>() {
            "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
            "",
            "22 13 17 11  0",
            "8  2 23  4 24",
            "21  9 14 16  7",
            "6 10  3 18  5",
            "1 12 20 15 19",
            "",
            "3 15  0  2 22",
            "9 18 13 17  5",
            "19  8  7 25 23",
            "20 11 10 24  4",
            "14 21 16 12  6",
            "",
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            "2  0 12  3  7"
        };

        public override void Q1()
        {

            (var bingoNumbers, List<BingoBoard> loBoards) = Boards();
                 
            int iWinningNumber = -1;
            foreach(var bN in bingoNumbers)
            {
                int iNumber = int.Parse(bN);
                foreach(var board in loBoards)
                {
                    if (board.NumberDrawn(iNumber))
                    {
                        iWinningNumber = iNumber * board.Score();
                        break;
                    }
                }
                if (iWinningNumber != -1)
                    break;
            }

            if (iWinningNumber != -1)
            {
                Console.WriteLine(iWinningNumber);
            }

        }

        public override void Q2()
        {
            (var bingoNumbers, List<BingoBoard> loBoards) = Boards();

            int iWinningNumber = -1;
            foreach (var bN in bingoNumbers)
            {
                int iNumber = int.Parse(bN);
                int i = 0;
                while (i < loBoards.Count())
                {
                    var board = loBoards[i];
                    if (board.NumberDrawn(iNumber))
                    {
                        if (loBoards.Count() == 1)
                            iWinningNumber = iNumber * board.Score();
                        
                        loBoards.RemoveAt(i);
                    }
                    else
                        i++;              
                }
                if (iWinningNumber != -1)
                    break;
            }

            if (iWinningNumber != -1)
            {
                Console.WriteLine(iWinningNumber);
            }
        }

        private (string [], List<BingoBoard>) Boards()
        {
            var lsInput = Input;
            //var lsInput = Test;

            var bingoNumbers = lsInput[0].Split(',');

            List<BingoBoard> Boards = new();

            for (int i = 2; i < lsInput.Count(); i += 6)
            {
                List<string> Board = new();
                for (int j = 0; j < 5; j++)
                    Board.Add(lsInput[i + j]);
                Boards.Add(new(Board));
            }

            return (bingoNumbers, Boards);
            
        }

    }

    public class BingoBoard
    {
        public bool Won { get; private set; }
        private List<List<BingoNumber>> Board;

        public BingoBoard(List<string> lsBoard)
        {
            Board = new();
            for (int i=0; i<lsBoard.Count();i++)
            {
                var split = lsBoard[i].Split(" ").Where(x=>!x.Equals("") && ! x.Equals(null)).ToList();
                if (split.Count() == 5)
                {
                    var Row = new List<BingoNumber>();
                    foreach(var s in split)
                        Row.Add(new(int.Parse(s)));

                    Board.Add(Row);
                }
            }
        }

        public bool NumberDrawn(int iNumber)
        {

            for(int i=0; i<5;i++)
            {
                var xFound = false;
                for (int j = 0; j < 5; j++)
                {
                    if (Board[i][j].Check(iNumber))
                    {
                        xFound = true;
                        break;
                    }
                }
                if (xFound)
                {
                    return Bingo();
                }
            }
                
            return false;

        }

        public bool Bingo()
        {
            for (int i = 0; i < 5; i++)
            {
                int iCount = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (Board[i][j].Marked)
                    {
                        iCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (iCount == 5)
                {
                    Won = true;
                    return true;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                int iCount = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (Board[j][i].Marked)
                    {
                        iCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (iCount == 5)
                {
                    Won = true;
                    return true;
                }
            }

            return false;
        }
    
        public int Score()
        {
            int iScore = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (!Board[i][j].Marked)                   
                        iScore += Board[i][j].Number;        

            return iScore;
        }
        
    
    }
    public class BingoNumber
    {
        public int Number { get; private set; }
        public bool Marked { get; private set; }
        public BingoNumber(int iNumber)
        {
            Number = iNumber;
        }
        public bool Check(int iNumberToCheck)
        {
            if (Number == iNumberToCheck)
            {
                Marked = true;
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }

}
