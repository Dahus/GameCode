using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.UnitsSystem;

public class FunctionRobotNode : MonoBehaviour,IFuncManager
{

    private ChoosedObject _choosedObject;
    private FactoryForUnits _factory;


    private EventCustom _eventCreateSniperDrone;

    public void Setup()
    {
        _choosedObject = GetComponent<ChoosedObject>();
        _choosedObject.Setup();
        //_unit = new ParametresForUnits();
        WriteInDictionary();
    }
    public void WriteInDictionary()
    {
        throw new System.NotImplementedException();
    }
}
