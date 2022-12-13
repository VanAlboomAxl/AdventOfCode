using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Helper
    {

        public static List<string> ReadInput(string sLocation)
        {
            return System.IO.File.ReadAllLines(sLocation).ToList();
        }

  

        public List<T> CopyList<T>(List<T> ListIn)
        {
            T[] ArrayOut = new T[ListIn.Count()];
            ListIn.CopyTo(ArrayOut);
            return ArrayOut.ToList();
        }

    }
}
