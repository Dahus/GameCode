using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;
using Game.UnitsSystem;
using Game.FightSystem;
public class WarRobotFunction : MonoBehaviour, IFuncManager
{
    private EventCustom _eventWalk;
    private EventCustom _eventAttack;
    private ChoosedObject _choosedObject;
    [SerializeField] private AbstractUnit _unit;

    public void Setup()
    {
        _choosedObject = GetComponent<ChoosedObject>();
        _choosedObject.Setup();
        WriteInDictionary();
    }

    public void SetupUnit(AbstractUnit unit)
    {
        _unit = unit;
    }
    public void WriteInDictionary()
    {
        _eventWalk = new EventCustom();
        _eventAttack = new EventCustom();
        _eventWalk.AddListener(FoundEndHex);
        _eventAttack.AddListener(Attack);
        _choosedObject.AddDictionaryEvent(1,_eventWalk);
        _choosedObject.AddDictionaryParameters(1, null);
        _choosedObject.AddDictionaryEvent(2, _eventAttack);
        _choosedObject.AddDictionaryParameters(2, null);

    }
    
    private void FoundEndHex(object[] parameters)
    {
        Observer.instance.GetMoveObserver().SetUnit(_unit);
        //Observer.instance.FlagMoveObs(true);
        Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        //Debug.LogError("��������� ����");
    }

    private void Attack(object[] parameters)
    {
        //_unit.
        if (_unit.FightActionPoints < _unit.ActionPoints)
        {
            Observer.instance.GetPlayerObserver().SetAttackObject(_unit.gameObject);
            Observer.instance.StartAttack();
            _unit.GetComponent<IAttack>().GetReadyAttack();
        }
        else
        {
            Debug.LogError("Не хватает энергии");
        }
    }
    
}
