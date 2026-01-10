using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;
using MonoGame.Extended.Screens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimberLineOfBestFit
{
    internal class Line
    {
        public Vector2 startPoint;
        public Vector2 endPoint;
        public float Slope;
        public float yint;
        public Vector2 Screen;
        public Graph graph;

        public Line(Graph Graph, Vector2 screen, float Yint, float slope)
        {
            graph = Graph;
            Screen = screen;
            yint = Yint;
            Slope = slope;
            startPoint = new Vector2(0, yint);
            endPoint = new Vector2(Screen.X, (slope * Screen.X) + yint);
            //endPoint = new Vector2( (-1 * yint) / Slope, 0);
            //startPoint = new Vector2(0, Screen.Y - yint);
            //endPoint = new Vector2((Screen.Y - yint) / Slope, 0);
        }

        public static Line CreateRandomLine(Graph graph, Vector2 Screen)
        {
            Random random = new Random();
            Vector2 coord1 = new Vector2(random.Next(0, 101), random.Next(0, 101));
            Vector2 coord2 = new Vector2(random.Next(0, 101), random.Next(0, 101));

            float slope = (coord2.X - coord1.X) / (coord2.Y - coord1.Y);
            float yint = coord1.Y - (slope * coord1.X);

            return new Line(graph, Screen, yint, slope);
        }
        public Line CalculateLineOfBestFit()
        {
            float slope = ( (graph.points.Count * SumOfProductOfXY()) - (SumOfX() * SumOfY()) ) / ( (graph.points.Count * SumOfXSquared()) - ((float)Math.Pow(SumOfX(), 2)) );
            float yint = ( SumOfY() - (slope * SumOfX()) ) / graph.points.Count;

            return new Line(graph, Screen, yint, slope);
        }
        public float SumOfX()
        {
            float sum = 0;
            for(int i = 0; i < graph.points.Count; i++)
            {
                sum += graph.points[i].X;
            }
            return sum;
        }
        public float SumOfXSquared()
        {
            float sum = 0;
            for (int i = 0; i < graph.points.Count; i++)
            {
                sum += (float)Math.Pow(graph.points[i].X, 2);
            }
            return sum;
        }
        public float SumOfY()
        {
            float sum = 0;
            for (int i = 0; i < graph.points.Count; i++)
            {
                sum += graph.points[i].Y;
            }
            return sum;
        }
        public float SumOfProductOfXY()
        {
            float sum = 0;
            for (int i = 0; i < graph.points.Count; i++)
            {
                sum += (graph.points[i].X * graph.points[i].Y);
            }
            return sum;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawLine(startPoint, endPoint, color, 3);
        }
    }
}
