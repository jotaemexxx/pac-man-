using System.Collections.Generic;
using PacMan.Models;

namespace PacMan.Services
{
    public class DfsService
    {
        public List<int> FindPath(Graph graph, int start, int end)
        {
            Stack<int> stack = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, int> cameFrom = new Dictionary<int, int>();

            stack.Push(start);
            visited.Add(start);

            while(stack.Count > 0)
            {
                int atual = stack.Pop();

                if(atual == end)
                {
                    List<int> path = new List<int>();
                    int passo = end;

                    while(passo != start)
                    {
                        path.Add(passo);
                        passo = cameFrom[passo];
                    }

                    path.Add(start);
                    path.Reverse();
                    return path;
                }

                foreach(int neighbor in graph.AdjacencyList[atual])
                {
                    if(!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        cameFrom[neighbor] = atual;
                        stack.Push(neighbor);
                    }
                }

            }

            return new List<int>();

        }

    }
}
