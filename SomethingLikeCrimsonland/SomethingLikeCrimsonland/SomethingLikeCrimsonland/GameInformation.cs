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
    static class GameInformation
    {
        public static int MaxEnemysCount { get; set; }
        public static int PlayerPoints { get; set; }
        public static int GetMonstersCount(int playerExperiance, GameTime gameTime)
        {
            return (playerExperiance / 100 < 300 ? 300 : playerExperiance / 100);
            
            //return 16384;

            if (playerExperiance < 100)
                return 3;
            if (playerExperiance < 200)
                return 4;
            if (playerExperiance < 300)
                return 7;
            if (playerExperiance < 400)
                return 10;
            if (playerExperiance < 500)
                return 15;
            if (playerExperiance < 600)
                return 25;
            //if(playerExperiance < 14000)
            //    return playerExperiance / 300;
            return playerExperiance / 400;
        }
        public static EnemyLevel GetMonstersType(int playerExperiance, GameTime gameTime)
        {
            if (playerExperiance < 100)
                return EnemyLevel.level1;
            else if (playerExperiance < 500)
                return EnemyLevel.level2;
            else return EnemyLevel.level3;
        }
        public static PlayerObject CurrentPlayer { get; set; }
        public static SpriteBatch GameSpriteBatch { get; set; }
        public static int PlayerLevel { get; set; }
        public static int PlayerAvaliableLevel { get; set; }
        public static Rectangle ScreenRectangle { get; set; }
        public static Rectangle GameField { get; set; }
    }
}
