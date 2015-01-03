using Common.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace Common.Screens
{
    public abstract class GameScreen : IUpdateable, IDrawable, ILoadContent
    {
        public bool BackKeyExits { get; set; }

        protected GameScreen()
        {
            BackKeyExits = true;
        }

        public GlobalState GlobalState { get; set; }

        public virtual void LoadContent(ContentManager contentManager) { }

        public virtual void Update()
        {
            if (BackKeyExits && GlobalState.CheckBackKeyPressed())
                GlobalState.GoBack();
        }

        public virtual void Draw() { }
    }
}