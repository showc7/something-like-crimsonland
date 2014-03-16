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

namespace WeaponEntity1
{
    [Serializable]
    public class WeaponEntity1 : WeaponObject
    {
        public WeaponEntity1(Texture2D texture)
            :base(texture)
        {

        }

        //public override List<BulletObject> Fire(float playerX, float playerY, float mouseX, float mouseY)
        public override List<BulletObject> Fire(PlayerObject player,/* float playerX, float playerY,*/ float mouseX, float mouseY)
        {
            List<BulletObject> bullets = new List<BulletObject>();
            if ((DateTime.Now - timer).CompareTo(TimeBetweenFiers) >= 0)
            {
                if (CurrentClipsCount > 0)
                {
                    CurrentClipsCount--;
                    // bullet1
                    BulletObject bullet = (BulletObject)Bullet.GetObject();
                    bullet.Position = new Vector2(player.Position.X,player.Position.Y);
                    bullet.Radius = 5;
                    bullet.Demage = 20;
                    bullet.Speed = 4;
                    bullet.Rotation = player.Rotation;
                    bullet.Velocity = new Vector2((float)(bullet.Speed * Math.Cos(bullet.Rotation)), (float)(bullet.Speed * Math.Sin(bullet.Rotation)));
                    timer = DateTime.Now;
                    bullets.Add(bullet);
                    // bullet2
                    bullet = (BulletObject)Bullet.GetObject();
                    bullet.Position = new Vector2(player.Position.X, player.Position.Y);
                    bullet.Radius = 5;
                    bullet.Demage = 20;
                    bullet.Speed = 4;
                    bullet.Rotation = player.Rotation + 0.3f;
                    bullet.Velocity = new Vector2((float)(bullet.Speed * Math.Cos(bullet.Rotation)), (float)(bullet.Speed * Math.Sin(bullet.Rotation)));
                    timer = DateTime.Now;
                    bullets.Add(bullet);
                    // bullet3
                    bullet = (BulletObject)Bullet.GetObject();
                    bullet.Position = new Vector2(player.Position.X, player.Position.Y);
                    bullet.Radius = 5;
                    bullet.Demage = 20;
                    bullet.Speed = 4;
                    bullet.Rotation = player.Rotation - 0.3f;
                    bullet.Velocity = new Vector2((float)(bullet.Speed * Math.Cos(bullet.Rotation)), (float)(bullet.Speed * Math.Sin(bullet.Rotation)));
                    timer = DateTime.Now;
                    bullets.Add(bullet);
                }
                else
                {
                    timer = DateTime.Now + ReloadTime;
                    CurrentClipsCount = ClipSize;
                }
            }
            return bullets;
            //return base.Fire(playerX, playerY, mouseX, mouseY);
        }
    }
}
