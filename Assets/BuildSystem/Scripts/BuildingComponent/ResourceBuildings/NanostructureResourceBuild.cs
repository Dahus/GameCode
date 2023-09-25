using Game.BuildingSystem;
using Game.Economics;
using Game.GlobalData;
using Game.GlobalSystems;
using Game.GridSystem;
using Game.Player;
using System.Collections.Generic;
using UnityEngine;


public class NanostructureResourceBuild : AbstractBuild
{
    private DictionaryResource _resourcePlace;
    [SerializeField] private int maxMaining = 20;
    [SerializeField] private int neededMetal = 40;
    [SerializeField] private bool activeGainResource = false;

    

   

    public override bool CreateBuilding(GameObject place, PlayerManager player)
    {
        var hex = place.GetComponent<GridHex>();
        _resourcePlayer = player.GetResources();
        var obj = Instantiate(gameObject, place.transform.position, Quaternion.identity, place.transform);
        obj.TryGetComponent(out Building build);
        obj.GetComponent<SpriteRenderer>().color = player.GetColor();
        _resourcePlace = hex.ResourceHex;
        hex.AddObjectInHex(obj);

        return true;
    }

    public override void BeginPlayerTurn()
    {
        GainResource();
    }

    private void GainResource()
    {
        if (!activeGainResource)
            return;

        if (_resourcePlayer.GetValue(ConstantNameResource.METAL) < neededMetal)
            return;

        DictionaryResource.SubAttributeDictionary(_resourcePlayer, new Game.Types.KeyValuePair<string, int>(ConstantNameResource.METAL, neededMetal));
        DictionaryResource.AddAttributeDictionary(_resourcePlayer, new Game.Types.KeyValuePair<string, int>(ConstantNameResource.NANO_STRUCTURE, maxMaining));
    }

    public void SwitchActive()
    {
        activeGainResource = !activeGainResource;
    }

}