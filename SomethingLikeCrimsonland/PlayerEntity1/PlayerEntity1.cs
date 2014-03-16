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

namespace PlayerEntity1
{
    [Serializable]
    public class PlayerEntity1 : PlayerObject
    {
        public PlayerEntity1(Texture2D texture)
            : base (texture)
        {

        }
    }
}
