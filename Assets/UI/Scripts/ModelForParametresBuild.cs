using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.BuildingSystem;
public class ModelForParametresBuild : ModelForParametres
{
    public float Health { get; set; }
    public override void UpdateInfoObject(GameObject obj)
    {
        var build = obj.GetComponent<AbstractBuild>();
        Health = build.GetLife();
        Name = build.GetName();
    }

    public override void UpdateUIParametres(UIParametresLogic uiParametres)
    {
        uiParametres.UpdateUI(0, Name);
        uiParametres.UpdateUI(1, Health.ToString());
    }
}
