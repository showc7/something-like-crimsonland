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
    public class PlayerObject : GameObject
    {
        public int Experiance { get; set; }
        public WeaponObject Weapon { get; set; }
        public List<GameObject> Bag { get; set; }
        public PlayerObject(Texture2D texture)
            : base (texture)
        {

        }
        public void Hit(GameObject obj)
        {
            obj.HealthPoints -= this.HealthPoints;
        }
    }
}
