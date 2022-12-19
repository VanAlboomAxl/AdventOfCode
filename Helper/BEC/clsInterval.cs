using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.BEC
{
    public class clsInterval : IComparable<clsInterval>
    {
        public long start { get; set; }
        public long end { get; set; }
        public clsInterval(long start, long end)
        {
            this.start = start;
            this.end = end;
        }

        public int CompareTo(clsInterval other)
        {
            if (start == other.start)
            {
                return end.CompareTo(other.end);
            }
            return start.CompareTo(other.start);

            //if (start == other.start)
            //{
            //    if (end > other.end) return 1;
            //    else if (end < other.end) return -1;
            //    else return 0;
            //}
            //if (start > other.start) return 1;
            //else if (start < other.start) return -1;
            //else return 0;
        }
        public override string ToString()
        {
            return $"[{start},{end}]";
        }

    }
}
