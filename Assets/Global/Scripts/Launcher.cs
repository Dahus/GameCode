using System.Collections.Generic;
using Game.BuildingSystem;
using UnityEngine;
using Game.Grid;
using Game.GridSystem;
using Game.Player;
using UnityEngine.Serialization;
using Game.UnitsSystem;
using Game.Events;
using Game.GlobalData;

namespace Game.GlobalSystems
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private EntityLocator _entityLocator;
        [SerializeField] private StartPrefab _startPrefab;
        [SerializeField] private ParametersGame _parametersGame;

        public void Start()
        {
            Vector2Int gridSize = _parametersGame.GetGridSize();
            _entityLocator.GetGridBuilder().SetGridSize(gridSize);
            _entityLocator.GetGridBuilder().CreateGrid(_entityLocator.GetButtonLogic(), _entityLocator.GetUIParametersLogic());

            _entityLocator.GetCSVReaderTechnology().SetupTechnologyTrees();

            var chitPlayer = Instantiate(_startPrefab.GetPlayerPrefab());
            chitPlayer.gameObject.AddComponent<FactoryForBuild>();
            chitPlayer.gameObject.AddComponent<FactoryForUnits>();
            chitPlayer.GetComponent<FactoryForBuild>().SetupFactory(_startPrefab.GetFactoryBuildPrefab().GetBuilding(), chitPlayer, _entityLocator.GetCSVReader());
            chitPlayer.GetComponent<FactoryForBuild>().SetupUIResource(_entityLocator.GetUIResource());
            chitPlayer.GetComponent<FactoryForUnits>().SetupFactory(_startPrefab.GetFactoryUnitPrefab().GetUnits(), chitPlayer, _entityLocator.GetCSVReader(), _entityLocator.GetPathfinding());
            chitPlayer.GetComponent<FactoryForUnits>().SetupUIResource(_entityLocator.GetUIResource());
            chitPlayer.SetupDrawHealthBar(_entityLocator.GetDrawHealthBar());
            Observer.instance.GetChitObserver().SetFactoryBuilds(chitPlayer.GetComponent<FactoryForBuild>());
            Observer.instance.GetChitObserver().SetFactoryUnits(chitPlayer.GetComponent<FactoryForUnits>());
            chitPlayer.SetupPlayer("Chit", Color.yellow);
            Economics.DictionaryResource.ADDRESOURCECHIT(chitPlayer.GetResources());

            var firstPlayer = Instantiate(_startPrefab.GetPlayerPrefab());
            /*  firstPlayer.name = _parametersGame.GetNamePlayer(0);

              //player.SetMapSetting(gridSize, new Vector2Int(gridSize.x / 2, gridSize.y / 2), _gridManager, 2);

              firstPlayer.gameObject.AddComponent<FactoryForBuild>();
              firstPlayer.gameObject.AddComponent<FactoryForUnits>();

              firstPlayer.SetupDrawHealthBar(_entityLocator.GetDrawHealthBar());
              firstPlayer.SetupPlayer(_parametersGame.GetNamePlayer(0), _parametersGame.GetColorPlayer(0));
              firstPlayer.SetupUIPlayer(_entityLocator.GetUIResource());
              firstPlayer.SetIcon(_parametersGame.GetIcon(0));
              firstPlayer.GetComponent<FactoryForBuild>().SetupFactory(_startPrefab.GetFactoryBuildPrefab().GetBuilding(), firstPlayer, _entityLocator.GetCSVReader());
              firstPlayer.GetComponent<FactoryForBuild>().SetupUIResource(_entityLocator.GetUIResource());
              firstPlayer.GetComponent<FactoryForUnits>().SetupFactory(_startPrefab.GetFactoryUnitPrefab().GetUnits(), firstPlayer, _entityLocator.GetCSVReader(), _entityLocator.GetPathfinding());
              firstPlayer.GetComponent<FactoryForUnits>().SetupUIResource(_entityLocator.GetUIResource());

              _entityLocator.GetStepManager().AddInQueue(firstPlayer);
              _entityLocator.Players.Add(firstPlayer);
              */

            firstPlayer = CreatePlayer(firstPlayer, false, 0);
            _entityLocator.GetStepManager().AddInQueue(firstPlayer);
            _entityLocator.Players.Add(firstPlayer);

            for (int i = 1; i < _parametersGame.GetCountPlayer(); i++)
            {
                var player = Instantiate(_startPrefab.GetPlayerPrefab());
                player = CreatePlayer(player, true, i);
                _entityLocator.GetStepManager().AddInQueue(player);
                _entityLocator.Players.Add(player);
                //player.name = _parametersGame.GetNamePlayer(i);
                //player.SetIcon(_parametersGame.GetIcon(i));
                ////player.SetMapSetting(gridSize, new Vector2Int(gridSize.x / 2, gridSize.y / 2), _gridManager, 2);
                //player.SetupDrawHealthBar(_entityLocator.GetDrawHealthBar());
                //player.gameObject.AddComponent<FactoryForBuild>();
                //player.gameObject.AddComponent<FactoryForUnits>();
                //player.gameObject.AddComponent<PlayerAI>();

                //player.SetupPlayer(_parametersGame.GetNamePlayer(i), _parametersGame.GetColorPlayer(i));
                //player.SetupUIPlayer(_entityLocator.GetUIResource());
                //player.GetComponent<FactoryForBuild>().SetupFactory(_startPrefab.GetFactoryBuildPrefab().GetBuilding(), player, _entityLocator.GetCSVReader());
                //player.GetComponent<FactoryForBuild>().SetupUIResource(_entityLocator.GetUIResource());
                //player.GetComponent<FactoryForUnits>().SetupFactory(_startPrefab.GetFactoryUnitPrefab().GetUnits(), player, _entityLocator.GetCSVReader(), _entityLocator.GetPathfinding());
                //player.GetComponent<FactoryForUnits>().SetupUIResource(_entityLocator.GetUIResource());

                //player.GetComponent<PlayerAI>().SetupBot(player, player.name, _entityLocator.GetStepManager(), new Coefficients(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)));

                //_entityLocator.GetStepManager().AddInQueue(player);
                //_entityLocator.Players.Add(player);
            }

            var listPlayers = _entityLocator.GetStepManager().GetPlayers();

            for (int i = 0; i < listPlayers.Count; i++)
            {
                if (i + 1 == listPlayers.Count)
                {
                    listPlayers[i].SetNextPlayer(listPlayers[0]);
                    break;
                }
                listPlayers[i].SetNextPlayer(listPlayers[i + 1]);
            }

            CreateStartCCForPlayers();

            firstPlayer.SetNextPlayer(_entityLocator.GetStepManager().GetPlayers()[1]);

            _entityLocator.GetStepManager().SetupStartPlayer();
            // _entityLocator.GetGeneratorResource().CreateGridResource();

            // _entityLocator.GetGeneratorBiome().CreateRelief();
            _entityLocator.GetStorageTechnologyBuild().SetupAllTechnologies();
            _entityLocator.GetStorageGenerators().StartGenerate();

            //GridHex hex = GameGrid.Grid();

            //_entityLocator.GetWavedAlgorithm().SearchAllHexesForDamage(GameGrid.Grid[5,5], 4);
            //_entityLocator.GetWavedAlgorithm().SearchAllHexesForAction(GameGrid.Grid[5, 5], 4);

        }

        private PlayerManager CreatePlayer(PlayerManager player, bool IsBot, int id)
        {
            player.name = _parametersGame.GetNamePlayer(id);
            player.SetIcon(_parametersGame.GetIcon(id));
            //player.SetMapSetting(gridSize, new Vector2Int(gridSize.x / 2, gridSize.y / 2), _gridManager, 2);
            player.SetupDrawHealthBar(_entityLocator.GetDrawHealthBar());
            player.gameObject.AddComponent<FactoryForBuild>();
            player.gameObject.AddComponent<FactoryForUnits>();

            player.SetupPlayer(_parametersGame.GetNamePlayer(id), _parametersGame.GetColorPlayer(id));
            player.SetupUIPlayer(_entityLocator.GetUIResource());
            player.GetComponent<FactoryForBuild>().SetupFactory(_startPrefab.GetFactoryBuildPrefab().GetBuilding(), player, _entityLocator.GetCSVReader());
            player.GetComponent<FactoryForBuild>().SetupUIResource(_entityLocator.GetUIResource());
            player.GetComponent<FactoryForUnits>().SetupFactory(_startPrefab.GetFactoryUnitPrefab().GetUnits(), player, _entityLocator.GetCSVReader(), _entityLocator.GetPathfinding());
            player.GetComponent<FactoryForUnits>().SetupUIResource(_entityLocator.GetUIResource());
            if (IsBot)
            {
                player.gameObject.AddComponent<PlayerAI>();
                player.GetComponent<PlayerAI>().SetupBot(player, player.name, _entityLocator.GetStepManager(), new Coefficients(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)));
            }
            return player;
        }

      

        private void CreateStartCCForPlayers()
        {

            foreach (var player in _entityLocator.Players)
            {
                player.GetFactoryBuild().CreateStartBuild(ConstantNameBuild.CC, GameGrid.GetRandomHexInGrid().gameObject);
            }
        }
    }
}