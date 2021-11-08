using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace pong {

    class PaddleOne : Sprite {

        private Vector2 defaultPosition;

        public PaddleOne(Texture2D texture, Vector2 position) : base(texture, position, new Vector2(0f, 5f)) {
            this.defaultPosition = position;
        }

        public override Vector2 Direction { 
            get {
                Vector2 inputDirection = Vector2.Zero;
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    inputDirection.Y += 1;

                return inputDirection * speed;
            }
        }

        public override void Update(GameTime game, Rectangle clientBounds) {
            position += Direction;
            if (position.Y < 0)
                position.Y = 0;
            if (position.Y > clientBounds.Height - texture.Height)
                position.Y = clientBounds.Height - texture.Height;
        }

        public void resetPosition() {
            this.position = defaultPosition;
        }
    }
}
