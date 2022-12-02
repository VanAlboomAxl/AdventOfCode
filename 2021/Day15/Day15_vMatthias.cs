using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021.Matthias
{

    public class Day15 : Day<List<Position>>
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

        public override List<Position> Convert(List<string> Input)
        {
            var lliInput = Helper.Convertor_LLI(Input);

            List<Position> oMap = new();
            Dictionary<(int, int), Position> dConvert = new();
            for(int r=0;r<lliInput.Count;r++)
                for(int c=0;c<lliInput[r].Count;c++)
                {
                    Position p = new(c, r, lliInput[r][c]);
                    oMap.Add(p);
                    dConvert.Add((c, r), p);
                    if (c!= 0)
                        p.AddNeighbor(dConvert[(c - 1, r)]);
                    if (r != 0)
                        p.AddNeighbor(dConvert[(c, r-1)]);
                }
            return oMap;
        }

        public List<Position> oMap;
        public Day15()
        {
            oMap = Input;
            //oMap = Test;
        }

        public override void Q1()
        {
            var start = oMap[0];
            var goal = oMap.Last();
            Func<Position, int> CostFunction = s => s.Value;
            List<Position> loPath = Astar(start, goal, CostFunction);
            Console.WriteLine((loPath.Sum(x => x.Value) - oMap[0].Value).ToString());
        }

        public override void Q2()
        {

            //UpdateMapForQ2(Helper.Convertor_LLI(_lsTest));
            UpdateMapForQ2(Helper.Convertor_LLI(Helper.ReadInput(InputLocation)));

            //WriteMap();

            var start = oMap[0];
            var goal = oMap.Last();
            Func<Position, int> CostFunction = s => s.Value;
            List<Position> loPath = Astar(start, goal, CostFunction);
            
            //Console.WriteLine(risk);
            Console.WriteLine((loPath.Sum(x => x.Value) - oMap[0].Value).ToString()); 
        }
        
        private void UpdateMapForQ2(List<List<int>> lliInput)
        {

            int irangeX = lliInput[0].Count();
            int irangeY = lliInput.Count();

            List<Position> oNewMap = new();
            Dictionary<(int, int), Position> dConvert = new();
            for (int r = 0; r < lliInput.Count; r++)
                for (int c = 0; c < lliInput[r].Count; c++)
                {
                    Position p = new(c, r, lliInput[r][c]);
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

                        Position oPrev = dConvert[(c, yPrev)];

                        int y = j * irangeY + r;
                        int iRisk = oPrev.Value + 1;
                        if (iRisk > 9) iRisk = 1;
                        liRow.Add(iRisk);
                        Position p = new(c, y, iRisk);
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

                        Position oPrev = dConvert[(xPrev, r)];

                        int x = i * lliInput[r].Count + c;

                        int iRisk = oPrev.Value + 1;
                        if (iRisk > 9) iRisk = 1;

                        Position p = new(x, r, iRisk);
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
       
        public List<T> Astar<T>(T start, T goal, Func<T, int> CostFunction) where T : Position
        {
            Queue<T> openSet = new();
            openSet.Enqueue(start);
            Dictionary<T, T> cameFrom = new();

            Dictionary<T, int> gScore = new();
            gScore[start] = 0;

            while (openSet.TryDequeue(out T cur))
            {
                if (cur.Equals(goal))
                {
                    return reconstruct_path(cameFrom, cur);
                }

                foreach (T item in cur.Neighbors)
                {
                    if (item.Equals(goal))
                    {

                    }
                    var tentGScore = gScore[cur] + CostFunction(item);
                    if (tentGScore < gScore.GetValueOrDefault(item, int.MaxValue))
                    {
                        cameFrom[item] = cur;
                        gScore[item] = tentGScore;
                        openSet.Enqueue(item);
                        //openSet = openSet.OrderBy(x => fScore[x]).ToList();
                    }
                }

            }

            return null;
        }

        public List<T> reconstruct_path<T>(Dictionary<T, T> cameFrom, T current)
        {
            List<T> totalPath = new() { current };
            while (cameFrom.TryGetValue(current, out current))
            {
                totalPath.Insert(0, current);
            }
            return totalPath;

        }

        

    }
    public class Position
    {
        public List<Position> Neighbors { get; private set; }
        public override string ToString()
        {
            return $"({X},{Y})";
        }
        public Position(int x, int y, int value)
        {
            Neighbors = new();
            X = x;
            Y = y;
            Value = value;
            while (Value > 9)
            {
                Value -= 9;
            }


        }

        public void AddNeighbor(Position neig, bool x = true)
        {
            Neighbors.Add(neig);
            if (x) neig.AddNeighbor(this, false);
        }

        public int manhattan(Position other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }

        public void FindPositionsAround(Dictionary<(int, int), Position> grid)
        {
            positionsAround = new List<Position>();
            if (grid.TryGetValue((X - 1, Y), out Position left)) positionsAround.Add(left);
            if (grid.TryGetValue((X, Y - 1), out Position above)) positionsAround.Add(above);
            if (grid.TryGetValue((X + 1, Y), out Position right)) positionsAround.Add(right);
            if (grid.TryGetValue((X, Y + 1), out Position below)) positionsAround.Add(below);
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Value { get; private set; }

        public List<Position> positionsAround { get; set; }
    }

}
