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

        public static List<List<int>> Convertor_LLI(List<string> lsInput)
        {
            List<List<int>> lliInput = new();
            foreach (var s in lsInput)
            {
                List<int> li = new();
                foreach (var c in s)
                    li.Add(int.Parse(c.ToString()));
                lliInput.Add(li);
            }
            return lliInput;
        }

        public List<T> CopyList<T>(List<T> ListIn)
        {
            T[] ArrayOut = new T[ListIn.Count()];
            ListIn.CopyTo(ArrayOut);
            return ArrayOut.ToList();
        }

    }
}
