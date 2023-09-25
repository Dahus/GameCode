using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.BuildingSystem;
using Game.UnitsSystem;
using Game.GlobalData;

namespace Game.UI
{
    public class UIButtonRobotFactory : UIButtonBuild
    {
        [SerializeField] private RobotFactory _roboticNode;

        private string tank = ConstantNameUnit.TANK;
        private string howitzer = ConstantNameUnit.HOWITZER;
        private string coloss = ConstantNameUnit.COLOSS;
        private string rszo = ConstantNameUnit.RSZO;

        private FactoryForUnits _factory;
        public override void Setup()
        {
            ClearActions();
            _factory = Player.GetFactoryUnits();
            AddActions(CreateTank);
            AddActions(CreateHowitzer);
            AddActions(CreateColoss);
            AddActions(CreateRSZO);
        }

        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            buttons[0].onClick.AddListener(CreateTank);
            texts[0].text = "Танк";
            buttons[1].onClick.AddListener(CreateHowitzer);
            texts[1].text = "Артелерия";
            buttons[2].onClick.AddListener(CreateColoss);
            texts[2].text = "Колосс";
            buttons[3].onClick.AddListener(CreateRSZO);
            texts[3].text = "РСЗО";
        }
        public void CreateTank()
        {
            var unit = _factory.SearchUnit(tank);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(),TypeObjectProduction.Unit,unit.GetUnitsData().GetIcon());
        }

        public void CreateHowitzer()
        {
            var unit = _factory.SearchUnit(howitzer);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
        }

        public void CreateColoss()
        {
            var unit = _factory.SearchUnit(coloss);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
        }
        
        public void CreateRSZO()
        {
            var unit = _factory.SearchUnit(rszo);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
        }




        private void Start()
        {
            _roboticNode = GetComponent<RobotFactory>();
        }
    }
}
