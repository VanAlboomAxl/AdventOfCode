using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day16 : Day
    {
        public override int _iDay { get { return 16; } }

        public class clsValve
        {
            public string Name { get; private set; }
            public long FlowRate { get; private set; }
            public List<string> lsPipes { get; private set; }
            public List<clsValve> Pipes { get; private set; }
            public clsValve(string s)
            {
                //string sRegexString = @"Valve ([A-Z]+) has flow rate=(\d+); tunnel[s]* lead[s]* to valve[s]* ([A-Z]+)(, [A-Z]+)*";
                string sRegexString = @"Valve ([A-Z]+) has flow rate=(\d+); tunnel[s]* lead[s]* to valve[s]* (.+)";
                Regex rg = new Regex(sRegexString);
                GroupCollection matches = rg.Matches(s)[0].Groups;
                Name = matches[1].Value;
                FlowRate = long.Parse(matches[2].Value);
                Pipes = new();
                lsPipes = matches[3].Value.Replace(" ", "").Split(",").ToList();
                
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public override string Q1()
        {

            day16_redditSolution solution = new();
            return solution.PartTwo(Data).ToString();


            return null; //skip to do part 2
            var lsInput = Data;
            List<clsValve> Valves = new();
            foreach(var s in lsInput) Valves.Add(new(s));
            foreach (var v in Valves) foreach (var s in v.lsPipes) v.Pipes.Add(Valves.Where(x => x.Name.Equals(s)).First());
            clsValve current = Valves.Where(v => v.Name.Equals("AA")).First();
            return logic(30,0,current,null, new(), Valves.Where(x=>x.FlowRate>0).Count()).ToString();
        }
        //don't work 
        public long logic_v1(int iMinutesLeft, long pressureReleased, clsValve current, clsValve previous, List<clsValve> openValves, int iMaxOpenValves)
        {
            if (openValves.Count == iMaxOpenValves)
            {
                while (iMinutesLeft > 0)
                {
                    iMinutesLeft--; //move
                    pressureReleased += openValves.Sum(x => x.FlowRate);
                    if (iMinutesLeft == 0) return pressureReleased;
                }
            }

            long lMaxPressureReleased = 0;
            foreach(var v in current.Pipes)
            {
                if (v.Equals(previous) && current.Pipes.Count >1) continue;

                int iMinsLeft = iMinutesLeft;
                List<clsValve> myopenValves = new(openValves);
                // move to v
                iMinsLeft--; //move
                pressureReleased += myopenValves.Sum(x => x.FlowRate);
                if (iMinsLeft == 0) 
                    return pressureReleased;
                //opening
                if (v.FlowRate >0 && !myopenValves.Contains(v))
                {

                    for(int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            // don't open this valve
                        }
                        else
                        {
                            // open this valve
                            iMinsLeft--;
                            pressureReleased += myopenValves.Sum(x => x.FlowRate);
                            if (iMinsLeft == 0)
                                return pressureReleased;
                            myopenValves.Add(v);
                        }
                        // don't open this valve
                        long lTotalReleased = logic(iMinsLeft, pressureReleased, v, current, myopenValves, iMaxOpenValves);
                        if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;
                    }

                }
                else
                {
                    long lTotalReleased = logic(iMinsLeft, pressureReleased, v,current, myopenValves, iMaxOpenValves);
                    if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased= lTotalReleased;
                }
            }
            //if (iMinsLeft == iMinutesLeft)
            //{
            //    while(iMinutesLeft > 0)
            //    {
            //        iMinutesLeft--; //move
            //        lMaxPressureReleased += openValves.Sum(x => x.FlowRate);
            //        if (iMinutesLeft == 0) return lMaxPressureReleased;
            //        iMinutesLeft--; //move
            //        lMaxPressureReleased += openValves.Sum(x => x.FlowRate);
            //        if (iMinutesLeft == 0) return lMaxPressureReleased;
            //    }
            //}
            return lMaxPressureReleased;
        }
        
        public long logic(int iMinutesLeft, long pressureReleased, clsValve current, clsValve previous, List<clsValve> openValves, int iMaxOpenValves)
        {

            if (openValves.Count == iMaxOpenValves)
            {
                return wait(iMinutesLeft, pressureReleased, current, previous, openValves, iMaxOpenValves);
            }
            else
            {
                long lMaxPressureReleased = 0;
                long lTotalReleased = 0;
                //lTotalReleased = wait(iMinutesLeft, pressureReleased, current, previous,  new(openValves), iMaxOpenValves);
                //if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;

                if (!openValves.Contains(current) && current.FlowRate >0)
                {
                    lTotalReleased = openValve(iMinutesLeft, pressureReleased, current, previous, new(openValves), iMaxOpenValves);
                    if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;
                }

                foreach (var v in current.Pipes)
                {
                    if (v.Equals(previous) && current.Pipes.Count > 1) continue;
                    lTotalReleased = moveTo(iMinutesLeft, pressureReleased, v, current, new(openValves), iMaxOpenValves);
                    if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;
                }

                return lMaxPressureReleased;

            }
        }
        public long wait(int iMinutesLeft, long pressureReleased, clsValve current, clsValve previous, List<clsValve> openValves, int iMaxOpenValves)
        {
            iMinutesLeft--;
            pressureReleased += openValves.Sum(x => x.FlowRate);
            if (iMinutesLeft == 0) return pressureReleased;
            return logic(iMinutesLeft, pressureReleased, current, previous, openValves, iMaxOpenValves);
        }
        public long moveTo(int iMinutesLeft, long pressureReleased, clsValve current, clsValve previous, List<clsValve> openValves, int iMaxOpenValves)
        {
            iMinutesLeft--;
            pressureReleased += openValves.Sum(x => x.FlowRate);
            if (iMinutesLeft == 0) return pressureReleased;
            return logic(iMinutesLeft, pressureReleased, current, previous, openValves, iMaxOpenValves);
        }
        public long openValve(int iMinutesLeft, long pressureReleased, clsValve current, clsValve previous, List<clsValve> openValves, int iMaxOpenValves)
        {
            iMinutesLeft--;
            pressureReleased += openValves.Sum(x => x.FlowRate);
            if (iMinutesLeft == 0) return pressureReleased;
            openValves.Add(current);
            return logic(iMinutesLeft, pressureReleased, current, previous, openValves, iMaxOpenValves);
        }
        
        public override string Q2()
        {
            var lsInput = Data;
            List<clsValve> Valves = new();
            foreach (var s in lsInput) Valves.Add(new(s));
            foreach (var v in Valves) foreach (var s in v.lsPipes) v.Pipes.Add(Valves.Where(x => x.Name.Equals(s)).First());
            clsValve current = Valves.Where(v => v.Name.Equals("AA")).First();
            return logic_2(26, 0, new() { current, current }, new(), Valves.Where(x => x.FlowRate > 0).Count()).ToString();
        }
        //takes to long --> do different
        public long logic_2(int iMinutesLeft, long pressureReleased, List<clsValve> currents, List<clsValve> openValves, int iMaxOpenValves)
        {
            if (openValves.Count == iMaxOpenValves)
            {
                while(true)
                {
                    iMinutesLeft--;
                    pressureReleased += openValves.Sum(x => x.FlowRate);
                    if (iMinutesLeft == 0) return pressureReleased;
                }
            }
            else
            {
                iMinutesLeft--;
                pressureReleased += openValves.Sum(x => x.FlowRate);
                if (iMinutesLeft == 0) return pressureReleased;

                long lMaxPressureReleased = 0;
                long lTotalReleased = 0;

                // 4 possibilities 

                // 1 both move
                int iMinsLeft = iMinutesLeft;
                List<clsValve> myOpenValves = new(openValves);
                foreach(var p1 in currents[0].Pipes)
                    foreach(var p2 in currents[1].Pipes)
                    {
                        lTotalReleased = logic_2(iMinutesLeft, pressureReleased, new() { p1,p2 }, new(openValves), iMaxOpenValves);
                        if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;
                    }

                // 2 1 move, 1 open
                if (!openValves.Contains(currents[1]) && currents[1].FlowRate >0)
                {
                    iMinsLeft = iMinutesLeft;
                    myOpenValves = new(openValves);
                    myOpenValves.Add(currents[1]);
                    foreach (var p1 in currents[0].Pipes)
                    {
                        lTotalReleased = logic_2(iMinutesLeft, pressureReleased, new() { p1,currents[1]}, new(myOpenValves), iMaxOpenValves);
                        if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;
                    }
                }


                // 3 1 open, 1 move
                if (!openValves.Contains(currents[0]) && currents[0].FlowRate > 0)
                {
                    iMinsLeft = iMinutesLeft;
                    myOpenValves = new(openValves);
                    myOpenValves.Add(currents[0]);
                    foreach (var p2 in currents[1].Pipes)
                    {
                        lTotalReleased = logic_2(iMinutesLeft, pressureReleased, new() { currents[0], p2 }, new(myOpenValves), iMaxOpenValves);
                        if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;
                    }
                }

                // 4 both open 
                if (currents[0] != currents[1] && 
                    !openValves.Contains(currents[0]) && currents[0].FlowRate > 0 &&
                    !openValves.Contains(currents[1]) && currents[1].FlowRate > 0)
                {
                    iMinsLeft = iMinutesLeft;
                    myOpenValves = new(openValves);
                    myOpenValves.Add(currents[0]);
                    myOpenValves.Add(currents[1]);
                    lTotalReleased = logic_2(iMinutesLeft, pressureReleased, new() { currents[0], currents[1] }, new(myOpenValves), iMaxOpenValves);
                    if (lTotalReleased > lMaxPressureReleased) lMaxPressureReleased = lTotalReleased;
                }

                return lMaxPressureReleased;

            }
        }


    }
    public class Day16_v2 : Day
    {
        public override int _iDay { get { return 16; } }

        public class clsValve
        {
            public string Name { get; private set; }
            public long FlowRate { get; private set; }
            public List<string> lsPipes { get; set; }
            public List<clsPipe> Pipes { get; private set; }
            public clsValve(string s)
            {
                //string sRegexString = @"Valve ([A-Z]+) has flow rate=(\d+); tunnel[s]* lead[s]* to valve[s]* ([A-Z]+)(, [A-Z]+)*";
                string sRegexString = @"Valve ([A-Z]+) has flow rate=(\d+); tunnel[s]* lead[s]* to valve[s]* (.+)";
                Regex rg = new Regex(sRegexString);
                GroupCollection matches = rg.Matches(s)[0].Groups;
                Name = matches[1].Value;
                FlowRate = long.Parse(matches[2].Value);
                Pipes = new();
                lsPipes = matches[3].Value.Replace(" ", "").Split(",").ToList();

            }
            public override string ToString()
            {
                return Name;
            }
        }
        public class clsPipe
        {
            public clsValve valve { get; private set; }
            public int time { get; private set; }
            public clsPipe(clsValve valve, int time)
            {
                this.valve = valve;
                this.time = time;
            }
            public override string ToString()
            {
                return $"{valve}:{time}";
            }
        }

        public override string Q1()
        {
            //return null; //skip to do part 2
            var lsInput = Data;
            List<clsValve> AllValves = new();
            List<clsValve> Valves = new();
            clsValve current = null;
            foreach (var s in lsInput)
            {
                clsValve v = new(s);
                if (v.Name.Equals("AA")) current = v;
                Valves.Add(v); 
                AllValves.Add(v); 
            }
            /*while(Valves.Count > 0)
            {
                clsValve v = Valves[0]; Valves.RemoveAt(0);
                List<string> pipesToCheck = new();
                while (v.lsPipes.Count > 0)
                {
                    string sPipe = v.lsPipes[0]; v.lsPipes.RemoveAt(0); 
                    var valve = AllValves.Where(x => x.Name.Equals(sPipe)).First();
                    if (valve.FlowRate > 0)
                    {
                        v.Pipes.Add(new(valve, 1));
                    }
                    else if (valve.lsPipes.Count == 0)
                    {
                        foreach(var p in valve.Pipes)
                            if(p.valve != v)
                                v.Pipes.Add(new(p.valve, p.time+1));
                    }
                    else
                    {
                        pipesToCheck.Add(sPipe);
                    }
                }
                v.lsPipes = pipesToCheck;
                if(v.lsPipes.Count > 0)
                {
                    Valves.Add(v);
                }
            }*/

            foreach (var v in Valves) 
                foreach (var s in v.lsPipes)            
                    v.Pipes.Add(new(Valves.Where(x => x.Name.Equals(s)).First(), 1));
                
            foreach (var v in Valves)
                foreach (var v2 in Valves)
                    if (v != v2 && v2.FlowRate > 0 && v.Pipes.Where(x=>x.valve==v2).FirstOrDefault() == null)
                        v.Pipes.Add(new(v2, Astar(v, v2)));

            //get alle valves that need to be opened --> input for logic
            
            return null;
            //return logic(30, 0, current, null, new(), Valves.Where(x => x.FlowRate > 0).Count()).ToString();
        }

        int Astar(clsValve start, clsValve goal)
        {
            Dictionary<clsValve, int> gScore = new();
            Queue<clsValve> openSet = new();
            openSet.Enqueue(start);
            gScore[start] = 0;
            while (openSet.TryDequeue(out clsValve current))
            {
                foreach (var neighbor in current.Pipes)
                {
                    int tentative_gScore = gScore[current] + neighbor.time;
                    if (tentative_gScore < gScore.GetValueOrDefault(neighbor.valve, int.MaxValue))
                    {
                        gScore[neighbor.valve] = tentative_gScore;
                        openSet.Enqueue(neighbor.valve);
                    }

                }

            }
            return  gScore[goal];
        }

        int logic(int iTimeLeft , clsValve current)
        {

        }



        public override string Q2()
        {
            return null;
        }

    }

}
