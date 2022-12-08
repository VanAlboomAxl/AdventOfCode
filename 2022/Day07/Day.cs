using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day7 : Day
    {
        public override int _iDay { get { return 7; } }

        public (dir,List<dir>) convert(List<string> data)
        {
            var lsInput = Data;
            dir outer = new("/", null);
            List<List<string>> cmds = new();
            List<string> cmd = new() { lsInput[1] };
            for (int i = 2; i < lsInput.Count; i++)
            {
                string s = lsInput[i];
                if (s.StartsWith('$'))
                {
                    cmds.Add(cmd);
                    cmd = new();
                }
                cmd.Add(s);
            }
            cmds.Add(cmd);
            dir current = outer;
            List<dir> allDirs = new();
            allDirs.Add(outer);
            foreach (var c in cmds)
            {
                string sCMD = c[0];
                string[] asCMD = sCMD.Split(' ');
                if (asCMD[1].Equals("cd"))
                {
                    if (asCMD[2].Equals("..")) current = current.parent;
                    else current = current.directories.Where(x => x.name.Equals(asCMD[2])).First();
                }
                else //ls
                    for (int i = 1; i < c.Count; i++)
                    {
                        string sBrowse = c[i];
                        string[] asBrowse = sBrowse.Split(' ');
                        if (asBrowse[0].Equals("dir"))
                        {
                            dir n = new(asBrowse[1], current);
                            current.directories.Add(n);
                            allDirs.Insert(0, n);
                        }
                        else
                        {
                            current.files.Add(new(asBrowse[1], long.Parse(asBrowse[0])));
                        }
                    }

            }

            return (outer, allDirs);
        }
        dir outer;
        List<dir> allDirs;
        public override string Q1()
        {
            (outer, allDirs)  = convert(Data);

            List<dir> ldLogic = new();
            foreach (var d in allDirs)
            {
                if (d.filesize() <= 100000)
                {
                    ldLogic.Add(d);
                }
            }
            long l = 0;
            foreach (var d in ldLogic) l += d.filesize();
            return l.ToString();
        }


        public override string Q2()
        {
            long lStillAvailable = 70000000 - outer.filesize();
            long lNeededToClear = 30000000 - lStillAvailable;
            dir dToDelete = null;
            dToDelete = allDirs.Where(x => x.filesize() >= lNeededToClear).OrderBy(x => x.filesize()).First();
            //foreach(var d in allDirs)
            //    if(d.filesize() >= lNeededToClear)
            //    {
            //        if (dToDelete == null) dToDelete = d;
            //        else if (d.filesize()<dToDelete.filesize()) dToDelete = d;
            //    }

            return dToDelete.filesize().ToString();
        }

        public class dir
        {
            public dir parent { get; private set; }
            public string name { get; private set; }
            public List<file> files { get; private set; }
            public List<dir> directories { get; private set; }
            public dir(string name, dir parent )
            {
                this.parent = parent;
                this.name = name;
                files = new();
                directories = new();
            }
            private long _lSize;
            public long filesize()
            {
                if (_lSize > 0) return _lSize;
                foreach (var f in files) _lSize += f.size;
                foreach (var d in directories) _lSize += d.filesize();
                return _lSize;
            }
            public override string ToString()
            {
                return name;
            }
        }
        public class file
        {
            public string name { get; private set; }
            public long size { get; private set; }
            public file(string name, long size)
            {
                this.name = name;
                this.size = size;
            }
            public override string ToString()
            {
                return name;
            }
        }
    }
}
