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

namespace SomethingLikeCrimsonland
{
    class EnemyMovementHard : IMovementStrategy
    {
        public void Move(EnemyObject enemy, GameObject obj)
        {
            if (enemy.Position.X > obj.Position.X - 2 && enemy.Position.Y > obj.Position.Y - 2 && enemy.Position.X < obj.Position.X + 2 && enemy.Position.Y < obj.Position.Y + 2)
                return;

            enemy.Rotation = (float)Math.Atan2(obj.Position.Y - enemy.Position.Y, obj.Position.X - enemy.Position.X);
            enemy.Velocity = new Vector2(enemy.Speed * (float)Math.Cos(enemy.Rotation), enemy.Speed * (float)Math.Sin(enemy.Rotation));
            enemy.Position += enemy.Velocity;
        }
    }
}
