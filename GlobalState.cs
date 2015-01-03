using System.Collections.Generic;
using System.Linq;
using Common.Screens;
using Common.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Common
{
    public class GlobalState
    {
        #region Graphics and drawing

        public GraphicsDeviceManager Graphics { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }

        #endregion

        #region Resource management

        public ContentManager Content { get; set; }

        #endregion

        #region Time management

        public GameTime CurrentTime { get; set; }
        public GameTime LastTime { get; set; }

        #endregion

        #region Navigation

        public Stack<GameScreen> BackStack { get; set; }
        public GameScreen CurrentScreen { get; set; }

        public void NavigateTo<TGameScreen>(TGameScreen gameScreen = default(TGameScreen))
            where TGameScreen : GameScreen, new()
        {
            if (CurrentScreen != null)
                BackStack.Push(CurrentScreen);

            CurrentScreen = gameScreen ?? new TGameScreen
            {
                GlobalState = this
            };
        }

        public void GoBack()
        {
            CurrentScreen = BackStack.LastOrDefault();
            if (BackStack.Any())
                BackStack.Pop();
        }

        #endregion

        #region Interactivity

        public TouchHandler TouchHandler { get; set; }

        #endregion

        public GlobalState()
        {
            BackStack = new Stack<GameScreen>();
            TouchHandler = new TouchHandler();
        }

        /// <summary>
        /// Update time for the world and touch events.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            LastTime = CurrentTime;
            CurrentTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            TouchHandler.Update();
        }


        #region Static utility methods

        /// <summary>
        /// Helper method that checks if the back key is pressed.
        /// This only works for systems with back key and/or gamepad.
        /// </summary>
        /// <returns></returns>
        public static bool CheckBackKeyPressed()
        {
            return (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed);
        }

        #endregion
    }
}