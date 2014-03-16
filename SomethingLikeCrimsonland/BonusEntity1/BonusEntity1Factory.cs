using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entities;

namespace BonusEntity1
{
    [GameObject("Bonus")]
    public class BonusEntity1Factory : IFactory
    {
        protected Queue<BonusEntity1> _bonuses;

        public BonusEntity1Factory()
        {
            _bonuses = new Queue<BonusEntity1>();
        }

        public GameObject GetObject()
        {
            if (_bonuses.Count == 0)
            {
                for (int i = 0; i < 10;i++ )
                    _bonuses.Enqueue(new BonusEntity1(TexturesManager.GetTexture("health")));
            }
            BonusEntity1 bonus = _bonuses.Dequeue();
            return bonus;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj is BonusEntity1)
                _bonuses.Enqueue((BonusEntity1)obj);
        }
    }
}
