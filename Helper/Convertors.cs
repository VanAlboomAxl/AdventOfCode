using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Convertors
    {
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
        public static List<List<long>> Numbers(List<string> lsInput)
        {
            List<List<long>> lliInput = new();
            foreach (var s in lsInput)
            {
                List<long> li = new();
                foreach (var c in s)
                    li.Add(long.Parse(c.ToString()));
                lliInput.Add(li);
            }
            return lliInput;
        }

        public static List<long> Number(List<string> lsInput)
        {
            List<long> result = new();
            foreach(var s in lsInput)
            {
                long l = -1;
                if (!string.IsNullOrEmpty(s))
                    l = long.Parse(s);
                result.Add(l);
            }
            return result;
        }
    }
}
