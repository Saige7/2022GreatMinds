
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFindMaze
{
    internal class Maze
    {
        private Graph<Vector2> graph;
        public int Size;
        public Vertex<Vector2> start;
        public Vertex<Vector2> end;
        private QuickUnion<Vector2> quickUnion;
        private QuickFind<Vector2> quickFind;
        private int wallSize;

        public Maze(int size)
        {
            graph = new Graph<Vector2>();
            Size = size;
            wallSize = 400 / size;

            for (int i = 0; i < size * size; i++)
            {
                Vertex<Vector2> newVertex = new Vertex<Vector2>(new Vector2((wallSize / 2) + (wallSize * (i % size)), (wallSize / 2) + (wallSize * (i / size))));
                graph.AddVertex(newVertex);
            }
            for (int i = 0; i < graph.VertexCount; i++)
            {
                if (i + 1 < graph.VertexCount && (i + 1) % size != 0)
                {
                    graph.AddEdge(graph.vertices[i], graph.vertices[i + 1], wallSize);
                }
                if (i + size < graph.VertexCount)
                {
                    graph.AddEdge(graph.vertices[i], graph.vertices[i + size], wallSize);
                }
            }

            start = graph.vertices[0];
            end = graph.vertices[graph.VertexCount - 1];
            quickUnion = new QuickUnion<Vector2>(graph.vertices);
            quickFind = new QuickFind<Vector2>(graph.vertices);
        }

        public void Generate()
        {
            while (!quickFind.AreConnected(start, end))
            {
                Random random = new Random();
                int randomEdge = random.Next(0, graph.edges.Count);

                if (!quickFind.AreConnected(graph.edges[randomEdge].StartingPoint, graph.edges[randomEdge].EndingPoint))
                {
                    quickFind.Union(graph.edges[randomEdge].StartingPoint, graph.edges[randomEdge].EndingPoint);
                    graph.RemoveEdge(graph.edges[randomEdge].StartingPoint, graph.edges[randomEdge].EndingPoint);

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < graph.edges.Count; i++)
            {
                Vector2 center;
                Vector2 start;
                Vector2 end;

                if (graph.edges[i].StartingPoint.Value.X == graph.edges[i].EndingPoint.Value.X)
                {
                    center = new Vector2(graph.edges[i].StartingPoint.Value.X, (graph.edges[i].StartingPoint.Value.Y + graph.edges[i].EndingPoint.Value.Y) / 2);
                    start = new Vector2(center.X - graph.edges[i].Distance, center.Y);
                    end = new Vector2(center.X + graph.edges[i].Distance, center.Y);
                }
                else
                {
                    center = new Vector2((graph.edges[i].StartingPoint.Value.X + graph.edges[i].EndingPoint.Value.X) / 2, graph.edges[i].StartingPoint.Value.Y);
                    start = new Vector2(center.X, center.Y - graph.edges[i].Distance);
                    end = new Vector2(center.X, center.Y + graph.edges[i].Distance);
                }

                spriteBatch.DrawLine(start, end, new Color(127, 152, 102), 5, 0);

            }
        }
    }
}
