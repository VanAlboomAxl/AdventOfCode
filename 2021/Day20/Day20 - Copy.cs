using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    // still slow
    public class Day20_2 : Day
    {
        public override int _iDay { get { return 20; } }

        internal override List<string> _lsTest => new List<string> {
            "..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#",
            "",
            "#..#.",
            "#....",
            "##..#",
            "..#..",
            "..###"
        };


        private List<bool> lxAlgorithm;

        public override void Q1()
        {
            var lsInput = Input;
            lsInput = Test;

            lxAlgorithm = new();
            foreach (var c in lsInput[0])
                if (c.Equals('#'))
                    lxAlgorithm.Add(true);
                else
                    lxAlgorithm.Add(false);

            List<List<bool>> llxImage = new();
            for (int i = 2; i < lsInput.Count(); i++)
            {
                List<bool> lxImage = new();
                foreach (var c in lsInput[0])
                    if (c.Equals('#'))
                        lxImage.Add(true);
                    else
                        lxImage.Add(false);
                llxImage.Add(lxImage);
            }

            //ShowImage(llxImage);

            for (int i = 0; i < 2; i++)
            {
                bool xLight = i % 2 == 1;
                //xLight = false;
                Console.WriteLine(i + 1);
                llxImage = Enhance(llxImage, xLight);
                //ShowImage(llxImage);
            }

            Int64 iCount = 0;
            foreach (List<bool> lx in llxImage)
                foreach (bool x in lx)
                    if (x) iCount++;
            Console.WriteLine("Result: " + iCount);
        }

        private List<string> Expand(List<string> lsImage, int iExpand, bool xLight=false)
        {

            char cPixel = '.';
            if (xLight) cPixel = '#';

            string sExtra = "";
            for(int i = 0; i < iExpand; i++)
                sExtra += cPixel;
            

            List<string> lsResultImage = new();
            int iLength = lsImage[0].Length;
            for (int i = 0; i < iExpand; i++)
            {
                string sImage = "";
                sImage += sExtra;
                for (int j = 0; j < iLength; j++)
                    sImage += cPixel;
                sImage += sExtra;
                lsResultImage.Add(sImage);
            }
            for (int i = 0; i < lsImage.Count(); i++)
            {
                string sImage = "";
                sImage += sExtra;
                sImage += lsImage[i];
                sImage += sExtra;
                lsResultImage.Add(sImage);
            }
            for (int i = 0; i < iExpand; i++)
            {
                string sImage = "";
                sImage += sExtra;
                for (int j = 0; j < iLength; j++)
                    sImage += cPixel;
                sImage += sExtra;
                lsResultImage.Add(sImage);
            }

            return lsResultImage;
        }

        private void ShowImage(List<List<bool>> lsImage)
        {
            foreach (var sRow in lsImage)
            {
                foreach (var c in sRow)
                    if(c) Console.Write('#');
                    else Console.Write('.');
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private List<List<bool>> Enhance(List<List<bool>> llxImage, bool extraLight)
        {

            List<List<bool>> llxResultImage = new();

            for (int y = -1; y < llxImage.Count+1; y++)
            {
                List<bool> lxLine = new();
                for (int x = -1; x < llxImage[0].Count + 1; x++)
                {
                    lxLine.Add(PixelLogic(llxImage, extraLight,x,y));
                }

                llxResultImage.Add(lxLine);
            }

            return llxResultImage;
        }

        private bool PixelLogic(List<List<bool>> llxImage, bool extraLight, int x, int y)
        {

            string sPixel = "";
            for (int i = y - 1; i < y + 2; i++)
                for (int j = x - 1; j < x + 2; j++)
                {
                    bool c = extraLight;
                    try
                    {
                        c = llxImage[i][j];
                    }
                    catch
                    {
                        c = extraLight;
                    }
                    if (c)
                        sPixel += "1";
                    else
                        sPixel += "0";

                }
                    
            return lxAlgorithm[(int)Convert.ToInt32(sPixel, 2)];
        }

        public override void Q2()
        {
            //var lsInput = Input;
            ////lsInput = Test;

            //lxAlgorithm = new();
            //foreach (var c in lsInput[0])
            //    if (c.Equals('#'))
            //        lxAlgorithm.Add(true);
            //    else
            //        lxAlgorithm.Add(false);

            //List<List<bool>> llxImage = new();
            //for (int i = 2; i < lsInput.Count(); i++)
            //{
            //    List<bool> lxImage = new();
            //    foreach (var c in lsInput[0])
            //        if (c.Equals('#'))
            //            lxImage.Add(true);
            //        else
            //            lxImage.Add(false);
            //    llxImage.Add(lxImage);
            //}

            ////ShowImage(llxImage);

            //for (int i = 0; i < 50; i++)
            //{
            //    bool xLight = i % 2 == 1;
            //    //xLight = false;
            //    Console.WriteLine(i + 1);
            //    llxImage = Enhance(llxImage, xLight);
            //    ShowImage(llxImage);
            //}

            //Int64 iCount = 0;
            //foreach (List<bool> lx in llxImage)
            //    foreach (bool x in lx)
            //        if (x) iCount++;
            //Console.WriteLine("Result: " + iCount);
        }


    }
}
