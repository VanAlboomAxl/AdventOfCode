using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace AdventOfCode
{
    public class Day01 : Day
    {
        public override int _iDay { get { return 1; } }
      

        public override void Q1()
        {
            var lsInput = Input;
            //lsInput = Test;
            var data = Convertors.Number(lsInput);
            List<long> result = logic(data);
            Console.WriteLine(result.Max());
        }
      


        public override void Q2()
        {
            var lsInput = Input;
            //lsInput = Test;

            var data = Convertors.Number(lsInput);
            List<long> result = logic(data);
            var descendingOrder = result.OrderByDescending(i => i).ToList();
            Console.WriteLine(descendingOrder[0] + descendingOrder[1]+ descendingOrder[2]);

        }

        public List<long> logic(List<long> data)
        {
            List<long> result = new();
            result.Add(0);
            foreach (var d in data)
            {
                if (d < 0) result.Add(0);
                else result[result.Count - 1] = result.Last() + d; ;
            }
            return result;
        }

    }
}
