using System.Collections.Generic;
using Game.Economics;
using Game.GlobalData;
using Game.GridSystem;
using UnityEngine;
using Game.Player;
using Game.UI;
using UnityEngine.Serialization;


namespace Game.BuildingSystem
{
    public class FactoryForBuild : MonoBehaviour
    {
        [SerializeField] private List<AbstractBuild> _builds = new List<AbstractBuild>();
        [SerializeField] private List<AbstractBuild> _buildsCreate = new List<AbstractBuild>();

        private PlayerManager _player;
        private CSVReader _csvReader;

        private UIResource _uiResource;
        public void SetupFactory(List<AbstractBuild> buildRace, PlayerManager player, CSVReader csvReader)
        {
            _builds = buildRace;
            _player = player;
            _csvReader = csvReader;
        }

        public string SearchName(int id)
        {
            return _builds[id].GetName();
        }

        public List<AbstractBuild> GetBuilding()
        {
            return _builds;
        }
        public void CreateBuild(string name, GameObject place)
        {
            if (Observer.instance.Check(place.GetComponent<GridHex>()))
            {
                AbstractBuild build = SearchBuild(name);
                if (!DictionaryResource.CheckAvailabilityResource(_player.GetResources(), _csvReader.GetResourceBuild(name)))
                    return;
                var flag = build.CreateStartObject(place, _player);
                if (flag)
                    SubstractionResource(name);
                //_resourcePlayer.DebugLogger();
            }
        }

        public void CreateStartBuild(string name, GameObject place)
        {
            AbstractBuild build = SearchBuild(name);
            build.CreateBuilding(place, _player);
        }

        public void SetupUIResource(UIResource uiResource)
        {
            _uiResource = uiResource;
        }


        public AbstractBuild SearchBuild(string key)
        {
            for (int i = 0; i < _builds.Count; i++)
            {
                Debug.Log("Name" + _builds[i].GetName());
                Debug.Log("Key" + key);
                if (_builds[i].GetName() == key)
                {
                    Debug.Log("Здание Найдено");
                    return _builds[i];
                }
            }

            return null;
        }

        private void SubstractionResource(string name)
        {
            DictionaryResource.SubstractionResourcePlayer(_player.GetResources(), _csvReader.GetResourceBuild(name));
            _uiResource.UpdateResource(_player.GetResources());
        }
    }
}