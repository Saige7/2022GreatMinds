using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BTreeAssignment
{
    class BTree<T> where T : IComparable<T>
    {
        public Node<T> root;
        public int Count;
        public BTree()
        {
            root = new Node<T>();
            Count = 0;
        }

        //both functions recursive
        //always check for 4 nodes first when inserting

        public Node<T> Search(T value, Node<T> current)
        {

            int index = 0;
            while (index < current.Value.Count && current.Value[index].CompareTo(value) < 0)
            {
                if (current.Value[index].CompareTo(value) == 0)
                {
                    return current;
                }
                index++;
            }



            if (current.Children.Count == 0)
            {
                return null;
            }

            current.Children[index] = Search(value, current.Children[index]);

            return current;
        }

        public void Insert(T value)
        {
            if (root.Value.Count == 3)
            {
                root = PreemptiveTopNodeSplit(root);
            }

            root = Insert(value, root);
        }
        public Node<T> Insert(T value, Node<T> current)
        {
            int index = 0;
            while (index < current.Value.Count && current.Value[index].CompareTo(value) < 0)
            {
                index++;
            }
            //index last node

            if (current.Children.Count == 0)
            {
                current.Value.Insert(index, value);
                Count++;
                return current;
            }
            //index check split

            if (current.Children[index].Value.Count == 3)
            {
                current = PreemptiveSplit(current, index);
                while (index < current.Value.Count && current.Value[index].CompareTo(value) < 0)
                {
                    index++;
                }
            }
            //Recurse
            current.Children[index] = Insert(value, current.Children[index]);

            return current;
        }
        public Node<T> PreemptiveTopNodeSplit(Node<T> node)
        {
            Node<T> nodeLeft = new Node<T>();
            Node<T> nodeRight = new Node<T>();

            nodeLeft.Value.Add(node.Value[0]);
            nodeRight.Value.Add(node.Value[2]);

            node.Value.RemoveAt(0);
            node.Value.RemoveAt(1);

            if (node.Children.Count != 0)
            {
                nodeLeft.Children.Add(node.Children[0]);
                nodeLeft.Children.Add(node.Children[1]);

                nodeRight.Children.Add(node.Children[2]);
                nodeRight.Children.Add(node.Children[3]);

                node.Children.Clear();
            }

            node.Children.Add(nodeLeft);
            node.Children.Add(nodeRight);

            return node;
        }
        public Node<T> PreemptiveSplit(Node<T> parent, int index)
        {
            Node<T> node = parent.Children[index];

            parent.Value.Insert(index, node.Value[1]);
            parent.Children.Insert(index + 1, new Node<T>(node.Value[2]));
            node.Value.RemoveRange(1, 2);

            return parent;
        }
    }
}
