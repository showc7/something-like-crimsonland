using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Entities;

namespace SomethingLikeCrimsonland
{
    /// <summary>
    /// Showing during the game
    /// </summary>
    class GameTimeMenu
    {
        SpriteFont healthFont;
        Vector2 healthFontPosition;
        Vector2 experianceFontPosition;
        Vector2 levelupFontPosition;
        private int _playerLevel;
        public GameTimeMenu()
        {
            healthFont = TexturesManager.GetFont("SpriteFont1");
            healthFontPosition = new Vector2(3,3);
            experianceFontPosition = new Vector2(3,20);
            levelupFontPosition = new Vector2(400,3);
        }

        public void Redraw()
        {
            string playerHelath = (GameInformation.CurrentPlayer.HealthPoints).ToString() + "/" + (GameInformation.CurrentPlayer.MaxHealthPoints).ToString();
            GameInformation.GameSpriteBatch.DrawString(healthFont,playerHelath,healthFontPosition,Color.White);
            string playerExperiance = GameInformation.CurrentPlayer.Experiance.ToString();
            GameInformation.GameSpriteBatch.DrawString(healthFont, playerExperiance, experianceFontPosition, Color.White);
            if(_playerLevel < GameInformation.PlayerLevel)
            {
                string LevelUP = "LEVEL UP";
                GameInformation.GameSpriteBatch.DrawString(healthFont, LevelUP, levelupFontPosition, Color.White);
            }
        }
    }
}
