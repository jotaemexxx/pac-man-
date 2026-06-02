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

        public GameService()
        {
            MazeService mazeService = new MazeService();

            int[,] maze = {
                {1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 1 },
                {1, 0, 1, 0, 1 },
                {1, 0, 0, 0, 0 },
                {1, 1, 1, 1, 1 }

            };

            Graph = mazeService.BuildGraph(maze);
            EndGame = false;

            Pacman = new Player(new Node(6, 1, 1));

            Ghosts = new List<Ghost>();
            Ghosts.Add(new Ghost(new Node( 16, 3, 1), "red"));

            Dots = new List<int>(Graph.AdjacencyList.Keys);

            Nodes = new Dictionary<int, Node>();

            for (int r = 0; r < maze.GetLength(0); r++)
            {
                for (int c = 0; c < maze.GetLength(1); c++)
                {
                    if (maze[r, c] == 0)
                    {
                        int id = r * maze.GetLength(1) + c;
                        Nodes[id] = new Node(id, r, c);
                    }
                }
            }

        }

        public void MovePlayer(int destination)
        {
            
            if (Graph.AdjacencyList[Pacman.CurrentNode.Id].Contains(destination))
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
            BfsService bfsService = new BfsService();

            foreach(Ghost ghost in Ghosts)
            {

                List<int> path = bfsService.FindPath(Graph, ghost.CurrentNode.Id, Pacman.CurrentNode.Id);

                if(path.Count > 1)
                {
                    ghost.CurrentNode = Nodes[path[1]];
                }
            }
        }

        public bool CheckCollision()
        {
            foreach(Ghost ghost in Ghosts)
            {
                if(ghost.CurrentNode.Id == Pacman.CurrentNode.Id)
                {
                    return true;
                }
            }

            return false;

        }

        public bool CheckWin()
        {
            return Dots.Count == 0;
        }

    }
}
