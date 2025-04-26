namespace UnweightedUnDirectedGraphAssignment
{
    class Graph<T> where T : IComparable<T>
    {
        public List<Vertex<T>> Vertices;
        public int VerticesCount => Vertices.Count;

        public Graph()
        {
            Vertices = new List<Vertex<T>>();
        }
        public bool AddVertex(Vertex<T> givenVertex)
        {
            if (givenVertex != null && givenVertex.NeighborCount == 0 && !Vertices.Contains(givenVertex))
            {
                Vertices.Add(givenVertex);
                return true;
            }
            return false;
        }
        public bool RemoveVertex(Vertex<T> givenVertex)
        {
            if (Vertices.Contains(givenVertex))
            {
                givenVertex.Neighbors.Clear();
                Vertices.Remove(givenVertex);
                return true;
            }
            return false;
        }
        public bool AddEdge(Vertex<T> a, Vertex<T> b)
        {
            if (a != null && b != null && Vertices.Contains(a) && Vertices.Contains(b))
            {
                a.Neighbors.Add(b);
                b.Neighbors.Add(a);
                return true;
            }
            return false;
        }
        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            if (a != null && b != null && Vertices.Contains(a) && Vertices.Contains(b) && a.Neighbors.Contains(b) && b.Neighbors.Contains(a))
            {
                a.Neighbors.Remove(b);
                b.Neighbors.Remove(a);
                return true;
            }
            return false;
        }
        public Vertex<T> Search(T givenValue)
        {
            int current = -1;
            for (int i = 0; i < VerticesCount; i++)
            {
                if (givenValue.CompareTo(Vertices[i].Value) == 0)
                {
                    current = i;
                    break;
                }
            }

            if (current == -1)
            {
                return null;
            }

            return Vertices[current];
        }

        //public Queue<Vertex<T>> PreOrderTraversal()
        //{
        //    Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
        //    Queue<Vertex<T>> queue = new Queue<Vertex<T>>();

        //    stack.Push(Vertices[0]);
        //    while (stack.Count != 0)
        //    {
        //        Vertex<T> poppedVertex = stack.Pop();
        //        queue.Enqueue(poppedVertex);

        //        for (int i = 0; i < poppedVertex.NeighborCount; i++)
        //        {
        //            if (queue.Contains(poppedVertex.Neighbors[poppedVertex.NeighborCount - i - 1]))
        //            {
        //                continue;
        //            }
        //            stack.Push(poppedVertex.Neighbors[poppedVertex.NeighborCount - i - 1]);
        //        }
        //    }

        //    return queue;
        //}
        //public Queue<Vertex<T>> RecursivePreOrderTraversal()
        //{
        //    if (Vertices[0] == null)
        //    {
        //        throw new Exception("empty graph");
        //    }

        //    Queue<Vertex<T>> queue = new Queue<Vertex<T>>();

        //    RecursivePreOrderTraversal(0,Vertices[0], queue);
        //    return queue;
        //}
        //public void RecursivePreOrderTraversal(int index,Vertex<T> currentVertex, Queue<Vertex<T>> queue)
        //{
        //    if (currentVertex == null)
        //    {
        //        return;
        //    }

        //    if (!queue.Contains(currentVertex))
        //    {
        //        queue.Enqueue(currentVertex);
        //    }
        //    if(currentVertex.NeighborCount == 1)
        //    {
        //        currentVertex = currentVertex.Neighbors[0];
        //    }
        //    //if (currentVertex.NeighborCount <= index)
        //    //{
        //    //    RecursivePreOrderTraversal(index, currentVertex.Neighbors[currentVertex.NeighborCount - 1], queue);
        //    //}

        //    RecursivePreOrderTraversal(index + 1, currentVertex.Neighbors[index], queue);
        //}
        //public List<Vertex<T>> InOrderTraversal()
        //{
        //    Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
        //    List<Vertex<T>> list = new List<Vertex<T>>();

        //    Vertex<T> current = Vertices[0];

        //    for (int i = 0; i < current.NeighborCount; i++)
        //    {
        //        if (stack.Contains(current.Neighbors[i]))
        //        {
        //            continue;
        //        }
        //        stack.Push(current);
        //        current = current.Neighbors[i];
        //        i = 0;
        //    }

        //    stack.Push(current);
        //    while (stack.Count != 0)
        //    {
        //        Vertex<T> poppedVertex = stack.Pop();
        //        list.Add(poppedVertex);
        //        for (int i = 0; i < poppedVertex.NeighborCount; i++)
        //        {
        //            if (list.Contains(poppedVertex.Neighbors[i]) || stack.Contains(poppedVertex.Neighbors[i]))
        //            {
        //                continue;
        //            }

        //            stack.Push(poppedVertex.Neighbors[i]);
        //        }
        //    }

        //    return list;
        //}
        //public Stack<Vertex<T>> PostOrderTraversal()
        //{
        //    Stack<Vertex<T>> storingStack = new Stack<Vertex<T>>();
        //    Stack<Vertex<T>> stack = new Stack<Vertex<T>>();

        //    stack.Push(Vertices[0]);
        //    while (stack.Count != 0)
        //    {
        //        Vertex<T> poppedVertex = stack.Pop();
        //        storingStack.Push(poppedVertex);

        //        for (int i = 0; i < poppedVertex.NeighborCount; i++)
        //        {
        //            if (storingStack.Contains(poppedVertex.Neighbors[i]))
        //            {
        //                continue;
        //            }
        //            stack.Push(poppedVertex.Neighbors[i]);
        //        }
        //    }
        //    return storingStack;
        //}

        public List<Vertex<T>> BreadthFirstTraversal()
        {
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            List<Vertex<T>> results = new List<Vertex<T>>();

            queue.Enqueue(Vertices[0]);
            while (queue.Count != 0)
            {
                Vertex<T> dequeuedVertex = queue.Dequeue();
                results.Add(dequeuedVertex);
                for (int i = 0; i < dequeuedVertex.NeighborCount; i++)
                {
                    if (results.Contains(dequeuedVertex.Neighbors[i]))
                    {
                        continue;
                    }
                    queue.Enqueue(dequeuedVertex.Neighbors[i]);
                }

            }

            return results;
        }

        public List<Vertex<T>> DepthFirstTraversal()
        {
            List<Vertex<T>> list = new List<Vertex<T>>();
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();

            stack.Push(Vertices[0]);

            while (stack.Count != 0)
            {
                Vertex<T> poppedNode = stack.Pop();
                list.Add(poppedNode);

                for (int i = 0; i < poppedNode.NeighborCount; i++)
                {
                    if (!list.Contains(poppedNode.Neighbors[poppedNode.NeighborCount - i - 1]))
                    {
                        stack.Push(poppedNode.Neighbors[poppedNode.NeighborCount - i - 1]);
                    }
                }
            }

            return list;
        }

        public List<Vertex<T>> RecursiveDepthFirst()
        {
            if (Vertices[0] == null)
            {
                throw new Exception("graph is empty");
            }

            List<Vertex<T>> list = new List<Vertex<T>>();

            RecursiveDepthFirst(0, Vertices[0], list);

            return list;
        }
        public void RecursiveDepthFirst(int index, Vertex<T> currentVertex, List<Vertex<T>> list)
        {
            if (list.Count == VerticesCount || currentVertex.visted)
            {
                return;
            }

            if (!list.Contains(currentVertex))
            {
                list.Add(currentVertex);
                currentVertex.visted = true;
            }
            if (index >= currentVertex.NeighborCount)
            {
                return;
            }
            for (int i = 0; i < currentVertex.NeighborCount; i++)
            {
                if (!currentVertex.Neighbors[i].visted)
                {
                    RecursiveDepthFirst(index + 1, currentVertex.Neighbors[i], list);
                }
            }
        }
        public Stack<Vertex<T>> SingleSourceShortestPathBFS(Vertex<T> start, Vertex<T> end)
        {
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            List<Vertex<T>> path = new List<Vertex<T>>();
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();

            Dictionary<Vertex<T>, Vertex<T>> parentMap = new Dictionary<Vertex<T>, Vertex<T>>();
            queue.Enqueue(start);

            while (queue.Peek() != end && queue.Count != 0)
            {
                Vertex<T> dequeuedVertex = queue.Dequeue();

                for (int i = 0; i < dequeuedVertex.NeighborCount; i++)
                {
                    if (!parentMap.ContainsKey(dequeuedVertex.Neighbors[i]))
                    {
                        parentMap.Add(dequeuedVertex.Neighbors[i], dequeuedVertex);
                    }
                    queue.Enqueue(dequeuedVertex.Neighbors[i]);
                }
            }

            Vertex<T> currentVertex = end;
            while (currentVertex != start)
            {
                stack.Push(currentVertex);
                currentVertex = parentMap[currentVertex];
            }
            stack.Push(start);

            return stack;
        }
    }
}