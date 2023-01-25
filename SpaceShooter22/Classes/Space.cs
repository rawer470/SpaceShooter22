using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceShooter22.Classes
{
    class Space
    {
        // поля
        private Texture2D texture;
        private Vector2 position1, position2;

        public Space()
        {
            position1 = new Vector2(0, -950);
            position2 = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("space");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position1, Color.White);
            spriteBatch.Draw(texture, position2, Color.White);
        }

        public void Update()
        {
            position1.Y += 1;
            position2.Y += 1;

            if (position1.Y == 0)
            {
                position1.Y = -950;
                position2.Y = 0;
            }
        }
    }
}
