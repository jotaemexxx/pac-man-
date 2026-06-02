using PacMan.Models;
using System.Collections.Generic;
using System.Numerics;

namespace PacMan.Services
{
    public class MazeService
    {
        public Graph BuildGraph(int[,] maze)
        {
            Graph graph = new Graph();

            int totalLinhas = maze.GetLength(0);
            int totalColunas = maze.GetLength(1);


            for(int r = 0; r < totalLinhas; r++)
            {
                for(int c = 0; c < totalColunas; c++)
                {
                    if (maze[r, c] == 1) continue;
                    {
                        if(c + 1 < totalColunas && maze[r, c + 1] == 0)
                        {
                            graph.AddEdge(r * totalColunas + c, r * totalColunas + (c + 1));
                        }

                        if (c - 1 >= 0 && maze[r, c - 1] == 0)
                        {
                            graph.AddEdge(r * totalColunas + c, r * totalColunas + (c - 1));
                        }

                        if(r + 1 < totalLinhas && maze[r + 1 , c] == 0)
                        {
                            graph.AddEdge(r * totalColunas + c, (r + 1) * totalColunas + c);
                        }
                        if(r - 1 >= 0 && maze[r - 1, c] == 0)
                        {
                            graph.AddEdge(r * totalColunas + c, (r - 1) * totalColunas + c);
                        }
                    }
                  
                }
            }

            return (graph);

        }

    }
}
