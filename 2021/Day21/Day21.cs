using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public class Day21 : Day
    {
        public override int _iDay { get { return 21; } }

        internal override List<string> _lsTest => new List<string> {
            "Player 1 starting position: 4",
            "Player 2 starting position: 8"
        };

        public override void Q1()
        {
            ////input
            //int iP1_Pos = 10;
            //int iP2_Pos = 8;

            ////test
            ////int iP1_Pos = 4;
            ////int iP2_Pos = 8;

            //int iP1_Score = 0, iP2_Score = 0;
            //bool xPlayer = false;

            //while(iP1_Score<1000 & iP2_Score < 1000)
            //{
            //    xPlayer = !xPlayer;
            //    int iMove = Role();
            //    if (xPlayer)
            //    {
            //        //player 1
            //        iP1_Pos = Move(iP1_Pos, iMove);
            //        iP1_Score += iP1_Pos;
            //    }
            //    else
            //    {
            //        //player 2
            //        iP2_Pos = Move(iP2_Pos, iMove);
            //        iP2_Score += iP2_Pos;
            //    }
            //}

            //Console.WriteLine($"Player1: {iP1_Score}");
            //Console.WriteLine($"Player2: {iP2_Score}");

            //if (xPlayer)
            //{
            //    //player 1 wins
            //    Console.WriteLine($"Player 1 wins");
            //    Console.WriteLine($"Result: {iP2_Score*iRoles}");
            //}
            //else
            //{
            //    Console.WriteLine($"Player 2 wins");
            //    Console.WriteLine($"Result: {iP1_Score * iRoles}");
            //}
        }

        int iDice = 1;
        int iRoles = 0;
        public int Role()
        {
            int iResult = iDice;
            iDice++; iRoles++;
            if (iDice > 100)
                iDice = 1;
            iResult += iDice;
            iDice++; iRoles++;
            if (iDice > 100)
                iDice = 1;
            iResult += iDice;
            iDice++; iRoles++;
            if (iDice > 100)
                iDice = 1;

            return iResult;
        }

        public int Move(int Pos, int iMove)
        {
            iMove = iMove%10;
            Pos += iMove;
            if (Pos > 10)
                Pos = Pos % 10;
            return Pos;
        }


        Int64 iWinsP1 = 0;
        Int64 iWinsP2 = 0;

        List<int> RoleOutcomes = new() { 1, 2, 3 };

        public override void Q2()
        {
            //input
            int iP1_Pos = 10;
            int iP2_Pos = 8;

            //test
            //int iP1_Pos = 4;
            //int iP2_Pos = 8;
            diceRolls();
            Logic(true, iP1_Pos, 0, iP2_Pos, 0,1);

            Console.WriteLine($"Player1: {iWinsP1}");
            Console.WriteLine($"Player2: {iWinsP2}");

        }

        Dictionary<int, int> rolls;

        private void diceRolls()
        {
            rolls = new();
            for (int die1 = 1; die1 <= 3; die1++)
                for (int die2 = 1; die2 <= 3; die2++)
                    for (int die3 = 1; die3 <= 3; die3++)
                    {
                        int die = die1 + die2 + die3;
                        if (rolls.ContainsKey(die))
                            rolls[die]++;
                        else rolls.Add(die, 1);
                    }
        }


        public void Logic(bool xPlayer, int iP1_Pos, int iP1_Score, int iP2_Pos, int iP2_Score, Int64 iOccurences)
        {
            if (iP1_Score >= 21)
            {
                iWinsP1 += iOccurences;
                return;
            }

            if (iP2_Score >= 21) 
            { 
                iWinsP2 += iOccurences;
                return;
            }
            
            foreach ((int die, int occurence) in rolls)
            {

                int newScore1 = iP1_Score;
                int newScore2 = iP2_Score;
                int newPos1 = iP1_Pos;
                int newPos2 = iP2_Pos;
                if (xPlayer)
                {
                    newPos1 += die;
                    if (newPos1 > 10) newPos1 -= 10;
                    newScore1 += newPos1;
                }
                else
                {
                    newPos2 += die;
                    if (newPos2 > 10) newPos2 -= 10;
                    newScore2 += newPos2;
                }

                Logic(!xPlayer, newPos1, newScore1, newPos2, newScore2, iOccurences * occurence);
            }
    
        }
        
        public void Logic1(bool xPlayer, int iP1_Pos, int iP1_Score, int iP2_Pos, int iP2_Score)
        {
            xPlayer = !xPlayer;
            foreach (int iRole1 in RoleOutcomes)
                foreach (int iRole2 in RoleOutcomes)
                    foreach (int iRole3 in RoleOutcomes)
                    {
                        int iRole = iRole1 + iRole2 + iRole3;
                        if (xPlayer)
                        {
                            //player 1
                            int iNewPos = Move(iP1_Pos, iRole);
                            int iNewScore = iP1_Score + iNewPos;
                            if (iNewScore > 21)
                                iWinsP1++;
                            else
                                Logic1(xPlayer, iNewPos, iNewScore, iP2_Pos, iP2_Score);

                        }
                        else
                        {
                            //player 2
                            int iNewPos = Move(iP2_Pos, iRole);
                            int iNewScore = iP2_Score + iNewPos;
                            if (iNewScore > 21)
                                iWinsP2++;
                            else
                                Logic1(xPlayer, iP1_Pos, iP1_Score, iNewPos, iNewScore);
                        }
                    }

        }
        public void Logic0(bool xPlayer, int iP1_Pos, int iP1_Score, int iP2_Pos, int iP2_Score)
        {
            xPlayer = !xPlayer;
            foreach (int iRole in RoleOutcomes)
            {
                if (xPlayer)
                {
                    //player 1
                    int iNewPos = Move(iP1_Pos, iRole);
                    int iNewScore = iP1_Score + iNewPos;
                    if (iNewScore > 21)
                        iWinsP1++;
                    else
                        Logic0(xPlayer, iNewPos, iNewScore, iP2_Pos, iP2_Score);

                }
                else
                {
                    //player 2
                    int iNewPos = Move(iP2_Pos, iRole);
                    int iNewScore = iP2_Score + iNewPos;
                    if (iNewScore > 21)
                        iWinsP2++;
                    else
                        Logic0(xPlayer, iP1_Pos, iP1_Score, iNewPos, iNewScore);
                }
            }

        }
    
    }
}
