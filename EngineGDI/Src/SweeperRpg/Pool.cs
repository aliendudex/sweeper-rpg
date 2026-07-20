using System;
using System.Collections.Generic;

namespace EngineGDI.Src.SweeperRpg
{
    public class Pool<T>
        where T : class, IResettable
    {
        private readonly Queue<T> objects = new Queue<T>();

        public void Add(T obj)
        {
            objects.Enqueue(obj);
        }

        public T Get(Func<T> creator)
        {
            if (objects.Count > 0)
            {
                T obj = objects.Dequeue();
                obj.Reset();
                return obj;
            }

            return creator();
        }

        public void Return(T obj)
        {
            obj.Reset();
            objects.Enqueue(obj);
        }
    }
}
