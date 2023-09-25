using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.BuildingSystem;
using Game.Player;
using Game.GridSystem;

public class Fort : AbstractBuild
{

    [SerializeField] private ModificatorFight _mod;

    public override bool CreateBuilding(GameObject place, PlayerManager player)
    {
        _mod = GetComponent<ModificatorFight>();
        if (place.TryGetComponent(out ModificatorHex hex))
        {
            hex.AddModificator(_mod);
        }
        return base.CreateBuilding(place, player);
    }
    public override void DestroyObject()
    {
        Debug.LogError("Стараюсь удалить модификатор");
        _place.GetComponent<ModificatorHex>().DeleteModificator(_mod);
        base.DestroyObject();
    }
}
