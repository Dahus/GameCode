using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.GridSystem;
using Game.Player;
using Game.UnitsSystem;

namespace Game.BuildingSystem
{
    public class TradingTerminal : AbstractBuild ,ICreateBuildingFunction
    {


        [SerializeField] private FactoryForUnits _factoryUnits;
        public int Number = 0;
        public bool _flag;
        public UnitInProduction[] _units = new UnitInProduction[6];

        public void InternationalTrade()
        {
            Debug.LogError("Начнем торговать");
        }

        public void Create(string name, int step, TypeObjectProduction typeObjectProduction, Sprite icon)
        {
            if (Number < 6)
            {
                //var unit = new UnitInProduction(name, step);
                _units[Number].Step = step;
                _units[Number].Name = name;
                Number++;
                //_unitsString.Add(unit.GetName());
                Debug.LogError(_units[0].Name + " ");
            }
        }

        public void Setup(FactoryForUnits factory)
        {
            _factoryUnits = factory;
            _units = new UnitInProduction[6];
            Number = 0;
        }

        public override bool CreateBuilding(GameObject place, PlayerManager player)
        {
            Setup(player.GetFactoryUnits());
            return base.CreateBuilding(place, player);

        }

        public bool CheckHex()
        {
            GridHex hex = _place.GetComponent<GridHex>();
            List<GridHex> grids = hex.NeighbourArray;
            for (int i = 0; i < grids.Count; i++)
            {
                if (Observer.instance.Check(grids[i]) && grids[i].HexInfoPathfinder.Walkable)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
