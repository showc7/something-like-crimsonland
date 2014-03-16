using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameObjectAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EnemyLevel Level { get; set; }
        public Type type { get; set; }
        public string GameObjectType { get; set; }
        public string TextureName { get; set; }

        public GameObjectAttribute(string gameObjectType)
        {
            GameObjectType = gameObjectType;
        }

        public GameObjectAttribute(string name, string description, string gameObjcetType, string textureName, EnemyLevel level)
        {
            Name = name;
            Description = description;
            GameObjectType = gameObjcetType;
            TextureName = textureName;
            Level = level;
        }
    }
}
