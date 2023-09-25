using System.Collections;
using System.Collections.Generic;
using Game.BuildingSystem;
using Game.GlobalData;
using Game.Player;
using Game.UnitsSystem;
using UnityEngine;

public class FunctionCC : MonoBehaviour,IFuncManager
{
    private EventCustom _eventCreate;
    private EventCustom _eventAttack;
    private ChoosedObject _choosedObject;
    [SerializeField] private CommandCenter _commandCenter;
    [SerializeField] private FactoryForUnits _factory;
    private ParametresForUnits _unit;
    [SerializeField] private Sprite[] _Icons;


    public void Setup()
    {
        _choosedObject = GetComponent<ChoosedObject>();
        _choosedObject.Setup();
        _unit = new ParametresForUnits();
        WriteInDictionary();
    }

    public void SetupFactoryCC(FactoryForUnits factory)
    {
        _factory = factory;
    }

    public void SetupCC(CommandCenter commandCenter)
    {
        _commandCenter = commandCenter;
    }

    
    public void WriteInDictionary()
    {
        _eventCreate = new EventCustom();
        _eventAttack = new EventCustom();
        _eventCreate.Setup("Create", _Icons[0]);
        _eventAttack.Setup("Attack", _Icons[1]);
        _eventCreate.AddListener(CreateUnit);
        _eventAttack.AddListener((Attack));
        _unit.Name = ConstantNameUnit.WARROBOT;
        _unit.Step = _factory.SearchUnit(_unit.Name).GetUnitsData().GetTimeCreateUnit();
      
        
        _choosedObject.AddDictionaryEvent(1, _eventCreate);
        _choosedObject.AddDictionaryParameters(1, _unit);
        _choosedObject.AddDictionaryEvent(2, _eventAttack);
        _choosedObject.AddDictionaryParameters(2, null);
    }
   
    private void CreateUnit(object[] parameters)
    {
        ParametresForUnits par = (ParametresForUnits)parameters[0];
       // _commandCenter.Create(par.Name,par.Step);
        
    }

    private void Attack(object[] parametres)
    {
        Debug.Log("Ударили кого то");
    }
    
}
