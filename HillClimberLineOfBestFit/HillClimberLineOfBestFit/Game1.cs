using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HillClimberLineOfBestFit
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        Vector2 Screen;

        Graph graph;
        HillClimber hillClimber;

        MouseState mouse;
        MouseState previous;
        KeyboardState key;
        KeyboardState previousKey;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 100;
            _graphics.PreferredBackBufferWidth = 100;
            _graphics.ApplyChanges();

            Screen = GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            graph = new Graph(Screen);
            hillClimber = new HillClimber(Screen, graph);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previous = mouse;
            mouse = Mouse.GetState();

            previousKey = key;
            key = Keyboard.GetState();

            graph.Update(mouse, previous);            
            hillClimber.Update(key, previousKey);    

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            spriteBatch.Begin();

            graph.Draw(spriteBatch);
            hillClimber.Draw(spriteBatch);
            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
