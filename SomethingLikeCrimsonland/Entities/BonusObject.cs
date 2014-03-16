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
    public class BonusObject : GameObject
    {
        public bool IsPermanent { get; set; }
        public DateTime AppiarTime { get; set; }
        public TimeSpan AliveTime { get; set; }
        public GameObject Bag { get; set; }

        public BonusObject(Texture2D texture)
            : base (texture)
        {

        }

        public BonusObject(Texture2D texture, TimeSpan aliveTime)
            : this (texture)
        {
            AliveTime = aliveTime;
        }

        public virtual void Apply(PlayerObject player)
        {

        }

        public virtual void Action(PlayerObject player, ObjectsStore objectsStore)
        {

        }

        public virtual void ChangeBegin(object sender, EventArgs e)
        {

        }

        public virtual void ChangeEnd(object sender, EventArgs e)
        {

        }
    }
}
