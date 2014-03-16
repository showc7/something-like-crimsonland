using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entities;

namespace Entities
{
    [Serializable]
    public class ObjectsStore
    {
        protected List<PlayerObject> _players;
        protected List<EnemyObject> _enemys;
        protected List<BonusObject> _bonuses;
        protected List<BulletObject> _bullets;
        protected List<WeaponObject> _weapons;

        public List<PlayerObject> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        public List<EnemyObject> Enemys
        {
            get { return _enemys; }
            set { _enemys = value; }
        }

        public List<BulletObject> Bullets
        {
            get { return _bullets; }
            set { _bullets = value; }
        }

        public List<BonusObject> Bonuses
        {
            get { return _bonuses; }
            set { _bonuses = value; }
        }

        public List<WeaponObject> Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }
        public ObjectsStore()
        {
            _players = new List<PlayerObject>();
            _enemys = new List<EnemyObject>();
            _bonuses = new List<BonusObject>();
            _bullets = new List<BulletObject>();
            _weapons = new List<WeaponObject>();
        }

        public void Remove(GameObject obj)
        {
            if(obj is EnemyObject)
            {
                _enemys.Remove(obj as EnemyObject);
            }
            else if(obj is BonusObject)
            {
                _bonuses.Remove(obj as BonusObject);
            }
            else if(obj is BulletObject)
            {
                _bullets.Remove(obj as BulletObject);
            }
            else if(obj is WeaponObject)
            {
                _weapons.Remove(obj as WeaponObject);
            }
            else if(obj is PlayerObject)
            {
                _players.Remove(obj as PlayerObject);
            }
        }

        public void Add(GameObject obj)
        {
            if (obj is EnemyObject)
            {
                _enemys.Add(obj as EnemyObject);
            }
            else if (obj is BonusObject)
            {
                _bonuses.Add(obj as BonusObject);
            }
            else if (obj is BulletObject)
            {
                _bullets.Add(obj as BulletObject);
            }
            else if (obj is WeaponObject)
            {
                _weapons.Add(obj as WeaponObject);
            }
            else if (obj is PlayerObject)
            {
                _players.Add(obj as PlayerObject);
            }
        }
    }
}
