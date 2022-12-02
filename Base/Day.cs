using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public abstract class Day
    {

        public int Year { get { return -1; } }
        public string Location { get { return @"Y:\Repositories\AdventOfCode"; } }

        public abstract int _iDay { get; }

        public string FolderLocation
        {
            get
            {
                if (_iDay < 10)
                    return $"{Location}\\{Year}\\Day0{_iDay}";
                return $"{Location}\\{Year}\\Day{_iDay}";
            }
        }

        public string InputLocation { get { return $"{FolderLocation}\\input.txt";  } }
        public List<string> Input   { get { return Helper.ReadInput(InputLocation); } }

        public string TestLocation { get { return $"{FolderLocation}\\test.txt";  } }
        public List<string> Test   { get { return Helper.ReadInput(TestLocation); } }

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
