using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{

    public interface INeighbors<T> 
    {
        public List<T> Neighbors { get; }
        public abstract bool AddNeighbor(T x);
        public abstract int TravelCost(T x);
        public abstract int StayCost();
    }
    
    public class SearchAlgorithm
    {
        
        public static (List<T>, int) Astar<T>(T start, T goal) where T : INeighbors<T>
        {
            Queue<T> openSet = new();
            openSet.Enqueue(start);

            Dictionary<T, T> cameFrom = new();

            Dictionary<T, int> gScore = new();
            gScore[start] = 0;

            //Dictionary<T, int> fScore = new();
            //fScore[start] = start.StayCost();

            while (openSet.TryDequeue(out T current))
            {

                if (current.Equals(goal))
                    return (Reconstruct_Path(cameFrom, current), gScore[current]);
          
                foreach (var neighbor in current.Neighbors)
                {
                    if (neighbor.Equals(start))
                    {

                    }
                    int tentative_gScore = gScore[current] + current.TravelCost(neighbor);
                    if (tentative_gScore < gScore.GetValueOrDefault(neighbor, int.MaxValue))
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentative_gScore;
                        //fScore[neighbor] = tentative_gScore + neighbor.StayCost();
                        openSet.Enqueue(neighbor);
                    }

                }

            }



            return (null, -1); //failure
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
        private static List<T> Reconstruct_Path2<T>(Dictionary<T, T> Path, T current) where T : INeighbors<T>
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
        public int x { get; private set; }
        public int y { get; private set; }
        public int Risk { get; private set; }

        public List<clsPosition> Neighbors { get; private set; }
        public clsPosition(int iX, int iY, int iRisk)
        {
            x = iX;
            y = iY;
            Risk = iRisk;
            Neighbors = new();
        }

        public bool AddNeighbor(clsPosition neighbor)
        {
            if (Neighbors.Contains(neighbor)) return false;
            Neighbors.Add(neighbor);
            neighbor.AddNeighbor(this);
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

    public class Day15 : Day<List<clsPosition>>
    {
        public override int _iDay { get { return 15; } }

        internal override List<string> _lsTest => new List<string>() {
            "1163751742",
            "1381373672",
            "2136511328",
            "3694931569",
            "7463417111",
            "1319128137",
            "1359912421",
            "3125421639",
            "1293138521",
            "2311944581"
        };

        public override List<clsPosition> Convert(List<string> Input)
        {
            var lliInput = Helper.Convertor_LLI(Input);

            List<clsPosition> oMap = new();
            Dictionary<(int, int), clsPosition> dConvert = new();
            for(int r=0;r<lliInput.Count;r++)
                for(int c=0;c<lliInput[r].Count;c++)
                {
                    clsPosition p = new(c, r, lliInput[r][c]);
                    oMap.Add(p);
                    dConvert.Add((c, r), p);
                    if (c!= 0)
                        p.AddNeighbor(dConvert[(c - 1, r)]);
                    if (r != 0)
                        p.AddNeighbor(dConvert[(c, r-1)]);
                }
            return oMap;
        }

        public List<clsPosition> oMap;
        public Day15()
        {
            oMap = Input;
            //oMap = Test;
        }

        public override void Q1()
        {
            (List<clsPosition> loPath, int risk) = SearchAlgorithm.Astar(oMap[0], oMap.Last());
            Console.WriteLine(risk);
            Console.WriteLine((loPath.Sum(x => x.Risk) - oMap[0].Risk).ToString());
        }

        public override void Q2()
        {

            //UpdateMapForQ2(Helper.Convertor_LLI(_lsTest));
            UpdateMapForQ2(Helper.Convertor_LLI(Helper.ReadInput(InputLocation)));

            //WriteMap();

            var start = oMap[0];
            var goal = oMap.Last();

            (List<clsPosition> loPath, int risk) = SearchAlgorithm.Astar(start, goal);
            
            Console.WriteLine(risk);
            Console.WriteLine((loPath.Sum(x => x.Risk) - oMap[0].Risk).ToString()); 
        }
        
        private void UpdateMapForQ2(List<List<int>> lliInput)
        {

            int irangeX = lliInput[0].Count();
            int irangeY = lliInput.Count();

            List<clsPosition> oNewMap = new();
            Dictionary<(int, int), clsPosition> dConvert = new();
            for (int r = 0; r < lliInput.Count; r++)
                for (int c = 0; c < lliInput[r].Count; c++)
                {
                    clsPosition p = new(c, r, lliInput[r][c]);
                    oNewMap.Add(p);
                    dConvert.Add((c, r), p);
                    if (c != 0)
                        p.AddNeighbor(dConvert[(c - 1, r)]);
                    if (r != 0)
                        p.AddNeighbor(dConvert[(c, r - 1)]);
                }

            for (int j = 1; j < 5; j++)
            {
                for (int r = 0; r < irangeY; r++)
                {
                    List<int> liRow = new();
                    for (int c = 0; c < irangeX; c++)
                    {
                        int yPrev = ((j - 1) * irangeY) + r;

                        clsPosition oPrev = dConvert[(c, yPrev)];

                        int y = j * irangeY + r;
                        int iRisk = oPrev.Risk + 1;
                        if (iRisk > 9) iRisk = 1;
                        liRow.Add(iRisk);
                        clsPosition p = new(c, y, iRisk);
                        oNewMap.Add(p);
                        dConvert.Add((c, y), p);
                        if (c != 0)
                            p.AddNeighbor(dConvert[(c - 1, y)]);
                        if (y != 0)
                            p.AddNeighbor(dConvert[(c, y - 1)]);
                    }
                    lliInput.Add(liRow);
                }
            }

            for (int i = 1; i < 5; i++)
                for (int r = 0; r < irangeY*5; r++)
                {
                    for (int c = 0; c < irangeX; c++)
                    {
                        int xPrev = ((i - 1) * irangeX) + c;

                        clsPosition oPrev = dConvert[(xPrev, r)];

                        int x = i * lliInput[r].Count + c;

                        int iRisk = oPrev.Risk + 1;
                        if (iRisk > 9) iRisk = 1;

                        clsPosition p = new(x, r, iRisk);
                        oNewMap.Add(p);
                        dConvert.Add((x, r), p);
                        if (x != 0)
                            p.AddNeighbor(dConvert[(x - 1, r)]);
                        if (r != 0)
                            p.AddNeighbor(dConvert[(x, r - 1)]);
                    }
                }

            
            oMap = oNewMap;

        }
        private void WriteMap()
        {
            Dictionary<int, Dictionary<int, int>> dic = new();
            foreach(var pos in oMap)
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
    }
   
}
