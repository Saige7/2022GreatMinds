namespace AvlTreeAssignment
{
    public class Node<T>
    {
        public T Value;
        public int height;
        public Node<T> right;
        public Node<T> left;

        public Node(T value)
        {
            Value = value;
        }

        public int Balance
        {
            get 
            {
                if(right == null && left == null)
                {
                    return 0;
                }
                if(right == null)
                {
                    return 0 - left.height;
                }
                else if(left == null)
                {
                    return right.height - 0;
                }
                else
                {
                    return right.height - left.height;
                }
            }
        }
        public int ChildCount
        {
            get
            {
                if(right != null && left != null)
                {
                    return 2;
                }
                else if(right != null || left != null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
    public class AVLTree<T> where T : IComparable<T>
    {
        public int count;
        public Node<T> root;

        public int Height(Node<T> givenNode)
        {
            if (givenNode.Value == null || (givenNode.left == null && givenNode.right == null))
            {
                return 0;
            }
            else if (givenNode.left == null || givenNode.right == null)
            {
                if (givenNode.left == null)
                {
                    givenNode.height = givenNode.right.height + 1;
                }
                if (givenNode.right == null)
                {
                    givenNode.height = givenNode.left.height + 1;
                }
            }
            else if (givenNode.left.height > givenNode.right.height)
            {
                givenNode.height = givenNode.left.height + 1;
            }
            else
            {
                givenNode.height = givenNode.right.height + 1;
            }
            return givenNode.height;
        }
        public void RotateLeft(Node<T> givenNode)
        {
            Node<T> savedNode = givenNode.right;
            givenNode.right = givenNode.right.left;
            savedNode.left = givenNode;

            givenNode.height = Height(givenNode);
            savedNode.height = Height(savedNode);
        }
        public void RotateRight(Node<T> givenNode)
        {
            Node<T> savedNode = givenNode.left;
            givenNode.left = givenNode.left.right;
            savedNode.right = givenNode;

            givenNode.height = Height(givenNode);
            savedNode.height = Height(savedNode);
        }
        public void Rotations(Node<T> givenNode)
        {
            while (givenNode.Balance > 1)
            {
                if (givenNode.Balance == 2)
                {
                    if (givenNode.right.Balance <= -1)
                    {
                        RotateRight(givenNode.right);
                        RotateLeft(givenNode);
                    }
                    else
                    {
                        RotateLeft(givenNode);
                    }
                }
                else
                {
                    RotateLeft(givenNode);
                }
            }
            while (givenNode.Balance < -1)
            {
                if (givenNode.Balance == -2)
                {
                    if (givenNode.left.Balance >= 1)
                    {
                        RotateLeft(givenNode.left);
                        RotateRight(givenNode);
                    }
                    else
                    {
                        RotateRight(givenNode);
                    }
                }
                else
                {
                    RotateRight(givenNode);
                }
            }
        }
        private Node<T> InsertRecursion(Node<T> insertNode, Node<T> current)
        {
            if(current == null)
            {
                current = insertNode;
                return insertNode;
            }

            if (insertNode.Value.CompareTo(current.Value) < 0)
            {
                Node<T> left = InsertRecursion(insertNode, current.left);
                current.left = left;
            }
            else
            {
                Node<T> right = InsertRecursion(insertNode, current.right);
                current.right = right;
            }

            current.height = Height(current);
            return current;
        }
        public void Insert(T givenValue)
        {
            Node<T> insertNode = new Node<T>(givenValue);
            Node<T> current = root;

            if(root == null)
            {
                root = insertNode;
                root.height = 1;
                count++;
                return;
            }

            root = InsertRecursion(insertNode, current);
            Rotations(root);
            count++;
        }
        private Node<T> DeleteRecursion(T givenValue, Node<T> current)
        {
            if(current == null)
            {
                return null;
            }
            
            if(givenValue.CompareTo(current.Value) == 0)
            {
                if(current.left == null && current.right == null)
                {
                    return null;
                }
                else if(current.left == null || current.right == null)
                {
                    if(current.left == null && current.right != null)
                    {
                        return current.right;
                    }
                    else
                    {
                        return current.left;
                    }
                }
                else
                {
                    Node<T> savedNode = current;
                    current = current.left;
                    while(current.right.right != null)
                    {
                        current = current.right;
                    }
                    savedNode.Value = current.right.Value;
                    current.right = current.right.left;
                }
            }
            else if(givenValue.CompareTo(current.Value) < 0)
            {
                Node<T> left = DeleteRecursion(givenValue, current.left);
                current.left = left;
            }
            else
            {
                Node<T> right = DeleteRecursion(givenValue, current.right);
                current.right = right;
            }

            current.height = Height(current);
            return current;
        }
        public void Delete(T givenValue)
        {
            Node<T> current = root;
            
            if(root == null)
            {
                throw new Exception("tree is empty");
            }
            
            root = DeleteRecursion(givenValue, current);
            Rotations(root);
            count--;
        }

        //public Queue<T> BreadthFirstRecursion(Queue<T> levelOrder, Node<T> current)
        //{
        //    if (levelOrder.Count == 0)
        //    {
        //        return levelOrder;
        //    }

        //    levelOrder.Enqueue(current.Value);
        //    levelOrder.Dequeue();
        //}
        //public Queue<T> BreadthFirstSearch()
        //{
        //    Queue<T> levelOrder = new Queue<T>();
        //    Node<T> current = root;

        //    if (root == null)
        //    {
        //        throw new Exception("tree is empty");
        //    }
        //}

        public Queue<T> PreOrderRecursion(Queue<T> preOrderQueue, Node<T> current)
        {
            if(current == null)
            {
                return preOrderQueue;
            }

            preOrderQueue.Enqueue(current.Value);

            PreOrderRecursion(preOrderQueue, current.left);
            PreOrderRecursion(preOrderQueue, current.right);

            return preOrderQueue;
        }
        public Queue<T> PreOrder() 
        {
            Queue<T> preOrderQueue = new Queue<T>();
            Node<T> current = root;

            if(root == null)
            {
                throw new Exception("tree is empty");
            }

            preOrderQueue = PreOrderRecursion(preOrderQueue, current);
            return preOrderQueue;
        }
        public Queue<T> InOrderRecursion(Queue<T> inOrderQueue, Node<T> current)
        {
            if(current == null)
            {
                return inOrderQueue;
            }

            InOrderRecursion(inOrderQueue, current.left);
            inOrderQueue.Enqueue(current.Value);
            InOrderRecursion(inOrderQueue, current.right);

            return inOrderQueue;
        }
        public Queue<T> InOrder()
        {
            Queue<T> inOrderQueue = new Queue<T>();
            Node<T> current = root;

            if(root == null)
            {
                throw new Exception("tree is empty");
            }

            inOrderQueue = InOrderRecursion(inOrderQueue, current);
            return inOrderQueue;
        }
        public Queue<T> PostOrderRecursion(Queue<T> postOrderQueue, Node<T> current)
        {
            if(current == null)
            {
                return postOrderQueue;
            }

            PostOrderRecursion(postOrderQueue, current.left);
            PostOrderRecursion(postOrderQueue, current.right);
            postOrderQueue.Enqueue(current.Value);

            return postOrderQueue;
        }
        public Queue<T> PostOrder()
        {
            Queue<T> postOrderQueue = new Queue<T>();
            Node<T> current = root;

            if(root == null)
            {
                throw new Exception("tree is empty");
            }

            postOrderQueue = PostOrderRecursion(postOrderQueue, current);
            return postOrderQueue;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();
            Queue<int> queue = new Queue<int>();

            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(4);
            tree.Insert(9);
            tree.Insert(3);
            tree.Insert(5);
            tree.Insert(8);

            queue = tree.PreOrder();
            int count = queue.Count;

            for(int i = 0; i < count; i++)
            {
                Console.WriteLine(queue.Dequeue());
            }

            Console.WriteLine();

            queue = tree.InOrder();
            for(int i = 0; i < count; i++)
            {
                Console.WriteLine(queue.Dequeue());
            }

            Console.WriteLine();

            queue = tree.PostOrder();
            for(int i = 0; i < count; i++)
            {
                Console.WriteLine(queue.Dequeue());
            }
        }
    }
}