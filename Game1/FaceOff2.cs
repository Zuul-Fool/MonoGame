using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FaceOff2 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D textureBall;
        Texture2D bg;
        Vector2 eetuPos;
        float eetuSpeed;
        SpriteFont font;
        int Score = 0;

        enum GameState
        {
            MainMenu,
            Gameplay,
            Defeat,
        }

        private GameState _state;

        public FaceOff2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            eetuPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            eetuSpeed = 500f;
            font = Content.Load<SpriteFont>("Score");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            textureBall = Content.Load<Texture2D>("eetuface");
            bg = Content.Load<Texture2D>("bg");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (_state)
            {
                case GameState.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.Gameplay:
                    UpdateGameplay(gameTime);
                    break;
                case GameState.Defeat:
                    UpdateDefeat(gameTime);
                    break;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected void UpdateMainMenu(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _state = GameState.Gameplay;
            }
        }

        protected void UpdateGameplay(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
                eetuPos.Y -= eetuSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.S))
                eetuPos.Y += eetuSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.A))
                eetuPos.X -= eetuSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.D))
                eetuPos.X += eetuSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            eetuPos.X = Math.Min(Math.Max(textureBall.Width / 2, eetuPos.X), graphics.PreferredBackBufferWidth - textureBall.Width / 2);
            eetuPos.Y = Math.Min(Math.Max(textureBall.Height / 2, eetuPos.Y), graphics.PreferredBackBufferHeight - textureBall.Height / 2);

            if (kstate.IsKeyDown(Keys.G))
                Score++;

            if (Keyboard.GetState().IsKeyDown(Keys.F))
                _state = GameState.Defeat;
            
        }

        protected void UpdateDefeat(GameTime gameTime)
        {

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            switch (_state)
            {
                case GameState.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case GameState.Gameplay:
                    DrawGameplay(gameTime);
                    break;
                case GameState.Defeat:
                    DrawDefeat(gameTime);
                    break;
            }

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        protected void DrawMainMenu(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Welcome to Face Off 2: Electric Boogaloo: ", new Vector2(100, 100), Color.DeepPink);
            spriteBatch.End();
        }
        protected void DrawGameplay(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bg, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.Draw(textureBall, eetuPos, null, Color.White, 0f, new Vector2(textureBall.Width / 2, textureBall.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Score: " + Score, new Vector2(100, 100), Color.HotPink);
            spriteBatch.End();
        }
        protected void DrawDefeat(GameTime gameTime)
        {

        }
    }
}
