using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021
{
    //very slow
    public class Day20 : Day
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

        public override void Q1()
        {
            //var lsInput = Input;
            ////lsInput = Test;

            
            //string sAlgorithm = lsInput[0];

            //List<string> lsImage = new();
            //for(int i = 2; i < lsInput.Count(); i++)
            //    lsImage.Add(lsInput[i]);

            //ShowImage(lsImage);

            //for (int i=0;i<2; i++)
            //{
            //    bool xLight = i%2 == 1;
            //    //xLight = false;
            //    Console.WriteLine(i+1);
            //    lsImage = Enhance(sAlgorithm, lsImage,xLight);
            //    ShowImage(lsImage);
            //}

            //Int64 iCount = 0;
            //foreach (var s in lsImage)
            //    foreach (char c in s)
            //        if (c.Equals('#'))
            //            iCount++;
            //Console.WriteLine("Result: " + iCount);
        }
        public void Q1_v0()
        {
            var lsInput = Input;
            lsInput = Test;

            string sAlgorithm = lsInput[0];
            List<string> lsImage = new();
            List<string> lsResultImage = new();
            int iLength = lsInput[2].Length;
            for (int i = 0; i < 3; i++)
            {
                string sImage = "...";
                for (int j = 0; j < iLength; j++)
                    sImage += ".";
                sImage += "...";
                lsImage.Add(sImage);
                //lsResultImage.Add(sImage);
                Console.WriteLine(sImage);
            }
            for (int i = 2; i < lsInput.Count(); i++)
            {
                string sImage = "...";
                //lsResultImage.Add(sImage);
                sImage += lsInput[i];
                sImage += "...";
                lsImage.Add(sImage);
                Console.WriteLine(sImage);
            }
            for (int i = 0; i < 3; i++)
            {
                string sImage = "...";
                for (int j = 0; j < iLength; j++)
                    sImage += ".";
                sImage += "...";
                lsImage.Add(sImage);
                Console.WriteLine(sImage);
            }

            Console.WriteLine();

            (int, int) iiXrange = (3, 3 + iLength);
            (int, int) iiYrange = (3, 3 + lsInput.Count - 2);

            for (int y = iiYrange.Item1; y < iiYrange.Item2; y++)
            {
                string sLine = "...";
                for (int x = iiXrange.Item1; x < iiXrange.Item2; x++)
                {
                    string sPixel = "";
                    for (int i = y - 1; i < y + 2; i++)
                        for (int j = x - 1; j < x + 2; j++)
                            if (lsImage[i][j].Equals('#'))
                                sPixel += "1";
                            else
                                sPixel += "0";

                    //Console.Write(lsImage[y][x]);
                    //Console.WriteLine(sPixel);
                    //Console.WriteLine(Convert.ToInt32(sPixel, 2));
                    sLine += sAlgorithm[(int)Convert.ToInt32(sPixel, 2)].ToString();
                    //Console.WriteLine();

                }

                sLine += "...";
                lsResultImage.Add(sLine);
                //Console.WriteLine();
            }
            foreach (var sRow in lsResultImage)
            {
                foreach (var c in sRow)
                    Console.Write(c);
                Console.WriteLine();
            }


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

        private void ShowImage(List<string> lsImage)
        {
            //Console.WriteLine();
            foreach (var sRow in lsImage)
            {
                foreach (var c in sRow)
                    Console.Write(c);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private List<string> Enhance0(string sAlgorithm, List<string> lsImage, bool xLight)
        {
            lsImage = Expand(lsImage, 3, xLight);

            List<string> lsResultImage = new();
            (int, int) iiXrange = (2, lsImage[0].Length-2);
            (int, int) iiYrange = (2, lsImage.Count - 2);

            for (int y = iiYrange.Item1; y < iiYrange.Item2; y++)
            {
                //string sLine = "...";
                string sLine = "";
                for (int x = iiXrange.Item1; x < iiXrange.Item2; x++)
                {
                    string sPixel = "";
                    for (int i = y - 1; i < y + 2; i++)
                        for (int j = x - 1; j < x + 2; j++)
                            if (lsImage[i][j].Equals('#'))
                                sPixel += "1";
                            else
                                sPixel += "0";

                    //Console.Write(lsImage[y][x]);
                    //Console.WriteLine(sPixel);
                    //Console.WriteLine(Convert.ToInt32(sPixel, 2));
                    sLine += sAlgorithm[(int)Convert.ToInt32(sPixel, 2)].ToString();
                    //Console.WriteLine();

                }

                //sLine += "...";
                lsResultImage.Add(sLine);
                //Console.WriteLine();
            }

            return lsResultImage;
        }

        private List<string> Enhance(string sAlgorithm, List<string> lsImage, bool extraLight)
        {

            List<string> lsResultImage = new();

            for (int y = -1; y < lsImage.Count+1; y++)
            {
                string sLine = "";
                for (int x = -1; x < lsImage[0].Length + 1; x++)
                {
                    sLine += PixelLogic(sAlgorithm,lsImage,extraLight,x,y);
                }

                lsResultImage.Add(sLine);
            }

            return lsResultImage;
        }

        private char PixelLogic(string sAlgorithm, List<string> lsImage, bool extraLight, int x, int y)
        {
            char cPixel = '.';
            if (extraLight) cPixel = '#';

            string sPixel = "";
            for (int i = y - 1; i < y + 2; i++)
                for (int j = x - 1; j < x + 2; j++)
                {
                    char c = cPixel;
                    try
                    {
                        c = lsImage[i][j];
                    }
                    catch
                    {
                        c = cPixel;
                    }
                    if (c.Equals('#'))
                        sPixel += "1";
                    else
                        sPixel += "0";

                }
                    
            return sAlgorithm[(int)Convert.ToInt32(sPixel, 2)];
        }

        public override void Q2()
        {
            var lsInput = Input;
            //lsInput = Test;


            string sAlgorithm = lsInput[0];

            List<string> lsImage = new();
            for (int i = 2; i < lsInput.Count(); i++)
                lsImage.Add(lsInput[i]);

            //ShowImage(lsImage);

            for (int i = 0; i < 50; i++)
            {
                bool xLight = i % 2 == 1;
                //xLight = false;
                Console.WriteLine(i + 1);
                lsImage = Enhance(sAlgorithm, lsImage, xLight);
                //ShowImage(lsImage);
            }

            Int64 iCount = 0;
            foreach (var s in lsImage)
                foreach (char c in s)
                    if (c.Equals('#'))
                        iCount++;
            Console.WriteLine("Result: " + iCount);
        }


    }
}
