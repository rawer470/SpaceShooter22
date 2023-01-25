using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;   // for debug
using SpaceShooter22.Classes.UI;

namespace SpaceShooter22.Classes
{
    // Head Up Display
    class HUD
    {
        private Vector2 position;

        private Label labelScore;

        public HUD()
        {
            labelScore = new Label("Good luck Alexy", new Vector2(0, 500), Color.Red);
        }
        

        public void LoadContent(ContentManager manager)
        {
            labelScore.LoadContent(manager);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            labelScore.Draw(spriteBatch);
        }
    }
}
