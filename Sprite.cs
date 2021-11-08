using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace pong {

    abstract class Sprite {

        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle collisionRect;
        protected Vector2 speed;
        public abstract Vector2 Direction { get; }
        public Rectangle CollisionRect {
            get {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    texture.Width,
                    texture.Height);
            }
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 speed) {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
        }

        public abstract void Update(GameTime game, Rectangle clientBounds);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
