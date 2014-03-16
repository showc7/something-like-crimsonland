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

namespace BulletsEntity1
{
//    [GameObject(Description="none",Level=EnemyLevel.level1,Name="bullets",type=typeof(BulletEntity1),GameObjectType="Bullet")]
    [GameObject("Bullet")]
    public class BulletsEntity1Factory : IFactory
    {
        protected Queue<BulletEntity1> _bullets;

        public BulletsEntity1Factory()
        {
            _bullets = new Queue<BulletEntity1>();
        }

        public GameObject GetObject()
        {
            if (_bullets.Count == 0)
            {
                for (int i = 0; i < 10;i++ )
                    _bullets.Enqueue(new BulletEntity1(TexturesManager.GetTexture("weapon")));
            }
            BulletEntity1 bullet = _bullets.Dequeue();
            bullet.Demage = 20;
            bullet.Scale = 1.0f;
            return bullet;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj is BulletEntity1)
                _bullets.Enqueue((BulletEntity1)obj);
        }
    }
}
