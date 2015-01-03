using Microsoft.Xna.Framework.Graphics;

namespace Common.Sprites
{
    public class Sprite
    {
        public SpriteBatch SpriteBatch { get; set; }

        public Sprite(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
        }
    }
}