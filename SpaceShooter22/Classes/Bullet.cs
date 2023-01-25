using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceShooter22.Classes
{
    class Bullet
    {
        // поля
        private Texture2D texture;
        private Vector2 position;
        private bool isVisible;
        private Color color;
        private int speed;
        private Rectangle rectangle;

        // свойства

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

        // конструктор
        public Bullet(Vector2 position)
        {
            this.position = position;
            isVisible = true;
            color = Color.White;
            speed = 4;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("playerbullet");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position + new Vector2(-texture.Width/2, 0), color);
        }

        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);

            position.Y -= speed;

            if (position.Y < 0 - texture.Height)
            {
                isVisible = false;
            }
        }
    }
}
