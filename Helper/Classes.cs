using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.BEC;
namespace Helper
{
    public class Classes
    {

        public static IList<clsInterval> Merge(IList<clsInterval> intervals)
        {
            var outVal = new SortedSet<clsInterval>(intervals);
            for (int i = 0; i < outVal.Count - 1; i++)
            {
                clsInterval curr = outVal.ElementAt(i);
                clsInterval next = outVal.ElementAt(i + 1);
                if (next.start - curr.end < 1)
                {
                    curr.end = Math.Max(curr.end, next.end);
                    outVal.Remove(next);
                    i--;
                }
            }
            return outVal.ToList();
        }

    }
}
