using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceShooter22.Classes
{
    class Player
    {
        // поля
        private Vector2 position;
        private Texture2D texture;
        private ContentManager manager;

        private Rectangle rectangle;

        // weapon
        private int weaponTime = 0;
        private int weaponDelay = 15;
        private Bullet bullet;
        private List<Bullet> bullets = new List<Bullet>(); // обойма
        private int numBullet = 0;


        private int health=10;
        private int score=0;
        private bool isVisible = true;

        // свойства

        public List<Bullet> Bullets
        {
            get { return bullets; }
        }

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
        }

        // конструктор
        public Player()
        {
            position = new Vector2(300, 450);
            texture = null;
        }

        // загрузка контента
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("player");

            this.manager = manager;   // !!!
        }

        // прорисовка
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(spriteBatch);
            }
        }

        // обновление
        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);

            // получаем статус клаиватуры
            KeyboardState keyState = Keyboard.GetState();

            // SHOOT
            Shoot();
            // вызов стрельбы
            if (keyState.IsKeyDown(Keys.Space))
            {
                weaponTime++;

                if (weaponTime >= weaponDelay)  // прошло ~1 сек
                {
                    //System.Diagnostics.Debug.WriteLine("SHOOT!!!");

                    weaponTime = 0;

                    // генерация пулек
                    if (bullets.Count < 19)
                    {
                        Vector2 bulletPos = new Vector2(position.X + texture.Width/2
                            , position.Y);

                        Bullet b = new Bullet(bulletPos);
                        b.LoadContent(manager);

                        bullets.Add(b);
                    }
                }
            }

            // управление
            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y += 5f;
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y -= 5f;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                position.X += 5f;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                position.X -= 5f;
            }

            // стеночки LEFT
            if (position.X <= 0)
            {
                position.X = 0;
            }

            // RIGHT
            if (position.X +  texture.Width >= 800)
            {
                position.X = 800 - texture.Width;
            }

            // TOP
            if (position.Y <= 0)
            {
                position.Y = 0;
            }

            // BOTTOM
            if (position.Y + texture.Height >= 600)
            {
                position.Y = 600 - texture.Height;
            }
        }

        // user-методы
        private void Shoot()
        {
            // Update
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
            }


            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].IsVisible == false)
                {
                    bullets.Remove(bullets[i]);
                    i--;
                    numBullet--;
                }
            }

            //System.Diagnostics.Debug.WriteLine("Count: " + bullets.Count);
        }
    }
}
