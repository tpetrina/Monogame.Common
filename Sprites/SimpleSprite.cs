using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Common.Sprites
{
    public class SimpleSprite : Sprite
    {
        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public Color Color { get; set; }

        public SimpleSprite(SpriteBatch spriteBatch)
            : base(spriteBatch)
        {
            this.Color = Color.White;
        }

        public void Draw()
        {
            SpriteBatch.Draw(Texture, Position, Color);
        }
    }
}
