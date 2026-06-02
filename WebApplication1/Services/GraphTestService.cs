using System.Collections.Generic;
using PacMan.Models;

namespace PacMan.Services
{
    public class GraphTestService
    {
        public void Run()
        {
            MazeService mazeService = new MazeService();
            BfsService bfsService = new BfsService();

            int[,] maze = {
        { 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1 }
    };

            Graph graph = mazeService.BuildGraph(maze);

            // fantasma no nó 16, Pac-Man no nó 8
            List<int> path = bfsService.FindPath(graph, 16, 8);

            Console.WriteLine("Caminho do fantasma:");
            foreach (int no in path)
            {
                Console.Write($"{no} → ");
            }
        }
    }
}
