namespace BinarySearchTreeAssignment
{
    class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node<T> root;
        public int Count { get; private set; }

        public BinarySearchTree()
        {
        }

        public Node<T> Search(T givenValue)
        {
            Node<T> currentNode = root;
            if (root == null)
            {
                throw new Exception("Tree is empty");
            }
            if (root.Value.CompareTo(givenValue) == 0)
            {
                return root;
            }
            while (currentNode != null && currentNode.Value.CompareTo(givenValue) != 0)
            {
                if (currentNode.Value.CompareTo(givenValue) < 0)
                {
                    currentNode = currentNode.right;
                }
                else
                {
                    currentNode = currentNode.left;
                }
            }
            return currentNode;
        }
        public void Insert(T givenValue)
        {
            Node<T> nodeToInsert = new Node<T>(givenValue);
            Node<T> currentNode = root;
            bool isInserted = false;
            if (root == null)
            {
                root = nodeToInsert;
                Count++;
                return;
            }

            while (isInserted == false)
            {
                if (currentNode.Value.CompareTo(nodeToInsert.Value) < 0)
                {
                    if (currentNode.right == null)
                    {
                        currentNode.right = nodeToInsert;
                        isInserted = true;
                    }
                    else
                    {
                        currentNode = currentNode.right;
                    }
                }
                else
                {
                    if (currentNode.left == null)
                    {
                        currentNode.left = nodeToInsert;
                        isInserted = true;
                    }
                    else
                    {
                        currentNode = currentNode.left;
                    }
                }
            }
        }

        public Node<T> Minimum()
        {
            Node<T> currentNode = root;
            if (root == null)
            {
                throw new Exception("Tree is empty");
            }

            while (currentNode.left != null)
            {
                currentNode = currentNode.left;
            }
            return currentNode;
        }
        public Node<T> Maximum()
        {
            Node<T> currentNode = root;
            if (root == null)
            {
                throw new Exception("Tree is empty");
            }

            while (currentNode.right != null)
            {
                currentNode = currentNode.right;
            }
            return currentNode;
        }

        public void Delete(Node<T> givenNode)
        {
            Node<T> currentNode = root;
            Node<T> parent = null;
            if (root == null)
            {
                throw new Exception("Tree is empty");
            }
            while (currentNode != null && currentNode.Equals(givenNode) == false)
            {
                if (currentNode.Value.CompareTo(givenNode.Value) < 0)
                {
                    parent = currentNode;
                    currentNode = currentNode.right;
                }
                else
                {
                    parent = currentNode;
                    currentNode = currentNode.left;
                }
            }
            if (givenNode.right == null && givenNode.left == null)
            {
                if (givenNode == parent.left)
                {
                    parent.left = null;
                }
                else
                {
                    parent.right = null;
                }
            }
            else if (givenNode.right == null || givenNode.left == null)
            {
                if (givenNode == parent.left)
                {
                    if (givenNode.right == null)
                    {
                        parent.left = givenNode.left;
                    }
                    else
                    {
                        parent.left = givenNode.right;
                    }
                }
                else
                {
                    if (givenNode.right == null)
                    {
                        parent.right = givenNode.left;
                    }
                    else
                    {
                        parent.right = givenNode.right;
                    }
                }
            }
            else
            {
                currentNode = givenNode.left;
                while (currentNode.right != null)
                {
                    parent = currentNode;
                    currentNode = currentNode.right;
                }

                givenNode.Value = currentNode.Value;

                Delete(currentNode);
            }
        }
        public Queue<T> PreOrderTraversal()
        {
            Stack<Node<T>> preOrderStack = new Stack<Node<T>>();
            Queue<T> preOrderQueue = new Queue<T>();

            if (root == null)
            {
                throw new Exception("Tree is empty");
            }

            preOrderStack.Push(root);

            while (preOrderStack.Count != 0)
            {
                Node<T> poppedNode = preOrderStack.Pop();
                preOrderQueue.Enqueue(poppedNode.Value);
                if (poppedNode.right != null)
                {
                    preOrderStack.Push(poppedNode.right);
                }
                if (poppedNode.left != null)
                {
                    preOrderStack.Push(poppedNode.left);
                }
            }
            return preOrderQueue;
        }
        public Queue<T> PreOrderStart()
        {
            if (root == null)
            {
                throw new Exception("tree is empty");
            }
            Queue<T> preOrderRecursiveQueue = new Queue<T>();
            PreOrderRecursive(root, preOrderRecursiveQueue);
            return preOrderRecursiveQueue;
        }
        public void PreOrderRecursive(Node<T> currentNode, Queue<T> preOrderRecursiveQueue)
        {
            if (currentNode == null)
            {
                return;
            }

            preOrderRecursiveQueue.Enqueue(currentNode.Value);

            PreOrderRecursive(currentNode.left, preOrderRecursiveQueue);
            PreOrderRecursive(currentNode.right, preOrderRecursiveQueue);
        }
        public Queue<T> InOrderTraversal()
        {
            Stack<Node<T>> inOrderStack = new Stack<Node<T>>();
            Queue<T> inOrderQueue = new Queue<T>();
            Node<T> currentNodeParent = null;
            Node<T> currentNode = root;
            inOrderStack.Push(null);

            if (root == null)
            {
                throw new Exception("Tree is empty");
            }

            while (currentNode != null)
            {
                while (currentNode.left != null)
                {
                    currentNodeParent = currentNode;
                    currentNode = currentNode.left;
                    inOrderStack.Push(currentNodeParent);
                }

                inOrderQueue.Enqueue(currentNode.Value);

                if (currentNode.right != null)
                {
                    currentNode = currentNode.right;
                    continue;
                }

                bool shouldLoop = true;

                while (currentNode != null && shouldLoop == true)
                {
                    Node<T> poppedNode = inOrderStack.Pop();
                    if (poppedNode == null)
                    {
                        currentNode = poppedNode;
                        break;
                    }
                    inOrderQueue.Enqueue(poppedNode.Value);

                    if (poppedNode.right != null)
                    {
                        currentNode = poppedNode.right;
                        shouldLoop = false;
                    }
                }
            }
            return inOrderQueue;
        }
        public Queue<T> PostOrderTraversal()
        {
            Stack<(Node<T>, bool)> postOrderStack = new Stack<(Node<T>, bool)>();
            Queue<T> postOrderQueue = new Queue<T>();
            (Node<T>, bool) rootItem = (root, true);

            postOrderStack.Push(rootItem);

            if (root == null)
            {
                throw new Exception("Tree is empty");
            }

            while (postOrderStack.Count != 0)
            {
                (Node<T>, bool) topOfStack = postOrderStack.Peek();
                bool leftVisted = true;
                bool rightVisted = true;

                if (topOfStack.Item1.right != null && postOrderQueue.Contains(topOfStack.Item1.right.Value) == true)
                {
                    rightVisted = false;
                }
                if (topOfStack.Item1.left != null && postOrderQueue.Contains(topOfStack.Item1.left.Value) == true)
                {
                    leftVisted = false;
                }

                (Node<T>, bool) ItemsRight = (topOfStack.Item1.right, rightVisted);
                (Node<T>, bool) ItemsLeft = (topOfStack.Item1.left, leftVisted);

                if (topOfStack.Item2 == false)
                {
                    (Node<T>, bool) poppedNode = postOrderStack.Pop();
                    postOrderQueue.Enqueue(poppedNode.Item1.Value);
                }
                else
                {
                    if (topOfStack.Item1.left == null && topOfStack.Item1.right == null)
                    {
                        topOfStack = postOrderStack.Pop();
                        topOfStack.Item2 = false;
                        postOrderStack.Push(topOfStack);
                        continue;
                    }
                    else if (ItemsRight.Item2 == false && ItemsLeft.Item2 == false)
                    {
                        topOfStack = postOrderStack.Pop();
                        topOfStack.Item2 = false;
                        postOrderStack.Push(topOfStack);
                        continue;
                    }

                    if (topOfStack.Item1.right != null)
                    {
                        postOrderStack.Push(ItemsRight);
                    }
                    if (topOfStack.Item1.left != null)
                    {
                        postOrderStack.Push(ItemsLeft);
                    }
                }
            }
            return postOrderQueue;
        }
        public Queue<T> BreadthFirstTraversal()
        {
            Queue<Node<T>> traversalQueue = new Queue<Node<T>>();
            Queue<T> resultsQueue = new Queue<T>();

            traversalQueue.Enqueue(root);

            if (root == null)
            {
                throw new Exception("Tree is empty");
            }

            while (traversalQueue.Count != 0)
            {
                Node<T> topOfQueue = traversalQueue.Dequeue();

                resultsQueue.Enqueue(topOfQueue.Value);

                if (topOfQueue.left != null)
                {
                    traversalQueue.Enqueue(topOfQueue.left);
                }
                if (topOfQueue.right != null)
                {
                    traversalQueue.Enqueue(topOfQueue.right);
                }
            }
            return resultsQueue;

        }
    }
}