using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day12 : Day
    {
        public override int _iDay { get { return 12; } }

        public (List<clsPosition>,clsPosition,clsPosition) Convert()
        {
            var lsInput = Data;
            clsPosition oStart = null;
            clsPosition oGoal = null;
            List<clsPosition> oMap = new();
            Dictionary<(int, int), clsPosition> dConvert = new();
            for (int r = 0; r < lsInput.Count; r++)
                for (int c = 0; c < lsInput[r].Length; c++)
                {
                    char me = lsInput[r][c];
                    bool xGoal = false;
                    if (me.Equals('E')) { me = 'z'; xGoal = true; }
                    bool xStart = false;
                    if (me.Equals('S')) { me = 'a'; xStart = true; }
                    int iMe = (int)me;
                    clsPosition p = new(iMe, c, r, 1);
                    if (xGoal) oGoal =p;
                    if (xStart) oStart = p;
                    oMap.Add(p);
                    dConvert.Add((c, r), p);
                    if (c != 0) p.AddNeighbor(dConvert[(c - 1, r)]);
                    if (r != 0) p.AddNeighbor(dConvert[(c, r - 1)]);
                }
            return (oMap,oStart,oGoal);
        }

        public override string Q1()
        {
            (var oMap,var oStart,var oGoal) = Convert();
            (List<clsPosition> loPath, int risk) = SearchAlgorithm.Astar(new() { oStart }, oGoal);

            return risk.ToString();
        }
        public (List<clsPosition>, List<clsPosition>, clsPosition) Convert2()
        {
            List<clsPosition> oStart = new();
            clsPosition oGoal = null;
            List<clsPosition> oMap = new();
            Dictionary<(int, int), clsPosition> dConvert = new();
            for (int r = 0; r < Data.Count; r++)
                for (int c = 0; c < Data[r].Length; c++)
                {
                    char me = Data[r][c];
                    bool xGoal = false;
                    if (me.Equals('E')) { me = 'z'; xGoal = true; }
                    bool xStart = false;
                    if (me.Equals('S') || me.Equals('a')) { me = 'a'; xStart = true; }
                    int iMe = (int)me;
                    clsPosition p = new(iMe, c, r, 1);
                    if (xGoal) oGoal = p;
                    if (xStart) oStart.Add(p);
                    oMap.Add(p);
                    dConvert.Add((c, r), p);
                    if (c != 0) p.AddNeighbor(dConvert[(c - 1, r)]);
                    if (r != 0) p.AddNeighbor(dConvert[(c, r - 1)]);
                }
            return (oMap, oStart, oGoal);
        }
        public override string Q2()
        {
            (var oMap, var oStart, var oGoal) = Convert2();
            (List<clsPosition> loPath, int risk) = SearchAlgorithm.Astar(oStart, oGoal);
            return risk.ToString();
        }

        public List<clsPosition> oMap;

        private void WriteMap()
        {
            Dictionary<int, Dictionary<int, int>> dic = new();
            foreach (var pos in oMap)
            {
                if (dic.ContainsKey(pos.y))
                    dic[pos.y].Add(pos.x, pos.Risk);
                else
                {
                    Dictionary<int, int> sub = new();
                    sub.Add(pos.x, pos.Risk);
                    dic.Add(pos.y, sub);
                }
            }

            var keys = dic.Keys.ToList();
            keys.Sort();
            foreach (var y in keys)
            {
                var Xkeys = dic[y].Keys.ToList();
                Xkeys.Sort();
                foreach (var x in Xkeys)
                {
                    Console.Write(dic[y][x]);
                }
                Console.WriteLine();
            }
        }

        public interface INeighbors<T>
        {
            public List<T> Neighbors { get; }
            public abstract bool AddNeighbor(T x, bool y);
            public abstract int TravelCost(T x);
            public abstract int StayCost();
        }

        public class SearchAlgorithm
        {

            //public static (List<T>, int) Astar<T>(T start, T goal) where T : INeighbors<T>
            //{
            //    Queue<T> openSet = new();
            //    openSet.Enqueue(start);

            //    Dictionary<T, T> cameFrom = new();

            //    Dictionary<T, int> gScore = new();
            //    gScore[start] = 0;

            //    //Dictionary<T, int> fScore = new();
            //    //fScore[start] = start.StayCost();

            //    while (openSet.TryDequeue(out T current))
            //    {

            //        //if (current.Equals(goal))
            //        //    return (Reconstruct_Path(cameFrom, current), gScore[current]);

            //        foreach (var neighbor in current.Neighbors)
            //        {
            //            //if (neighbor.Equals(start))
            //            //{

            //            //}
            //            int tentative_gScore = gScore[current] + current.TravelCost(neighbor);
            //            if (tentative_gScore < gScore.GetValueOrDefault(neighbor, int.MaxValue))
            //            {
            //                cameFrom[neighbor] = current;
            //                gScore[neighbor] = tentative_gScore;
            //                //fScore[neighbor] = tentative_gScore + neighbor.StayCost();
            //                openSet.Enqueue(neighbor);
            //            }

            //        }

            //    }
            //    return (Reconstruct_Path(cameFrom, goal), gScore[goal]);

            //    return (null, -1); //failure
            //}
            //private static List<T> Reconstruct_Path<T>(Dictionary<T, T> Path, T current) where T : INeighbors<T>
            //{
            //    List<T> loPath = new() { current };
            //    while (Path.ContainsKey(current))
            //    {
            //        T oPrev = Path[current];
            //        loPath.Insert(0, oPrev);
            //        current = oPrev;
            //    }
            //    return loPath;
            //}
            //private static List<T> Reconstruct_Path2<T>(Dictionary<T, T> Path, T current) where T : INeighbors<T>
            //{
            //    List<T> loPath = new() { current };
            //    while (Path.ContainsKey(current))
            //    {
            //        T oPrev = Path[current];
            //        loPath.Insert(0, oPrev);
            //        current = oPrev;
            //    }
            //    return loPath;
            //}

            public static (List<T>, int) Astar<T>(List<T> start, T goal) where T : INeighbors<T>
            {
                Dictionary<T, T> cameFrom = new();
                Dictionary<T, int> gScore = new();
                Queue<T> openSet = new();
                foreach (var s in start)
                {
                    openSet.Enqueue(s);
                    gScore[s] = 0;
                }
                while (openSet.TryDequeue(out T current))
                {
                    foreach (var neighbor in current.Neighbors)
                    {
                        int tentative_gScore = gScore[current] + current.TravelCost(neighbor);
                        if (tentative_gScore < gScore.GetValueOrDefault(neighbor, int.MaxValue))
                        {
                            cameFrom[neighbor] = current;
                            gScore[neighbor] = tentative_gScore;
                            openSet.Enqueue(neighbor);
                        }

                    }

                }
                return (Reconstruct_Path(cameFrom, goal), gScore[goal]);
            }
            private static List<T> Reconstruct_Path<T>(Dictionary<T, T> Path, T current) where T : INeighbors<T>
            {
                List<T> loPath = new() { current };
                while (Path.ContainsKey(current))
                {
                    T oPrev = Path[current];
                    loPath.Insert(0, oPrev);
                    current = oPrev;
                }
                return loPath;
            }
        }

        public class clsPosition : INeighbors<clsPosition>
        {
            public int Value { get; private set; }
            public int x { get; private set; }
            public int y { get; private set; }
            public int Risk { get; private set; }

            public List<clsPosition> Neighbors { get; private set; }
            public clsPosition(int Value,int iX, int iY, int iRisk)
            {
                this.Value = Value;
                x = iX;
                y = iY;
                Risk = iRisk;
                Neighbors = new();
            }

            public bool AddNeighbor(clsPosition neighbor, bool x=true)
            {
                if (Neighbors.Contains(neighbor)) return false;
                //int delta = Math.Abs(Value - neighbor.Value);
                //int delta = Value - neighbor.Value;
                int delta = neighbor.Value - Value;
                if (delta < 2)//&& delta >= 0)
                {
                    Neighbors.Add(neighbor);
                }
                if (x) neighbor.AddNeighbor(this, false);
                return true;
            }

            public override string ToString()
            {
                return $"({x},{y})";
            }

            public int TravelCost(clsPosition neighbor)
            {
                if (!Neighbors.Contains(neighbor)) return -1;
                return neighbor.Risk;
            }
            public int StayCost()
            {
                return 0;
            }
        }

    }
}
