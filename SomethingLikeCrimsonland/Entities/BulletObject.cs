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

namespace Entities
{
    [Serializable]
    public class BulletObject : GameObject
    {
        public BulletObject(Texture2D texture)
            : base (texture)
        {

        }

        public void Hit(GameObject obj)
        {
            int demage = obj.HealthPoints;
            obj.HealthPoints -= this.Demage;
            this.Demage -= demage;
        }

        public override void Move(GameObject obj)
        {
            this.Position += this.Velocity;
        }
    }
}
