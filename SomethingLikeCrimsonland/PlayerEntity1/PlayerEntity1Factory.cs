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
    //[GameObject(Name = "Player1", Description = "moust common player", type=typeof(PlayerEntity1))]
    [GameObject("Player")]
    public class PlayerEntity1Factory : IFactory
    {
        protected Queue<PlayerEntity1> _players;

        public PlayerEntity1Factory()
        {
            _players = new Queue<PlayerEntity1>();
        }

        public GameObject GetObject()
        {
            if (_players.Count == 0)
            {
                for (int i = 0; i < 4;i++ )
                    _players.Enqueue(new PlayerEntity1(TexturesManager.GetTexture("player")));
            }
            PlayerEntity1 player = _players.Dequeue();
            player.Alive = true;
            player.Experiance = 0;
            player.HealthPoints = 1000;
            player.MaxHealthPoints = 1000;
            player.Speed = 7;
            player.Velocity = new Vector2(7,7);
            return player;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj is PlayerEntity1)
                _players.Enqueue((PlayerEntity1)obj);
        }
    }
}
