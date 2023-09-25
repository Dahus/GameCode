using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.GridSystem;

namespace Game.Player
{
    public class ActionObserver : MonoBehaviour
    {
        private IAction _objectAction;


        public void SetObjectAction(IAction objAction)
        {
            _objectAction = objAction;
            Observer.instance.SetStateObserver(StateObserver.ActionObserver);
        }

        public void Execute(GridHex hex)
        {
            _objectAction.Execute(hex);
            Observer.instance.SetStateObserver(StateObserver.Standart);
        }
    }

}