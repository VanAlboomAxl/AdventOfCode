using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day16 : Day
    {
        public override int _iDay { get { return 16; } }

        public override void Q1()
        {
            int j = 0;
            List<clsField> Fields = new();
            List<clsTicket> Tickets = new();
            for(int i=0;i<Input.Count;i++)
            {
                string s = Input[i];
                if (string.IsNullOrEmpty(s))
                {
                    j++;
                    i++;
                }
                else
                {
                    if (j == 0) Fields.Add(new(s));
                    else if (j == 2) Tickets.Add(new(s));
                }
            }

            List<int> NotFound = new();
            foreach(clsTicket t in Tickets)
                foreach(int v in t.Values)
                {
                    var xFound = false;
                    foreach(var f in Fields)
                    {
                        if (f.CheckInRange(v))
                        {
                            xFound = true;
                            break;
                        }
                    }
                    if (!xFound)
                        NotFound.Add(v);
                }
            Console.WriteLine(NotFound.Sum());
        }

        public override void Q2()
        {
            int j = 0;
            Fields = new();
            List<clsTicket> Tickets = new();
            clsTicket me = null;

            //var lData = Helper.ReadInput($"{FolderLocation}\\test2.txt"); 
            var lData = Input;
            for (int i = 0; i < lData.Count; i++)
            {
                string s = lData[i];
                if (string.IsNullOrEmpty(s))
                {
                    j++;
                    i++;
                }
                else
                {
                    if (j == 0) Fields.Add(new(s));
                    else if (j == 1) me = new(s);
                    else if (j == 2) Tickets.Add(new(s));
                }
            }

            List<clsTicket> ValidTickets = new();
            foreach (clsTicket t in Tickets)
            {
                var xValid = true;
                foreach (int v in t.Values)
                {
                    var xFound = false;
                    foreach (var f in Fields)
                    {
                        if (f.CheckInRange(v))
                        {
                            xFound = true;
                            break;
                        }
                    }
                    if (!xFound)
                    {
                        xValid = false;
                        break;
                    }
                }
                if (xValid) ValidTickets.Add(t);
            }

            dMapping = new();
            dPossibilities = new();

            for(int i = 0; i < ValidTickets[0].Values.Count; i++)
            {
                List<int> FieldValues = new();
                foreach (clsTicket t in ValidTickets)
                    FieldValues.Add(t.Values[i]);

                var Results = GetPossibleFields(Fields, FieldValues);
                if(Results.Count == 1)
                {
                    FoundMapping(Results[0], i);
                    //dMapping.Add(Results[0], i);
                    //Fields.Remove(Results[0]);
                    //foreach(var k in dPossibilities.Keys)
                    //{
                    //    var val = dPossibilities[k];
                    //    if (val.Contains(Results[0]))
                    //    {
                    //        val.Remove(Results[0]);
                    //        if (val.Count==1)
                    //        {
                    //            dMapping.Add(val[0], k);
                    //            Fields.Remove(val[0]);
                    //        }
                    //    }
                    //}
                }
                else
                {
                    dPossibilities.Add(i, Results);
                }
            }

            long lResult = 1;
            foreach(clsField f in dMapping.Keys)
            {
                if (f.Name.StartsWith("departure"))
                    lResult *= me.Values[dMapping[f]];
                
            }

        }

        private List<clsField> Fields;
        private Dictionary<clsField, int> dMapping;
        private Dictionary<int, List<clsField>> dPossibilities;
        
        private List<clsField> GetPossibleFields(List<clsField> Fields, List<int> TicketValues)
        {
            List<clsField> loResult = new();
            foreach(var f in Fields)
            {
                var xPossible = true;
                foreach(var v in TicketValues)
                {
                    if (!f.CheckInRange(v))
                    {
                        xPossible = false;
                        break;
                    }
                }
                if (xPossible)
                    loResult.Add(f);
            }
            return loResult;
        }

        private void FoundMapping(clsField f, int iIndex)
        {
            dMapping.Add(f, iIndex);
            Fields.Remove(f);

            foreach (var k in dPossibilities.Keys)
            {
                var val = dPossibilities[k];
                if (val.Contains(f))
                {
                    val.Remove(f);
                    if (val.Count == 1)
                    {
                        FoundMapping(val[0], k);
                    }
                }
            }
        }

        public class clsField
        {
            public string Name { get; private set; }
            public List<(int,int)> Ranges { get; private set; }
            public clsField(string sField)
            {
                Ranges = new();
                string[] Split1 = sField.Split(':');
                Name = Split1[0];
                var Split2 = Split1[1].Trim().Split("or");
                foreach(var s in Split2)
                {
                    string[] Split3 = s.Split('-');
                    Ranges.Add((int.Parse(Split3[0]), int.Parse(Split3[1])));
                }
            }

            public bool CheckInRange(int i)
            {
                foreach((int min, int max) in Ranges)
                    if (i >= min && i <= max)
                        return true;         
                return false;
            }

            public override string ToString()
            {
                string s = "";
                for (int i = 0; i < Ranges.Count - 1; i++)
                    s += $"[{Ranges[i].Item1},{Ranges[i].Item2}],";
                s += $"[{Ranges[Ranges.Count-1].Item1},{Ranges[Ranges.Count - 1].Item2}]";

                return $"{Name} {s}";
            }
        }

        public class clsTicket
        {
            public List<int> Values { get; private set; }
            public clsTicket(string sTicket)
            {
                Values = sTicket.Split(",").Select(x=>int.Parse(x)).ToList();
            }
            public override string ToString()
            {
                return string.Join(",", Values.Select(n => n.ToString()).ToArray());
            }
        }

    }
}
