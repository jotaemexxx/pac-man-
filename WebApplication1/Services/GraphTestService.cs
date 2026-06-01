using System.Collections.Generic;
using PacMan.Models;

namespace PacMan.Services
{
    public class GraphTestService
    {
        public void Run()
        {
            Graph graph = new Graph();

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);

            graph.PrintGraph();

        }
    }
}
