using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Screens;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HillClimberLineOfBestFit
{
    internal class HillClimber
    {
        public Vector2 Screen;
        public Graph graph;
        private Line LineOfBestFit;
        private Line CurrentLine;
        private bool RunningHillClimber;

        private Random Rand;

        public HillClimber(Vector2 screen, Graph Graph)
        {
            graph = Graph;
            Screen = screen;

            Rand = new Random(1);
            RunningHillClimber = false;
        }

        private Line Mutate()
        {
            float slopeOrYint = Rand.Next(2);
            Line current = new Line(graph, Screen, CurrentLine.yint, CurrentLine.Slope);

            if (slopeOrYint == 0)
            {
                float slopeChange = Rand.Next(2);
                if (slopeChange == 0)
                {
                    slopeChange = current.Slope + (float)-0.1;

                }
                else
                {
                    slopeChange = current.Slope + (float)0.1;
                }

                current.endPoint.Y = (slopeChange * current.endPoint.X) + current.yint;
                current.Slope = slopeChange;
            }
            else
            {
                float yintChange = Rand.Next(2);
                if (yintChange == 0)
                {
                    yintChange = current.yint + (float)-0.1;

                }
                else
                {
                    yintChange = current.yint + (float)0.1;
                }

                current.startPoint.Y = (current.Slope * current.startPoint.X) + yintChange;
                current.endPoint.Y = (current.Slope * current.endPoint.X) + yintChange;

                current.yint = yintChange;
            }

            return current;
        }
        private float MAE(Line mutatedLine)
        {
            return (float)(Math.Abs(LineOfBestFit.yint - mutatedLine.yint) + Math.Abs(LineOfBestFit.Slope - mutatedLine.Slope)) / 2;
        }
        public bool RunHillClimber()
        {
            float initialError = MAE(CurrentLine);
            
            bool keepRunning = initialError != 0;

            if(keepRunning)
            {
                Line mutatedLine = Mutate();
                float error = MAE(mutatedLine);
                if (error < initialError)
                {
                    CurrentLine = mutatedLine;
                    //initialError = error;
                }

                Debug.WriteLine($"Error: {error}\nLine of Best: y = {LineOfBestFit.Slope}x + {LineOfBestFit.yint}\nCurrent Line: y = {CurrentLine.Slope}x + {CurrentLine.yint}");
            }
      

            return keepRunning;
        }

        public void Update(KeyboardState key, KeyboardState previousKey)
        {         
            if(previousKey.IsKeyUp(Keys.Space) && key.IsKeyDown(Keys.Space))
            {                
                CurrentLine = Line.CreateRandomLine(graph, Screen);
                LineOfBestFit = CurrentLine.CalculateLineOfBestFit();
                RunningHillClimber = true;
            }

            if (RunningHillClimber)
            {
                RunningHillClimber = RunHillClimber();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (LineOfBestFit != null)
            {
                LineOfBestFit.Draw(spriteBatch, Color.Black);
            }
            if (CurrentLine != null)
            {
                CurrentLine.Draw(spriteBatch, Color.Red);
            }
        }
    }
}
