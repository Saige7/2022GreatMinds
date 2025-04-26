using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;

namespace Straigh4
{
     static class Extensions
    {
        public static void DrawEdge(this Edge<(Vector2, Player, bool)> edge, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(new Vector2(edge.StartingPoint.Value.Item1.X + 4, edge.StartingPoint.Value.Item1.Y + 4), 
                new Vector2(edge.EndingPoint.Value.Item1.X + 4, edge.EndingPoint.Value.Item1.Y + 4), Color.DimGray, 4, 0);
        }
    }
}
