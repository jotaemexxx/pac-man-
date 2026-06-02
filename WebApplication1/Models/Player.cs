using System.Collections.Generic;


namespace PacMan.Models
{
    public class Player
    {
        public Node CurrentNode { get; set; }
        public int Score { get; set; }

        public Player(Node StartNode)
        {
            CurrentNode = StartNode;
            Score = 0;

        }


    }
}
