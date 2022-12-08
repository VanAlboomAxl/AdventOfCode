using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
namespace AdventOfCode
{
    public class Day8 : Day
    {
        public override int _iDay { get { return 8; } }
      
        public override string Q1()
        {
            var data = Convertors.Convertor_LLI(Data);

            long lVisible = data[0].Count * data.Count;

            for(int i=1; i<data.Count-1;i++)
                for(int j = 1; j < data[i].Count-1; j++)
                {
                    int iHeight = data[i][j];
                    
                    //left
                    bool xVisible = true;
                    for(int l= j - 1; l >= 0; l--)
                    {
                        if (data[i][l]>= iHeight)
                        {
                            xVisible = false;
                            break;
                        }
                    }
                    if (xVisible)
                    {
                        // tree is visible from left 
                        // skip rest off checks
                        continue;
                    }

                    //right
                    xVisible = true;
                    for (int r = j + 1; r< data[i].Count; r++)
                    {
                        if (data[i][r] >= iHeight)
                        {
                            xVisible = false;
                            break;
                        }
                    }
                    if (xVisible)
                    {
                        // tree is visible from right 
                        // skip rest off checks
                        continue;
                    }

                    //up
                    xVisible = true;
                    for (int u = i -1; u >= 0; u--)
                    {
                        if (data[u][j] >= iHeight)
                        {
                            xVisible = false;
                            break;
                        }
                    }
                    if (xVisible)
                    {
                        // tree is visible from down 
                        // skip rest off checks
                        continue;
                    }

                    //down
                    xVisible = true;
                    for (int d = i + 1; d< data[i].Count; d++)
                    {
                        if (data[d][j] >= iHeight)
                        {
                            xVisible = false;
                            break;
                        }
                    }
                    if (xVisible)
                    {
                        // tree is visible from down 
                        // skip rest off checks
                        continue;
                    }

                    // if here from nowhere visible --> tree not visible
                    lVisible--;
                }

            return lVisible.ToString();
        }
      
        public override string Q2()
        {
            var data = Convertors.Convertor_LLI(Data);

            long lVisible = data[0].Count * data.Count;
            long lMax = 0;

            for (int i = 1; i < data.Count - 1; i++)
                for (int j = 1; j < data[i].Count - 1; j++)
                {
                    int iHeight = data[i][j];

                    long lTreesToSee = 0;

                    //left
                    long lLeft = 0;
                    for (int l = j - 1; l >= 0; l--)
                    {
                        lLeft++;
                        if (data[i][l] >= iHeight)
                        {
                            break;
                        }
                    }
                    //right
                    long lRight = 0;
                    for (int r = j + 1; r < data[i].Count; r++)
                    {
                        lRight++;
                        if (data[i][r] >= iHeight)
                        {
                            break;
                        }
                    }
                    //up
                    long lUp = 0;
                    for (int u = i - 1; u >= 0; u--)
                    {
                        lUp++;
                        if (data[u][j] >= iHeight)
                        {
                            break;
                        }
                    }
                    //down
                    long lDown=0;
                    for (int d = i + 1; d < data[i].Count; d++)
                    {
                        lDown++;
                        if (data[d][j] >= iHeight)
                        {
                            break;
                        }
                    }
                    lTreesToSee = lLeft * lDown * lUp * lRight;
                    if (lTreesToSee > lMax) lMax = lTreesToSee;
                }

            return lMax.ToString();
        }

    }
}
