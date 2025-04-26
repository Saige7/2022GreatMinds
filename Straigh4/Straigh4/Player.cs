using System;

using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Straigh4
{
    internal class Player
    {
        public Sprite[] GamePieces;
        public Sprite selectedPiece;

        public Player(Sprite sprite)
        {
            GamePieces = new Sprite[4];
            selectedPiece = null;

            for (int i = 0; i < GamePieces.Length; i++)
            {
                GamePieces[i] = new Sprite(sprite.Texture, new Vector2(sprite.Position.X, sprite.Position.Y + (i * 60)), sprite.Color, sprite.SourceRect);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GamePieces.Length; i++)
            {
                GamePieces[i].Draw(spriteBatch);
            }
        }

        private void Update(MouseState mouse, bool whichPlayer, Sprite piece, MouseState previous, Player otherPlayer)
        {
            if (piece.Hitbox.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && previous.LeftButton == ButtonState.Released)
            {
                if (whichPlayer)
                {
                    if (otherPlayer.selectedPiece != null)
                    {
                        otherPlayer.selectedPiece.Color = Color.DarkGoldenrod;
                        otherPlayer.selectedPiece = null;
                    }

                    if (selectedPiece == piece)
                    {
                        piece.Color = Color.DarkRed;
                        selectedPiece = null;
                    }
                    else
                    {
                        piece.Color = Color.Firebrick;
                        if (selectedPiece != null)
                        {
                            selectedPiece.Color = Color.DarkRed;

                        }
                        selectedPiece = piece;
                    }
                }
                else
                {
                    if (otherPlayer.selectedPiece != null)
                    {
                        otherPlayer.selectedPiece.Color = Color.DarkRed;
                        otherPlayer.selectedPiece = null;
                    }

                    if (selectedPiece == piece)
                    {
                        piece.Color = Color.DarkGoldenrod;
                        selectedPiece = null;
                    }
                    else
                    {
                        piece.Color = Color.Goldenrod;
                        if (selectedPiece != null)
                        {
                            selectedPiece.Color = Color.DarkGoldenrod;
                        }
                        selectedPiece = piece;
                    }
                }
            }
        }

        public void UpdatePieces(MouseState mouse, bool whichPlayer, MouseState previous, Player otherPlayer)
        {
            for (int i = 0; i < GamePieces.Length; i++)
            {
                Update(mouse, whichPlayer, GamePieces[i], previous, otherPlayer);
            }
        }

        public bool checkWin()
        {
            bool check1 = true;
            bool check2 = true;

            for (int i = 1; i < GamePieces.Length; i++)
            {
                if (!(GamePieces[i].Position.X > 130 && GamePieces[i].Position.X < 480))
                {
                    return false;
                }

                if (GamePieces[i - 1].Position.X != GamePieces[i].Position.X)
                {
                    check1 = false;
                }
                if (GamePieces[i - 1].Position.Y != GamePieces[i].Position.Y)
                {
                    check2 = false;
                }
            }
      
            return (check1 || check2);
        }

        public bool allInBoard()
        {
            for (int i = 0; i < GamePieces.Length; i++)
            {
                if (!(GamePieces[i].Position.X > 130 && GamePieces[i].Position.X < 480))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
