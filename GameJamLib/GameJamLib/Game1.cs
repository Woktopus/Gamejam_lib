using GameJamLib.Utils.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJamLib
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Texture2D background_main;
		Rectangle background_main_rect;

		ParallaxingBackground background_parallax_1;
		ParallaxingBackground background_parallax_2;

		Animation testAnimation;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            //setup du serviceHelperet ajout du KeyboardService aux Components
            ServiceHelper.Game = this;
            Components.Add(new KeyboardService(this));
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

			// Rect to draw the background to
			background_main_rect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

			// Parallaxes
			background_parallax_1 = new ParallaxingBackground();
			background_parallax_2 = new ParallaxingBackground();

			testAnimation = new Animation();

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

			// Load background
			background_main = Content.Load<Texture2D>("Graphics/Tutorial/mainbackground");

			// Load parallaxes
			background_parallax_1.Initialize(Content, "Graphics/Tutorial/bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
			background_parallax_2.Initialize(Content, "Graphics/Tutorial/bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);

			Vector2 testAnimation_position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
			testAnimation.LoadContent(Content, /*"Graphics/Tutorial/shipAnimation"*/"Graphics/CodingMadeEasyPlatformer/Hero1", Color.White, ""/*"Graphics/Tutorial/gameFont"*/, Color.Chocolate, "PLOP", testAnimation_position, 200, new Vector2(3, 4));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
			testAnimation.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

            // TODO: Add your update logic here

			// Update parallaxes
			background_parallax_1.Update(gameTime);
			background_parallax_2.Update(gameTime);


            if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.Q))
			{
				testAnimation.isActive = true;
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.W))
			{
				testAnimation.isActive = false;
			}

            if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.E))
			{
				testAnimation.SetSpeed(50);
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.R))
			{
				testAnimation.SetSpeed(200);
			}

            if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.T))
			{
				testAnimation.rotation = 0.6f;
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.Y))
			{
				testAnimation.rotation = 0.0f;
			}

            if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.U))
			{
				testAnimation.scale = 4.0f;
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.I))
			{
				testAnimation.scale = 1.0f;
			}

            if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.O))
			{
				testAnimation.alpha = 0.6f;
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.P))
			{
				testAnimation.alpha = 1.0f;
			}

            if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.Down))
			{
				testAnimation.SelectAnimation(0);
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.Left))
			{
				testAnimation.SelectAnimation(1);
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.Right))
			{
				testAnimation.SelectAnimation(2);
			}
            else if (ServiceHelper.Get<KeyboardService>().IsKeyDown(Keys.Up))
			{
				testAnimation.SelectAnimation(3);
			}

			testAnimation.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			// Start drawing
			spriteBatch.Begin();

			// Draw background
			spriteBatch.Draw(background_main, background_main_rect, Color.White);

			// Draw parallaxes
			background_parallax_1.Draw(spriteBatch);
			background_parallax_2.Draw(spriteBatch);

			testAnimation.Draw(spriteBatch);

			// Stop drawing
			spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
