using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;   // !!! для контента
using System.Diagnostics;   // for debug

using System.Collections.Generic;   // для листа

using SpaceShooter22.Classes;  // подключаем наши классы

namespace SpaceShooter22
{

    public enum GameType {Menu , Game };

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static GameType gameType = GameType.Menu;
        private Player player = new Player();
        private Space space = new Space();

        private List<Asteroid> asteroidTeam = new List<Asteroid>();

        private Explosion explosion = new Explosion(Vector2.Zero);
        private List<Explosion> explosions = new List<Explosion>();

        private HUD hud = new HUD();

        private Menu menu = new Menu();

        private int asteroidsCount = 5;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // задать размер области
            
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            player.LoadContent(Content);
            space.LoadContent(Content);

            for (int i = 0; i < asteroidsCount; i++)
            {
                Asteroid a = new Asteroid();
                a.LoadContent(Content);
                asteroidTeam.Add(a);
            }

            explosion.LoadContent(Content);

            hud.LoadContent(Content);

            menu.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
           // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed )
              //  Exit();

            
            switch(gameType)
            {
                case GameType.Menu:
                    UpdateMenu(gameTime);
                    break;
                case GameType.Game:
                    UpdateGame(gameTime);
                    break;
            }
            // TODO: Add your update logic here
           

         

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


            switch (gameType)
            {
                case GameType.Menu:
                    DrawMenu(_spriteBatch);
                    break;
                case GameType.Game:
                    DrawGame(_spriteBatch);
                    break;
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawGame(SpriteBatch _spriteBatch)
        {
            space.Draw(_spriteBatch);

            for (int i = 0; i < asteroidTeam.Count; i++)
            {
                asteroidTeam[i].Draw(_spriteBatch);
            }


            player.Draw(_spriteBatch);

            //explosion.Draw(_spriteBatch);
            DrawExplosions(_spriteBatch);

            hud.Draw(_spriteBatch);
        }

        private void DrawMenu(SpriteBatch _spriteBatch)
        {
            space.Draw(_spriteBatch);
            menu.Draw(_spriteBatch);
        }

        private void UpdateGame(GameTime gameTime)
        {
            player.Update();
            space.Update();
            for (int i = 0; i < asteroidTeam.Count; i++)
            {
                asteroidTeam[i].Update();
            }
            ManagerAsteroids();
            UpdateCollision();
            UpdateExplosions(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameType = GameType.Menu;
            }
        }

        private void UpdateMenu(GameTime gameTime)
        {
            space.Update();
            menu.Update();
        }


        private void ManagerAsteroids()
        {
            // удаление астеройдов
            for (int i = 0; i < asteroidTeam.Count; i++)
            {
                if (asteroidTeam[i].IsVisible == false)
                {
                    // удаляем астреойд из коллекции
                    asteroidTeam.Remove(asteroidTeam[i]);
                    i--;   // ВАЖНО!!! ОЧЕНЬ!!!
                }
            }

            // добавление
            if (asteroidTeam.Count < asteroidsCount)
            {
                Asteroid a = new Asteroid();
                a.LoadContent(Content);
                asteroidTeam.Add(a);
            }

        }

        public void UpdateCollision()
        {
            foreach (var a in asteroidTeam)
            {
                // столкновение с игроком
                if (a.Rectangle.Intersects(player.Rectangle))
                {
                    System.Diagnostics.Debug.WriteLine("Intersects!!!");
                    
                    // START алгоритм взрыва
                    Explosion exp = new Explosion();
                    exp.LoadContent(Content);

                    Vector2 pos = new Vector2(a.Rectangle.X + (exp.Width - a.Width)/2, 
                        a.Rectangle.Y + (exp.Height - a.Height)/2);

                    exp.SetPosition(pos);  // установка позиции взрыва

                    explosions.Add(exp);
                    // END алгоритм взрыва

                    a.IsVisible = false;

                 
                }

                // столкновение с пульками игрока
                foreach (var b in player.Bullets)
                {
                    if (a.Rectangle.Intersects(b.Rectangle))
                    {
                        a.IsVisible = false;
                        b.IsVisible = false;

                        // START алгоритм взрыва
                        Explosion exp = new Explosion();
                        exp.LoadContent(Content);

                        Vector2 pos = new Vector2(a.Rectangle.X + (exp.Width - a.Width) / 2,
                            a.Rectangle.Y + (exp.Height - a.Height) / 2);

                        exp.SetPosition(pos);  // установка позиции взрыва

                        explosions.Add(exp);
                        // END алгоритм взрыва
                    }
                }
            }
        }

        public void UpdateExplosions(GameTime gameTime)
        {
            foreach (Explosion exp in explosions)
            {
                exp.Update(gameTime);
            }

            for (int i = 0; i < explosions.Count; i++)
            {
                if (explosions[i].IsVisible == false)
                {
                    explosions.RemoveAt(i);
                    i--;                        // !!! 
                }
            }
        }

        public void DrawExplosions(SpriteBatch spriteBatch)
        {
            foreach (Explosion exp in explosions)
            {
                exp.Draw(spriteBatch);
            }
        }
    }
}
