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
            int iDimensions = 50;
            var lsInput = Data;
            var cmd = Data.Last();
            lsInput.Remove(cmd);
            lsInput.Remove("");
            string sLength = "";
            int direction = 0;
            int x = 0, y = 0;

            char[,] vlak = new char[iDimensions,iDimensions];
            Dictionary<char[,], Dictionary<string, char[,]>> cube = new();
            for(int j = 0; j<lsInput.Count; j+=iDimensions) //y
                for (int i = 0; i < lsInput[0].Length; i+=iDimensions) //x
                    if (lsInput[0][i] == '.' || lsInput[0][i]=='#')
                    {
                        for (int dj = 0; dj < iDimensions; dj++)
                            for (int di = 0; di < iDimensions; di++)
                                vlak[di, dj] = lsInput[j + dj][i + di];
                        x = i;
                        break;
                    }
            foreach (var c in cmd)
            {
                if (char.IsDigit(c)) sLength += c;
                else
                {
                    int iMove = int.Parse(sLength);
                    sLength = "";

                    q2Logic(lsInput, iMove, ref x, ref y, direction);

                    if (c.Equals('R')) direction++;
                    else direction--;
                    if (direction > 3) direction = 0;
                    if (direction < 0) direction = 3;
                }
            }
            q2Logic(lsInput, int.Parse(sLength), ref x, ref y, direction);

            return $"{1000 * (y + 1) + 4 * (x + 1) + direction}";
        }

        void q2Logic(List<string> map, int move, ref int x, ref int y, int direction)
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
        bool q2Move(List<string> map, int move, ref int x, ref int y, int direction)
        {
            int iDimension = 50;
            int iVlak = 0;
            if (y >= 0 && y < iDimension && x >= iDimension && x < iDimension*2) iVlak = 0;
            if (y >= iDimension && y < 2*iDimension && x >= iDimension && x < iDimension*2) iVlak = 1;
            if (y >= iDimension*2 && y < 3*iDimension && x >= iDimension && x < iDimension*2) iVlak = 1;



            bool xCouldMove = false;
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
                    // do some cubic shit here
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
            return xCouldMove;
        }

        public class location 
        { 
            public int x { get; set; }
            public int y { get; set; }
            public location left { get; set; }
            public location right { get; set; }
            public location up { get; set; }
            public location down { get; set; }
        }
        public class wall: location { }
        public class open: location { }
        public class side
        {
            char[,] vlak { get; set; }
            public side left { get; set; }
            public side right { get; set; }
            public side up { get; set; }
            public side down { get; set; }
        }
    }
}
