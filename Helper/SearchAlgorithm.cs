using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{

    public interface INeighbors<T>
    {
        public List<T> Neighbors { get; }
        public abstract bool AddNeighbor(T x, bool y);
        public abstract int TravelCost(T x);
        public abstract int StayCost();
    }

    public class SearchAlgorithm
    {

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

}
