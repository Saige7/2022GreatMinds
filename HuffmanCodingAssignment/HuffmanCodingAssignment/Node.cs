using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCodingAssignment
{
    internal class Node
    {
        public Node Left;
        public Node Right;
        public Node Parent;
        public char Value;
        public int weight;

        public Node(char value)
        {
            Value = value;
            Parent = null;
            weight = 0;
        }
    }
}
