using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;

namespace Straigh4
{
    internal class Board<T> where T : IEquatable<T>
    {
        public Sprite[,] Points;
        public Graph<(Vector2, Player, bool)> graph;
        public bool whoseTurn;

        public Board(Texture2D texture, Vector2 position, Rectangle sourceRect)
        {
            Points = new Sprite[5,5];
            graph = new Graph<(Vector2, Player, bool)>();
            whoseTurn = true;

            for (int i = 0; i < Points.GetLength(0); i++)
            {
                for (int j = 0; j < Points.GetLength(1); j++)
                {
                    Sprite point = new Sprite(texture, new Vector2(position.X + (60 * j), position.Y + (60 * i)), Color.White, sourceRect);
                    Points[i, j] = point;
                    Vertex<(Vector2, Player, bool)> pointVertex = new Vertex<(Vector2, Player, bool)>((point.Position, null, false));
                    graph.AddVertex(pointVertex);
                }
            }

            for (int i = 0; i < graph.VertexCount - 1; i++)
            {
                if (graph.Vertices[i + 1].Value.Item1.Y == graph.Vertices[i].Value.Item1.Y)
                {
                    graph.AddUndirectedEdge(graph.Vertices[i], graph.Vertices[i + 1], 60);
                }

                if ((i + 5) < graph.VertexCount && graph.Vertices[i + 5].Value.Item1.X == graph.Vertices[i].Value.Item1.X)
                {
                    graph.AddUndirectedEdge(graph.Vertices[i], graph.Vertices[i + 5], 60);
                }
            }

            graph.AddUndirectedEdge(graph.Vertices[2], graph.Vertices[6], 85);
            graph.AddUndirectedEdge(graph.Vertices[2], graph.Vertices[8], 85);
            graph.AddUndirectedEdge(graph.Vertices[6], graph.Vertices[10], 85);
            graph.AddUndirectedEdge(graph.Vertices[7], graph.Vertices[11], 85);
            graph.AddUndirectedEdge(graph.Vertices[7], graph.Vertices[13], 85);
            graph.AddUndirectedEdge(graph.Vertices[8], graph.Vertices[14], 85);
            graph.AddUndirectedEdge(graph.Vertices[10], graph.Vertices[16], 85);
            graph.AddUndirectedEdge(graph.Vertices[11], graph.Vertices[17], 85);
            graph.AddUndirectedEdge(graph.Vertices[13], graph.Vertices[17], 85);
            graph.AddUndirectedEdge(graph.Vertices[14], graph.Vertices[18], 85);
            graph.AddUndirectedEdge(graph.Vertices[16], graph.Vertices[22], 85);
            graph.AddUndirectedEdge(graph.Vertices[18], graph.Vertices[22], 85);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawPolygon(new Vector2(324, 224), new MonoGame.Extended.Shapes.Polygon([new(-120, 0), new(0, -120), new(120, 0), new(0, 120)]), Color.Chocolate, 42);

            for (int i = 0; i < graph.Edges.Count; i++)
            {
                Extensions.DrawEdge(graph.Edges[i], spriteBatch);
            }

            for (int i = 0; i < Points.GetLength(0); i++)
            {
                for (int j = 0; j < Points.GetLength(1); j++)
                {
                    Points[i, j].Draw(spriteBatch);
                }
            }
        }

        private Vertex<(Vector2, Player, bool)> getVertexByPosition(Vector2 position)
        {
            for (int i = 0; i < graph.VertexCount; i++) 
            {
                if (graph.Vertices[i].Value.Item1 == position)
                {
                    return graph.Vertices[i];
                }
            }
            return null;
        }

        private void Update(MouseState mouse, Sprite point, Sprite piece, MouseState previous, Player player, bool whichPlayer)
        {
            if (piece != null && piece.Position.X > 130 && piece.Position.X < 480 && !player.allInBoard())
            {
                return;
            }
            if (piece != null && point.Hitbox.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && previous.LeftButton == ButtonState.Released && whichPlayer == whoseTurn)
            {
                if (piece.Position.X > 130 && piece.Position.X < 480)
                {
                    Vertex<(Vector2, Player, bool)> currentPieceVertex = getVertexByPosition(new Vector2(piece.Position.X + (piece.Hitbox.Width / 2),
                        piece.Position.Y + (piece.Hitbox.Height / 2)));
                    Vertex<(Vector2, Player, bool)> vertexToMoveTo = getVertexByPosition(point.Position);

                    if (graph.GetEdge(currentPieceVertex, vertexToMoveTo) == null)
                    {
                        return;
                    }
                }

                piece.Position.X = point.Position.X - (piece.Hitbox.Width / 2);
                piece.Position.Y = point.Position.Y - (piece.Hitbox.Height / 2);

                if (whichPlayer)
                {
                    player.selectedPiece.Color = Color.DarkRed;
                }
                else
                {
                    player.selectedPiece.Color = Color.DarkGoldenrod;
                }
                player.selectedPiece = null;

                Vertex<(Vector2, Player, bool)> vertex = getVertexByPosition(point.Position);
                vertex.Value.Item2 = player;
                vertex.Value.Item3 = true;
                whoseTurn = !whoseTurn;
            }
        }
        
        public void UpdatePoints(MouseState mouse, Sprite piece, MouseState previous, Player player, bool whichPlayer)
        {
            for (int i = 0; i < Points.GetLength(0); i++)
            {
                for(int j = 0; j < Points.GetLength(1); j++)
                {
                    Update(mouse, Points[i, j], piece, previous, player, whichPlayer);
                }
            }
        }
    }
}
