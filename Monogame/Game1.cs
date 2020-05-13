using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //sprite variables
        Texture2D target_Sprite;
        Texture2D crosshairs_Spite;
        Texture2D background_Spite;
        //font variable
        SpriteFont gameFont;


        Vector2 targetPosition = new Vector2(300,300);
        const int TARGET_RADIUS = 45;

        //variable to allow mouse to be used
        MouseState mState;
        bool mReleased = true;
        float mouseTargetDistance;

        int score;
        float timer = 10f;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //show mouse when app is running
            IsMouseVisible = false;

            

        }

        
        protected override void Initialize()
        {
          
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
          
            spriteBatch = new SpriteBatch(GraphicsDevice);

            target_Sprite = Content.Load<Texture2D>("target");
            crosshairs_Spite = Content.Load<Texture2D>("crosshairs");
            background_Spite = Content.Load<Texture2D>("sky");

            gameFont = Content.Load<SpriteFont>("galleryFont");

           }

      
        protected override void UnloadContent()
        {
            }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (timer > 0)
            {
                // timer is going to decrease by the total gametime
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // get state of the mouse and anytime
            mState = Mouse.GetState();

            // calculate distance between mouse and target
            mouseTargetDistance = Vector2.Distance(targetPosition, new Vector2(mState.X, mState.Y)); 

            // everytime left button is clicked, increase score variable by 1
            if (mState.LeftButton == ButtonState.Pressed && mReleased)
            {
                if (mouseTargetDistance < TARGET_RADIUS && timer > 0)
                {
                    score++;

                    // move target to random position
                    Random rand = new Random();
                    targetPosition.X = rand.Next(TARGET_RADIUS,graphics.PreferredBackBufferWidth - TARGET_RADIUS + 1);
                    targetPosition.Y = rand.Next(TARGET_RADIUS, graphics.PreferredBackBufferHeight - TARGET_RADIUS + 1);
                }
                mReleased = false;
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // start game
            spriteBatch.Begin();

            //background
            spriteBatch.Draw(background_Spite, new Vector2(0,0), Color.White);

            // show score
            spriteBatch.DrawString(gameFont, "Score = " + score.ToString(), new Vector2(3, 3), Color.White);

            // hide target when game ends
            if (timer > 0)
            {
                //target - offset by TARGET_RADIUS to centre the sprite hitbox
                spriteBatch.Draw(target_Sprite, new Vector2(targetPosition.X - TARGET_RADIUS, targetPosition.Y - TARGET_RADIUS), Color.White);
            }
            //show timer
            spriteBatch.DrawString(gameFont, "Time left: " + Math.Ceiling(timer).ToString(), new Vector2(3, 40), Color.White);

            spriteBatch.Draw(crosshairs_Spite, new Vector2(mState.X - 25, mState.Y - 25), Color.White);


            // End game
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
