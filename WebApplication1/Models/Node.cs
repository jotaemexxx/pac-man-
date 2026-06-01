using System.Collections.Generic;

namespace PacMan.Models
{
    public class Node
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public List<int> Neighbors { get; set; }

        public Node(int id, int row, int col)
        {
            Id = id;
            Row = row;
            Col = col;
            Neighbors = new List<int>();
        }

    }
}
