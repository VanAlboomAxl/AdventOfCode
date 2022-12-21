using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day19 : Day
    {
        public override int _iDay { get { return 19; } }

        public record Blueprint(int id, int oreRobotCost,int clayRobotCost, (int,int) obsidianRobotCost, (int,int) geodeRobotCost );

        List<Blueprint> convert(List<string> blueprints)
        {
            string sRegexString = @"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.";
            Regex rg = new Regex(sRegexString);
            List<Blueprint> result = new();
            foreach(var s in blueprints)
            {
                GroupCollection matches = rg.Matches(s)[0].Groups;
                result.Add(new(int.Parse(matches[1].Value),
                    int.Parse(matches[2].Value), int.Parse(matches[3].Value),
                    (int.Parse(matches[4].Value), int.Parse(matches[5].Value)),
                    (int.Parse(matches[6].Value), int.Parse(matches[7].Value))));
            }
            return result;
        }
        public override string Q1()
        {
            return "skip to part 2";
            var blueprints = convert(Data);
            long result = 0;
            for(int i=0;i<blueprints.Count;i++)
            {
                var blueprint = blueprints[i];
                result += (i + 1) * logic_v5(blueprint, 24);
            }
            return result.ToString();
        }
        int logic(Blueprint bp, int iMinutes)
        {
            int iMaxOreForRobot = new int[] {bp.oreRobotCost, bp.clayRobotCost,bp.obsidianRobotCost.Item1, bp.geodeRobotCost.Item1 }.Max();
            int iMaxClayForRobot = bp.obsidianRobotCost.Item2;
            int iMaxObsidianForRobot = bp.geodeRobotCost.Item2;
            //int[] materials = { 0, 0, 0, 0 };
            //int[] robots    = { 1, 0, 0, 0 };
            HashSet<(int[], int[])> possibilities = new();
            possibilities.Add((new int[] { 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0 }));
            for (int i=0; i < iMinutes; i++)
            {
                HashSet<(int[], int[])> new_possibilities = new();

                foreach(var p in possibilities)
                {
                    int[] materials = p.Item1;
                    int[] robots    = p.Item2;
                    if (materials[0] >= bp.oreRobotCost && robots[0]< iMaxOreForRobot) 
                    {
                        new_possibilities.Add((
                            new int[] { materials[0]- bp.oreRobotCost + robots[0], materials[1] + robots[1], materials[2] + robots[2], materials[3] + robots[3] },
                            new int[] { robots[0]+ 1, robots[1], robots[2], robots[3] }
                        ));
                    }
                    if (materials[0] >= bp.clayRobotCost && robots[1] < iMaxClayForRobot)
                    {
                        new_possibilities.Add((
                            new int[] { materials[0] - bp.clayRobotCost + robots[0], materials[1] + robots[1], materials[2] + robots[2], materials[3] + robots[3] },
                            new int[] { robots[0] , robots[1]+1, robots[2], robots[3] }
                        ));
                    }
                    if (materials[0] >= bp.obsidianRobotCost.Item1 && materials[1] >= bp.obsidianRobotCost.Item2 && robots[2] < iMaxObsidianForRobot) 
                    {
                        new_possibilities.Add((
                            new int[] { materials[0] - bp.obsidianRobotCost.Item1 + robots[0], materials[1]- bp.obsidianRobotCost.Item2 + robots[1], materials[2] + robots[2], materials[3] + robots[3] },
                            new int[] { robots[0], robots[1] , robots[2]+1, robots[3] }
                        ));
                    }
                    if (materials[0] >= bp.geodeRobotCost.Item1 && materials[2] >= bp.geodeRobotCost.Item2) 
                    {
                        new_possibilities.Add((
                            new int[] { materials[0] - bp.geodeRobotCost.Item1 + robots[0], materials[1] + robots[1], materials[2]- bp.geodeRobotCost.Item2 + robots[2], materials[3]+ robots[3]},
                            new int[] { robots[0], robots[1], robots[2] , robots[3]+1 }
                        ));
                    }
                    new_possibilities.Add((
                        new int[] { materials[0] + robots[0], materials[1] + robots[1], materials[2] + robots[2], materials[3] + robots[3] },
                        new int[] { robots[0], robots[1], robots[2], robots[3] }
                    ));

                }

                possibilities = new_possibilities;
            }
            return 0;
        }
        int logic_v2(Blueprint bp, int iMinutes)
        {
            int iMaxOreForRobot = new int[] { bp.oreRobotCost, bp.clayRobotCost, bp.obsidianRobotCost.Item1, bp.geodeRobotCost.Item1 }.Max();
            int iMaxClayForRobot = bp.obsidianRobotCost.Item2;
            int iMaxObsidianForRobot = bp.geodeRobotCost.Item2;


            HashSet<(int[], int[])> possibilities = new();
            possibilities.Add((new int[] { 0, 0, 0, 0 }, new int[] { 1, 0, 0, 0 }));
            for (int i = 0; i < iMinutes; i++)
            {
                if (i == iMinutes - 6)
                {

                }
                HashSet<(int[], int[])> new_possibilities = new();

                int maxGeodes = 0;
                foreach (var p in possibilities)
                {
                    int[] materials = p.Item1;
                    int[] robots = p.Item2;
                    if (materials[0] >= bp.geodeRobotCost.Item1 && materials[2] >= bp.geodeRobotCost.Item2)
                    {
                        int[] new_materials = new int[] { materials[0] - bp.geodeRobotCost.Item1 + robots[0], materials[1] + robots[1], materials[2] - bp.geodeRobotCost.Item2 + robots[2], materials[3] + robots[3] };
                        int[] new_robots = new int[] { robots[0], robots[1], robots[2], robots[3] + 1 };
                        int posGeodes = cheat(bp, new_materials, new_robots, iMinutes - i-1);
                        //if (posGeodes >= maxGeodes)
                        if (posGeodes > 0)
                        {
                            maxGeodes = posGeodes;
                            new_possibilities.Add((new_materials, new_robots));
                        }
                    }
                    if (materials[0] >= bp.obsidianRobotCost.Item1 && materials[1] >= bp.obsidianRobotCost.Item2 && robots[2] < iMaxObsidianForRobot)
                    {
                        int[] new_materials = new int[] { materials[0] - bp.obsidianRobotCost.Item1 + robots[0], materials[1] - bp.obsidianRobotCost.Item2 + robots[1], materials[2] + robots[2], materials[3] + robots[3] };
                        int[] new_robots = new int[] { robots[0], robots[1], robots[2] + 1, robots[3] };
                        int posGeodes = cheat(bp, new_materials, new_robots, iMinutes - i - 1);
                        //if (posGeodes >= maxGeodes)
                        if (posGeodes > 0)
                        {
                            maxGeodes = posGeodes;
                            new_possibilities.Add((new_materials, new_robots));
                        }
                    }
                    if (materials[0] >= bp.clayRobotCost && robots[1] < iMaxClayForRobot)
                    {
                        int[] new_materials = new int[] { materials[0] - bp.clayRobotCost + robots[0], materials[1] + robots[1], materials[2] + robots[2], materials[3] + robots[3] };
                        int[] new_robots = new int[] { robots[0], robots[1] + 1, robots[2], robots[3] };
                        int posGeodes = cheat(bp, new_materials, new_robots, iMinutes - i - 1);
                        //if (posGeodes >= maxGeodes)
                        if (posGeodes > 0)
                        {
                            maxGeodes = posGeodes;
                            new_possibilities.Add((new_materials, new_robots));
                        }
                    }
                    if (materials[0] >= bp.oreRobotCost && robots[0] < iMaxOreForRobot)
                    {

                        int[] new_materials = new int[] { materials[0] - bp.oreRobotCost + robots[0], materials[1] + robots[1], materials[2] + robots[2], materials[3] + robots[3] };
                        int[] new_robots = new int[] { robots[0] + 1, robots[1], robots[2], robots[3] };

                        int posGeodes = cheat(bp, new_materials, new_robots, iMinutes - i - 1);
                        //if (posGeodes >= maxGeodes)
                        if (posGeodes > 0)
                        {
                            maxGeodes = posGeodes;
                            new_possibilities.Add((new_materials, new_robots));
                        }

                    }
                    int[] new_materials2 = new int[] { materials[0] + robots[0], materials[1] + robots[1], materials[2] + robots[2], materials[3] + robots[3] };
                    int[] new_robots2 = new int[] { robots[0], robots[1], robots[2] , robots[3] };
                    int posGeodes2 = cheat(bp, new_materials2, new_robots2, iMinutes - i - 1);
                    //if (posGeodes2 >= maxGeodes)
                    if (posGeodes2 > 0)
                    {
                        maxGeodes = posGeodes2;
                        new_possibilities.Add((new_materials2, new_robots2));
                    }

                }

                possibilities = new_possibilities.OrderByDescending(x => x.Item2[3]).ToHashSet();
            }
            int iMaxGeodes = 0;
            foreach (var p in possibilities)
                if (p.Item1[3] > iMaxGeodes)
                    iMaxGeodes = p.Item1[3];
            return iMaxGeodes;
        }
        int cheat(Blueprint bp, int[] materials, int[] robots, int iMinutesLeft)
        {
            /*
            And that's right -- so I let my solver cheat. When cheating, the following rule changes are applied:
                Ore costs are set to zero. Geode robots only cost obsidian. Obsidian robots only cost clay. Clay robots are free.
                The factory can produce 1 robot of each type each minute, rather than just 1 robot each minute.

            These rule changes have two very useful properties:
                There is has exactly one optimal solution which is solvable by a very simple greedy algorithm: If you can build a robot, do so.
                It will always produce a number of geodes greater than or equal to the number of geodes produced if the rules are followed properly.
            */
            for(int i = 0; i < iMinutesLeft; i++)
            {
                int[] new_materials =  materials.ToArray();
                int[] new_robots = robots.ToArray();
                new_robots[0]++;
                //if (materials[0] >= bp.clayRobotCost )
                //{
                    //new_materials[0] -= bp.clayRobotCost;
                    new_robots[1]++;
                //}
                if (materials[1] >= bp.obsidianRobotCost.Item2)
                {
                    new_materials[1] -= bp.obsidianRobotCost.Item2;
                    new_robots[2]++;
                }
                if (materials[2] >= bp.geodeRobotCost.Item2)
                {
                    new_materials[2] -= bp.geodeRobotCost.Item2;
                    new_robots[3]++;
                }
                materials = new int[] { new_materials[0] + robots[0], new_materials[1] + robots[1], new_materials[2] + robots[2], new_materials[3] + robots[3] };
                robots = new_robots;
            }
            return materials[3];
        }

        //works, but takes to long
        int logic_v3(Blueprint bp, int iMinutes)
        {
            int[] materials = { 0, 0, 0, 0 };
            int[] robots    = { 1, 0, 0, 0 };
            int iMaxGeodes = 0;
            HashSet<node> ending = new();
            Queue<node> openSet = new();
            openSet.Enqueue(new(materials,robots,iMinutes));
            while (openSet.TryDequeue(out node current))
            {
                bool xFinalCal = false;
                if (current.time == 0)
                {
                    //ending.Add(current);
                    int iFinal = finalGeodes(current);
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    continue;
                }
                if (cheat(bp, current.materials, current.robots, current.time) == 0) continue;

                if (current.robots[2] > 0)
                {
                    //possible to make an geode robot
                    var geoNode = createGeodeNode(bp, current);
                    if (geoNode != null) openSet.Enqueue(geoNode);
                    else
                    {
                        int iFinal = finalGeodes(current);
                        xFinalCal = true;
                        if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    }
                }
                if (current.robots[1] > 0)
                {
                    //possible to make an obsidian robot
                    var obsiNode = createObsidianNode(bp, current);
                    if (obsiNode != null) openSet.Enqueue(obsiNode);
                    else if(!xFinalCal)
                    {
                        int iFinal = finalGeodes(current);
                        xFinalCal = true;
                        if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    }
                }
                // make clay robot
                var clayNode = createClayNode(bp, current);
                if (clayNode != null) 
                {
                    openSet.Enqueue(clayNode);
                    
                }
                else if (!xFinalCal)
                {
                    int iFinal = finalGeodes(current);
                    xFinalCal = true;
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                }

                //make ore robot
                var oreNode = createOreNode(bp, current);
                if (oreNode != null) openSet.Enqueue(oreNode);
                else if (!xFinalCal)
                {
                    int iFinal = finalGeodes(current);
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                }
            }
            return iMaxGeodes;
        }
        //doesn't work anymore
        int logic_v4(Blueprint bp, int iMinutes)
        {
            int[] materials = { 0, 0, 0, 0 };
            int[] robots = { 1, 0, 0, 0 };
            int iMaxGeodes = 0;
            HashSet<node> ending = new();
            Queue<node> openSet = new();
            Dictionary<int, int> gScore = new();
            gScore.Add(1, 0);
            gScore.Add(2, 0);
            gScore.Add(3, 0);
            openSet.Enqueue(new(materials, robots, iMinutes));
            while (openSet.TryDequeue(out node current))
            {
                bool xFinalCal = false;
                if (current.time == 0)
                {
                    //ending.Add(current);
                    int iFinal = finalGeodes(current);
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    continue;
                }
                //if (cheat(bp, current.materials, current.robots, current.time) == 0) continue;

                if (current.robots[2] > 0)
                {
                    //possible to make an geode robot
                    var geoNode = createGeodeNode(bp, current);
                    if (geoNode != null) //openSet.Enqueue(geoNode);
                    {
                        if (geoNode.robots[3] == 1)
                        {
                            // made first obsidi robot --> what time do i have left?
                            //impossible to have better result if time for first obsi robot is lower than best result
                            if (gScore[3] <= geoNode.time)
                            {
                                gScore[3] = geoNode.time;
                                openSet.Enqueue(geoNode);
                            }
                        }
                        else
                        {
                            openSet.Enqueue(geoNode);
                        }
                    }
                    else
                    {
                        int iFinal = finalGeodes(current);
                        xFinalCal = true;
                        if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    }
                }
                if (current.robots[1] > 0)
                {
                    //possible to make an obsidian robot
                    var obsiNode = createObsidianNode(bp, current);
                    if (obsiNode != null) //openSet.Enqueue(obsiNode);
                    {
                        if (obsiNode.robots[2] == 1)
                        {
                            // made first obsidi robot --> what time do i have left?
                            //impossible to have better result if time for first obsi robot is lower than best result
                            if (gScore[2] <= obsiNode.time)
                            {
                                gScore[2] = obsiNode.time;
                                openSet.Enqueue(obsiNode);
                            }
                        }
                        else
                        {
                            openSet.Enqueue(obsiNode);
                        }
                    }    
                    else if (!xFinalCal)
                    {
                        int iFinal = finalGeodes(current);
                        xFinalCal = true;
                        if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    }
                }
                // make clay robot
                var clayNode = createClayNode(bp, current);
                if (clayNode != null)
                {
                    if (clayNode.robots[1] == 1 ) 
                    {
                        // made first clay robot --> what time do i have left?
                        //impossible to have better result if time for first clay robot is lower than best result
                        if(gScore[1] <= clayNode.time)
                        {
                            gScore[1] = clayNode.time;
                            openSet.Enqueue(clayNode);
                        }
                    }
                    else
                    {
                        openSet.Enqueue(clayNode);
                    }
                }
                else if (!xFinalCal)
                {
                    int iFinal = finalGeodes(current);
                    xFinalCal = true;
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                }

                //make ore robot
                var oreNode = createOreNode(bp, current);
                if (oreNode != null) openSet.Enqueue(oreNode);
                else if (!xFinalCal)
                {
                    int iFinal = finalGeodes(current);
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                }
            }
            return iMaxGeodes;
        }
        // works again, a bit faster than 3, but still slow
        // 38s for test data
        // xs for input data
        int logic_v5(Blueprint bp, int iMinutes)
        {
            int[] materials = { 0, 0, 0, 0 };
            int[] robots = { 1, 0, 0, 0 };
            int iMaxGeodes = 0;
            HashSet<node> ending = new();
            Queue<node> openSet = new();

            Dictionary<string, int> gScore = new();

            node start = new(materials, robots, iMinutes);
            gScore.Add(start.ToString(), start.time);
            openSet.Enqueue(start);

            while (openSet.TryDequeue(out node current))
            {
                if (gScore[current.ToString()] < current.time)
                {
                    //find a better way to same node --> skip this node
                    continue;
                }

                bool xFinalCal = false;
                if (current.time == 0)
                {
                    //ending.Add(current);
                    int iFinal = finalGeodes(current);
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    continue;
                }
                //if (cheat(bp, current.materials, current.robots, current.time) == 0) continue;

                if (current.robots[2] > 0)
                {
                    //possible to make an geode robot
                    var geoNode = createGeodeNode(bp, current);
                    if (geoNode != null && geoNode.time > gScore.GetValueOrDefault(geoNode.ToString(), 0))
                    {
                        gScore[geoNode.ToString()] = geoNode.time;
                        openSet.Enqueue(geoNode);
                    }
                    else
                    {
                        int iFinal = finalGeodes(current);
                        xFinalCal = true;
                        if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    }
                }
                if (current.robots[1] > 0)
                {
                    //possible to make an obsidian robot
                    var obsiNode = createObsidianNode(bp, current);
                    if (obsiNode != null && obsiNode.time > gScore.GetValueOrDefault(obsiNode.ToString(), 0))
                    {
                        gScore[obsiNode.ToString()] = obsiNode.time;
                        openSet.Enqueue(obsiNode);
                    }
                    else if (!xFinalCal)
                    {
                        int iFinal = finalGeodes(current);
                        xFinalCal = true;
                        if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                    }
                }
                // make clay robot
                var clayNode = createClayNode(bp, current);
                if (clayNode != null && clayNode.time > gScore.GetValueOrDefault(clayNode.ToString(), 0))
                {
                    gScore[clayNode.ToString()] = clayNode.time;
                    openSet.Enqueue(clayNode);
                }
                else if (!xFinalCal)
                {
                    int iFinal = finalGeodes(current);
                    xFinalCal = true;
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                }

                //make ore robot
                var oreNode = createOreNode(bp, current);
                if (oreNode != null && oreNode.time > gScore.GetValueOrDefault(oreNode.ToString(), 0))
                {
                    gScore[oreNode.ToString()] = oreNode.time; 
                    openSet.Enqueue(oreNode);
                }
                else if (!xFinalCal)
                {
                    int iFinal = finalGeodes(current);
                    if (iFinal > iMaxGeodes) iMaxGeodes = iFinal;
                }
            }
            
            return iMaxGeodes;
        }
        //idea: score geven op basis van totaal aantal materialen geminded ? 
        
        node createGeodeNode(Blueprint bp, node n)
        {
            int[] materials = n.materials.ToArray();
            int[] robots = n.robots.ToArray();
            for (int i = 0; i < n.time; i++)
            {
                if (materials[0] >= bp.geodeRobotCost.Item1 && materials[2] >= bp.geodeRobotCost.Item2)
                {
                    materials[0] += robots[0]-bp.geodeRobotCost.Item1;
                    materials[1] += robots[1];
                    materials[2] += robots[2] - bp.geodeRobotCost.Item2;
                    materials[3] += robots[3];
                    robots[3]++;
                    return new(materials, robots, n.time - i-1);
                }
                materials[0] += robots[0];
                materials[1] += robots[1];
                materials[2] += robots[2];
                materials[3] += robots[3];
            }
            return null;
        }
        node createObsidianNode(Blueprint bp, node n)
        {
            int[] materials = n.materials.ToArray();
            int[] robots = n.robots.ToArray();
            for (int i = 0; i < n.time; i++)
            {
                if (materials[0] >= bp.obsidianRobotCost.Item1 && materials[1] >= bp.obsidianRobotCost.Item2)
                {
                    materials[0] += robots[0] - bp.obsidianRobotCost.Item1;
                    materials[1] += robots[1] - bp.obsidianRobotCost.Item2;
                    materials[2] += robots[2];
                    materials[3] += robots[3];
                    robots[2]++;
                    return new(materials, robots, n.time - i-1);
                }
                materials[0] += robots[0];
                materials[1] += robots[1];
                materials[2] += robots[2];
                materials[3] += robots[3];
            }
            return null;
        }
        node createClayNode(Blueprint bp, node n)
        {
            int[] materials = n.materials.ToArray();
            int[] robots = n.robots.ToArray();
            for (int i = 0; i < n.time; i++)
            {
                if (materials[0] >= bp.clayRobotCost)
                {
                    materials[0] += robots[0] - bp.clayRobotCost;
                    materials[1] += robots[1];
                    materials[2] += robots[2];
                    materials[3] += robots[3];
                    robots[1]++;
                    return new(materials, robots, n.time - i-1);
                }
                materials[0] += robots[0];
                materials[1] += robots[1];
                materials[2] += robots[2];
                materials[3] += robots[3];
            }
            return null;
        }
        node createOreNode(Blueprint bp, node n)
        {
            int[] materials = n.materials.ToArray();
            int[] robots = n.robots.ToArray();
            for(int i=0; i < n.time; i++)
            {
                if (materials[0] >= bp.oreRobotCost)
                {
                    materials[0] += robots[0] - bp.oreRobotCost;
                    materials[1] += robots[1];
                    materials[2] += robots[2];
                    materials[3] += robots[3];
                    robots[0]++;
                    return new(materials, robots, n.time - i-1);
                }
                materials[0] += robots[0];
                materials[1] += robots[1];
                materials[2] += robots[2];
                materials[3] += robots[3];
            }
            return null;
        }
        int finalGeodes(node n)
        {
            
            int[] materials = n.materials.ToArray();
            int[] robots = n.robots.ToArray();
            if (robots[3] == 0) return 0;
            for (int i = 0; i < n.time; i++)
            {
                materials[3] += robots[3];
            }
            return materials[3];
        }
        
        class node
        {
            public int time { get; private set; }
            public int[] materials { get; private set; }
            public int[] robots { get; private set; }
            public node(int[] materials, int[] robots, int iTimeLeft)
            {
                time = iTimeLeft;
                this.materials = materials;
                this.robots = robots;
            }
            public override string ToString()
            {
                return $"M:{string.Join(",",materials)}; R: {string.Join(",", robots)}";
            }
        }


        public override string Q2()
        {
            var blueprints = convert(Data);
            long result = 1;
            for (int i = 0; i < blueprints.Count && i<3; i++)
            {
                var blueprint = blueprints[i];
                result *= logic_v5(blueprint, 32);
            }
            return result.ToString();
        }

    }
}
