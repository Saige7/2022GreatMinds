using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimberLineOfBestFit
{
    internal class Graph
    {
        public List<Vector2> points;
        public Vector2 Screen;
        public Graph(Vector2 screen)
        {
            Screen = screen;
            points = new List<Vector2>();
        }

        public void Update(MouseState mouse, MouseState previous)
        {
            if (previous.LeftButton == ButtonState.Released && mouse.LeftButton == ButtonState.Pressed)
            {
                points.Add(new Vector2(mouse.X, mouse.Y));
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < points.Count; i++)
            {
                spriteBatch.DrawPoint(points[i], Color.AliceBlue, 2);
            }
        }
    }
}
