using Game.BuildingSystem;
using Game.Economics;
using Game.GlobalData;
using Game.GlobalSystems;
using Game.GridSystem;
using Game.Player;
using System.Collections.Generic;
using UnityEngine;

public class CrystalResourceBuild : AbstractBuild
{
    private DictionaryResource _resourcePlace;
    [SerializeField] private int maxMaining = 100;
   

    public override bool CreateBuilding(GameObject place, PlayerManager player)
    {
        _place = place;
        _player = player;
        var hex = place.GetComponent<GridHex>();
        if (hex.ResourceType != ResourceType.Crystal)
        {
            Debug.LogError("Это место не содержит ресурс 'Кристалл'");
            return false;
        }

        _resourcePlayer = player.GetResources();
        var obj = Instantiate(gameObject, place.transform.position, Quaternion.identity, place.transform);
        obj.TryGetComponent(out Building build);
        obj.GetComponent<SpriteRenderer>().color = player.GetColor();
        _resourcePlace = hex.ResourceHex;
        hex.AddObjectInHex(obj);
        player.AddObjectPlayer(obj);
        return true;
    }

    public override void BeginPlayerTurn()
    {
        GainResource(); 
    }

    private void GainResource()
    {
        _resourcePlace = _place.GetComponent<GridHex>().ResourceHex;
        _resourcePlayer = _player.GetResources();
        
        if (_resourcePlace.GetValue(ConstantNameResource.ENERGY_CRISTAL) <= 0)
        {
            Debug.LogError("Тут и сдох");
            return;
        }

        if (_resourcePlace.GetValue(ConstantNameResource.ENERGY_CRISTAL) < maxMaining)
            maxMaining = _resourcePlace.GetValue(ConstantNameResource.ENERGY_CRISTAL);

        DictionaryResource.SubAttributeDictionary(_resourcePlace, new Game.Types.KeyValuePair<string, int>(ConstantNameResource.ENERGY_CRISTAL, maxMaining));
        DictionaryResource.AddAttributeDictionary(_resourcePlayer, new Game.Types.KeyValuePair<string, int>(ConstantNameResource.ENERGY_CRISTAL, maxMaining));
    }

}
