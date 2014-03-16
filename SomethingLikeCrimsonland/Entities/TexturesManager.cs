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
    public static class TexturesManager
    {
        public static ContentManager Content { get; set; }
        private static Dictionary<string, Texture2D> _textures = new Dictionary<string,Texture2D>();
        private static Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();
        public static Texture2D GetTexture(string name)
        {
            if(!_textures.ContainsKey(name))
            {
                _textures.Add(name,Content.Load<Texture2D>(name));
            }
            return _textures[name];
        }
        public static SpriteFont GetFont(string name)
        {
            if (!_fonts.ContainsKey(name))
            {
                _fonts.Add(name, Content.Load<SpriteFont>(name));
            }
            return _fonts[name];
        }
    }
}
