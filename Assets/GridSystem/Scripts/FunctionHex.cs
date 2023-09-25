using System;
using System.Collections;
using System.Collections.Generic;
using Game.BuildingSystem;
using Game.Player;
using UnityEngine;


namespace Game.Events
{
    public class FunctionHex : MonoBehaviour, IFuncManager
    {
        private EventCustom _event;
        private List<ParameterName> _parameterNameBuild;

        [SerializeField] private FactoryForBuild _factory;
        [SerializeField] private Sprite[] _icons;
        private ChoosedObject _choosedObject;

        private void Start()
        {
            _choosedObject = GetComponent<ChoosedObject>();
            _choosedObject.Setup();
            _parameterNameBuild = new List<ParameterName>();
            
        }

        public void SetFactoryForBuild(FactoryForBuild factory)
        {
            ClearList();
            _choosedObject.ClearDictionary();
            _factory = factory;
            WriteInDictionary();
        }

        public void WriteInDictionary()
        {
            _event = new EventCustom();

            _event.AddListener(Build);
            
            _parameterNameBuild.Add(new ParameterName());
            _parameterNameBuild.Add(new ParameterName());
            _parameterNameBuild.Add(new ParameterName());
           // _parameterNameBuild.Add(new ParameterName());
            
            _parameterNameBuild[0].Name = _factory.SearchName(0);
            _parameterNameBuild[1].Name = _factory.SearchName(1);
            _parameterNameBuild[2].Name = _factory.SearchName(2);
            // _parameterNameBuild[3].Name = _factory.SearchName(3);

            _event.Setup("create1,", _icons[0]);
            _choosedObject.AddDictionaryEvent(1, _event);
            _choosedObject.AddDictionaryParameters(1, _parameterNameBuild[0]);
            _event = new EventCustom();
            _event.AddListener(Build);
            _event.Setup("create1,", _icons[1]);
            _choosedObject.AddDictionaryEvent(2, _event);
            _choosedObject.AddDictionaryParameters(2, _parameterNameBuild[1]);
           
            _event = new EventCustom();
            _event.AddListener(Build);
            _event.Setup("create2,", _icons[2]);
            _choosedObject.AddDictionaryEvent(3, _event);
            _choosedObject.AddDictionaryParameters(3, _parameterNameBuild[2]);
          //  _choosedObject.AddDictionaryEvent(4, _event);
          //  _choosedObject.AddDictionaryParameters(4, _parameterNameBuild[3]);
        }

        public void ClearList()
        {
            _parameterNameBuild.Clear();
        }
        
        
        private void Build(object[] parameters)
        {
            ParameterName par = (ParameterName)parameters[0];
            Debug.Log("Строю " + par.Name);
            _factory.CreateBuild(par.Name,Observer.instance.GetHexagon());
        }
    }
}
