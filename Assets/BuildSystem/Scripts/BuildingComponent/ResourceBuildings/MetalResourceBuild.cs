using Game.BuildingSystem;
using Game.Economics;
using Game.GlobalData;
using Game.GlobalSystems;
using Game.GridSystem;
using Game.Player;
using System.Collections.Generic;
using UnityEngine;

public class MetalResourceBuild : AbstractBuild
{
    private DictionaryResource _resourcePlace;
    private string _resourceTypePlace;
    [SerializeField] private int maxMaining = 50;

    


    public override bool CreateBuilding(GameObject place, PlayerManager player)
    {
        var hex = place.GetComponent<GridHex>();
        if (hex.ResourceType != ResourceType.Metal || hex.ResourceType != ResourceType.Nanostruct)
        {
            Debug.LogError("Это место не содержит ресурс 'Металл' или 'Обломки корабля'");
            return false;
        }

        switch (hex.ResourceType)
        {
            case ResourceType.Metal:
                _resourceTypePlace = ConstantNameResource.METAL;
                break;
            case ResourceType.Nanostruct:
                _resourceTypePlace = ConstantNameResource.NANO_STRUCTURE;
                break;
        }

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
        if (_resourcePlace.GetValue(_resourceTypePlace) <= 0)
            return;

        if (_resourcePlace.GetValue(_resourceTypePlace) < maxMaining)
            maxMaining = _resourcePlace.GetValue(_resourceTypePlace);

        DictionaryResource.SubAttributeDictionary(_resourcePlace, new Game.Types.KeyValuePair<string, int>(_resourceTypePlace, maxMaining));
        DictionaryResource.AddAttributeDictionary(_resourcePlayer, new Game.Types.KeyValuePair<string, int>(_resourceTypePlace, maxMaining));
    }

}