using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Scripts.Utils
{
    public class StateMachineQueue<TObj> : IEnumerable where TObj : Enum
    {
        private List<TObj> objectList = new List<TObj>();
        private readonly int maxSize;
        
        public StateMachineQueue(int maxSize = 5)
        {
            this.maxSize = maxSize;
        }

        public IEnumerator GetEnumerator()
        {
            return objectList.GetEnumerator();
        }

        public void EndPeek(TObj obj)
        {
            if (Equals(objectList.LastOrDefault(), obj))
            {
                return;
            }

            if (objectList.Count < maxSize)
            {
                objectList.Add(obj);
            }
            else
            {
                for (int i = 0; i < objectList.Count - 2; i++)
                {
                    objectList[i] = objectList[i + 1];
                }
                
                objectList[objectList.Count - 1] = obj;
            }
        }

        public TObj Enqueue()
        {
            var ob = objectList.LastOrDefault();

            objectList.Remove(ob);
            
            return objectList.LastOrDefault();
        }
    }
}