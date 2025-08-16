using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    public class RedBlack<T> where T : IComparable<T>
    {
        private Node<T> root;
        public int Count { get; set; }

        public RedBlack()
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
            root = Remove(nodeToRemove, root);
            root.IsRed = false;
        }
        private Node<T> Remove(Node<T> nodeToRemove, Node<T> current)
        {
            //read reference.txt

            if (current == null)
            {
                return null;
            }

            if (nodeToRemove.Value.CompareTo(current.Value) < 0)
            {
                MoveRedLeft(current);
                
                current.Left = Remove(nodeToRemove, current.Left);
            }
            else
            {
                if (IsRed(current.Left))
                {
                    current = RotateRight(current);
                   // parent.Right = current;
                }

                MoveRedRight(current);

                if (current.Value.Equals(nodeToRemove.Value))
                {
                    if (current.Left == null && current.Right == null)
                    {
                        return null;
                    }
                    else if (current.Left == null || current.Right == null)
                    {
                        if (current.Left == null)
                        {
                            return current.Right;
                        }
                        else
                        {
                            return current.Left;
                        }
                    }

                    Node<T> replacement = current.Right;
                    while (replacement.Left != null)
                    {
                        replacement = replacement.Left;
                    }

                    current.Value = replacement.Value;

                    current.Right = Remove(replacement, current.Right);                               
                }
                else
                {
                    current.Right = Remove(nodeToRemove, current.Right);
                }
            }

            current = FixUp(current);
            return current;
        }
        private void MoveRedRight(Node<T> node)
        {
            //if node.right is a 2 node(if it has 2 black children)
            if (!IsRed(node.Right) && node.Right != null && !IsRed(node.Right.Right) && !IsRed(node.Right.Left))
            {
                FlipColor(node);

                if (node.Left != null && IsRed(node.Left.Left))
                {
                    node = RotateRight(node);
                    FlipColor(node);
                }
            }
            return;
        }
        private void MoveRedLeft(Node<T> node)
        {
            //if node.left is a 2 node
            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                FlipColor(node);

                if (node.Right != null && IsRed(node.Right.Left))
                {
                    node = RotateRight(node);
                    RotateLeft(node);
                }
            }
        }
        private Node<T> FixUp(Node<T> node)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
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
                node.Left = RotateLeft(node.Left);
                
                if (IsRed(node.Left) && IsRed(node.Left.Left))
                {
                    node = RotateRight(node);
                }
            }

            return node;
        }
        public Queue<T> TraversalStart()
        {
            if (root == null)
            {
                throw new Exception("tree is empty");
            }
            Queue<T> queue = new Queue<T>();
            Traversal(root, queue);
            return queue;
        }
        public void Traversal(Node<T> currentNode, Queue<T> queue)
        {
            if (currentNode == null)
            {
                return;
            }

            Traversal(currentNode.Left, queue);
            queue.Enqueue(currentNode.Value);
            Traversal(currentNode.Right, queue);
        }
        public Node<T> Search(T value)
        {
            Node<T> nodeToSearch = new Node<T>(value);
            return Search(nodeToSearch, root);
        }
        private Node<T> Search(Node<T> nodeToSearch, Node<T> current)
        {
            if (current == null)
            {
                return current;
            }

            if (nodeToSearch.Value.CompareTo(current.Value) < 0)
            {
                return Search(nodeToSearch, current.Left);
            }
            else if (nodeToSearch.Value.CompareTo(current.Value) > 0)
            {
                return Search(nodeToSearch, current.Right);
            }
            else
            {
                return current;
            }
        }
    }
}
