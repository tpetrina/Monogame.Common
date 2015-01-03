using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Common
{
    public class Base2DGame : Game
    {
        public GlobalState GlobalState { get; set; }

        public Base2DGame()
        {
            GlobalState = new GlobalState
            {
                Graphics = new GraphicsDeviceManager(this)
                {
                    IsFullScreen = true
                },
                GraphicsDevice =  GraphicsDevice
            };
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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            GlobalState.GraphicsDevice = GraphicsDevice;
            GlobalState.Content = Content;

            // Create a new SpriteBatch, which can be used to draw textures.
            GlobalState.SpriteBatch = new SpriteBatch(GraphicsDevice);

            if (GlobalState.CurrentScreen != null)
                GlobalState.CurrentScreen.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            base.Update(gameTime);

            // first update global state
            GlobalState.Update(gameTime);
            if (GlobalState.CurrentScreen == null)
                Exit();
            else
                GlobalState.CurrentScreen.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (GlobalState.CurrentScreen != null)
                GlobalState.CurrentScreen.Draw();
        }
    }
}
