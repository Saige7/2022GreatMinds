using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreeAssignment
{
    class Node<T>
    {
        public List<T> Value;
        public List<Node<T>> Children;

        public Node()
        {
            Value = new List<T>(3);
            Children = new List<Node<T>>(4);
        }
        public Node(T value)
        {
            Value = new List<T>(3);
            Children = new List<Node<T>>(4);
            Value.Add(value);
        }
    }
}
