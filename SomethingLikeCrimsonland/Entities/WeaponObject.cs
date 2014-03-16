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
    public class WeaponObject : GameObject
    {
        public int ClipSize { get; set; }
        public int CurrentClipsCount { get; set; }
        public TimeSpan ReloadTime { get; set; }
        public IFactory Bullet { get; set; }
        public TimeSpan TimeBetweenFiers { get; set; }

        protected DateTime timer;

        public WeaponObject(Texture2D texture)
            : base (texture)
        {

        }

        public virtual List<BulletObject> Fire(PlayerObject player,/* float playerX, float playerY,*/ float mouseX, float mouseY)
        {
            return null;
            /*
            List<BulletObject> bullets = new List<BulletObject>();
            if ((DateTime.Now - timer).CompareTo(TimeBetweenFiers) >= 0)
            {
                if (CurrentClipsCount > 0)
                {

                    CurrentClipsCount--;
                    BulletObject bullet = (BulletObject)Bullet.GetObject();
                    //BulletObject bullet = new BulletObject(Textures.LoadBullet(), 200, 20);
                    //   bullet.Position = new Vector2(playerX, playerY);
                    //bullet.Position = new Vector2(player.Position.X,player.Position.Y);
                    //bullet.Radius = 5;
                    //float c = Math.Abs(mouseX - playerX) / Math.Abs(mouseY - playerY);
                    //bullet.Velocity = new Vector2(5 * c,5 * (1 - c));
                    //bullet.Velocity = new Vector2((float)(2 * Math.Cos(ObjectManager.Player.Rotation)), (float)(2 * Math.Sin(ObjectManager.Player.Rotation)));
                    //bullet.Rotation = ObjectManager.Player.Rotation;
                    //bullet.Rotation = (float)Math.Atan2(mouseState.Y - ObjectManager.Player.Position.Y, mouseState.X - ObjectManager.Player.Position.X);
                    //ObjectManager.AddObject(bullet);
                    timer = DateTime.Now;
                    bullets.Add(bullet);
                    //return bullets;
                    //*/
                //}
            /*
                else
                {
                    timer = DateTime.Now + ReloadTime;
                    CurrentClipsCount = ClipSize;
                    //return null;
                }
            }
            return bullets;
            //else return null;
             */
        }

        public void Reload()
        {
            timer = DateTime.Now + ReloadTime;
            CurrentClipsCount = ClipSize;
        }

        public event EventHandler BeginReload;

        public void OnReload()
        {
            if(BeginReload != null)
            {
                BeginReload(this,new EventArgs());
            }
        }
    }
}
