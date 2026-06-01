using System.Collections.Generic;



namespace PacMan.Models

{
    public class Graph
    {
        public Dictionary<int, List<int>> AdjacencyList { get; set; }

        public Graph()
        {
            AdjacencyList = new Dictionary<int, List<int>>();
        }

        public void AddEdge(int source, int destination)
        {
            if(!AdjacencyList.ContainsKey(source))
            {
                AdjacencyList[source] = new List<int>();
            }

            if(!AdjacencyList.ContainsKey(destination))
            {
                AdjacencyList[destination] = new List<int>();
            }

            AdjacencyList[source].Add(destination);
            AdjacencyList[destination].Add(source);
        }

        public void PrintGraph()
        {
            foreach (var vertex in AdjacencyList)
            {
                Console.Write($"{vertex.Key} -> ");

                foreach(var neighbor in vertex.Value)
                {
                    Console.Write($"{neighbor} ");
                }

                Console.WriteLine();
            }
        }

    }
}
