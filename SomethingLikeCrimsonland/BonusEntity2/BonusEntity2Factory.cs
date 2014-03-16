using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entities;

namespace BonusEntity2
{
    [GameObject("Bonus")]
    public class BonusEntity2Factory
    {
        //[GameObject("Bonus")]
        //public class BonusEntity1Factory : IFactory
        //{
        protected Queue<BonusEntity2> _bonuses;

        //public BonusEntity1Factory()
        public BonusEntity2Factory()
        {
            _bonuses = new Queue<BonusEntity2>();
        }

        public GameObject GetObject()
        {
            if (_bonuses.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                    _bonuses.Enqueue(new BonusEntity2(TexturesManager.GetTexture("helth")));
            }
            BonusEntity2 bonus = _bonuses.Dequeue();
            return bonus;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj is BonusEntity2)
                _bonuses.Enqueue((BonusEntity2)obj);
        }
        //}
    }
}
