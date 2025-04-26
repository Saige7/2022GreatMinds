using System.Text.Json;

namespace WeightedDirectedGraphsAssignment
{
    public class Graph<T> where T : IComparable<T>
    {
        private List<Vertex<T>> vertices;
        public IReadOnlyList<Vertex<T>> Vertices => vertices;
        public IReadOnlyList<Edge<T>> Edges { get; }

        public int VertexCount => vertices.Count;
        
        public Graph()
        {
            vertices = new List<Vertex<T>>();
        }

        public void AddVertex(Vertex<T> vertex)
        {
            if (vertex != null && vertex.NeighborCount == 0 && !vertices.Contains(vertex))
            {
                vertices.Add(vertex);
            }
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (vertices.Contains(vertex))
            {
                for (int i = 0; i < VertexCount; i++)
                {
                    for (int j = 0; j < vertices[i].NeighborCount; j++)
                    {
                        if (vertices[i].Neighbors[j].EndingPoint == vertex)
                        {
                            vertices[i].Neighbors.RemoveAt(j);
                        }
                    }
                }

                vertices.Remove(vertex);
                return true;
            }

            return false;
        }

        public bool AddEdge(Vertex<T> a, Vertex<T> b, float distance)
        {
            if (a != null && b != null && vertices.Contains(a) && vertices.Contains(b) && GetEdge(a, b) == null)
            {
                Edge<T> edge = new Edge<T>(a, b, distance);
                a.Neighbors.Add(edge);
                return true;
            }

            return false;
        }

        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            if (a != null && b != null)
            {
                for (int i = 0; i < VertexCount; i++)
                {
                    for (int j = 0; j < vertices[i].NeighborCount; j++)
                    {
                        if (vertices[i].Neighbors[j].EndingPoint == b && vertices[i].Neighbors[j].StartingPoint == a)
                        {
                            vertices[i].Neighbors.RemoveAt(j);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public Vertex<T> Search(T value)
        {
            int index = -1;

            for (int i = 0; i < VertexCount; i++)
            {
                if (vertices[i].Value.CompareTo(value) == 0)
                {
                    index = i;
                }
            }

            if (index == -1)
            {
                return null;
            }
            return vertices[index];
        }

        public Edge<T> GetEdge(Vertex<T> a, Vertex<T> b)
        {
            if (a != null && b != null)
            {
                for (int i = 0; i < VertexCount; i++)
                {
                    for (int j = 0; j < vertices[i].NeighborCount; j++)
                    {
                        if (vertices[i].Neighbors[j].EndingPoint == b && vertices[i].Neighbors[j].StartingPoint == a)
                        {
                            return vertices[i].Neighbors[j];
                        }
                    }
                }
            }

            return null;
        }

        public Stack<Vertex<T>> DepthFirst(Vertex<T> start, Vertex<T> end)
        {
            Stack<Vertex<T>> result = new Stack<Vertex<T>>();
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            Dictionary<Vertex<T>, Vertex<T>> parentMap = new Dictionary<Vertex<T>, Vertex<T>>();

            stack.Push(start);
            while (stack.Peek() != end && stack.Count != 0)
            {
                Vertex<T> poppedVertex = stack.Pop();

                for (int i = poppedVertex.NeighborCount - 1; i >= 0; i--)
                {
                    if (!parentMap.ContainsKey(poppedVertex.Neighbors[i].EndingPoint))
                    {
                        parentMap.Add(poppedVertex.Neighbors[i].EndingPoint, poppedVertex);
                    }
                    stack.Push(poppedVertex.Neighbors[i].EndingPoint);
                }
            }

            float cost = 0;
            Vertex<T> currentVertex = end;

            while (currentVertex != start)
            {
                result.Push(currentVertex);

                Edge<T> edge = GetEdge(parentMap[currentVertex], currentVertex);
                cost += edge.Distance;

                currentVertex = parentMap[currentVertex];
            }
            result.Push(start);

            Console.WriteLine();
            Console.WriteLine(cost);

            return result;
        }

        public Stack<Vertex<T>> BreadthFirst(Vertex<T> start, Vertex<T> end)
        {
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            List<Vertex<T>> path = new List<Vertex<T>>();
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            Dictionary<Vertex<T>, Vertex<T>> parentMap = new Dictionary<Vertex<T>, Vertex<T>>();
            
            queue.Enqueue(start);

            while (queue.Peek() != end && queue.Count != 0)
            {
                Vertex<T> poppedVertex = queue.Dequeue();

                for (int i = 0; i < poppedVertex.NeighborCount; i++)
                {
                    if (!parentMap.ContainsKey(poppedVertex.Neighbors[i].EndingPoint))
                    {
                        parentMap.Add(poppedVertex.Neighbors[i].EndingPoint, poppedVertex);
                    }
                    queue.Enqueue(poppedVertex.Neighbors[i].EndingPoint);
                }
            }

            float cost = 0;
            Vertex<T> currentVertex = end;

            while (currentVertex != start)
            {
                stack.Push(currentVertex);

                Edge<T> edge = GetEdge(parentMap[currentVertex], currentVertex);
                cost += edge.Distance;
                
                currentVertex = parentMap[currentVertex];
            }
            stack.Push(start);

            Console.WriteLine(cost);

            return stack;
        }

    }
}