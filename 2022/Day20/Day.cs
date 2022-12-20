using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day20 : Day
    {
        public override int _iDay { get { return 20; } }

        List<num> convert(List<string> data, bool xUpdate = false)
        {
            List<num> result = new();
            foreach (var s in data) result.Add(new(long.Parse(s),xUpdate));
            return result;
        }
        public override string Q1()
        {
            List<num> data = convert(Data);
            List<num> dataOriginal = new(data);
            num n0 = null;
            foreach(var n in dataOriginal)
            {
                if (n.val == 0) n0 = n;
                int index = data.IndexOf(n);
                data.RemoveAt(index);
                long iNewIndex = index + n.val;

                while(iNewIndex<=0 || iNewIndex>= dataOriginal.Count)
                {
                    if (iNewIndex <= 0) iNewIndex += dataOriginal.Count - 1;
                    else if (iNewIndex >=dataOriginal.Count) iNewIndex -= (dataOriginal.Count - 1);
                }

                //if (iNewIndex <= 0) iNewIndex += dataOriginal.Count - 1;
                //else if (iNewIndex >=dataOriginal.Count) iNewIndex -= (dataOriginal.Count - 1);
                data.Insert((int)iNewIndex, n);
            }

            long iResult = 0;
            int[] aiChecks = new int[] { 1000, 2000, 3000 };
            foreach(var i in aiChecks)
            {
                int index = (data.IndexOf(n0) + i) % data.Count;
                if (index >= dataOriginal.Count) index -= (dataOriginal.Count - 1);
                iResult += data[index].val;
            }

            return iResult.ToString();
        }

        public override string Q2()
        {
            // index is 1 miss, but follow is right --> which matters
            List<num> data = convert(Data,true);
            List<num> dataOriginal = new(data);
            num n0 = null;

            for (int i = 0; i < 10; i++)
                foreach (var n in dataOriginal)
                {
                    if (n.val == 0) n0 = n;

                    long iMoves = n.val;

                    //each length -1 you get back on same list
                    iMoves = iMoves % (dataOriginal.Count - 1);

                    int index = data.IndexOf(n);
                    data.RemoveAt(index);
                    long iNewIndex = index + iMoves;

                    while (iNewIndex <= 0 || iNewIndex >= dataOriginal.Count)
                    {
                        if (iNewIndex <= 0) iNewIndex += dataOriginal.Count - 1;
                        else if (iNewIndex >= dataOriginal.Count) iNewIndex -= (dataOriginal.Count - 1);
                    }

                    //if (iNewIndex <= 0) iNewIndex += dataOriginal.Count - 1;
                    //else if (iNewIndex >=dataOriginal.Count) iNewIndex -= (dataOriginal.Count - 1);
                    data.Insert((int)iNewIndex, n);
                }

            long iResult = 0;
            int[] aiChecks = new int[] { 1000, 2000, 3000 };
            foreach (var i in aiChecks)
            {
                int index = (data.IndexOf(n0) + i) % data.Count;
                if (index >= dataOriginal.Count) index -= (dataOriginal.Count - 1);
                iResult += data[index].val;
            }

            return iResult.ToString();
        }

        class num
        {
            public long val { get; private set; }
            public num(long v, bool xUpdate)
            {
                val = v;
                if (xUpdate) val *= 811589153;
            }
            
            public override string ToString()
            {
                return val.ToString();
            }
        }
    }
}
