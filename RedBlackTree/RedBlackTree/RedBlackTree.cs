using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    class RedBlackTree<T> where T : IComparable<T>
    {
        Node<T> root;
        int Count { get; set; }

        public RedBlackTree()
        {  
        }
        //false == black; true == red
        public void Add(T value)
        {
            if (root == null)
            {
                root = new Node<T>(value);
                root.IsRed = false;
                Count++;
                return;
            }

            Node<T> nodeToInsert = new Node<T>(value);
            nodeToInsert.IsRed = true;
            Node<T> current = root;
            Add(nodeToInsert, current);
        }
        public Node<T> Add(Node<T> nodeToInsert, Node<T> current)
        {
            if (current == null)
            {
                current = nodeToInsert;
                Count++;
                return current;
            }
            
            if (current.Left != null && current.Right != null && current.Left.IsRed == true && current.Right.IsRed == true)
            {
                FlipColor(current);
            }

            if(current.Value.CompareTo(nodeToInsert.Value) == 0)
            {
                throw new Exception("no");
            }
            else if (current.Value.CompareTo(nodeToInsert.Value) > 0)
            {
                current.Left = Add(nodeToInsert, current.Left);
            }
            else
            {
               current.Right = Add(nodeToInsert, current.Right);
            }

            if (current.Right != null && current.Right.IsRed)
            {
                RotateLeft(current);
            }
            if (current.Left != null && current.Left.Left != null && current.Left.IsRed && current.Left.Left.IsRed)
            {
                RotateRight(current);
            }

            return current;
        }
        public void FlipColor(Node<T> node)
        {
            node.IsRed = !node.IsRed;
            node.Left.IsRed = !node.Left.IsRed;
            node.Right.IsRed = !node.Right.IsRed;
        }
        public Node<T> RotateLeft(Node<T> node)
        {
            node.IsRed = true;
            Node<T> savedNode = node.Right;
            savedNode.IsRed = false;
            node.Right = node.Right.Left;
            savedNode.Left = node;

            return node;
        }
        public Node<T> RotateRight(Node<T> node)
        {
            node.IsRed = true;
            Node<T> savedNode = node.Left;
            savedNode.IsRed = false;
            node.Left = node.Left.Right;
            savedNode.Right = node;

            return node;
        }
        public bool isRed()
        {
            return false;
        }
        public void Remove()
        {

        }
        public void MoveRedRight()
        {

        }
        public void MoveRedLeft()
        {

        }
    }
}
