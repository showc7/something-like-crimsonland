using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public interface IFactory
    {
        GameObject GetObject();
        void ReturnObject(GameObject obj);
    }
}
