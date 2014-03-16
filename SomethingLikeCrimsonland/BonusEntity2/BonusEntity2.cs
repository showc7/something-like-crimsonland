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
using BulletsEntity1;

namespace BonusEntity2
{
    [Serializable]
    public class BonusEntity2 : BonusObject
    {
        private BulletsEntity1.BulletsEntity1Factory bulletsFactory;
        private DateTime previousShoot;
        private TimeSpan timeBetweenShoots;
        public BonusEntity2(Texture2D texture)
            : base (texture)
        {
            bulletsFactory = new BulletsEntity1.BulletsEntity1Factory();
            previousShoot = DateTime.Now;
            timeBetweenShoots = new TimeSpan(0,0,1);
        }
        public BonusEntity2(Texture2D texture, TimeSpan aliveTime)
            : base (texture,aliveTime)
        {

        }

        public override void Apply(PlayerObject player)
        {
            if (player.Bag == null)
                player.Bag = new List<GameObject>();
            player.Bag.Add(this);
            player.HealthPoints = Math.Min(player.HealthPoints + 20,player.MaxHealthPoints);
        }
        public override void Action(PlayerObject player, ObjectsStore objectsStore)
        {
            if (previousShoot.Add(timeBetweenShoots).CompareTo(DateTime.Now) < 0)
            {
                BulletsEntity1.BulletsEntity1Factory f = new BulletsEntity1Factory();
                
                BulletObject bullet = (BulletObject)f.GetObject();
                bullet.Position = new Vector2(player.Position.X,player.Position.Y);
                bullet.Rotation = player.Rotation + (float) Math.PI;
                bullet.Speed = 7;
                bullet.Radius = 5;
                bullet.Demage = 20;
                bullet.Velocity = new Vector2((float)(bullet.Speed * Math.Cos(bullet.Rotation)), (float)(bullet.Speed * Math.Sin(bullet.Rotation)));
                objectsStore.Add(bullet);
                /*
                bullet = (BulletObject)f.GetObject();
                bullet.Position = new Vector2(player.Position.X, player.Position.Y);
                bullet.Rotation = player.Rotation + 1.58f;
                bullet.Speed = 7;
                bullet.Radius = 5;
                bullet.Demage = 20;
                bullet.Velocity = new Vector2((float)(bullet.Speed * Math.Cos(bullet.Rotation)), (float)(bullet.Speed * Math.Sin(bullet.Rotation)));
                objectsStore.Add(bullet);

                bullet = (BulletObject)f.GetObject();
                bullet.Position = new Vector2(player.Position.X, player.Position.Y);
                bullet.Rotation = player.Rotation + 0.98f;
                bullet.Speed = 7;
                bullet.Radius = 5;
                bullet.Demage = 20;
                bullet.Velocity = new Vector2((float)(bullet.Speed * Math.Cos(bullet.Rotation)), (float)(bullet.Speed * Math.Sin(bullet.Rotation)));
                objectsStore.Add(bullet);
                */
                // throw bullets on the field
                previousShoot = DateTime.Now;
            }
        }
    }
}
