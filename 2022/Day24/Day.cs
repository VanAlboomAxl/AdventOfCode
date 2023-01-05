using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day24 : Day
    {
        public override int _iDay { get { return 24; } }

        int xMax = 0;
        int yMax = 0;
        List<string> lsInput;
        public override string Q1()
        {
            return "pass to q2";
            lsInput = Data;
            (int, int) start = (1, 0);
            //(int, int) goal = (lsInput.Count - 1, lsInput[lsInput.Count - 2].Length-2);
            (int, int) goal = (lsInput[lsInput.Count - 2].Length-2, lsInput.Count - 1);
            xMax = goal.Item1;
            yMax = goal.Item2-1;
            List<(int, int)> left = new();
            List<(int, int)> right = new();
            List<(int, int)> up = new();
            List<(int, int)> down = new();
            for(int j = 1; j < lsInput.Count-1; j++) //y
            {
                for (int i = 1; i < lsInput[j].Length - 1; i++) //x
                {
                    char c = lsInput[j][i];
                    switch (c)
                    {
                        case '<': left.Add((i, j)); break;
                        case '>': right.Add((i, j)); break;
                        case 'v': down.Add((i, j)); break;
                        case '^': up.Add((i, j)); break;
                    }
                }
            }
            //node nStart = new(start, 0, left, right, up, down);
            node nStart = new(1,0);

            return $"{logic(start,goal,ref left,ref right,ref up,ref down)}";
        }
      
        int logic((int,int) start, (int, int) goal, ref List<(int, int)> left, ref List<(int, int)> right, ref List<(int, int)> up, ref List<(int, int)> down)
        {
            HashSet<(int,int)> nodes = new() { start };
            int time = 0;
            bool xGoalReached = false;
            while (!xGoalReached)
            {
                (left, right, up, down) = blizzardMove(left, right, up, down);
                HashSet<(int,int)> newNodes = new();
                time++;
                foreach (var n in nodes)
                {
                    int dx = n.Item1 - 1;
                    int dy = n.Item2;

                    //left
                    if (canMove(dx, dy, left, right, up, down)) 
                    {
                        if (dx == goal.Item1 && dy == goal.Item2)
                        {
                            xGoalReached=true;
                            break;
                        }
                        newNodes.Add(new(dx, dy)); 
                    }
                    //right
                    dx = n.Item1 + 1;
                    if (canMove(dx, dy, left, right, up, down))
                    {
                        if (dx == goal.Item1 && dy == goal.Item2)
                        {
                            xGoalReached = true;
                            break;
                        }
                        newNodes.Add(new(dx, dy));
                    }
                    //up
                    dx = n.Item1;
                    dy = n.Item2 - 1;
                    if (canMove(dx, dy, left, right, up, down))
                    {
                        if (dx == goal.Item1 && dy == goal.Item2)
                        {
                            xGoalReached = true;
                            break;
                        }
                        newNodes.Add(new(dx, dy));
                    }
                    //down
                    dy = n.Item2 + 1;
                    if (canMove(dx, dy, left, right, up, down))
                    {
                        if (dx == goal.Item1 && dy == goal.Item2)
                        {
                            xGoalReached = true;
                            break;
                        }
                        newNodes.Add(new(dx, dy));
                    }
                    //wait
                    if (canMove(n.Item1, n.Item2, left, right, up, down))
                    {
                        if (n.Item1 == goal.Item1 && n.Item2 == goal.Item2)
                        {
                            xGoalReached = true;
                            break;
                        }
                        newNodes.Add(new(n.Item1, n.Item2));
                    }
                }
                nodes = newNodes;
            }

            return time;
        }
        (List<(int, int)>, List<(int, int)>, List<(int, int)>, List<(int, int)>) blizzardMove(List<(int, int)> left, List<(int, int)> right, List<(int, int)> up, List<(int, int)> down)
        {
            List<(int, int)> newLeft = new() { };
            foreach (var v in left)
            {
                int x = v.Item1 - 1;
                if (x == 0) x = xMax;
                newLeft.Add((x, v.Item2));
            }
            List<(int, int)> newRight = new() { };
            foreach (var v in right)
            {
                int x = v.Item1 + 1;
                if (x > xMax) x = 1;
                newRight.Add((x, v.Item2));
            }
            List<(int, int)> newUp = new() { };
            foreach (var v in up)
            {
                int y = v.Item2 - 1;
                if (y == 0) y = yMax;
                newUp.Add((v.Item1, y));
            }
            List<(int, int)> newDown = new() { };
            foreach (var v in down)
            {
                int y = v.Item2 + 1;
                if (y > yMax) y = 1;
                newDown.Add((v.Item1, y));
            }
            return (newLeft, newRight, newUp, newDown);
        }
        public override string Q2()
        {
            lsInput = Data;
            (int, int) start = (1, 0);
            (int, int) goal = (lsInput[lsInput.Count - 2].Length - 2, lsInput.Count - 1);
            xMax = goal.Item1;
            yMax = goal.Item2 - 1;
            List<(int, int)> left = new();
            List<(int, int)> right = new();
            List<(int, int)> up = new();
            List<(int, int)> down = new();
            for (int j = 1; j < lsInput.Count - 1; j++) //y
            {
                for (int i = 1; i < lsInput[j].Length - 1; i++) //x
                {
                    char c = lsInput[j][i];
                    switch (c)
                    {
                        case '<': left.Add((i, j)); break;
                        case '>': right.Add((i, j)); break;
                        case 'v': down.Add((i, j)); break;
                        case '^': up.Add((i, j)); break;
                    }
                }
            }

            int iTimeToGoal = logic(start, goal, ref left, ref right, ref up, ref down);
            int iTimeBackToStart = logic(goal, start, ref left, ref right, ref up, ref down);
            int iTimeBackToGoal = logic(start, goal, ref left, ref right, ref up, ref down);
            return $"{iTimeToGoal+iTimeBackToStart+iTimeBackToGoal}";
        }

        int logic_v0(node start, (int, int) goal, List<(int, int)> left, List<(int, int)> right, List<(int, int)> up, List<(int, int)> down)
        {
            HashSet<node> nodes = new() { start };
            int time = 0;
            bool xGoalReached = false;
            while (!xGoalReached)
            {
                (var newLeft, var newRight, var newUp, var newDown) = blizzardMove(left, right, up, down);
                left = newLeft;
                right = newRight;
                up = newUp;
                down = newDown;
                HashSet<node> newNodes = new();
                time++;
                foreach (var node in nodes)
                {
                    possibleMoves(node, newLeft, newRight, newUp, newDown).
                        ForEach(
                        x =>
                        {
                            if (x.x == goal.Item1 && x.y == goal.Item2) xGoalReached = true;
                            newNodes.Add(x);
                        }
                    );
                    if (xGoalReached)
                        break;
                }
                nodes = newNodes;
            }

            return time;
        }
        List<node> possibleMoves(node n, List<(int, int)> left, List<(int, int)> right, List<(int, int)> up, List<(int, int)> down)
        {
            List<node> result = new();

            int dx = n.x - 1;
            int dy = n.y;

            //left
            if (canMove(dx, dy, left, right, up, down)) result.Add(new(dx, dy));
            //right
            dx = n.x + 1;
            if (canMove(dx, dy, left, right, up, down)) result.Add(new(dx, dy));
            //up
            dx = n.x;
            dy = n.y - 1;
            if (canMove(dx, dy, left, right, up, down)) result.Add(new(dx, dy));
            //down
            dy = n.y + 1;
            if (canMove(dx, dy, left, right, up, down)) result.Add(new(dx, dy));
            //wait
            if (canMove(n.x, n.y, left, right, up, down)) result.Add(new(n.x, n.y));
            return result;
        }
        bool canMove(int x, int y, List<(int, int)> left, List<(int, int)> right, List<(int, int)> up, List<(int, int)> down)
        {
            if (y < 0 || y > lsInput.Count - 1) return false;
            if (x < 0 || x > lsInput[0].Length - 1) return false;
            if (left.Contains((x, y))) return false;
            if (right.Contains((x, y))) return false;
            if (up.Contains((x, y))) return false;
            if (down.Contains((x, y))) return false;
            if (lsInput[y][x] != '#') return true;
            return false;
        }
        class node
        {
            public int x { get; private set; }
            public int y { get; private set; }
            //public (int,int) location { get; private set; }
            //public int time { get; private set; }
            //public List<(int, int)> left { get; private set; }
            //public List<(int, int)> right { get; private set; }
            //public List<(int, int)> up { get; private set; }
            //public List<(int, int)> down { get; private set; }
            //public node((int, int) location,int time, List<(int, int)> left, List<(int, int)> right, List<(int, int)> up, List<(int, int)> down)
            //public node((int, int) location)//,int time, List<(int, int)> left, List<(int, int)> right, List<(int, int)> up, List<(int, int)> down)
            public node(int x, int y)//,int time, List<(int, int)> left, List<(int, int)> right, List<(int, int)> up, List<(int, int)> down)
            {
                this.x = x; this.y = y;
                //this.location = location;
                //this.left = left;
                //this.right = right;
                //this.up = up;
                //this.down = down;
                //this.time = time;
            }
            public override string ToString()
            {
                return $"{x},{y}";
                //return $"{location}";
                //return $"{location}@{time}";
            }
            public override bool Equals(object obj)
            {
                if (obj is node n)
                    if (n.x == x && n.y == y)
                        return true;
                return false;
            }
        }

    }
}
