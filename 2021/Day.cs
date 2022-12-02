using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    public abstract class Day
    {

        public abstract int _iDay { get; }

        public string InputLocation 
        { 
            get 
            {
                if (_iDay<10)
                    return @"Y:\Repositories\AdventOfCode\2021\Day0" + _iDay+@"\Input.txt";                   
                return @"Y:\Repositories\AdventOfCode\2021\Day" + _iDay+@"\Input.txt";
            } 
        }
        public List<string> Input
        {
            get
            {
                return Helper.ReadInput(InputLocation);
            }
        }

        internal abstract List<string> _lsTest { get; }
        public List<string> Test { get { return _lsTest; } }

        public abstract void Q1();
        public abstract void Q2();
        
        
    }

    public abstract class Day<T> : Day
    {
      
        public T Input
        {
            get
            {
                return Convert(base.Input);
            }
        }

        public abstract T Convert(List<string> Input);

        public T Test
        {
            get
            {
                return Convert(base.Test);
            }
        }

    }
}
