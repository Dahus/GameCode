using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Player;
using Game.Economics;
using Game.GridSystem;
using Game.UI;
using Game.GlobalData;

namespace Game.UnitsSystem
{
    public class FactoryForUnits : MonoBehaviour
    {
        [SerializeField] private List<AbstractUnit> _units = new List<AbstractUnit>();
        [SerializeField] private ResourcePlayer resourcePlayer;
        [SerializeField] private CSVReader _csvReader;
        [SerializeField] private PathfindingWithCoef _pathfinding;
        [SerializeField] private PlayerManager _player;
        private UIResource _uiResource;
        private Dictionary<AbstractUnit, bool> _openUnits = new Dictionary<AbstractUnit, bool>();
        public void SetupFactory(List<AbstractUnit> unitsRace, PlayerManager player, CSVReader csvReader, PathfindingWithCoef pathfinding)
        {
            _units = unitsRace;
            resourcePlayer = player.GetResources();
            _player = player;
            _csvReader = csvReader;
            _pathfinding = pathfinding;
            SetupDictionary();
        }

        public List<AbstractUnit> GetUnits()
        {
            return _units;
        }

        public void SetupUIResource(UIResource uiResource)
        {
            _uiResource = uiResource;
        }

        public void SetupDictionary()
        {
            _openUnits.Add(SearchUnit(ConstantNameUnit.WARROBOT), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.SNIPERDRONE), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.TANK), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.PARATROOPER), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.HOWITZER), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.COLOSS), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.STORMTROOPER), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.PHANTOM), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.RSZO), true);
            _openUnits.Add(SearchUnit(ConstantNameUnit.AUTOSCOUT), true);
                    }


        public void CreateUnit(string name, GameObject place)
        {
            GameObject _place = GetHexagonForPlacingUnit(place);

            if (_place != null)
            {
                AbstractUnit unit = SearchUnit(name);
                var obj = Instantiate(unit, _place.transform.position, Quaternion.identity, _player.transform);
                obj.Setup(_place, _player, _pathfinding);
                obj.GetComponent<AbstractUnit>().ObservationArea();
            }
        }

        public void CreateUnitChit(string name, GameObject place)
        {
            GameObject _place = GetHexagonForPlacingUnit(place);

            if (_place != null)
            {
                AbstractUnit unit = SearchUnit(name);
                var obj = Instantiate(unit, _place.transform.position, Quaternion.identity, _player.transform);
                obj.Setup(_place, _player, _pathfinding);
                //SubstractionResource(name);
          //      resourcePlayer.DebugLogger();
          //      obj.GetComponent<AbstractUnit>().ObservationArea();
            }
        }


       
        private GameObject GetHexagonForPlacingUnit(GameObject place)
        {
            //GridHex hex = Observer.instance.GetHexagon().GetComponent<GridHex>();
            GridHex hex = place.GetComponent<GridHex>();
            List<GridHex> grids = hex.NeighbourArray;

            for (int i = 0; i < grids.Count; i++)
            {
                if (Observer.instance.Check(grids[i]) && grids[i].HexInfoPathfinder.Walkable)
                {
                    return grids[i].gameObject;
                }

            }

            return null;
        }
        public AbstractUnit SearchUnit(string key)
        {
            for (int i = 0; i < _units.Count; i++)
            {

                if (_units[i].GetName() == key)
                {
                    
                    return _units[i];
                }
            }
            return null;
        }

        public string SearchName(int id)
        {
            return _units[id].GetName();
        }


        public bool CheckOpenUnit(string name)
        {
            return _openUnits[SearchUnit(name)];
        }

        public void SubstractionResource(string name)
        {
            DictionaryResource.SubstractionResourcePlayer(resourcePlayer, _csvReader.GetResourceUnit(name));
            _uiResource.UpdateResource(resourcePlayer);
        }

        public CSVReader GetCSVReader() => _csvReader;

        #region LogicTechnology
        public void OpenUnit(string name)
        {
            _openUnits[SearchUnit(name)] = true;
        }
        #endregion
    }
}
