using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;   // for debug

namespace SpaceShooter22.Classes
{
    class Explosion
    {
        // поля
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private int currentFrame;
        private int countFrames;        // кол-во кадров в спрайте
        private int widthFrame;
        private int heightFrame;
        private bool loop;              // зацикливание
        private float duration;         // длительность в мс
        private float durationOneFrame;  // длительность одного кадра
        private double totalDuration;    // сколько прошло времени
        private bool isVisible;

        // свойство
        public int Width
        {
            get
            {
                return rectangle.Width;
            }
        }

        public int Height
        {
            get
            {
                return rectangle.Height;
            }
        }

        public bool IsVisible
        {
            get { return isVisible; }
        }

        private void Config()
        {
            // config
            widthFrame = 117;
            heightFrame = 117;
            currentFrame = 9;
            countFrames = 17;
            loop = false;
            duration = 600;  // скорость анимации в ms
            totalDuration = 0;
            isVisible = true;

            durationOneFrame = duration / countFrames;
        }

        public Explosion(Vector2 position)
        {
            Config();

            this.position = position;
        }

        public Explosion()
        {
            Config();
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("explosion");
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle(currentFrame * widthFrame, 0, 
                widthFrame, heightFrame);

            totalDuration += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (totalDuration >= durationOneFrame)
            {
                currentFrame++;
                totalDuration = 0;
            }
            
           

            // end animation and looping animation
            if (currentFrame == countFrames)
            {
                if (loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    isVisible = false;
                } 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // рисование части текстуры
            spriteBatch.Draw(texture, position, rectangle, Color.White);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}
