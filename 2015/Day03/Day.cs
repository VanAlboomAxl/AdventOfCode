using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day3 : Day
    {
        public override int _iDay { get { return 3; } }

        public override string Q1()
        {
            var lsInput = Data[0];
            Dictionary<(int, int), int> map = new();
            int x = 0, y = 0;
            map.Add((0, 0), 1);
            foreach(char c in lsInput)
            {
                if (c == '>') x++;
                if (c == '<') x--;
                if (c == '^') y++;
                if (c == 'v') y--;
                if (map.ContainsKey((x, y))) map[(x,y)] = map[(x, y)]++;
                else map.Add((x, y), 1);
            }
            return map.Keys.Count.ToString();
        }

        public override string Q2()
        {
            var lsInput = Data[0];
            Dictionary<(int, int), int> map = new();
            int x = 0, y = 0;
            map.Add((0, 0), 2);
            for(int i = 0; i < lsInput.Length;i+=2)
            {
                char c = lsInput[i];
                if (c == '>') x++;
                if (c == '<') x--;
                if (c == '^') y++;
                if (c == 'v') y--;
                if (map.ContainsKey((x, y))) map[(x, y)] = map[(x, y)]++;
                else map.Add((x, y), 1);
            }
            x = 0; y = 0; 
            for (int i = 1; i < lsInput.Length; i += 2)
            {
                char c = lsInput[i];
                if (c == '>') x++;
                if (c == '<') x--;
                if (c == '^') y++;
                if (c == 'v') y--;
                if (map.ContainsKey((x, y))) map[(x, y)] = map[(x, y)]++;
                else map.Add((x, y), 1);
            }
            return map.Keys.Count.ToString();
        }
    }
}
