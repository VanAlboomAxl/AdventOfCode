//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AdventOfCode_2021.V2
//{
//    public abstract class clsNode
//    {
//        public List<clsNode> Neighbors { get; internal set; }
//        public void AddNeighbor(clsNode neighbor, bool xAddMeToNeighbor = true)
//        {
//            Neighbors.Add(neighbor);
//            if (xAddMeToNeighbor)
//                neighbor.AddNeighbor(this, false);
//        }
//    }
//    public interface iNode 
//    {
//        public List<iNode> Neighbors { get; }
//        public void AddNeighbor(iNode neighbor, bool xAddMeToNeighbor = true)
//        {
//            Neighbors.Add(neighbor);
//            if (xAddMeToNeighbor)
//                neighbor.AddNeighbor(this, false);
//        }
//    }

//    public abstract class clsAstar<T> where T: iNode
//    {
//        //https://en.wikipedia.org/wiki/A*_search_algorithm

//        /// <summary>
//        /// // h is the heuristic function. h(n) estimates the cost to reach goal from node n.
//        /// </summary>
//        /// <param name="p"></param>
//        /// <returns></returns>
//        public abstract int h(T p) ;

//        /// <summary>
//        ///  d(current,neighbor) is the weight of the edge from current to neighbor
//        /// </summary>
//        /// <param name="current"></param>
//        /// <param name="neighbor"></param>
//        /// <returns></returns>
//        public abstract int d(T current, T neighbor);
       
//        private List<T> Reconstruct_Path(Dictionary<T, T> Path, T current)
//        {
//            List<T> loPath = new() { current };
//            while (Path.ContainsKey(current))
//            {
//                T oPrev = Path[current];
//                loPath.Insert(0, oPrev);
//                current = oPrev;
//            }
//            return loPath;
//        }

//        /// <summary>
//        /// finds to best path! 
//        /// if goal can't be reach (null,-1) will be returned
//        /// </summary>
//        /// <param name="start"></param>
//        /// <param name="goal"></param>
//        /// <returns></returns>
//        public (List<T>, int) A_star<T>(T start, T goal) where T : iNode
//        {
//            List<T> openSet = new() { start };
//            Dictionary<T, T> cameFrom = new();
//            Dictionary<T, int> gScore = new();
//            gScore.Add(start, 0);
//            Dictionary<T, int> fScore = new();
//            fScore.Add(start, h(start));

//            while (openSet.Count > 0)
//            {
//                T current = openSet[0];

//                if (current.Equals(goal))
//                    return (Reconstruct_Path(cameFrom, current), gScore[current]);

//                openSet.Remove(current);

//                foreach (var neighbor in current.Neighbors)
//                {
//                    int tentative_gScore = gScore[current] + d(current, neighbor);
//                    if (!gScore.ContainsKey(neighbor) || tentative_gScore < gScore[neighbor])
//                    {
//                        if (cameFrom.ContainsKey(neighbor))
//                            cameFrom[neighbor] = current;
//                        else
//                            cameFrom.Add(neighbor, current);

//                        if (gScore.ContainsKey(neighbor))
//                            gScore[neighbor] = tentative_gScore;
//                        else
//                            gScore.Add(neighbor, tentative_gScore);

//                        if (fScore.ContainsKey(neighbor))
//                            fScore[neighbor] = tentative_gScore + h(neighbor);
//                        else
//                            fScore.Add(neighbor, tentative_gScore + h(neighbor));

//                        if (!openSet.Contains(neighbor))
//                            openSet.Add(neighbor);
//                    }

//                }

//            }
            
//            return (null, -1); //failure
//        }

//    }
    

//    public class Day15 : Day<List<clsPosition>>
//    {
//        public override int _iDay { get { return 11; } }

//        internal override List<string> _lsTest => new List<string>() {
//            "1163751742",
//            "1381373672",
//            "2136511328",
//            "3694931569",
//            "7463417111",
//            "1319128137",
//            "1359912421",
//            "3125421639",
//            "1293138521",
//            "2311944581"
//        };

//        public override List<clsPosition> Convert(List<string> Input)
//        {
//            var lliInput = Helper.Convertor_LLI(Input);

//            List<clsPosition> oMap = new();
//            Dictionary<(int, int), clsPosition> dConvert = new();
//            for(int r=0;r<lliInput.Count;r++)
//                for(int c=0;c<lliInput[r].Count;c++)
//                {
//                    clsPosition p = new(r, c, lliInput[r][c]);
//                    oMap.Add(p);
//                    dConvert.Add((r, c), p);
//                    if (r!= 0)
//                        p.AddNeighbor(dConvert[(r - 1, c)]);
//                    if (c != 0)
//                        p.AddNeighbor(dConvert[(r, c-1)]);
//                }
//            return oMap;
//        }

//        public List<clsPosition> oMap;
//        public cls15_Astar Astar;

//        public Day15()
//        {
//            Astar = new();
//            //oMap = Input;
//            oMap = Test;
//        }

//        public override void Q1()
//        {
//            (List<clsPosition> loPath, int risk) = Astar.A_star(oMap[0], oMap.Last());
//            Console.WriteLine(risk);
//        }

//        public override void Q2()
//        {
//            var lliInput = Input;
//            //var lliInput = Test;

           

//        }

//    }

//    public class cls15_Astar : clsAstar<clsPosition>
//    {
//        public override int h(clsPosition p)
//        {
//            return 0;
//        }
//        public override int d(clsPosition current, clsPosition neighbor)
//        {
//            return neighbor.Risk;
//        }
//    }

//    public class clsPosition: iNode
//    {
//        public int x { get; private set; }
//        public int y { get; private set; }
//        public int Risk { get; private set; }

//        public List<iNode> Neighbors { get; internal set; }
//        //List<iNode> iNode.Neighbors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//        public clsPosition(int iX, int iY, int iRisk)
//        {
//            x = iX;
//            y = iY;
//            Risk = iRisk;
//            Neighbors = new();
//        }

//        //public void AddNeighbor(clsPosition neighbor, bool xAddMeToNeighbor = true)
//        //{
//        //    Neighbors.Add(neighbor);
//        //    if(xAddMeToNeighbor) 
//        //        neighbor.AddNeighbor(this,false);
//        //}

//        public override string ToString()
//        {
//            return $"({x},{y})";
//        }
//    }

//}
