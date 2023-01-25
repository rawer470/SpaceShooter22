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
    internal class Menu
    {
        private List<Label> items;
        private string[] texts = { "Play", "Info", "Exit" };
        private Color color;
        private Color colorSelected;

        private int selected = 0;
        private KeyboardState keyboard; // состояние клавиатуры в данный момент
        private KeyboardState prevKeyboard;  // прошлое состояние клавиатуры

        private Vector2 position;

        public Menu()
        {
            items = new List<Label>();
            color = Color.White;
            colorSelected = Color.Red;

            Vector2 item_position = position;

            for (int i = 0; i < texts.Length; i++)
            {
                Label label = new Label(texts[i], item_position, color);
                items.Add(label);
                
               item_position.Y += 30;
            }
        }

        public void LoadContent(ContentManager manager)
        {
            foreach (var item in items)
            {
                item.LoadContent(manager);
                item.X  = 800 / 2 - item.Size.X / 2;
                item.Y =item.Y+ item.Size.Y + 200/2;
            }
        }

        public void Update()
        {
            keyboard = Keyboard.GetState();

            // Down
            if (keyboard.IsKeyDown(Keys.S) && (keyboard != prevKeyboard))
            {
                if (selected < items.Count - 1)
                {
                    items[selected].Color = color;
                    selected++;
                }
                else
                {
                    items[selected].Color = color;
                    selected = 0;
                }
            }

            // Up
            if (keyboard.IsKeyDown(Keys.W) && (keyboard != prevKeyboard))
            {
                if (selected > 0)
                {
                    items[selected].Color = color;
                    selected--;
                }
            }


            if (keyboard.IsKeyDown(Keys.Enter))
            {
                if (items[selected].Text == "Play")
                {
                    Game1.gameType = GameType.Game;
                }
                else if (items[selected].Text =="Exit")
                {
                    Environment.Exit(0);
                }
                else if (items[selected].Text =="Info")
                {
                    Game1.gameType = GameType.Game;

                }
            }
            prevKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            items[selected].Color = colorSelected;
            items.ForEach(item => item.Draw(spriteBatch));
        }
    }
}
