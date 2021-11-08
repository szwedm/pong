using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace pong {

    class Ball : Sprite {

        private Vector2 defaultPosition;
        private bool outOfGameBounds;

        public Vector2 Speed { get { return speed; } set { speed = value * speed; } }
        public Vector2 Position { get { return position; } }
        public bool OutOfGameBounds { get { return outOfGameBounds; } }
        
        public Ball(Texture2D texture, Vector2 position) : base(texture, position, new Vector2(2f)) {
            this.defaultPosition = position;
        }

        public override Vector2 Direction {
            get {
                return speed;
            }
        }

        public override void Update(GameTime game, Rectangle clientBounds) {
            position += Direction;
            if (position.Y < 0)
                Speed = new Vector2(1f, -1f);
            if (position.Y > clientBounds.Height - texture.Height)
                Speed = new Vector2(1f, -1f);
            if (position.X < 0) {
                outOfGameBounds = true;
                Speed = new Vector2(0f);
            }
            if (position.X + texture.Width > clientBounds.Width) {
                outOfGameBounds = true;
                Speed = new Vector2(0f);
            }
        }

        public void randomizeSpeed() {
            Vector2[] possibilities = {
                new Vector2(1f, 1f),
                new Vector2(-1f, 1f),
                new Vector2(1f, -1f),
                new Vector2(-1f, -1f)};
            var rand = new Random();
            int index = rand.Next(possibilities.Length);
            Speed = possibilities[index];
        }
        
        public void resetPosition() {
            this.position = defaultPosition;
            speed = new Vector2(2f);
            outOfGameBounds = false;
        }
    }
}
