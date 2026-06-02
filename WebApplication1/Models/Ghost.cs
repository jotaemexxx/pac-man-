using System.Collections.Generic;

namespace PacMan.Models
{
    public class Ghost
    {

        public Node CurrentNode { get; set; }
        public string Color { get; set; }

        public Ghost(Node startNode, string color)
        {
            CurrentNode = startNode;
            Color = color;
        }



    }
}
