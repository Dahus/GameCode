using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.GridSystem;
using Game.BuildingSystem;
using Game.Player;
using Game.GlobalData;
using Game.GlobalSystems;

namespace Game.UI
{
    public class UIButtonHex : UIButton
    {
        [SerializeField] private FactoryForBuild _factory;
        private string _cc = ConstantNameBuild.CC;
        private string _robotNode = ConstantNameBuild.ROBOTNODE;
        private string _fort = ConstantNameBuild.FORT;


        public override void Setup()
        {
            ClearActions();
            _factory = Player.GetFactoryBuild();
            for (int i = 0; i < _factory.GetBuilding().Count; i++)
            {
                AddActions(CreateBuild);
            }
        }
        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            //buttons[0].onClick.AddListener(CreateCC);
            buttons[0].onClick.AddListener(CreateRobotNode);
            texts[0].text = "Узел роботов";
            buttons[1].onClick.AddListener(CreateFort);
            texts[1].text = "Форт";
            buttons[2].onClick.AddListener(CreateRobotFactory);
            texts[2].text = "Завод роботов";
            buttons[3].onClick.AddListener(CreateRobotFerm);
            texts[3].text = "Ферма";

        }

        public void CreateBuild() => _factory.CreateBuild(_factory.SearchName(CurrentId), Observer.instance.GetHexagon());

        public void CreateCC() => _factory.CreateBuild(_cc, Observer.instance.GetHexagon());
        public void CreateRobotNode() => _factory.CreateBuild(_robotNode, Observer.instance.GetHexagon());
        public void CreateFort() => _factory.CreateBuild(_fort, Observer.instance.GetHexagon());
        public void CreateRobotFactory() => _factory.CreateBuild(ConstantNameBuild.ROBOTFACTORY, Observer.instance.GetHexagon());

        public void CreateRobotFerm() => _factory.CreateBuild(ConstantNameBuild.ROBOTFERM, Observer.instance.GetHexagon());


        public void OpenPanelOne()
        {
            var res = EntityLocator.instance.GetCSVReader().GetResourceBuild(_robotNode);
            var step = _factory.SearchBuild(_robotNode).GetBuildData().GetTimeBuilding();
            EntityLocator.instance.GetUIInfoManager().UpdatePanerResourceAndStep("Узел роботов", res, step);
        }

        public void OpenPanelTwo()
        {
            var res = EntityLocator.instance.GetCSVReader().GetResourceBuild(_fort);
            var step = _factory.SearchBuild(_fort).GetBuildData().GetTimeBuilding();
            EntityLocator.instance.GetUIInfoManager().UpdatePanerResourceAndStep("форт", res, step);
        }

        public void OpenPanelThree()
        {
            var res = EntityLocator.instance.GetCSVReader().GetResourceBuild(ConstantNameBuild.ROBOTFACTORY);
            var step = _factory.SearchBuild(ConstantNameBuild.ROBOTFACTORY).GetBuildData().GetTimeBuilding();
            EntityLocator.instance.GetUIInfoManager().UpdatePanerResourceAndStep("Завод роботов", res, step);
        }

        public void OpenPanelFour()
        {
            var res = EntityLocator.instance.GetCSVReader().GetResourceBuild(ConstantNameBuild.ROBOTFERM);
            var step = _factory.SearchBuild(ConstantNameBuild.ROBOTFERM).GetBuildData().GetTimeBuilding();
            EntityLocator.instance.GetUIInfoManager().UpdatePanerResourceAndStep("Ферма", res, step);
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
                case 3:
                    OpenPanelThree();
                    break;
                case 4:
                    OpenPanelFour();
                    break;
            }
        }
    }
}
