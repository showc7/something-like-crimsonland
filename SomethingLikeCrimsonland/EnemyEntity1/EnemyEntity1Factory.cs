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

namespace EnemyEntity1
{
    //[GameObject(Name = "Enemy1", Description = "the easiest enemy", Level = EnemyLevel.level1, type=typeof(EnemyEntity1))]
    [GameObject("Enemy")]
    public class EnemyEntity1Factory : IFactory
    {
        protected Queue<EnemyEntity1> _enemys;

        public EnemyEntity1Factory()
        {
            _enemys = new Queue<EnemyEntity1>();
        }

        public GameObject GetObject()
        {
            if (_enemys.Count == 0)
            {
                for (int i = 0; i < 10;i++ )
                    _enemys.Enqueue(new EnemyEntity1(TexturesManager.GetTexture("enemy")));
            }
            EnemyEntity1 enemy = _enemys.Dequeue();
            enemy.Demage = 1;
            enemy.HealthPoints = 10;
            enemy.Speed = 1;
            enemy.Radius = 16;
            enemy.Experiance = 8;
            enemy.timeBetweenHits = new TimeSpan(0,0,1);
            return enemy;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj is EnemyEntity1)
                _enemys.Enqueue((EnemyEntity1)obj);
        }
    }
}
