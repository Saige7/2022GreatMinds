
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;

using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Straigh4
{
    internal class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Color Color;
        public Rectangle SourceRect;
        public Rectangle Hitbox => new Rectangle(Position.ToPoint(), SourceRect.Size);

        public Sprite(Texture2D texture, Vector2 position, Color color, Rectangle? sourceRect)
        {
            Texture = texture;
            Position = position;
            Color = color;
            if (sourceRect == null) SourceRect = texture.Bounds;
            else SourceRect = (Rectangle)sourceRect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRect, Color);
        }


    }
}
