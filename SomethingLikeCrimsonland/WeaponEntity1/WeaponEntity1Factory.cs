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
    [GameObject("Weapon")]
    public class WeaponEntity1Factory : IFactory
    {
        protected Queue<WeaponEntity1> _weapons;

        public WeaponEntity1Factory()
        {
            _weapons = new Queue<WeaponEntity1>();
        }

        public GameObject GetObject()
        {
            if (_weapons.Count == 0)
            {
                for (int i = 0; i < 10;i++ )
                    _weapons.Enqueue(new WeaponEntity1(TexturesManager.GetTexture("weapon")));
            }
            WeaponEntity1 enemy = _weapons.Dequeue();
            return enemy;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj is WeaponEntity1)
                _weapons.Enqueue((WeaponEntity1)obj);
        }
    }
}
