using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedSetAssignment
{
    public class Node<T>
    {
        public Node<T> Right;
        public Node<T> Left;
        public T Value;
        public bool IsRed;

        public Node(T value)
        {
            Value = value;
        }
    }
}
