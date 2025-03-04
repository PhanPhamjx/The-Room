using System.Collections.Generic;
using UnityEngine;

namespace Project_TR.Events
{
    public class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> eventListener = new List<IGameEventListener<T>>(); 
        public void Raise(T item)
        {
            for (int i = eventListener.Count - 1; i >= 0; i--)
            {
                eventListener[i].OnEventRaised(item);
            }
        }
        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!eventListener.Contains(listener))
            {
                eventListener.Add(listener);
            }
        }   
        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (eventListener.Contains(listener))
            {
                eventListener.Remove(listener);
            }
        }   
    }

}
