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

using System.IO;

using Entities;

namespace SomethingLikeCrimsonland
{
    [Serializable]
    class ObjectsManager
    {
        private object locker1;
        //private Vector3 camera;
        //private Matrix gameMatrix;
        protected ObjectsStore _store;
        protected GameObjectFactory _factory;
        protected List<GameObject> _deletePlayers;
        protected List<GameObject> _deleteEnemys;
        protected List<GameObject> _deleteBullets;
        protected List<GameObject> _deleteBonuses;
        //protected SpriteBatch GameSpriteBatch;
        protected DateTime _mouseTime;
        public event EventHandler PlayerDie;
        //public Rectangle ScreenRectangle { get; set; }
        //public Rectangle GameField { get; set; }
        public PlayerObject Player
        {
            get { return _store.Players[0]; }
        }
        private void OnPlayerDie(PlayerObject player)
        {
            if(PlayerDie != null)
            {
                PlayerDie(player,new EventArgs());
            }
        }
        public ObjectsManager()
        {
            locker1 = new object();
            _store = new ObjectsStore();
            _factory = new GameObjectFactory();
            _store.Add(_factory.GetPlayer());
            //camera = new Vector3(-_store.Players[0].Position.X + 300, -_store.Players[0].Position.Y + 300, 0); // ???
            //camera = new Vector3(-_store.Players[0].Position.X,-_store.Players[0].Position.Y,0);
            _deletePlayers = new List<GameObject>();
            _deleteEnemys = new List<GameObject>();
            _deleteBullets = new List<GameObject>();
            _deleteBonuses = new List<GameObject>();
            GameInformation.CurrentPlayer = _store.Players[0];
        }

        public void UpdateObjectsState(KeyboardState keyboardState, MouseState mouseState, GameTime gameTime)
        {
            PlayerObject player = _store.Players[0];



            if(_mouseTime.Add(new TimeSpan(0,0,0,0,50)).CompareTo(DateTime.Now) < 0)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    //BulletObject bullet = _store.Players[0].Weapon.Fire(player.Position.X, player.Position.Y, mouseState.X, mouseState.Y);
                    //List<BulletObject> bullets = _store.Players[0].Weapon.Fire(player.Position.X, player.Position.Y, mouseState.X, mouseState.Y);
                    List<BulletObject> bullets = _store.Players[0].Weapon.Fire(player, mouseState.X, mouseState.Y);
                    //foreach(BulletObject bullet in _store.Players[0].Weapon.Fire(player.Position.X, player.Position.Y, mouseState.X, mouseState.Y))
                    if(bullets != null)
                    foreach(BulletObject bullet in bullets)
                    {
                        _store.Add(bullet);
                    }
                    /*
                    if (bullet != null)
                    {
                        float c = Math.Abs(mouseState.X - player.Position.X) / Math.Abs(mouseState.Y - player.Position.Y);
                        bullet.Speed = 4;
                        bullet.Demage = 20;
                        bullet.Velocity = new Vector2((float)(bullet.Speed * Math.Cos(_store.Players[0].Rotation)), (float)(bullet.Speed * Math.Sin(_store.Players[0].Rotation)));
                        bullet.Rotation = (float)Math.Atan2(mouseState.Y - _store.Players[0].Position.Y, mouseState.X - _store.Players[0].Position.X);
                        _store.Add(bullet);
                    }
                    */
                }
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    _store.Players[0].Weapon.Reload();
                }
                _mouseTime = DateTime.Now;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                player.Position = new Vector2(player.Position.X, player.Position.Y - player.Velocity.Y);
                //if(player.Position.Y + camera.Y < 200)
                //    camera.Y += player.Velocity.Y; 
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                player.Position = new Vector2(player.Position.X, player.Position.Y + player.Velocity.Y);
                //if(player.Position.Y + camera.Y > 400)
                //    camera.Y -= player.Velocity.Y; 
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                player.Position = new Vector2(player.Position.X - player.Velocity.X, player.Position.Y);
                //if(player.Position.X + camera.X < 200)
                //    camera.X += player.Velocity.X;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                player.Position = new Vector2(player.Position.X + player.Velocity.X, player.Position.Y);
                //if (player.Position.X + camera.X > 400)
                //    camera.X -= player.Velocity.X;
            }
            if(keyboardState.IsKeyDown(Keys.G))
            {
                string d = "sdkfs;kjfa;";
            }
            GameObjectPositionCheck(player);
            _store.Players[0].Rotation = (float)Math.Atan2(mouseState.Y - _store.Players[0].Position.Y, mouseState.X - _store.Players[0].Position.X);
            UpdateObjectsState2(gameTime);
        }
        public void GameObjectPositionCheck(GameObject obj)
        {
            if (obj.Position.X < GameInformation.ScreenRectangle.X + obj.Sprite.Width / 2)
                obj.Position = new Vector2(GameInformation.ScreenRectangle.X + obj.Sprite.Width / 2, obj.Position.Y);
            if (obj.Position.X > GameInformation.ScreenRectangle.X + GameInformation.ScreenRectangle.Width - obj.Sprite.Width / 2)
                obj.Position = new Vector2(GameInformation.ScreenRectangle.X + GameInformation.ScreenRectangle.Width - obj.Sprite.Width / 2, obj.Position.Y);
            if (obj.Position.Y < GameInformation.ScreenRectangle.Y + obj.Sprite.Height / 2)
                obj.Position = new Vector2(obj.Position.X, GameInformation.ScreenRectangle.Y + obj.Sprite.Height / 2);
            if (obj.Position.Y > GameInformation.ScreenRectangle.Y + GameInformation.ScreenRectangle.Height - obj.Sprite.Height / 2)
                obj.Position = new Vector2(obj.Position.X, GameInformation.ScreenRectangle.Y + GameInformation.ScreenRectangle.Height - obj.Sprite.Height / 2);
        }
        public void GameObjectPositionCheckEnemy(GameObject obj)
        {
            const int plus = 100;
            if (obj.Position.X < GameInformation.ScreenRectangle.X + obj.Sprite.Width / 2 - plus)
                obj.Position = new Vector2(GameInformation.ScreenRectangle.X + obj.Sprite.Width / 2 + plus, obj.Position.Y - plus);
            if (obj.Position.X > GameInformation.ScreenRectangle.X + GameInformation.ScreenRectangle.Width - obj.Sprite.Width / 2 + plus)
                obj.Position = new Vector2(GameInformation.ScreenRectangle.X + GameInformation.ScreenRectangle.Width - obj.Sprite.Width / 2 + plus, obj.Position.Y - plus);
            if (obj.Position.Y < GameInformation.ScreenRectangle.Y + obj.Sprite.Height / 2 - plus)
                obj.Position = new Vector2(obj.Position.X - plus, GameInformation.ScreenRectangle.Y + obj.Sprite.Height / 2 + plus);
            if (obj.Position.Y > GameInformation.ScreenRectangle.Y + GameInformation.ScreenRectangle.Height - obj.Sprite.Height / 2 + plus)
                obj.Position = new Vector2(obj.Position.X - plus, GameInformation.ScreenRectangle.Y + GameInformation.ScreenRectangle.Height - obj.Sprite.Height / 2 + plus);
        }
        public void UpdateObjectsState2(GameTime gameTime)
        {
            const int width = 2000;
            const int height = 2000;
            const int maxR = 15;
            const int plus = 300;
            
            // bullets impacts
            List<GameObject> [,] grid = new List<GameObject>[width / maxR,height / maxR];

            for (int i = 0; i < width / maxR; i++)
                for (int j = 0; j < height / maxR; j++)
                    grid[i,j] = new List<GameObject>();

            foreach (EnemyObject enemy in _store.Enemys)
            {
                grid[(int)(enemy.Position.X + plus) / maxR, (int)(enemy.Position.Y + plus) / maxR].Add(enemy);
            }

            foreach(BulletObject bullet in _store.Bullets)
            {
                foreach(EnemyObject enemy in grid[(int) (bullet.Position.X + plus) / maxR,(int) (bullet.Position.Y + plus) / maxR])
                {
                    if(bullet.Contains(enemy) && enemy.HealthPoints > 0)
                    {
                        bullet.Hit(enemy);
                        if(enemy.HealthPoints <= 0)
                        {
                            _deleteEnemys.Add(enemy);
                        }
                        if (bullet.Demage <= 0)
                            break;
                    }
                }
                
                bullet.Move(null);
                
                if (!GameInformation.GameField.Contains((int)bullet.Position.X, (int)bullet.Position.Y) || bullet.Demage <= 0)
                {
                    _deleteBullets.Add(bullet);
                }
            }
            // bonuses impacts
            PerformBonusesImpacts();
            // player impacts
            if (grid[(int)_store.Players[0].Position.X / maxR, (int)_store.Players[0].Position.Y / maxR] != null)
                foreach (EnemyObject enemy in grid[(int)(_store.Players[0].Position.X + plus) / maxR, (int)(_store.Players[0].Position.Y + plus) / maxR])
                {
                    if (_store.Players[0].Contains(enemy))
                    {
                        enemy.Hit(_store.Players[0]);
                        if (_store.Players[0].HealthPoints <= 0)
                            OnPlayerDie(_store.Players[0]);
                    }
                }

            UpdateBullets();
            UpdateBonuses();
            UpdatePlayers();
            UpdateEnemies(gameTime);

            PerformMovment();
        }
        /*
        public void UpdateObjectsState(GameTime gameTime)
        {
            //if (gameTime.IsRunningSlowly)
            //    return;
            PerformBulletImpacts();
            PerformBonusesImpacts();
            PerformPlayerImpacts();
            //System.Threading.Tasks.ParallelOptions po = new System.Threading.Tasks.ParallelOptions();
            //System.Threading.Tasks.ParallelLoopState s = new System.Threading.Tasks.ParallelLoopState();
            
            UpdateEnemies(gameTime);
            Action act1 = new Action(UpdateBullets);
            System.Threading.Tasks.Task t1 = new System.Threading.Tasks.Task(act1);
            t1.Start();
            Action act2 = new Action(UpdatePlayers);
            System.Threading.Tasks.Task t2 = new System.Threading.Tasks.Task(act2);
            t2.Start();
            //UpdateBullets();
            //UpdatePlayers();
            Action act3 = new Action(UpdateBonuses);
            System.Threading.Tasks.Task t3 = new System.Threading.Tasks.Task(act3);
            t3.Start();
            //UpdateBonuses();
            t1.Wait();
            t2.Wait();
            t3.Wait();
            PerformMovment();

        }
        */
        private void PerformBulletImpacts()
        {
            System.Threading.Tasks.Parallel.ForEach(_store.Bullets, (GameObject bullet) =>
            {
            //foreach (BulletObject bullet in _store.Bullets)
            //{
                foreach (EnemyObject enemy in _store.Enemys)
                {
                    lock (locker1)
                    {
                        if (enemy.HealthPoints > 0 && enemy.Contains(bullet))
                        {
                            (bullet as BulletObject).Hit(enemy);
                            if (enemy.HealthPoints <= 0)
                            {
                                enemy.Alive = false;
                                _deleteEnemys.Add(enemy);
                            }
                            if (bullet.Demage <= 0)
                            {
                                break;
                            }
                        }
                    }
                }
                (bullet as BulletObject).Position += (bullet as BulletObject).Velocity;
                if (!GameInformation.GameField.Contains((int)bullet.Position.X, (int)bullet.Position.Y) || bullet.Demage <= 0)
                {
                    _deleteBullets.Add(bullet);
                }
            //}
            });
        }
        private void PerformBonusesImpacts()
        {
            foreach (PlayerObject player in _store.Players)
            {
                foreach (BonusObject bonus in _store.Bonuses)
                {
                    if (player.Contains(bonus))
                    {
                        bonus.Apply(player);
                        _deleteBonuses.Add(bonus);
                    }
                    if (bonus.AppiarTime.Add(bonus.AliveTime).CompareTo(DateTime.Now) <= 0)
                    {
                        _deleteBonuses.Add(bonus);
                    }
                }
            }
        }
        private void PerformPlayerImpacts()
        {
            System.Threading.Tasks.Parallel.ForEach(_store.Enemys, (GameObject enemy) =>
            {
            //foreach(GameObject enemy in _store.Enemys)
                foreach (PlayerObject player in _store.Players)
                {
                    if (player.Contains(enemy))
                    {
                        (enemy as EnemyObject).Hit(player);

                        if (player.HealthPoints <= 0)
                        {
                            player.Alive = false;

                            _deletePlayers.Add(player);
                        }
                    }
                }
            });
        }
        private void UpdateEnemies(GameTime gameTime)
        {
            foreach (EnemyObject obj in _deleteEnemys)
            {
                _store.Players[0].Experiance += obj.Experiance;
                if (obj.Bag != null)
                {
                    foreach (GameObject gobj in obj.Bag)
                    {
                        gobj.Position = obj.Position;
                        if(gobj is BonusObject)
                        {
                            (gobj as BonusObject).AppiarTime = DateTime.Now;
                        }
                        _store.Add(gobj);
                    }
                }

                _store.Remove(obj);
                _factory.ReturnObject(obj);
                // for neural network
                //obj.fitenessInfo.Kills++;
            }

            _deleteEnemys.Clear();

            int monstersCount = GameInformation.GetMonstersCount(_store.Players[0].Experiance,gameTime);
            monstersCount -= _store.Enemys.Count;

            for (int i = 0; i < monstersCount; i++)
            {
                _store.Add(_factory.GetEnemy());
                int t = 0;
                if(_store.Enemys[i].Position.X != 200 || _store.Enemys[i].Position.Y != 200)
                    t = 10;
                //AddObject(GameObjectsManager.GetEnemy(rand.Next(ScreenRectangle.X, ScreenRectangle.Width), rand.Next(ScreenRectangle.X, ScreenRectangle.Width)));
            }

            //if (GameInformation.MaxEnemysCount < _store.Enemys.Count)
            //    GameInformation.MaxEnemysCount = _store.Enemys.Count;
            //if (GameInformation.MaxPlayersPoints < _store.Players[0].Experiance)
            //    GameInformation.MaxPlayersPoints = _store.Players[0].Experiance;
            GameInformation.PlayerPoints = _store.Players[0].Experiance;
            GameInformation.PlayerLevel = CalculatePlayerLevel(_store.Players[0].Experiance);
        }
        private int CalculatePlayerLevel(int experiance)
        {
            return experiance / 1000;
        }
        private void UpdateBullets()
        {
            foreach (GameObject obj in _deleteBullets)
            {
                _store.Remove(obj);
                _factory.ReturnObject(obj);
            }
            _deleteBullets.Clear();
        }
        private void UpdatePlayers()
        {
            foreach (GameObject obj in _deletePlayers)
            {
                _store.Remove(obj);
                //OnPlayerDie(obj as PlayerObject);
            }
            _deletePlayers.Clear();

            foreach(PlayerObject player in _store.Players)
            {
                if (player.Bag != null)
                {
                    foreach (GameObject gameObject in player.Bag)
                        if (gameObject is BonusObject)
                        {
                            (gameObject as BonusObject).Action(player, _store);
                        }
                }
            }
        }
        private void UpdateBonuses()
        {
            foreach (BonusObject bonus in _deleteBonuses)
            {
                _store.Remove(bonus);
            }
        }
        public void PerformMovment()
        {
            /*
            System.Threading.Tasks.Parallel.ForEach(_store.Bullets, (GameObject bullet) =>
            {
                bullet.Position += bullet.Velocity;
            });
            */
            System.Threading.Tasks.Parallel.ForEach(_store.Enemys, (GameObject enemy) =>
            {
            //foreach (EnemyObject enemy in _store.Enemys)
            //{
                //enemy.Rotation = (float)Math.Atan2(_store.Players[0].Position.Y - enemy.Position.Y, _store.Players[0].Position.X - enemy.Position.X);
                //enemy.Velocity = new Vector2(enemy.Speed * (float)Math.Cos(enemy.Rotation), enemy.Speed * (float)Math.Sin(enemy.Rotation));
                //enemy.Position += enemy.Velocity;
                enemy.Move(_store.Players[0]);
                GameObjectPositionCheckEnemy(enemy);
                // enemy position checking
                //GameObjectPositionCheck(enemy);
            //}
            });
        }
        public GameEventsHandler.GameState DrawObjects(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            GameInformation.GameSpriteBatch = spriteBatch;

            foreach (EnemyObject enemy in _store.Enemys)
                enemy.Draw(GameInformation.GameSpriteBatch);

            foreach (BonusObject bonus in _store.Bonuses)
                bonus.Draw(GameInformation.GameSpriteBatch);

            foreach (BulletObject bullet in _store.Bullets)
                bullet.Draw(GameInformation.GameSpriteBatch);

            foreach (PlayerObject player in _store.Players)
                player.Draw(GameInformation.GameSpriteBatch);

            spriteBatch.End();

            return GameEventsHandler.GameState.playing;
        }
        /*
        public void DrawObjects(SpriteBatch spriteBatch)
        {
            //gameMatrix = Matrix.CreateTranslation(camera);
            //spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.NonPremultiplied,SamplerState.PointClamp,DepthStencilState.None,RasterizerState.CullNone,null,gameMatrix);
            spriteBatch.Begin();
            GameInformation.GameSpriteBatch = spriteBatch;

            foreach (EnemyObject enemy in _store.Enemys)
            {
                enemy.Draw(GameInformation.GameSpriteBatch);
            }

            foreach (BonusObject bonus in _store.Bonuses)
            {
                bonus.Draw(GameInformation.GameSpriteBatch);
            }

            foreach (BulletObject bullet in _store.Bullets)
            {
                bullet.Draw(GameInformation.GameSpriteBatch);
            }

            foreach (PlayerObject player in _store.Players)
            {
                player.Draw(GameInformation.GameSpriteBatch);
            }
            spriteBatch.End();
        }
        */
    }
}
