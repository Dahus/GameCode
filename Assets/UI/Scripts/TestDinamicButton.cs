using System;
using System.Collections;
using System.Collections.Generic;
using Game.FightSystem;
using Game.Player;
using Game.UnitsSystem;
using UnityEngine;
using UnityEngine.UI;

public class TestDinamicButton : MonoBehaviour
{

    public AbstractUnit _unit;
    public IDamage _damageObject;

    public void SetUnit(AbstractUnit unit)
    {
        _unit = unit;
        gameObject.SetActive(true);
        gameObject.GetComponent<Button>().onClick.AddListener(Observer.instance.StartAttack);
        //Observer.instance.GetPlayerObserver().SetAttackObject(unit.GetComponent<IAttack>());
    }

    public void UnitNull()
    {
        _unit = null;
        gameObject.GetComponent<Button>().onClick.RemoveListener(Observer.instance.StartAttack);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        
        if(_unit != null)
        transform.position = Camera.main.WorldToScreenPoint(new Vector2(_unit.transform.position.x,_unit.transform.position.y+0.2f));
    }
}
