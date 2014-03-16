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
    public class EnemyObject : GameObject
    {
        private IMovementStrategy _movement;
        public IMovementStrategy MovementStrategy
        {
            get { return _movement; }
            set
            {
                if(value != null)
                    _movement = value;
            }
        }
        public int Experiance { get; set; }
        public List<GameObject> Bag { get; set; }
        public TimeSpan timeBetweenHits { get; set; }
        public DateTime PreviousRotation { get; set; }
        public TimeSpan RotationTime { get; set; }
        public float RotationSpeed { get; set; }
        protected DateTime prevHit;
        public EnemyObject()
        {
            PreviousRotation = DateTime.Now;
            RotationTime = new TimeSpan(0,0,0,0,100);
            RotationSpeed = 0.3f;
            prevHit = DateTime.Now;
        }
        public EnemyObject(Texture2D texture, IMovementStrategy movement)
            : base(texture)
        {
            _movement = movement;
        }
        public EnemyObject(Texture2D texture)
            : base(texture)
        {
            PreviousRotation = DateTime.Now;
            RotationTime = new TimeSpan(0, 0, 0, 0, 5);
            RotationSpeed = 0.01f;
            prevHit = DateTime.Now;
        }
        public void Hit(GameObject obj)
        {
            if (prevHit.Add(timeBetweenHits) < DateTime.Now)
            {
                obj.HealthPoints -= this.Demage;
                prevHit = DateTime.Now;
            }
        }
        public override void Move(GameObject obj)
        {
            _movement.Move(this,obj);
            /*
            //float a = Math.Abs(this.Position.Y - obj.Position.Y);
            //float b = Math.Abs(this.Position.X - obj.Position.X);
            //b = (float) Math.Sqrt(a*a + b*b);
            //this.Rotation = (Brain.Calculate(new float[] { a / b}))[0];
            //this.Rotation = (Brain.Calculate(new float[] { obj.Position.X, obj.Position.Y }))[0];

            float neededRotation = (float)Math.Atan2(obj.Position.Y - this.Position.Y, obj.Position.X - this.Position.X);

            if(neededRotation != this.Rotation && previousRotation.Add(rotationTime).CompareTo(DateTime.Now) < 0)
            {
                if(neededRotation < this.Rotation)
                {
                    this.Rotation -= rotationSpeed;
                    //if (this.Rotation < neededRotation)
                    //    this.Rotation = neededRotation;
                }
                else
                {
                    this.Rotation += rotationSpeed;
                    //if (this.Rotation > neededRotation)
                    //    this.Rotation = neededRotation;
                }

                previousRotation = DateTime.Now;
            }
            //this.Rotation = (float)Math.Atan2(obj.Position.Y - this.Position.Y, obj.Position.X - this.Position.X);
            
            this.Velocity = new Vector2(this.Speed * (float)Math.Cos(this.Rotation), this.Speed * (float)Math.Sin(this.Rotation));
            this.Position += this.Velocity;
            */
        }
    }
}
