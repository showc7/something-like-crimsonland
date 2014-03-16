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
    public class GameObject
    {
        public Texture2D Sprite { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Center { get; set; }
        public int Radius { get; set; }
        public Vector2 Velocity { get; set; }
        public bool Alive { get; set; } // may be not needed
        public int Demage { get; set; }
        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public float Speed { get; set; }
        public float Scale { get; set; }

        public GameObject()
        {

        }

        public GameObject(Texture2D loadedTexture)
        {
            Rotation = 0.0f;
            Position = Vector2.Zero;
            Sprite = loadedTexture;
            Center = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            Velocity = Vector2.Zero;
            Alive = true;
        }

        public bool Contains(GameObject obj) // Intersect
        {
            //return (Math.Sqrt(Math.Abs(this.Position.X + this.Center.X - obj.Center.X - obj.Position.X) * Math.Abs(this.Position.X + this.Center.X - obj.Center.X - obj.Position.X) + Math.Abs(this.Position.Y + this.Center.Y - obj.Center.Y - obj.Position.Y) * Math.Abs(this.Position.Y + this.Center.Y - obj.Center.Y - obj.Position.Y)) <= this.Radius + obj.Radius);
            return (Math.Sqrt(Math.Abs(this.Position.X  - obj.Position.X) * Math.Abs(this.Position.X - obj.Position.X) + Math.Abs(this.Position.Y - obj.Position.Y) * Math.Abs(this.Position.Y - obj.Position.Y)) <= this.Radius + obj.Radius);
        }

        public event EventHandler ChangeBegin;
        public event EventHandler ChangeEnd;

        public void OnChangeBegin(EventArgs e)
        {
            if (ChangeBegin != null)
                ChangeBegin(this,e);
        }
        public void OnChangeEnd(EventArgs e)
        {
            if (ChangeEnd != null)
                ChangeEnd(this,e);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, this.Position, null, Color.White, this.Rotation, this.Center, Scale, SpriteEffects.None, 0);
            //spriteBatch.Draw(this.Sprite, this.Position, null, Color.White, this.Rotation, this.Center, 1.0f, SpriteEffects.None, 0);
        }
        public virtual void Move(GameObject obj)
        {

        }
    }
}
