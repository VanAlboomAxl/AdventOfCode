using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day22 : Day
    {
        public override int _iDay { get { return 22; } }

        public override string Q1()
        {
            var lsInput = Data;
            var cmd = Data.Last();
            lsInput.Remove(cmd);
            lsInput.Remove("");
            string sLength = "";
            int direction = 0;
            int x=0, y=0;
            for(int i = 0; i < lsInput[0].Length;i++)
                if (lsInput[0][i] == '.')
                {
                    x = i;
                    break;
                }
            foreach(var c in cmd)
            {
                if (char.IsDigit(c)) sLength += c;
                else
                {
                    int iMove = int.Parse(sLength);
                    sLength = "";

                    logic(lsInput, iMove, ref x, ref y, direction);

                    if (c.Equals('R')) direction++;
                    else direction--;
                    if (direction > 3) direction = 0;
                    if(direction <0) direction = 3;
                }
            }
            logic(lsInput, int.Parse(sLength), ref x, ref y, direction);

            return $"{1000*(y+1)+4*(x+1)+direction}";
        }

        void logic(List<string> map,int move, ref int x,ref int y,int direction)
        {
            for (int i = 0; i < move; i++)
            {
                bool xCouldMove = false;
                // do the moving here
                if (direction == 0)
                {
                    // right
                    int dx = x + 1;
                    if (dx == map[y].Length) dx = 0;
                    char location = map[y][dx];
                    if (location.Equals('.')) { x = dx; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        for (int j = 0; j < x; j++)
                        {
                            location = map[y][j];
                            if (location.Equals('.')) { x = j; xCouldMove = true; break; }
                            else if (location.Equals('#')) { xCouldMove = false; break; }
                        }
                    }
                }
                else if (direction == 1)
                {
                    //down
                    int dy = y + 1;
                    if (dy == map.Count) dy = 0;
                    char location = x < map[dy].Length ? map[dy][x] : 'a';
                    if (location.Equals('.')) { y = dy; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        for (int j = 0; j < y; j++)
                        {
                            location = x < map[j].Length ? map[j][x] : 'a'; ;
                            if (location.Equals('.')) { y = j; xCouldMove = true; break; }
                            else if (location.Equals('#')) { xCouldMove = false; break; }
                        }
                    }
                }
                else if (direction == 2)
                {
                    //left
                    int dx = x - 1;
                    if (dx < 0) dx = map[y].Length - 1;
                    char location = map[y][dx];
                    if (location.Equals('.')) { x = dx; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        for (int j = map[y].Length - 1; j > x; j--)
                        {
                            location = map[y][j];
                            if (location.Equals('.')) { x = j; xCouldMove = true; break; }
                            else if (location.Equals('#')) { xCouldMove = false; break; }
                        }
                    }
                }
                else if (direction == 3)
                {
                    //up
                    int dy = y - 1;
                    if (dy < 0) dy = map.Count - 1;
                    char location = x < map[dy].Length ? map[dy][x] : 'a';
                    if (location.Equals('.')) { y = dy; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        for (int j = map.Count - 1; j > y; j--)
                        {
                            location = x < map[j].Length ? map[j][x] : 'a';
                            if (location.Equals('.')) { y = j; xCouldMove = true; break; }
                            else if (location.Equals('#')) { xCouldMove = false; break; }
                        }
                    }
                }
                if (!xCouldMove) break; //no point in trying again
            }
        }

        public override string Q2()
        {
            var lsInput = Input;
            //lsInput = Test;
            return "";
        }


    }
}
