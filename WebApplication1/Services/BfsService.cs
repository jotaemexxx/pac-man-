using System.Collections.Generic;
using PacMan.Models;

namespace PacMan.Services
{
    public class BfsService
    {
        public List<int> FindPath(Graph graph, int start, int end)
        {
            Queue<int> queue = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, int> cameFrom = new Dictionary<int, int>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                int atual = queue.Dequeue();

                if (atual == end)
                {
                    List<int> path = new List<int>();
                    int passo = end;

                    while (passo != start)
                    {
                        path.Add(passo);
                        passo = cameFrom[passo];
                    }

                    path.Add(start);
                    path.Reverse();
                    return path;
                }

                if (!graph.AdjacencyList.ContainsKey(atual)) continue;

                foreach (int neighbor in graph.AdjacencyList[atual])
                {
                    if (!graph.AdjacencyList.ContainsKey(neighbor)) continue;

                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        cameFrom[neighbor] = atual;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return new List<int>();
        }
    }
}