using System.Collections.Generic;
using PacMan.Models;

namespace PacMan.Services
{
    public class GraphTestService
    {
        public void Run()
        {
            // ─── Montando o labirinto ───
            MazeService mazeService = new MazeService();
            BfsService bfsService = new BfsService();
            DfsService dfsService = new DfsService();

            int[,] maze = {
                { 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1 }
            };

            Graph graph = mazeService.BuildGraph(maze);

            // ─── Imprime o grafo ───
            Console.WriteLine("=== GRAFO (Lista de Adjacência) ===");
            graph.PrintGraph();

            // ─── Teste BFS ───
            Console.WriteLine("\n=== BFS (Caminho mais curto) ===");
            Console.WriteLine("Fantasma no nó 16, Pac-Man no nó 8");
            List<int> bfsPath = bfsService.FindPath(graph, 16, 8);
            foreach (int no in bfsPath)
                Console.Write($"{no} → ");

            // ─── Teste DFS ───
            Console.WriteLine("\n\n=== DFS (Exploração) ===");
            Console.WriteLine("Fantasma no nó 16, Pac-Man no nó 8");
            List<int> dfsPath = dfsService.FindPath(graph, 16, 8);
            foreach (int no in dfsPath)
                Console.Write($"{no} → ");

            Console.WriteLine("\n");
        }
    }
}
