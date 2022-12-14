using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day13 : Day
    {
        public override int _iDay { get { return 13; } }

        public override string Q1()
        {
            var lsInput = Data;
            int iPair = 1;
            int iResult = 0;
            for (int i = 0; i < Data.Count; i += 3)
            {
                int j = 0; int k = 0;
                clsList l1 = convert(Data[i], ref j);
                clsList l2 = convert(Data[i + 1], ref k);
                if (logic(l1, l2) != false) 
                    iResult += iPair;
                iPair++;
            }
            return iResult.ToString();
        }
        public clsList convert(string s, ref int i)
        {
            clsList me = new();
            while(i< s.Length)
            {
                i++;
                char c = s[i];
                if (c.Equals('[')) me.list.Add(convert(s, ref i));
                else if (c.Equals(']')) return me;
                else if (c.Equals(',')){ }
                else
                {
                    string number = string.Concat(s.Skip(i).TakeWhile(char.IsDigit));
                    i += number.Length - 1; 
                    me.list.Add(new clsInteger(int.Parse(number)));
                }
            }
            return me;
        }
        public bool? logic(clsList l1, clsList l2)
        {
            for(int i=0; i< l1.list.Count; i++)
            {
                clsBase b1 = l1.list[i];
                if (l2.list.Count <= i)
                {
                    //list run out
                    return false;
                }
                clsBase b2 = l2.list[i];
                if (b1 is clsInteger i1)
                {
                    if (b2 is clsInteger i2) 
                    {
                        if (i1.value > i2.value) 
                            return false;
                        else if (i1.value < i2.value) 
                            return true;
                        else
                        {
                            // go to next item
                        }
                    }
                    else
                    {
                        var subList = new clsList();
                        subList.list.Add(i1);
                        bool? result = logic(subList, (clsList)b2);
                        if (result == true) return true;
                        if (result == false) return false;
                    }
                }
                else
                {
                    if (b2 is clsInteger i2)
                    {
                        var subList = new clsList();
                        subList.list.Add(i2);
                        bool? result = logic((clsList)b1, subList);
                        if (result == true) 
                            return true;
                        if (result == false) 
                            return false;
                    }
                    else
                    {
                        bool? result = logic((clsList)b1, (clsList)b2);
                        if (result == true)
                            return true;
                        if (result == false) 
                            return false;
                    }

                }
            }
            if (l1.list.Count < l2.list.Count) return true;
            return null;
        }

        public override string Q2()
        {
            var lsInput = Data;
            List<clsList> packets = new();
            for (int i = 0; i < Data.Count; i += 3)
            {
                int j = 0; int k = 0;
                clsList l1 = convert(Data[i], ref j);
                clsList l2 = convert(Data[i + 1], ref k);

                packets.Add(l1); 
                packets.Add(l2);
            }
            clsList dd1 = new clsList(); dd1.list.Add(new clsInteger(2));
            clsList d1 = new clsList(); d1.list.Add(dd1);
            clsList dd2 = new clsList(); dd2.list.Add(new clsInteger(6));
            clsList d2 = new clsList(); d2.list.Add(dd2);
            packets.Add(d1);
            packets.Add(d2);

            packets.Sort(
                delegate (clsList l1, clsList l2)
                {
                    if (logic(l1, l2) != false)
                        return -1;
                    return 1;
                });

            int iResult = (packets.FindIndex(x=>x==d1)+1) * ( packets.FindIndex(x=>x==d2)+1);

            return iResult.ToString();

        }

        public class clsBase { }
        public class clsList:clsBase
        {
            public List<clsBase> list { get; private set; }
            public clsList()
            {
                list = new();
            }
            public override string ToString()
            {
                return $"[{String.Join(',', list)}]";
            }
        }
        public class clsInteger: clsBase
        {
            public int value { get; set; }
            public clsInteger(int value)
            {
                this.value = value;
            }
            public override string ToString()
            {
                return value.ToString();
            }
        }

    }
}
