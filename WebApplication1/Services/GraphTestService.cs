using System.Collections.Generic;
using PacMan.Models;

namespace PacMan.Services
{
    public class GraphTestService
    {
        public void Run()
        {
            MazeService mazeService = new MazeService();

            int[,] maze = {

                {1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 1 },
                {1, 0, 1, 0, 1 },
                {1, 0, 0, 0, 1 },
                {1, 1 ,1, 1, 1 }

            };

            Graph graph = mazeService.BuildGraph(maze);
            graph.PrintGraph();

        }
    }
}
