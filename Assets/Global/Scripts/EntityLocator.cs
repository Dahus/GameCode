using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Events;
using Game.GlobalSystems;
using Game.Grid;
using Game.GridSystem;
using Game.UI;
using Game.Player;
using Game.UpgradeSystem;
using Game.TechnologyBuild;
using Game.Generators;

namespace Game.GlobalSystems
{
    public class EntityLocator : MonoBehaviour
    {
        public static EntityLocator instance;

        [SerializeField] private ButtonLogicManager _buttonLogicManager;
        [SerializeField] private StepManager _stepManager;
        [SerializeField] private CSVReader _csvReader;
        [SerializeField] private GridBuilder _gridBuilder;
        [SerializeField] private UIResource _uiResource;
        [SerializeField] private StorageGenerators storageGenerators;
        [SerializeField] private PathfindingWithCoef _pathfinding;
        [SerializeField] private WavedAlgorithm _wavedAlgorithm;
        [SerializeField] public List<PlayerManager> Players { get; set; }
        [SerializeField] private CSVReaderTechnology _csvReaderTechnology;
        [SerializeField] private CostUpgradeLocator _costUpgradeLocator;
        [SerializeField] private StorageTechnologyBuild _storageTechnologyBuild;
        [SerializeField] private CSVReaderUpgradeResource _CSVReaderUpgradeResource;

        [SerializeField] private DrawHealthBar _drawHealthBar;
        [SerializeField] private UIParametresLogic _uIParametresLogic;
        [SerializeField] private ParametersGame _parametresGame;
        [SerializeField] private PanelErrorInfo panelErrorInfo;
        [SerializeField] private UIInfoManager uIInfoManager;
        [SerializeField] private PanelInfoTechnologies panelInfoTechnologies;
        [SerializeField] private StorageIcons storageIcons;




        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Players = new List<PlayerManager>();
            }
        }

        #region Get
        public DrawHealthBar GetDrawHealthBar() => _drawHealthBar;
        public CSVReader GetCSVReader() => _csvReader;
        public ButtonLogicManager GetButtonLogic() => _buttonLogicManager;
        public StepManager GetStepManager() => _stepManager;
        public GridBuilder GetGridBuilder() => _gridBuilder;
        public UIResource GetUIResource() => _uiResource;
        public PathfindingWithCoef GetPathfinding() => _pathfinding;
        public WavedAlgorithm GetWavedAlgorithm() => _wavedAlgorithm;
        public CSVReaderTechnology GetCSVReaderTechnology() => _csvReaderTechnology;
        public UIParametresLogic GetUIParametersLogic() => _uIParametresLogic;
        public CostUpgradeLocator GetCostUpgradeLocator() => _costUpgradeLocator;
        public StorageTechnologyBuild GetStorageTechnologyBuild() => _storageTechnologyBuild;

        public CSVReaderUpgradeResource GetCSVReaderUpgradeResource() => _CSVReaderUpgradeResource;

        public StorageGenerators GetStorageGenerators() => storageGenerators;
        public ParametersGame GetParametersGame() => _parametresGame;

        public PanelErrorInfo GetPanelErrorInfo() => panelErrorInfo;

        public UIInfoManager GetUIInfoManager() => uIInfoManager;

        public PanelInfoTechnologies GetPanelInfoTechnologies() => panelInfoTechnologies;

        public StorageIcons GetStorageIcons() => storageIcons;


        #endregion

    }
}