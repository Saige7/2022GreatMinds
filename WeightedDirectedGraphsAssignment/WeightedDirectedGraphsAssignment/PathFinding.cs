using System.Drawing;

namespace WeightedDirectedGraphsAssignment
{
    public class PathFinding<T> where T : IComparable<T>
    {
        public Graph<T> Graph;
        public PathFinding(Graph<T> graph)
        {
            Graph = graph;
        }

        public Stack<Vertex<T>> DijkstraAlgorithm(Vertex<T> start, Vertex<T> end)
        {
            Queue<Vertex<T>> priorityQueue = new Queue<Vertex<T>>();
            Stack<Vertex<T>> results = new Stack<Vertex<T>>();
            Dictionary<Vertex<T>, (Vertex<T> founder, float distance)> info = new Dictionary<Vertex<T>, (Vertex<T> founder, float distance)>();

            for (int i = 0; i < Graph.VertexCount; i++)
            {
                Graph.Vertices[i].Visted = false;
                info.Add(Graph.Vertices[i], (null, float.MaxValue));
            }

            info[start] = (null, 0);
            priorityQueue.Enqueue(start);

            while (priorityQueue.Count != 0 || !end.Visted)
            {
                Vertex<T> dequeuedVertex = priorityQueue.Dequeue();
                for (int i = 0; i < dequeuedVertex.NeighborCount; i++)
                {
                    float tentativeDistance = info[dequeuedVertex].distance + dequeuedVertex.Neighbors[i].Distance;

                    if (tentativeDistance < info[dequeuedVertex.Neighbors[i].EndingPoint].distance)
                    {
                        info[dequeuedVertex.Neighbors[i].EndingPoint] = (dequeuedVertex, tentativeDistance);
                    }

                    if (dequeuedVertex.Neighbors[i].EndingPoint.Visted == false && !priorityQueue.Contains(dequeuedVertex.Neighbors[i].EndingPoint))
                    {
                        priorityQueue.Enqueue(dequeuedVertex.Neighbors[i].EndingPoint);
                    }
                }

                dequeuedVertex.Visted = true;
            }

            Vertex<T> currentVertex = end;
            while (currentVertex != start)
            {
                results.Push(currentVertex);
                currentVertex = info[currentVertex].founder;
            }
            results.Push(start);

            return results;
        }

        public Stack<Vertex<T>> AStarAlgorithm(Vertex<T> start, Vertex<T> end, Dictionary<Vertex<T>, Point> map)
        {
            Queue<Vertex<T>> priorityQueue = new Queue<Vertex<T>>();
            Stack<Vertex<T>> results = new Stack<Vertex<T>>();
            Dictionary<Vertex<T>, (Vertex<T> founder, float distance, float finalDistance)> info = new Dictionary<Vertex<T>, (Vertex<T> founder, float distance, float finalDistance)>();

            for (int i = 0; i < Graph.VertexCount; i++)
            {
                Graph.Vertices[i].Visted = false;
                info.Add(Graph.Vertices[i], (null, float.MaxValue, float.MaxValue));
            }

            info[start] = (null, 0, Heuristics(start, end, map));
            priorityQueue.Enqueue(start);

            while (priorityQueue.Count != 0 || !end.Visted)
            {
                Vertex<T> dequeuedVertex = priorityQueue.Dequeue();
                for (int i = 0; i < dequeuedVertex.NeighborCount; i++)
                {
                    float tentativeDistance = dequeuedVertex.Neighbors[i].Distance + info[dequeuedVertex].distance;

                    if (tentativeDistance < info[dequeuedVertex.Neighbors[i].EndingPoint].distance)
                    {
                        info[dequeuedVertex.Neighbors[i].EndingPoint] = (dequeuedVertex, tentativeDistance, tentativeDistance + Heuristics(dequeuedVertex.Neighbors[i].EndingPoint, end, map));
                    }

                    if (!dequeuedVertex.Neighbors[i].EndingPoint.Visted && !priorityQueue.Contains(dequeuedVertex.Neighbors[i].EndingPoint))
                    {
                        priorityQueue.Enqueue(dequeuedVertex.Neighbors[i].EndingPoint);
                    }
                }

                dequeuedVertex.Visted = true;
            }

            Vertex<T> currentVertex = end;
            while (currentVertex != start)
            {
                results.Push(currentVertex);
                currentVertex = info[currentVertex].founder;
            }
            results.Push(start);

            return results;
        }

        public float Heuristics(Vertex<T> node, Vertex<T> goal, Dictionary<Vertex<T>, Point> map)
        {
            float x = Math.Abs(map[node].X - map[goal].X);
            float y = Math.Abs(map[node].Y - map[goal].Y);

            return x + y;
        }

        public bool BellmanFordAlgorithm(Vertex<T> start)
        {
            Dictionary<Vertex<T>, (Vertex<T> founder, float distance)> info = new Dictionary<Vertex<T>, (Vertex<T> founder, float distance)>();

            for (int i = 0; i < Graph.VertexCount; i++)
            {
                info.Add(Graph.Vertices[i], (null, float.MaxValue));
            }

            info[start] = (null, 0);

            for (int s = 0; s < Graph.VertexCount - 1; s++)
            {
                for (int i = 0; i < Graph.VertexCount; i++)
                {
                    for (int j = 0; j < Graph.Vertices[i].NeighborCount; j++)
                    {
                        float tentativeDistance = info[Graph.Vertices[i]].distance + Graph.Vertices[i].Neighbors[j].Distance;

                        if (tentativeDistance < info[Graph.Vertices[i].Neighbors[j].EndingPoint].distance)
                        {
                            info[Graph.Vertices[i].Neighbors[j].EndingPoint] = (Graph.Vertices[i], tentativeDistance);
                        }
                    }
                }
            }


            for (int i = 0; i < Graph.VertexCount - 1; i++)
            {
                for (int j = 0; j < Graph.Vertices[i].NeighborCount; j++)
                {
                    float tentativeDistance = info[Graph.Vertices[i]].distance + Graph.Vertices[i].Neighbors[j].Distance;

                    if (tentativeDistance < info[Graph.Vertices[i].Neighbors[j].EndingPoint].distance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}