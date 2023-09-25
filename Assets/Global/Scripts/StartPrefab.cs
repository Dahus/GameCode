using System.Collections;
using System.Collections.Generic;
using Game.BuildingSystem;
using Game.Player;
using Game.UnitsSystem;
using UnityEngine;

public class StartPrefab : MonoBehaviour
{

    [SerializeField] private PlayerManager _playerPrefab;
    [SerializeField] private FactoryForBuild _prefabFactoryBuildRace;
    [SerializeField] private FactoryForUnits _prefabFactoryUnitsRace;


    public PlayerManager GetPlayerPrefab()
    {
        return _playerPrefab;
    }

    public FactoryForBuild GetFactoryBuildPrefab()
    {
        return _prefabFactoryBuildRace;
    }

    public FactoryForUnits GetFactoryUnitPrefab()
    {
        return _prefabFactoryUnitsRace;
    }
    
}
