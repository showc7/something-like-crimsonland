using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.IO;
using System.Reflection;

using Entities;

namespace SomethingLikeCrimsonland
{
    class AssemblysManager
    {
        protected string _directory;
        protected List<IFactory> _players;
        protected List<IFactory> _enemys;
        protected List<IFactory> _bonuses;
        protected List<IFactory> _bullets;
        protected List<IFactory> _weapons;

        public List<IFactory> PlayersFactories
        {
            get { return _players; }
        }

        public List<IFactory> EnemysFactories
        {
            get { return _enemys; }
        }

        public List<IFactory> BonusesFactories
        {
            get { return _bonuses; }
        }

        public List<IFactory> BulletsFactories
        {
            get { return _bullets; }
        }

        public List<IFactory> WeaponsFactories
        {
            get { return _weapons; }
        }

        public AssemblysManager()
        {
            _directory = Directory.GetCurrentDirectory() + "\\" + ConfigurationManager.AppSettings["dll"];
            _players = new List<IFactory>();
            _enemys = new List<IFactory>();
            _bonuses = new List<IFactory>();
            _bullets = new List<IFactory>();
            _weapons = new List<IFactory>();
            LoadDlls();
        }
        /*
        protected void LoadDlls()
        {
            string [] dlls = Directory.GetFiles(_directory,"*.dll");
            players = ( from file in dlls
                        let asm = Assembly.LoadFile(file)
                        from type in asm.GetExportedTypes()
                        where typeof(IFactory).IsAssignableFrom(type)
                        select (IFactory) Activator.CreateInstance(type)).ToArray();
        }
        */
        protected void LoadDlls()
        {
            string[] dlls = Directory.GetFiles(_directory,"*.dll");
            
            IFactory factory;

            foreach(string file in dlls)
            {
                Assembly asm = Assembly.LoadFile(file);
                foreach(Type type in asm.GetExportedTypes())
                {
                    if(typeof(IFactory).IsAssignableFrom(type))
                    {
                        factory = (IFactory) Activator.CreateInstance(type);
                        foreach(Attribute attribute in factory.GetType().GetCustomAttributes(false))
                        {
                            GameObjectAttribute attr = attribute as GameObjectAttribute;
                            if(attr != null)
                            {
                                if(attr.GameObjectType.Equals("Enemy"))
                                {
                                    _enemys.Add((IFactory) Activator.CreateInstance(type));
                                }
                                else if(attr.GameObjectType.Equals("Weapon"))
                                {
                                    _weapons.Add((IFactory) Activator.CreateInstance(type));
                                }
                                else if(attr.GameObjectType.Equals("Bullet"))
                                {
                                    _bullets.Add((IFactory) Activator.CreateInstance(type));
                                }
                                else if(attr.GameObjectType.Equals("Bonus"))
                                {
                                    _bonuses.Add((IFactory) Activator.CreateInstance(type));
                                }
                                else if(attr.GameObjectType.Equals("Player"))
                                {
                                    _players.Add((IFactory)Activator.CreateInstance(type));
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
