using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.BuildingSystem;
using Game.UnitsSystem;
using Game.GlobalData;
using Game.GlobalSystems;
using Game.Player;

namespace Game.UI
{
    public class UIButtonRobotNode : UIButtonBuild
    {

        [SerializeField] private RoboticNode _roboticNode;
        private string paratrooper = ConstantNameUnit.PARATROOPER;

        private string currentNameUnit;

        private FactoryForUnits _factory;
        public override void Setup()
        {
            ClearActions();
            _factory = Player.GetFactoryUnits();
            AddActions(CreateSniperDrone);
            AddActions(CreatePatatrooper);
            AddActions(CreateUpgradeStrength1);
            AddActions(CreateUpgradeDamage1);
            AddActions(CreateUpgradeSpeed1);
            AddActions(CreateStormtrooper);
            AddActions(CreateStormtrooper);
            AddActions(CreatePhantom);
        }

        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            //currentNameUnit = ConstantNameUnit.SNIPERDRONE;
            foreach(var but in buttons)
            {
                but.gameObject.SetActive(false);     
            }

            buttons[0].onClick.AddListener(CreateSniperDrone);
            buttons[0].gameObject.SetActive(true);
            texts[0].text = "Снайпер дрон";

            //currentNameUnit = ConstantNameUnit.PARATROOPER;
            buttons[1].onClick.AddListener(CreatePatatrooper);
            buttons[1].gameObject.SetActive(true);
            texts[1].text = "Десантник";


            if (!Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.STRENGTHROBOTS1))
            {
                buttons[2].onClick.AddListener(CreateUpgradeStrength1);
                buttons[2].gameObject.SetActive(true);
            }


            if (!Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.DAMAGEROBOTS1))
            {
                buttons[3].onClick.AddListener(CreateUpgradeDamage1);
                buttons[3].gameObject.SetActive(true);
            }


            if (!Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.SPEEDROBOTS1))
            {
                buttons[4].onClick.AddListener(CreateUpgradeSpeed1);
                buttons[4].gameObject.SetActive(true);
            }

            if (Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.STRENGTHROBOTS1) && !Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.STRENGTHROBOTS2) && Observer.instance.GetGameObserver().CheckUnitTypeTwo(Player))
            {
                buttons[2].onClick.AddListener(CreateUpgradeStrength2);
                buttons[2].gameObject.SetActive(true);
            }
            if (Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.DAMAGEROBOTS1) && !Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.DAMAGEROBOTS2) && Observer.instance.GetGameObserver().CheckUnitTypeTwo(Player))
            {
                buttons[3].onClick.AddListener(CreateUpgradeDamage2);
                buttons[3].gameObject.SetActive(true);
            }

            if (Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.SPEEDROBOTS1) && !Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.SPEEDROBOTS2) && Observer.instance.GetGameObserver().CheckUnitTypeTwo(Player))
            {
                buttons[4].onClick.AddListener(CreateUpgradeSpeed2);
                buttons[4].gameObject.SetActive(true);
            }

            buttons[6].onClick.AddListener(CreatePhantom);
            buttons[6].gameObject.SetActive(true);
            texts[6].text = "Фантом";

            buttons[7].onClick.AddListener(CreateStormtrooper);
            buttons[7].gameObject.SetActive(true);
            texts[7].text = "Штурмовик";



        }
        public void CreateSniperDrone()
        {
            var unit = _factory.SearchUnit(ConstantNameUnit.SNIPERDRONE);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
        }

        public void CreatePatatrooper()
        {
            var unit = _factory.SearchUnit(ConstantNameUnit.PARATROOPER);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
        }

        public void CreateStormtrooper()
        {
            var unit = _factory.SearchUnit(ConstantNameUnit.STORMTROOPER);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
        }

        public void CreatePhantom()
        {
            var unit = _factory.SearchUnit(ConstantNameUnit.PHANTOM);
            _roboticNode.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
        }


        public void CreateUpgradeStrength1()
        {
            var upgrade = EntityLocator.instance.GetStorageTechnologyBuild().GetTechnologyBuild(ConstantNameTechnologyBuild.STRENGTHROBOTS1);
            _roboticNode.Create(upgrade.GetName(), upgrade.GetCountMoves(), TypeObjectProduction.Upgrade, upgrade.GetIcon());
        }

        public void CreateUpgradeStrength2()
        {
            var upgrade = EntityLocator.instance.GetStorageTechnologyBuild().GetTechnologyBuild(ConstantNameTechnologyBuild.STRENGTHROBOTS2);
            _roboticNode.Create(upgrade.GetName(), upgrade.GetCountMoves(), TypeObjectProduction.Upgrade, upgrade.GetIcon());
        }

        public void CreateUpgradeDamage1()
        {
            var upgrade = EntityLocator.instance.GetStorageTechnologyBuild().GetTechnologyBuild(ConstantNameTechnologyBuild.DAMAGEROBOTS1);
            _roboticNode.Create(upgrade.GetName(), upgrade.GetCountMoves(), TypeObjectProduction.Upgrade, upgrade.GetIcon());
        }
        public void CreateUpgradeDamage2()
        {
            var upgrade = EntityLocator.instance.GetStorageTechnologyBuild().GetTechnologyBuild(ConstantNameTechnologyBuild.DAMAGEROBOTS2);
            _roboticNode.Create(upgrade.GetName(), upgrade.GetCountMoves(), TypeObjectProduction.Upgrade, upgrade.GetIcon());
        }



        public void CreateUpgradeSpeed1()
        {
            var upgrade = EntityLocator.instance.GetStorageTechnologyBuild().GetTechnologyBuild(ConstantNameTechnologyBuild.SPEEDROBOTS1);
            _roboticNode.Create(upgrade.GetName(), upgrade.GetCountMoves(), TypeObjectProduction.Upgrade, upgrade.GetIcon());
        }

        public void CreateUpgradeSpeed2()
        {
            var upgrade = EntityLocator.instance.GetStorageTechnologyBuild().GetTechnologyBuild(ConstantNameTechnologyBuild.SPEEDROBOTS2);
            _roboticNode.Create(upgrade.GetName(), upgrade.GetCountMoves(), TypeObjectProduction.Upgrade, upgrade.GetIcon());
        }



        private void Start()
        {
            _roboticNode = GetComponent<RoboticNode>();
        }
    }
}
