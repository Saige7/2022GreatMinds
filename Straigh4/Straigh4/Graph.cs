using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Straigh4
{
    internal class Graph<T> where T : IEquatable<T>
    {
        private List<Vertex<T>> vertices;
        public IReadOnlyList<Vertex<T>> Vertices => vertices;

        private List<Edge<T>> edges;
        public IReadOnlyList<Edge<T>> Edges => edges;

        public int VertexCount => vertices.Count;

        public Graph()
        {
            vertices = new List<Vertex<T>>();
            edges = new List<Edge<T>>();
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
                edges.Add(edge);
                return true;
            }

            return false;
        }

        public void AddUndirectedEdge(Vertex<T> a, Vertex<T> b, float distance)
        {
            AddEdge(a, b, distance);
            AddEdge(b, a, distance);
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
                            edges.Remove(vertices[i].Neighbors[j]);
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
                if (vertices[i].Value.Equals(value))
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
    }
}
