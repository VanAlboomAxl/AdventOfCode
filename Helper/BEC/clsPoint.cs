using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.BEC
{

    public class clsPoint
    {
        public long X { get; private set; }
        public long Y { get; private set; }
        public clsPoint(long x, long y)
        {
            X = x; Y = y;
        }
        public override bool Equals(object obj)
        {
            if (obj is clsPoint p) return Equals(p);
            return false;
        }
        public bool Equals(clsPoint other)
        {
            if (other.X == X && other.Y == Y) return true;
            return false;
        }
        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
        public long manhattenDistance(clsPoint p)
        {
            return Math.Abs(X - p.X) + Math.Abs(Y - p.Y);
        }
        public HashSet<clsPoint> pointsInManhattenDistance(long distance)
        {
            HashSet<clsPoint> map = new();
            for (long x = -distance; x <= distance; x++)
            {
                long dy = distance - Math.Abs(x);
                for (long y = -dy; y <= dy; y++)
                    map.Add(new(X + x, Y + y));
            }
            return map;
        }
    }
}
