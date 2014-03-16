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
    /// <summary>
    /// WARNING! Reimplement Get Methods
    /// </summary>
    class GameObjectFactory
    {
        protected AssemblysManager factories;
        protected Random random;
        public GameObjectFactory()
        {
            factories = new AssemblysManager();
            random = new Random();
        }

        public GameObject GetEnemy()
        {
            GameObject enemy = factories.EnemysFactories[0].GetObject();
            enemy.Speed = 0.5f;
            enemy.HealthPoints = 4;
            enemy.Scale = 0.5f;
            enemy.Radius = 2;
            /*
            if((enemy as EnemyObject).fitenessInfo == null)
                (enemy as EnemyObject).fitenessInfo = new FitnessInformation();
            (enemy as EnemyObject).Brain = NeuralNetworks.GetNetwork();
            //if((enemy as EnemyObject).Brain.Fitness == null)
                (enemy as EnemyObject).Brain.Fitness = (enemy as EnemyObject).fitenessInfo;
            //(enemy as EnemyObject).Brain.Fitness = (enemy as EnemyObject).fitenessInfo;
            */
            SetEnemyPosition(enemy);
            /*
            if (enemy.Position.X > 1000 || enemy.Position.Y > 1000)
            {
                enemy.Position = new Vector2(enemy.Position.X % 1000, enemy.Position.Y % 1000);
            }
            if (enemy.Position.X < -100 || enemy.Position.Y < -100)
            {
                enemy.Position = new Vector2(enemy.Position.X % -50, enemy.Position.Y % -50);
            }
            */
            if(GameInformation.PlayerPoints < 70000)
                SetEnemyBonuses((EnemyObject) enemy);
            SetEnemyStrategy((EnemyObject) enemy);
            return enemy;
        }

        private void SetEnemyPosition(GameObject obj)
        {
            
            int minX = GameInformation.GameField.X;
            int maxX = GameInformation.GameField.X + GameInformation.GameField.Width + 50;
            int minY = GameInformation.GameField.Y - 50;
            int maxY = GameInformation.GameField.Y;
            
            switch(random.Next(0,4))
            {
                case 0:
                    minX = GameInformation.GameField.X;
                    maxX = GameInformation.GameField.X + GameInformation.GameField.Width + 50;
                    minY = GameInformation.GameField.Y - 50;
                    maxY = GameInformation.GameField.Y;
                    break;
                case 1:
                    minX = GameInformation.GameField.X + GameInformation.GameField.Width;
                    maxX = GameInformation.GameField.X + GameInformation.GameField.Width + 50;
                    minY = GameInformation.GameField.Y;
                    maxY = GameInformation.GameField.Y + GameInformation.GameField.Height + 50;
                    break;
                case 2:
                    minX = GameInformation.GameField.X - 50;
                    maxX = GameInformation.GameField.X + GameInformation.GameField.Width;
                    minY = GameInformation.GameField.Y + GameInformation.GameField.Height;
                    maxY = GameInformation.GameField.Y + GameInformation.GameField.Height + 50;
                    break;
                case 3:
                    minX = GameInformation.GameField.X - 50;
                    maxX = GameInformation.GameField.X;
                    minY = GameInformation.GameField.Y - 50;
                    maxY = GameInformation.GameField.Y + GameInformation.GameField.Height;
                    break;
            }
            
            /*
            int minX = GameInformation.GameField.X;
            int maxX = GameInformation.GameField.X + GameInformation.GameField.Width;
            int minY = GameInformation.GameField.Y;
            int maxY = GameInformation.GameField.Y + GameInformation.GameField.Height;
            */
            obj.Position = new Vector2(random.Next(minX, maxX),random.Next(minY, maxY));
            
            //obj.Position = new Vector2(200, 200);
        }
        private void SetEnemyBonuses(EnemyObject obj)
        {
            if(random.Next(0,9) == 1)
            {
                obj.Bag = new List<GameObject>();
                GameObject bonus = GetBonus();
                if(bonus != null)
                obj.Bag.Add(bonus);
            }
        }
        private void SetEnemyStrategy(EnemyObject obj)
        {
            IMovementStrategy movement = new EnemyMovementEasy();
            if(GameInformation.PlayerPoints > 100000)
            {
                obj.Demage *= 7;
            }
            if(GameInformation.PlayerPoints > 200000)
            {
                obj.Demage = 50;
            }
            if(GameInformation.PlayerPoints > 50000)
            {
                obj.Speed += 2;
            }
            if(GameInformation.PlayerPoints > 30000)
            {
                movement = new EnemyMovementHard();
            }
            obj.MovementStrategy = movement;
        }

        public GameObject GetBullet()
        {
            return factories.BulletsFactories[0].GetObject();
        }

        public GameObject GetWeapon()
        {
            return factories.WeaponsFactories[0].GetObject();
        }

        public GameObject GetBonus()
        {
            BonusObject bonus = null;

            if (random.Next(1, 7) == 4)
            {
                bonus = (BonusObject)factories.BonusesFactories[0].GetObject();
                bonus.AliveTime = new TimeSpan(0, 0, 30);
                //bonus.Radius = 18;
            }
            
            if(random.Next(0,30) == 7)
            {
                bonus = (BonusObject)factories.BonusesFactories[1].GetObject();
                bonus.AliveTime = new TimeSpan(0, 0, 10);
                //bonus.Radius = 18;
            }

            if (bonus != null)
            {
                bonus.Radius = 18;
                bonus.Scale = 0.5f;
            }

            return bonus;
        }

        public GameObject GetPlayer()
        {
            PlayerObject player = (PlayerObject) factories.PlayersFactories[0].GetObject();
            //player.Speed = 0.8f;
            player.Radius = 16;
            player.Scale = 0.5f;
            player.Position = new Vector2(10,10);
            player.Velocity = new Vector2(2,2);
            player.Weapon = (WeaponObject) GetWeapon();
            player.Weapon.ClipSize = 30;
            player.Weapon.CurrentClipsCount = 30;
            player.Weapon.ReloadTime = new TimeSpan(0,0,2);
            player.Weapon.TimeBetweenFiers = new TimeSpan(0,0,0,0,50);
            player.Weapon.Bullet = factories.BulletsFactories[0];
            return player;
        }

        public void ReturnObject(GameObject obj)
        {            
            if(obj is EnemyObject)
            {
                factories.EnemysFactories[0].ReturnObject(obj);
            }
            else if(obj is BulletObject)
            {
                factories.BulletsFactories[0].ReturnObject(obj);
            }
            else if(obj is BonusObject)
            {
                factories.BonusesFactories[0].ReturnObject(obj);
            }
            else if(obj is WeaponObject)
            {
                factories.WeaponsFactories[0].ReturnObject(obj);
            }
            else if(obj is PlayerObject)
            {
                factories.PlayersFactories[0].ReturnObject(obj);
            }
        }
    }
}
