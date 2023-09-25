using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GlobalSystems;
using Game.Grid;
using Game.BuildingSystem;
using Game.UI;
using Game.UnitsSystem;
using Game.TechnologyBuild;
using Game.GridSystem;

namespace Game.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private string _playerNickname;
        //private EnumHexVisibleStatus[,] MapCellsVisibleStatus = new EnumHexVisibleStatus[0, 0];
        [SerializeField] private List<GameObject> gameObjectsPlayer = new List<GameObject>();
        [SerializeField] private List<GameObject> listRemoveGamObjects = new List<GameObject>();
        [SerializeField] private List<GameObject> listAddGameObjects = new List<GameObject>();

        [SerializeField] private Sprite _icon;
        private Color _colorPlayer;
        private ResourcePlayer _resource;

        private FactoryForBuild _factoryBuild;

        private FactoryForUnits _factoryUnits;

        private UIResource _uiresource;

        private PlayerTechnologies _playerTechnologies = new PlayerTechnologies();

        [SerializeField] private ObserveMap _observeMap = new ObserveMap();

        [SerializeField] private PlayerTechnologyBuild _playerTechnologiesBuild = new PlayerTechnologyBuild();

        [SerializeField] private PlayerManager _nextPlayer;

        public List<string> buyTechnologies = new List<string>();
        public List<string> learnTechnologies = new List<string>();
        public string currentTechnology;

        public Sprite GetIcon() => _icon;
        public void SetIcon(Sprite icon) => _icon = icon;
        [SerializeField] private DrawHealthBar _drawHealthBar;

        private bool canEndStep = false;
        public bool CanEndStep { get => canEndStep; set => canEndStep = value; }



        [SerializeField] private int count = 0;

        /*  public void SetMapSetting(Vector2Int GridSize, Vector2Int PlayerPosition, GridManager gridManager,
        int observeDistance)
    {
        MapCellsVisibleStatus = new EnumHexVisibleStatus[GridSize.x, GridSize.y];
        for (int i = 0; i < MapCellsVisibleStatus.GetLength(0); i++)
        for (int y = 0; y < MapCellsVisibleStatus.GetLength(1); y++)
            MapCellsVisibleStatus[i, y] = EnumHexVisibleStatus.FogWar;
        ObservedHexRecursedFunction(PlayerPosition, gridManager, observeDistance);
    }

    public void ObservedHexRecursedFunction(Vector2Int HexPosition, GridManager gridManager, int observeDistance)
    {
        if (observeDistance == 0) return;
        List<Vector2Int> playerNeighborsHexes = new List<Vector2Int>();
        playerNeighborsHexes.AddRange(gridManager.GetNeighbors(HexPosition));
        ObservedHexRecursedFunction(HexPosition, gridManager, observeDistance - 1);
    }
    */
        public void SetupPlayer(string playerNickname, Color color)
        {
            _playerNickname = playerNickname;
            _colorPlayer = color;
            _resource = new ResourcePlayer();
            _resource.SetupDictionary();
            _factoryBuild = GetComponent<FactoryForBuild>();
            _factoryUnits = GetComponent<FactoryForUnits>();
            _playerTechnologies.SetupTechnologies();
            _observeMap = new ObserveMap();
            _observeMap.Setup(EntityLocator.instance.GetParametersGame().GetGridSize());
        }

        public string GetPlayerNickname()
        {
            return _playerNickname;
        }

        public void SetupUIPlayer(UIResource ui)
        {
            _uiresource = ui;
        }

        public void SetupDrawHealthBar(DrawHealthBar healthBar)
        {
            _drawHealthBar = healthBar;
        }

        public FactoryForBuild GetFactoryBuild()
        {
            return _factoryBuild;
        }

        public FactoryForUnits GetFactoryUnits()
        {
            return _factoryUnits;
        }

        public PlayerManager GetNextPlayer()
        {
            return _nextPlayer;
        }

        public void SetNextPlayer(PlayerManager player)
        {
            _nextPlayer = player;
        }

        //public void SetupFactory(List<Building> buildsRace,CSVReader csvReader)
        //{
        //    _factoryBuild.SetupFactory(buildsRace, _resource, _colorPlayer,csvReader);
        //}

        //public void SetupFactory(List<AbstractUnit> buildUnits ,CSVReader csvReader)
        //{
        //    _factoryUnits.SetupFactory(buildUnits,_resource, _colorPlayer,csvReader);
        //}


        public void UpdateResource()
        {
            _uiresource.UpdateResource(_resource);
        }

        public void AddObjectPlayer(GameObject playerObject)
        {
            gameObjectsPlayer.Add(playerObject);
        }

        public void RemoveObjectPlayer(GameObject playerObject)
        {
            gameObjectsPlayer.Remove(playerObject);
        }

        /* Этот метод проходит по всем зависимостям (стройки/улучшения/исследования/добывающие здания/производящие здания) 
            и всем сообщает, что начался новый ход */
        public string NewStep()
        {
            foreach (var gameObj in gameObjectsPlayer)
            {
                gameObj.GetComponent<IStepPlayer>().BeginPlayerTurn();
            }

            for (int i = 0; i < listRemoveGamObjects.Count; i++)
            {
                gameObjectsPlayer.Remove(listRemoveGamObjects[i]);
                Destroy(listRemoveGamObjects[i]);
            }
            for (int i = 0; i < listAddGameObjects.Count; i++)
            {
                gameObjectsPlayer.Add(listAddGameObjects[i]);
            }
            listAddGameObjects.Clear();
            listRemoveGamObjects.Clear();
            if (_playerTechnologies.CheckCurrentTechnology())
            {
                Debug.Log(_playerTechnologies);
                _playerTechnologies.Learn(this);
            }
            /* Этот метод проходит по всем зависимостям (стройки/улучшения/исследования/добывающие здания/производящие здания) 
            и всем сообщает, что начался новый ход */
            return _playerNickname; // Потом удалить возврат нужно
        }


        public void EndStep()
        {
            //Debug.LogError(_playerNickname);
            count = 0;
            StartCoroutine(Test1());

        }

        /*  private void FixedUpdate()
          {
              if (EntityLocator.instance.GetStepManager().GetCurrentPlayer() != this)
                  return;
              if (canEndStep)
                  return;

              canEndStep = false;
              EntityLocator.instance.GetStepManager().NextPlayer();
          }*/


        public bool CheckEndStepAllObject()
        {
            var units = GetPlayerUnits();
            for (int i = 0; i < units.Count; i++)
            {
                if (!units[i].GetComponent<IStepPlayer>().CanEndStep)
                {
                    Debug.LogError("Ну я пиздец");
                    return false;
                }
            }
            return true;
        }
        public IEnumerator Test1()
        {

            if(count< gameObjectsPlayer.Count)
            {
                yield return StartCoroutine(gameObjectsPlayer[count].GetComponent<IStepPlayer>().CoroutineEndStep());
                // Debug.LogError(" Выполнил коротюну" + gameObjectsPlayer[count].name);
                count++;
                StartCoroutine(Test1());
            }
            else
            {
                EntityLocator.instance.GetStepManager().IsCoroutineTrue();
            }
            

            /* if (count >= gameObjectsPlayer.Count)
                 yield return null;
             else
             {
                // yield return StartCoroutine(_game)
             }*/


        }

        public void AddObjectInListRemoveObject(GameObject obj)
        {
            listRemoveGamObjects.Add(obj);
        }

        public void AddObjectInListAddObject(GameObject obj)
        {
            listAddGameObjects.Add(obj);
        }

        public Color GetColor()
        {
            return _colorPlayer;
        }

        public ResourcePlayer GetResources()
        {
            return _resource;
        }

        public PlayerTechnologies GetPlayerTechnologies()
        {
            return _playerTechnologies;
        }

        public PlayerTechnologyBuild GetPlayerTechnologyBuild() => _playerTechnologiesBuild;

        public List<AbstractUnit> GetPlayerUnits()
        {
            var playerUnits = new List<AbstractUnit>();
            foreach (var obj in gameObjectsPlayer)
            {
                if (obj.TryGetComponent<AbstractUnit>(out var unit))
                    playerUnits.Add(unit);
            }
            return playerUnits;
        }

        public List<AbstractBuild> GetPlayerBuilds()
        {
            var playerBuilds = new List<AbstractBuild>();
            foreach (var obj in gameObjectsPlayer)
            {
                if (obj.TryGetComponent<AbstractBuild>(out var build))
                {
                    playerBuilds.Add(build);
                }
                if (obj.TryGetComponent<CreateStartObject>(out var startBuild))
                {
                    playerBuilds.Add(startBuild.GetBuild());
                }
            }
            return playerBuilds;
        }

        public List<AbstractBuild> GetPlayerBuildsWithoutStartBuilds()
        {
            var playerBuilds = new List<AbstractBuild>();
            foreach (var obj in gameObjectsPlayer)
            {
                if (obj.TryGetComponent<AbstractBuild>(out var build))
                {
                    playerBuilds.Add(build);
                }
            }
            return playerBuilds;
        }

        public DrawHealthBar GetDrawHealthBar()
        {
            return _drawHealthBar;
        }

        public ObserveMap GetObserverMap() => _observeMap;
    }
}