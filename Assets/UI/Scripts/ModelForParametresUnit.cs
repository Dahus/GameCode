using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.UnitsSystem;
public class ModelForParametresUnit : ModelForParametres
{
    public float Health { get; set; }
    public float Damage { get; set; }
    public int ActionPoints { get; set; }

    public override void UpdateInfoObject(GameObject obj)
    {
        Debug.LogError("А тут" + obj.name);
        var unit = obj.GetComponent<AbstractUnit>();
        Health = unit.GetHealth();
        Name = unit.GetName();
        Damage = unit.GetDamage();
        ActionPoints = unit.ActionPoints;
    }

    public override void UpdateUIParametres(UIParametresLogic uiParametres)
    {
        uiParametres.UpdateUI(0, Name);
        uiParametres.UpdateUI(1, Health.ToString());
        uiParametres.UpdateUI(2, Damage.ToString());
        uiParametres.UpdateUI(3, ActionPoints.ToString());
    }
}
