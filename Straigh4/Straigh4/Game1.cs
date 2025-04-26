using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;

using System.Linq;

namespace Straigh4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        
        //Texture2D texture;
        //Vector2 scale = new Vector2(.01f, 1);
        //float rotation = 0;

        Texture2D baseTexture;
        Sprite player1Piece;
        Sprite player2Piece;

        Player player1;
        Player player2;
        Board<int> gameBoard;

        MouseState mouse;
        MouseState previous;

        Color background;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.PreferredBackBufferWidth = 655;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //texture = Content.Load<Texture2D>("redCircle");

            baseTexture = Content.Load<Texture2D>("square");

            player1Piece = new Sprite(baseTexture, new Vector2(120, 110), Color.DarkRed, new Rectangle(0, 0, 40, 40));
            player2Piece = new Sprite(baseTexture, new Vector2(485, 110), Color.DarkGoldenrod, new Rectangle(0, 0, 40, 40));

            player1 = new Player(player1Piece);
            player2 = new Player(player2Piece);

            gameBoard = new Board<int>(baseTexture, new Vector2(200, 100), new Rectangle(0, 0, 7, 7));

            background = Color.Black;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouse = Mouse.GetState();

            //scale.X += .01f;
            //rotation += MathHelper.ToRadians(5);


            // TODO: Add your update logic here

            player1.UpdatePieces(mouse, true, previous, player2);
            player2.UpdatePieces(mouse, false, previous, player1);

            if (player1.selectedPiece == null)
            {
                gameBoard.UpdatePoints(mouse, player2.selectedPiece, previous, player2, false);
            }
            else
            {
                gameBoard.UpdatePoints(mouse, player1.selectedPiece, previous, player1, true);
            }

            previous = mouse;

            if (player1.checkWin())
            {
                background = Color.DarkRed;
            }
            else if (player2.checkWin())
            {
                background = Color.DarkGoldenrod;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);
            spriteBatch.Begin();

            //var source = new Rectangle(534, 107, 95, 101);
            //spriteBatch.Draw(texture, new Vector2(200, 200), source, Color.White, rotation, source.Size.ToVector2() / 2, scale, SpriteEffects.FlipHorizontally, 0);
            
            gameBoard.Draw(spriteBatch);

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
