using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Entities;

namespace BonusEntity1
{
    [Serializable]
    public class BonusEntity1 : BonusObject
    {
        public BonusEntity1(Texture2D texture)
            : base (texture)
        {
            
        }
        public BonusEntity1(Texture2D texture, TimeSpan aliveTime)
            : base (texture,aliveTime)
        {

        }

        public override void Apply(PlayerObject player)
        {
            player.HealthPoints = Math.Min(player.HealthPoints + 20,player.MaxHealthPoints);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, this.Position, null, Color.White, this.Rotation, this.Center, 0.3f, SpriteEffects.None, 0);
            //spriteBatch.Draw(this.Sprite,this.Position,null,Color.White,this.Rotation,Vector2.Zero,0.5,SpriteEffects.None,0f);
        }
    }
}
