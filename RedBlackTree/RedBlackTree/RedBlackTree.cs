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
            root = Add(nodeToInsert, root);
            root.IsRed = false;
        }
        private Node<T> Add(Node<T> nodeToInsert, Node<T> current)
        {
            if (current == null)
            {
                current = nodeToInsert;
                Count++;
                return current;
            }
            
            if (IsRed(current.Left) && IsRed(current.Right))
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

            if (IsRed(current.Right))
            {
                current = RotateLeft(current);
            }

            if (current.Left != null && IsRed(current.Left) && IsRed(current.Left.Left))
            {
                current = RotateRight(current);
                FlipColor (current);
            }

            return current;
        }
        private void FlipColor(Node<T> node)
        {
            node.IsRed = !node.IsRed;
            if (node.Left != null)
            {
                node.Left.IsRed = !node.Left.IsRed;
            }
            if (node.Right != null)
            {
                node.Right.IsRed = !node.Right.IsRed;
            }
        }
        private Node<T> RotateLeft(Node<T> node)
        {
            node.IsRed = true;
            Node<T> savedNode = node.Right;
            savedNode.IsRed = false;
            node.Right = node.Right.Left;
            savedNode.Left = node;

            return savedNode;
        }
        private Node<T> RotateRight(Node<T> node)
        {
            node.IsRed = true;
            Node<T> savedNode = node.Left;
            savedNode.IsRed = false;
            node.Left = node.Left.Right;
            savedNode.Right = node;

            return savedNode;
        }
        private bool IsRed(Node<T> node)
        {
            if(node == null)
            {
                return false;
            }

            return node.IsRed;
        }
        public void Remove(T value)
        {
            if (root == null)
            {
                throw new Exception("error :D");
            }

            Node<T> nodeToRemove = new Node<T>(value);
            Count--;
            root = Remove(nodeToRemove, root, null);
            root.IsRed = false;
        }
        private Node<T> Remove(Node<T> nodeToRemove, Node<T> current, Node<T> parent)
        {
            if (current == null)
            {
                return null;
            }

            if (nodeToRemove.Value.CompareTo(current.Value) < 0)
            {
                if (current.Left != null && !IsRed(current.Left) && !IsRed(current.Left.Left))
                {
                    MoveRedLeft(current);
                }

                Remove(nodeToRemove, current.Left, current);
            }
            else
            {
                if (IsRed(current.Left))
                {
                    current = RotateRight(current);
                    parent.Right = current;
                }

                MoveRedRight(current);

                if (current.Value.CompareTo(nodeToRemove.Value) == 0)//should be a .Equals
                {
                    if (current.Left == null && current.Right == null)
                    {
                        if (parent.Left == current)
                        {
                            parent.Left = null;
                        }
                        else
                        {
                            parent.Right = null;
                        }
                    }
                    else
                    {
                        Node<T> replacement = current.Left;
                        while (replacement.Right != null)
                        {
                            parent = replacement;
                            replacement = replacement.Right;
                        }

                        current.Value = replacement.Value;

                        Remove(replacement, root, null);           
                    }
                }
                else
                {
                    Remove(nodeToRemove, current.Right, current);
                }
            }

            current = FixUp(current, parent);
            return current;
        }
        private void MoveRedRight(Node<T> node)
        {
            //if node.right is a 2 node(if it has 2 black children)
            if (/*!IsRed(node.Right) && */node.Right != null && node.Right.Right != null && node.Right.Left != null && !IsRed(node.Right.Right) && !IsRed(node.Right.Left))
            {
                FlipColor(node.Right);

                if (node.Right.Left != null && IsRed(node.Right.Left.Left))
                {
                    node.Right = RotateRight(node.Right);
                    FlipColor(node.Right);
                }
            }
            return;
        }
        private void MoveRedLeft(Node<T> node)
        {
            //if node.left is a 2 node
            //changed MoveRedRight***
            if (node.Left != null && !IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                FlipColor(node.Left);

                if (node.Right != null && IsRed(node.Right.Left))
                {
                    node = RotateRight(node.Right);
                    RotateLeft(node);
                }
            }
        }
        private Node<T> FixUp(Node<T> node, Node<T> parent)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
                if (parent != null)
                {
                    parent.Right = node;
                }

            }
            if (node.Left != null && IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColor(node);
            }
            if (node.Left != null && !IsRed(node.Left.Left) && IsRed(node.Left.Right))
            {
                if (IsRed(node.Left.Right))
                {
                    node = RotateLeft(node);
                }
                if (IsRed(node.Left) && IsRed(node.Left.Left))
                {
                    node = RotateRight(node);
                }
            }

            return node;
        }
    }
}
