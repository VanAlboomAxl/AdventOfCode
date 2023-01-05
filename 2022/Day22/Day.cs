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

        //tried folding 
        public string Q2_v0()
        {
            int iDimensions = 4;
            var lsInput = Data;
            var cmd = Data.Last();
            lsInput.Remove(cmd);
            lsInput.Remove("");
            string sLength = "";
            int direction = 0;
            int x = 0, y = 0;

            List<side> sides = new();
            Dictionary<(int,int),side> sideMapper = new();
            for(int j = 0; j<lsInput.Count; j+=iDimensions) //y
                for (int i = 0; i < lsInput[j].Length; i+=iDimensions) //x
                    if (lsInput[j][i] == '.' || lsInput[j][i]=='#')
                    {
                        side s = new(iDimensions);
                        s.locationX = i;
                        s.locationY = j;
                        for (int dj = 0; dj < iDimensions; dj++)
                            for (int di = 0; di < iDimensions; di++)
                                s.vlak[di, dj] = lsInput[j + dj][i + di];
                        x = i;
                        sideMapper.Add((i,j), s);
                        if (sideMapper.ContainsKey((i+iDimensions,j))) //right 
                        {
                            s.right = sideMapper[(i + iDimensions, j)];
                            s.right.left = s;
                        }
                        if (sideMapper.ContainsKey((i - iDimensions, j))) //left 
                        {
                            s.left = sideMapper[(i - iDimensions, j)];
                            s.left.right = s;
                        }
                        if (sideMapper.ContainsKey((i , j - iDimensions))) //up 
                        {
                            s.up = sideMapper[(i , j - iDimensions)];
                            s.up.down = s;
                        }
                        if (sideMapper.ContainsKey((i, j + iDimensions))) //down 
                        {
                            s.down = sideMapper[(i, j + iDimensions)];
                            s.down.up = s;
                        }
                        sides.Add(s);
                        //break;
                    }

            List<side> sidesToCheck = new(sides);
            while (sidesToCheck.Count>0)
            {
                side s = sidesToCheck[0];
                sidesToCheck.RemoveAt(0);

                if (s.left == null && s.up != null) { s.left = s.up.left; s.up.right = s; }
                if (s.left == null && s.down != null) { s.left = s.down.left; }
                if (s.right == null && s.up != null) { s.right = s.up.right; }
                if (s.right == null && s.down != null) { s.right = s.down.right; }
                if (s.up == null && s.left != null) { s.up = s.left.up; }
                if (s.up == null && s.right != null) { s.up = s.right.up; }
                if (s.down == null && s.left != null) { s.down = s.left.down; }
                if (s.down == null && s.right != null) { s.down = s.right.down; }
                if(s.down == null || s.up == null || s.right == null || s.left == null)
                    sidesToCheck.Add(s);
            }
            
            return $"{1000 * (y + 1) + 4 * (x + 1) + direction}";
        }
        //lets hardcode 
        //answer: 142380
        public override string Q2() 
        {
            var lsInput = Data;
            var cmd = Data.Last();
            lsInput.Remove(cmd);
            lsInput.Remove("");
            string sLength = "";
            int direction = 0;
            int x = 0, y = 0;
            for (int i = 0; i < lsInput[0].Length; i++)
                if (lsInput[0][i] == '.')
                {
                    x = i;
                    break;
                }

            Console.WriteLine($"start: [{x},{y}]"); 
            foreach (var c in cmd)
            {
                if (char.IsDigit(c)) sLength += c;
                else
                {
                    int iMove = int.Parse(sLength);
                    sLength = "";

                    q2Logic(lsInput, iMove, ref x, ref y, ref direction);
                    Console.WriteLine($"[{x},{y}]");
                    if (c.Equals('R')) direction++;
                    else direction--;
                    if (direction > 3) direction = 0;
                    if (direction < 0) direction = 3;
                }
            }
            q2Logic(lsInput, int.Parse(sLength), ref x, ref y, ref direction);

            return $"{1000 * (y + 1) + 4 * (x + 1) + direction}";
        }


        void q2Logic(List<string> map, int move, ref int x, ref int y, ref int direction)
        {
            for (int i = 0; i < move; i++)
            {
                bool xCouldMove = false;
                // do the moving here
                if (direction == 0)
                {
                    // right
                    int dx = x + 1;

                    char location = ' ';
                    if (dx < map[y].Length) location = map[y][dx];
                    if (location.Equals('.')) { x = dx; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        xCouldMove = q2Hardcode(map, ref x, ref y, ref direction);
                    }
                }
                else if (direction == 1)
                {
                    //down
                    int dy = y + 1;
                    char location = ' ';
                    if (dy < map.Count) location = x < map[dy].Length ? map[dy][x] : 'a';
                    if (location.Equals('.')) { y = dy; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        xCouldMove = q2Hardcode(map, ref x, ref y, ref direction);

                    }
                }
                else if (direction == 2)
                {
                    //left
                    int dx = x - 1; 
                    char location = ' ';
                    if (dx >= 0) location = map[y][dx];
                    if (location.Equals('.')) { x = dx; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        xCouldMove = q2Hardcode(map, ref x, ref y, ref direction);

                    }
                }
                else if (direction == 3)
                {
                    //up
                    int dy = y - 1;
                    char location = ' ';
                    if (dy >= 0) location = x < map[dy].Length ? map[dy][x] : 'a'; ;
                    if (location.Equals('.')) { y = dy; xCouldMove = true; }
                    else if (location.Equals('#')) { xCouldMove = false; }
                    else
                    {
                        //end of map --> check other end
                        xCouldMove= q2Hardcode(map,ref x, ref y, ref direction);
                    }
                }
                if (!xCouldMove) break; //no point in trying again
            }
        }
        //thx reddit dude
        bool q2Hardcode(List<string> map, ref int x, ref int y, ref int dir)
        {
            /* format
                 AB 
                 C
                ED
                F
            */
            int block = 50;
            int nextX = x;
            int nextY = y;
            int nextdir = dir;
            /*
             * if (x>=iDimension && x<2*iDimension && y>=0 && y < iDimension)
            {
                //A
                if(dir == 0)
                {
                    // go to B --> easy
                }
                else if(dir == 1)
                {
                    //go to C --> easy
                }
                else if(dir == 2)
                {
                    // --> go to E from right
                    dir = 0;
                    x = 0;
                }
                else if(dir == 3)
                {
                    // --> go to F from right
                    dir = 0;
                    x = 0;
                }
            }
            else if (x >= 2*iDimension && x < 3 * iDimension && y >= 0 && y < iDimension)
            {
                //B
            }
            else if (x >=  iDimension && x < 2 * iDimension && y >= iDimension && y < 2*iDimension)
            {
                //C
            }
            else if (x >= iDimension && x < 2 * iDimension && y >= 2*iDimension && y < 3 * iDimension)
            {
                //D
            }
            else if (x >= 0 && x < iDimension && y >= 2*iDimension && y < 3 * iDimension)
            {
                //E
            }
            else if (x >= 0 && x <  iDimension && y >= 3*iDimension && y < 4 * iDimension)
            {
                //F
            }
            */
            /*if (dir == 0)
            {
                nextX = x + 1;

                if (nextX == block * 3 && 0 <= y && y < block) // side2
                {
                    y = block * 3 - y - 1; 
                    x = block * 2 - 1;
                    dir = 2;
                }
                else if (nextX == block * 2 && block <= y && y < block * 2) // side 3
                {
                    y = block - 1; 
                    x = block + y;
                    dir = 3;
                }
                else if (nextX == block * 2 && block * 2 <= y && y < block * 3) // side 5
                {
                    y = block - (y - 2 * block) - 1; x = block * 3 - 1;
                    dir = 2;
                }
                else if (nextX == block && block * 3 <= y && y < block * 4) // side 6
                {
                    y = block * 3 - 1; x = y - 2 * block;
                    dir = 3;
                }
                else
                {
                    Console.Write("Error.");
                }
                
            }
            else if (dir == 1)
            {
                bool around = false;
                nextY = y + 1;
                if (nextY == block * 4 && 0 <= x && x < block) // side6
                {
                    y = 0; x = x + 2 * block;
                    dir = 1;
                }
                else if (nextY == block * 3 && block <= x && x < block * 2) // side5
                {
                    y = x + 2 * block; 
                    x = block - 1;
                    dir = 2;
                }
                else if (nextY == block && block * 2 <= x && x < block * 3) // side 2
                {
                    y = x - block; 
                    x = 2 * block - 1;
                    dir = 2;
                }
                else
                {
                    Console.Write("Error.");
                }
                
            }
            else if (dir == 2)
            {
                nextX = x - 1;

                if (nextX == block - 1 && 0 <= y && y < block) // side 1
                {
                    y = block * 3 - y - 1; 
                    x = 0;
                    dir = 0;
                }
                else if (nextX == block - 1 && block <= y && y < block * 2) // side3
                {
                    y = block * 2; 
                    x = y - block;
                    dir = 1;
                }
                else if (nextX == -1 && block * 2 <= y && y < block * 3) // side 4
                {
                    y = block - (y - block * 2) - 1; x = block;
                    dir = 0;
                }
                else if (nextX == -1 && block * 3 <= y && y < block * 4) // side6
                {
                    y = 0; x = y - block * 2;
                    dir = 1;
                }
                else
                {
                    Console.Write("Error.");
                }
                
            }
            else if (dir == 3)
            {
                nextY = y - 1;
                if (nextY == block * 2 - 1 && 0 <= x && x < block) // side 4
                {
                    y = block + x; x = block;
                    dir = 0;
                }
                else if (nextY == -1 && block <= x && x < block * 2) // side1
                {
                    y = x + block * 2; x = 0;
                    dir = 0;
                }
                else if (nextY == -1 && block * 2 <= x && x < block * 3) // side 2
                {
                    y = block * 4 - 1; x = x - block * 2;
                    dir = 3;
                }
                else
                {
                    Console.Write("Error.");
                }
            }
            */
            if (dir == 0)
            {
                nextX = x + 1;
                if (nextX == block * 3 && 0 <= y && y < block) // side2
                {
                    nextY = block * 3 - y - 1; nextX = block * 2 - 1;
                    nextdir = 2;
                }
                else if (nextX == block * 2 && block <= y && y < block * 2) // side 3
                {
                    nextY = block - 1; nextX = block + y;
                    nextdir = 3;
                }
                else if (nextX == block * 2 && block * 2 <= y && y < block * 3) // side 5
                {
                    nextY = block - (y - 2 * block) - 1; nextX = block * 3 - 1;
                    nextdir = 2;
                }
                else if (nextX == block && block * 3 <= y && y < block * 4) // side 6
                {
                    nextY = block * 3 - 1; nextX = y - 2 * block;
                    nextdir = 3;
                }
                else
                {
                    Console.Write("Error.");
                }
                
            }
            else if (dir == 1)
            {
                nextY = y + 1;
                if (nextY == block * 4 && 0 <= x && x < block) // side6
                {
                    nextY = 0; nextX = x + 2 * block;
                    nextdir = 1;
                }
                else if (nextY == block * 3 && block <= x && x < block * 2) // side5
                {
                    nextY = x + 2 * block; nextX = block - 1;
                    nextdir = 2;
                }
                else if (nextY == block && block * 2 <= x && x < block * 3) // side 2
                {
                    nextY = x - block; nextX = 2 * block - 1;
                    nextdir = 2;
                }
                else
                {
                    Console.Write("Error.");
                }
                
            }
            else if (dir == 2)
            {
                nextX = x - 1;           
                if (nextX == block - 1 && 0 <= y && y < block) // side 1
                {
                    nextY = block * 3 - y - 1; nextX = 0;
                    nextdir = 0;
                }
                else if (nextX == block - 1 && block <= y && y < block * 2) // side3
                {
                    nextY = block * 2; nextX = y - block;
                    nextdir = 1;
                }
                else if (nextX == -1 && block * 2 <= y && y < block * 3) // side 4
                {
                    nextY = block - (y - block * 2) - 1; nextX = block;
                    nextdir = 0;
                }
                else if (nextX == -1 && block * 3 <= y && y < block * 4) // side6
                {
                    nextY = 0; nextX = y - block * 2;
                    nextdir = 1;
                }
                else
                {
                    Console.Write("Error.");
                }
                
            }
            else if (dir == 3)
            {
                nextY = y - 1;
                
                if (nextY == block * 2 - 1 && 0 <= x && x < block) // side 4
                {
                    nextY = block + x; nextX = block;
                    nextdir = 0;
                }
                else if (nextY == -1 && block <= x && x < block * 2) // side1
                {
                    nextY = x + block * 2; nextX = 0;
                    nextdir = 0;
                }
                else if (nextY == -1 && block * 2 <= x && x < block * 3) // side 2
                {
                    nextY = block * 4 - 1; nextX = x - block * 2;
                    nextdir = 3;
                }
                else
                {
                    Console.Write("Error.");
                }
                
            }
            if (map[nextY][nextX] == '.') { x = nextX; y = nextY; dir = nextdir; return true; }
            return false;
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
            public int locationX { get; set; }
            public int locationY { get; set; }
            public char[,] vlak { get; set; }
            public side left { get; set; }
            public side right { get; set; }
            public side up { get; set; }
            public side down { get; set; }
            public side(int iDimensions)
            {
                vlak = new char[iDimensions, iDimensions];
            }
            public override string ToString()
            {
                return $"[{locationX},{locationY}]";
            }
        }
    }
}
