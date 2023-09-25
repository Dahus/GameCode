using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.BuildingSystem;
using Game.UnitsSystem;
using Game.GlobalData;
public class FunctionTT : MonoBehaviour,IFuncManager
{

    [SerializeField] private FactoryForUnits _factory;
    [SerializeField] private TradingTerminal _tradingTerminal;

    private EventCustom _eventCreate;
    private ChoosedObject _choosedObject;
    private ParametresForUnits _unit;
    public void WriteInDictionary()
    {
        _eventCreate = new EventCustom();
        _eventCreate.AddListener(CreateUnit);

        _unit.Name = ConstantNameUnit.SHUTTLE;
        _unit.Step = _factory.SearchUnit(_unit.Name).GetUnitsData().GetTimeCreateUnit();
        //_unit.Place = _choosedObject.GetPlace();

        _choosedObject.AddDictionaryEvent(1, _eventCreate);
        _choosedObject.AddDictionaryParameters(1, _unit);
    }


    private void CreateUnit(object[] parameters)
    {
        Debug.Log("создали юнита");
        ParametresForUnits par = (ParametresForUnits)parameters[0];
        Debug.LogError("Строю " + par.Name); 
       // _tradingTerminal.Create(par.Name, par.Step, TypeObjectProduction.Unit);
    }

    public void SetupFactoryTT(FactoryForUnits factory)
    {
        _factory = factory;
    }

    public void SetupTT(TradingTerminal tradingTerminal)
    {
        _tradingTerminal = tradingTerminal;
    }

    public void Setup()
    {
        _choosedObject = GetComponent<ChoosedObject>();
        _choosedObject.Setup();
        _unit = new ParametresForUnits();
        WriteInDictionary();
    }
   
}
