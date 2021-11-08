using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong {

    public class Game1 : Game {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D paddle1Texture, paddle2Texture, pongBallTexture, background;
        private SpriteFont font;
        private PaddleOne paddle1;
        private PaddleTwo paddle2;
        private Ball pongBall;

        int playerOneScore, playerTwoScore;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            playerOneScore = 0;
            playerTwoScore = 0;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            paddle1Texture = Content.Load<Texture2D>(@"paddle1");
            paddle2Texture = Content.Load<Texture2D>(@"paddle2");
            pongBallTexture = Content.Load<Texture2D>(@"pongBall");
            background = Content.Load<Texture2D>(@"pongBackground");
            font = Content.Load<SpriteFont>(@"font");

            paddle1 = new PaddleOne(paddle1Texture, new Vector2(
                0,
                Window.ClientBounds.Height / 2 - paddle1Texture.Height / 2));
            paddle2 = new PaddleTwo(paddle2Texture, new Vector2(
                Window.ClientBounds.Width - paddle2Texture.Width,
                Window.ClientBounds.Height / 2 - paddle1Texture.Height / 2));
            pongBall = new Ball(pongBallTexture, new Vector2(
                Window.ClientBounds.Width / 2 - pongBallTexture.Width / 2,
                Window.ClientBounds.Height / 2 - pongBallTexture.Height / 2));
            pongBall.randomizeSpeed();
        }

        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            paddle1.Update(gameTime, Window.ClientBounds);
            paddle2.Update(gameTime, Window.ClientBounds);
            pongBall.Update(gameTime, Window.ClientBounds);
            detectCollision();
            checkPongBallStatus();

            base.Update(gameTime);
        }

        private void checkPongBallStatus() {
            if (pongBall.OutOfGameBounds) {
                if (pongBall.Position.X < 0)
                    ++playerTwoScore;
                if (pongBall.Position.X + pongBallTexture.Width > Window.ClientBounds.Width)
                    ++playerOneScore;
                pongBall.resetPosition();
                pongBall.randomizeSpeed();
            }
        }

        private void detectCollision() {
            if (paddle1.CollisionRect.Intersects(pongBall.CollisionRect)) {
                pongBall.Speed = new Vector2(-1.4f, 1.4f);
            }
            if (paddle2.CollisionRect.Intersects(pongBall.CollisionRect)) {
                pongBall.Speed = new Vector2(-1.4f, 1.4f);
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(background,
                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);

            paddle1.Draw(gameTime, spriteBatch);
            paddle2.Draw(gameTime, spriteBatch);
            pongBall.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font,
                playerOneScore.ToString(),
                new Vector2(Window.ClientBounds.Width / 2 - 80, 10),
                Color.White,
                0f,
                Vector2.Zero,
                4f,
                SpriteEffects.None,
                1f);

            spriteBatch.DrawString(font,
                playerTwoScore.ToString(),
                new Vector2(Window.ClientBounds.Width / 2 + 50, 10),
                Color.White,
                0f,
                Vector2.Zero,
                4f,
                SpriteEffects.None,
                1f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
