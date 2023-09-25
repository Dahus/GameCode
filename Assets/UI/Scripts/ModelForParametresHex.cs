using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.GridSystem;
public class ModelForParametresHex : ModelForParametres
{
    public override void UpdateInfoObject(GameObject obj)
    {
        var hex = obj.GetComponent<GridHex>();
        Name = hex.name;
        base.UpdateInfoObject(obj);
    }

    public override void UpdateUIParametres(UIParametresLogic uiParametres)
    {
        uiParametres.UpdateUI(0, Name);
    }
}
