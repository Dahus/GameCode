using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI
{
    [Serializable]
    public class MenuButtonData
    {
        public int ID;
        public string Name;
        public Sprite Sprite;
        public UnityEvent Event;
       // public UnityEvent<T0> Event1;
       // public UnityEvent<T0,T1> Event2;
       // public UnityEvent<T0,T1,T2> Event3;
        public void Call()
        {
            Event?.Invoke();
        }
       /* public void Call(T0 t0,T1 t1)
        {
            Event2?.Invoke(t0,t1);
        }


        public void Call(T0 t0)
        {
            Event1 ?.Invoke(t0);
        }*/
    }
}
