using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceShooter22.Classes
{
    class Asteroid
    {
        // поля
        private Texture2D texture;
        private Vector2 position;
        private int speed;

        private Rectangle rectangle;

        private bool isVisible = true;

        // свойства

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

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
        }
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }

        public Asteroid()
        {
            Random random = new Random();

            speed = random.Next(2, 4);

            this.position.X = random.Next(0, 800 - 45);
            this.position.Y = random.Next(-600, -45);
        }

        public Asteroid(Vector2 position)
        {
            this.position = position;
            speed = 2;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("asteroid");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);

            position.Y += speed;

            if (position.Y > 600)
            {
                isVisible = false;
            }
        }
    }
}
