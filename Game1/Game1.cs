using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Game1.GameStates;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D textureBall;
        Texture2D bg;
        Vector2 eetuPos;
        float eetuSpeed;
        SpriteFont font;
        int Score = 0;

        public Game1()
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
            GameStateManager.Instance.SetContent(Content);
            GameStateManager.Instance.AddScreen(new MainGameState(GraphicsDevice));
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
            GameStateManager.Instance.UnloadContent();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GameStateManager.Instance.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumPurple);
            // TODO: Add your drawing code here
            GameStateManager.Instance.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(bg, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.Draw(textureBall, eetuPos, null, Color.White, 0f, new Vector2(textureBall.Width / 2, textureBall.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Score: "+ Score, new Vector2(100, 100), Color.HotPink);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
