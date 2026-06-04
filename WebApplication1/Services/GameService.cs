using System.Collections.Generic;
using PacMan.Models;

namespace PacMan.Services
{
    public class GameService
    {
        public Graph Graph { get; set; }
        public Player Pacman { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public bool EndGame { get; set; }
        public List<int> Dots { get; set; }
        public Dictionary<int, Node> Nodes { get; set; }

        private readonly BfsService _bfsService = new BfsService();

        private static readonly int[,] Maze = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
            {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,0,1},
            {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
            {1,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1},
            {1,1,1,1,0,1,0,0,0,0,0,0,0,1,0,1,1,1,1},
            {1,1,1,1,0,1,0,1,1,0,1,1,0,1,0,1,1,1,1},
            {0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0},
            {1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1},
            {1,1,1,1,0,1,0,0,0,0,0,0,0,1,0,1,1,1,1},
            {1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
            {1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
            {1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1},
            {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
            {1,0,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        public GameService()
        {
            MazeService mazeService = new MazeService();

            Graph = mazeService.BuildGraph(Maze);
            EndGame = false;

            Nodes = new Dictionary<int, Node>();

            for (int r = 0; r < Maze.GetLength(0); r++)
            {
                for (int c = 0; c < Maze.GetLength(1); c++)
                {
                    if (Maze[r, c] == 0)
                    {
                        int id = r * Maze.GetLength(1) + c;
                        Nodes[id] = new Node(id, r, c);
                    }
                }
            }

            Pacman = new Player(new Node(20 * 19 + 9, 20, 9));

            Ghosts = new List<Ghost>();
            Ghosts.Add(new Ghost(new Node(1 * 19 + 1, 1, 1), "#FF3B5C"));
            Ghosts.Add(new Ghost(new Node(1 * 19 + 17, 1, 17), "#00CFFF"));
            Ghosts.Add(new Ghost(new Node(4 * 19 + 1, 4, 1), "#FFB800"));
            Ghosts.Add(new Ghost(new Node(4 * 19 + 17, 4, 17), "#FF79C6"));

            Dots = new List<int>(Graph.AdjacencyList.Keys);
        }

        public void RestartGame()
        {
            Pacman = new Player(new Node(20 * 19 + 9, 20, 9));

            Ghosts = new List<Ghost>();
            Ghosts.Add(new Ghost(new Node(1 * 19 + 1, 1, 1), "#FF3B5C"));
            Ghosts.Add(new Ghost(new Node(1 * 19 + 17, 1, 17), "#00CFFF"));
            Ghosts.Add(new Ghost(new Node(4 * 19 + 1, 4, 1), "#FFB800"));
            Ghosts.Add(new Ghost(new Node(4 * 19 + 17, 4, 17), "#FF79C6"));

            Dots = new List<int>(Graph.AdjacencyList.Keys);
            EndGame = false;
        }

        public void MovePlayer(int destination)
        {
            if (Graph.AdjacencyList.ContainsKey(Pacman.CurrentNode.Id) &&
                Graph.AdjacencyList[Pacman.CurrentNode.Id].Contains(destination))
            {
                Pacman.CurrentNode = Nodes[destination];

                if (Dots.Contains(destination))
                {
                    Dots.Remove(destination);
                    Pacman.Score += 10;
                }
            }
        }

        public void MoveGhosts()
        {
            foreach (Ghost ghost in Ghosts)
            {
                List<int> path = _bfsService.FindPath(Graph, ghost.CurrentNode.Id, Pacman.CurrentNode.Id);

                if (path.Count > 1)
                    ghost.CurrentNode = Nodes[path[1]];
            }
        }

        public bool CheckCollision()
        {
            foreach (Ghost ghost in Ghosts)
            {
                if (ghost.CurrentNode.Id == Pacman.CurrentNode.Id)
                    return true;
            }
            return false;
        }

        public bool CheckWin()
        {
            return Dots.Count == 0;
        }
    }
}