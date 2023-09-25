using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.BuildingSystem;
using Game.UnitsSystem;
using Game.Player;

using Game.GlobalData;
using Game.GlobalSystems;
namespace Game.UI
{

    public class UIButtonCC : UIButtonBuild
    {

        [SerializeField] private CommandCenter _commandCenter;
        private string _strUnit = ConstantNameUnit.WARROBOT;
        private FactoryForUnits _factory;


        public override void Setup()
        {
            ClearActions();
            _factory = Player.GetFactoryUnits();
            AddActions(CreateUnit);
            AddActions(Attack);
            AddActions(CreateAutoScout);

        }
        public override void UpdateButton(List<Button> buttons,List<Text> texts)
        {
            buttons[0].onClick.AddListener(CreateUnit);
            buttons[1].onClick.AddListener(Attack);
            buttons[2].onClick.AddListener(CreateAutoScout);

        }

        public void CreateUnit()
        {
            var unit = _factory.SearchUnit(_strUnit);
            _commandCenter.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
            EntityLocator.instance.GetButtonLogic().UpdateQueueLogicManager();
        }

        public void Attack() 
        {
            
        }

        public void CreateAutoScout ()
        {
            var unit = _factory.SearchUnit(ConstantNameUnit.AUTOSCOUT);
            _commandCenter.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit);
            EntityLocator.instance.GetButtonLogic().UpdateQueueLogicManager();
        }

        private void Start()
        {
            _commandCenter = GetComponent<CommandCenter>();
        }

        public override void SetupChooseButtons(int number)
        {
            switch (number)
            {
                case 1:
                    OpenPanelOne();
                    break;
                case 2:
                    OpenPanelTwo();
                    break;
            }
        }


        public void OpenPanelOne()
        {
            var res = EntityLocator.instance.GetCSVReader().GetResourceUnit(_strUnit);
            var step = _factory.SearchUnit(_strUnit).GetUnitsData().GetTimeCreateUnit();
            EntityLocator.instance.GetUIInfoManager().UpdatePanerResourceAndStep("Робот", res, step);
        }

        public void OpenPanelTwo()
        {
            EntityLocator.instance.GetUIInfoManager().UpdatePanelStep("Атаковать", 2);
        }
        





    }
}
