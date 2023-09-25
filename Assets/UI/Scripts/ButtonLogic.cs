using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class ButtonLogic : MonoBehaviour
    {
        [SerializeField] private int id;
        private EventCustom _event;
        [SerializeField] private ChoosedObject _choosedObject;



        public void SetChoosedObject(ChoosedObject choosedObject)
        {
            _choosedObject = choosedObject;
        }
        
        public void SetEvent(EventCustom eventCustom)
        {
            _event = eventCustom;
            GetComponent<Image>().sprite = eventCustom.GetSprite();
        }

        public void SendEvent(params object[] parameters)
        {
            _event?.Invoke(parameters);
        }
        
        public void Click()
        {
            SendEvent(_choosedObject.SearchReturnObject(id));
        }
    }
}