using System;
using System.Collections;
using System.Collections.Generic;
using Game.BuildingSystem;
using Game.GlobalData;
using Game.UnitsSystem;
using UnityEngine;

namespace Game.Events
{
    public class EventLocator : MonoBehaviour
    {
        public static EventLocator instance;
        
        [SerializeField] private FunctionHex _functionHex;



        [SerializeField] private FunctionCC _functionCC;





        [SerializeField] private WarRobotFunction _warRobotFunction;

        [SerializeField] private GameObject _object;
        
        private ChoosedObject currentObject;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }
        }

        public void FactoryForEventer(FactoryForBuild factoryBuilds, FactoryForUnits factoryUnits)
        {
            _functionHex.SetFactoryForBuild(factoryBuilds);
            _functionCC.SetupFactoryCC(factoryUnits);
        }

        public ChoosedObject GetFunctionHexChoosedObject()
        {
            return _functionHex.GetComponent<ChoosedObject>();
        }

        public ChoosedObject GetFunctionBuildByName(string name)
        {
            switch (name)
            {
                case ConstantNameBuild.CC:
                    Debug.Log("Здание найдено");
                    _functionCC.SetupCC(_object.GetComponent<CommandCenter>());
                    currentObject = _functionCC.GetComponent<ChoosedObject>();
                    break;
            }

            return currentObject;
        }

        public ChoosedObject GetFunctionUnitsByName(string name, AbstractUnit unit)
        {
            switch (name)
            {
                case ConstantNameUnit.WARROBOT:
                    _warRobotFunction.SetupUnit(unit);
                    currentObject = _warRobotFunction.GetComponent<ChoosedObject>();
                    break;
            }
            return currentObject;
        }


       
        public void SetupBuildFunction()
        {
            
        }


        public void SetupEventBus()
        {
            _functionCC.Setup();
            _warRobotFunction.Setup();
           
        }

        public void SetObject(GameObject obj)
        {
            _object = obj;
        }
    }
}
