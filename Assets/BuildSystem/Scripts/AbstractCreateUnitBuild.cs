using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Game.UnitsSystem;
using Game.GridSystem;
using Game.Player;
using Game.Economics;
using Game.GlobalSystems;
using Game.GlobalData;
namespace Game.BuildingSystem
{
    public class AbstractCreateUnitBuild : AbstractBuild, ICreateBuildingFunction
    {
        [SerializeField] private FactoryForUnits _factoryUnits;
        [SerializeField] private bool _flag;
        [SerializeField] private UnitInProduction[] _units = new UnitInProduction[6];

        [SerializeField] private int _number = 6;


        public void SetupFactoryUnits(FactoryForUnits factory)
        {
            _factoryUnits = factory;
            _units = new UnitInProduction[6];
            _number = 0;
        }

        public Sprite GetUnitIcon(int id) => _units[id].spriteIcon;

        public void CreatingUnit()
        {
            if (!_isBroken)
            {

                if (_units[0].typeObjProduction == TypeObjectProduction.Unit)
                {
                    _flag = CheckHex();
                    if (_units[0].Step != 0)
                    {
                        _units[0].BeginPlayerTurn();
                        EntityLocator.instance.GetButtonLogic().UpdateTextCountStep(_units[0].Step);
                    }
                    if (_units[0].Step == 0 && _flag)
                    {
                        _factoryUnits.CreateUnit(_units[0].Name, _place);
                        NextUnit();
                    }
                }
                if (_units[0].typeObjProduction == TypeObjectProduction.Upgrade)
                {
                    if (_units[0].Step != 0)
                    {
                        _units[0].BeginPlayerTurn();
                    }
                    if (_units[0].Step == 0)
                    {
                        Player.GetPlayerTechnologyBuild().AddTechnology(EntityLocator.instance.GetStorageTechnologyBuild().GetTechnologyBuild(_units[0].Name));
                        NextUnit();
                    }
                }
            }
        }

        public void Create(string name, int step, TypeObjectProduction type, Sprite icon)
        {
            switch (type)
            {
                case TypeObjectProduction.Unit:

                    if (_factoryUnits.CheckOpenUnit(name))
                    {
                        if (DictionaryResource.CheckAvailabilityResource(_resourcePlayer, _factoryUnits.GetCSVReader().GetResourceUnit(name)) && (_number < 6))
                        {
                            _units[_number].Step = step;
                            _units[_number].MaxStep = step;
                            _units[_number].Name = name;
                            _units[_number].typeObjProduction = type;
                            _units[_number].spriteIcon = icon;
                            if (_number == 0)
                            {
                                EntityLocator.instance.GetButtonLogic().UpdateTextCountStep(_units[0].Step);
                            }
                            _number++;
                            _factoryUnits.SubstractionResource(name);

                        }
                    }
                    break;

                case TypeObjectProduction.Upgrade:
                    if (CheckUpgrade(name))
                    {
                        //  if(Player)
                        if (DictionaryResource.CheckAvailabilityResource(_resourcePlayer, EntityLocator.instance.GetStorageTechnologyBuild().GetResource(name)) && (_number < 6))
                        {
                            _units[_number].Step = step;
                            _units[_number].MaxStep = step;
                            _units[_number].Name = name;
                            _units[_number].typeObjProduction = type;
                            _units[_number].spriteIcon = icon;
                            if (_number == 0)
                            {
                                EntityLocator.instance.GetButtonLogic().UpdateTextCountStep(_units[0].Step);
                            }
                            _number++;
                            DictionaryResource.SubstractionResourcePlayer(_resourcePlayer, EntityLocator.instance.GetStorageTechnologyBuild().GetResource(name));
                            Player.UpdateResource();
                        }
                    }
                    break;
            }
            Observer.instance.GetObserverUpdateUI().UpdateQueueLogicUI();
        }


        public void DeleteObject(int id)
        {
            CancelObject(id);
            for (int i = id; i < 5; i++)
            {
                _units[i].Step = _units[i + 1].Step;
                _units[i].MaxStep = _units[i + 1].MaxStep;
                _units[i].Name = _units[i + 1].Name;
                _units[i].typeObjProduction = _units[i + 1].typeObjProduction;
                _units[i + 1].Step = -1;
                _units[i + 1].MaxStep = -1;
                _units[i + 1].Name = "";
            }
            _number = 0;
            for (int i = 0; i < 5; i++)
            {
                if (_units[i].Step != -1)
                {
                    _number = i + 1;
                }

            }
            EntityLocator.instance.GetButtonLogic().UpdateTextCountStep(_units[0].Step);
            Observer.instance.GetObserverUpdateUI().UpdateQueueLogicUI();
        }

        public void CancelObject(int id)
        {
            var step = _units[id].Step;
            var maxStep = _units[id].MaxStep;
            var name = _units[id].Name;
            var typeObjProduction = _units[id].typeObjProduction;

            switch (typeObjProduction)
            {
                case TypeObjectProduction.Unit:
                    ReturnResource(_factoryUnits.GetCSVReader().GetResourceUnit(name), maxStep, step);
                    var returnWorkers = _factoryUnits.GetCSVReader().GetResourceUnit(name).GetValue(ConstantNameResource.WORKERS);
                    DictionaryResource.AddAttributeDictionary(Player.GetResources(), new Game.Types.KeyValuePair<string, int>(ConstantNameResource.WORKERS, returnWorkers));
                    Player.UpdateResource();
                    break;
            }

        }


        public bool CheckUpgrade(string name)
        {
            foreach (var n in _units)
            {
                if (n.Name == name)
                {
                    return false;
                }
            }
            return true;
        }


        public int GetCountUnitInProduction() => _number;

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

        private void ReturnResource(DictionaryResource dictionaryResource, int stepMax, int currentStep)
        {
            var returnMetal = dictionaryResource.GetValue(ConstantNameResource.METAL);
            var returnCristal = dictionaryResource.GetValue(ConstantNameResource.ENERGY_CRISTAL);
            var returnNanoStructure = dictionaryResource.GetValue(ConstantNameResource.NANO_STRUCTURE);
            var returnCredits = dictionaryResource.GetValue(ConstantNameResource.CREDITS);



            returnMetal = (returnMetal / stepMax) * currentStep;
            returnCristal = (returnCristal / stepMax) * currentStep;
            returnNanoStructure = (returnNanoStructure / stepMax) * currentStep;
            returnCredits = (returnCredits / stepMax) * currentStep;

            DictionaryResource.AddAttributeDictionary(Player.GetResources(), new Game.Types.KeyValuePair<string, int>(ConstantNameResource.METAL, returnMetal));
            DictionaryResource.AddAttributeDictionary(Player.GetResources(), new Game.Types.KeyValuePair<string, int>(ConstantNameResource.ENERGY_CRISTAL, returnCristal));
            DictionaryResource.AddAttributeDictionary(Player.GetResources(), new Game.Types.KeyValuePair<string, int>(ConstantNameResource.NANO_STRUCTURE, returnNanoStructure));
            DictionaryResource.AddAttributeDictionary(Player.GetResources(), new Game.Types.KeyValuePair<string, int>(ConstantNameResource.CREDITS, returnCredits));
            //DictionaryResource resourceReturn = new DictionaryResource();




        }
        private void NextUnit()
        {
            for (int i = 0; i < 5; i++)
            {
                _units[i].Step = _units[i + 1].Step;
                _units[i].MaxStep = _units[i + 1].MaxStep;
                _units[i].Name = _units[i + 1].Name;
                _units[i].typeObjProduction = _units[i + 1].typeObjProduction;
            }
            if (_units[5].Step > 0)
            {
                _units[5].Step = -1;
                _units[5].MaxStep = -1;
                _units[5].Name = "";
                _units[5].typeObjProduction = TypeObjectProduction.Unit;
            }
            _number--;
            if (_number < 0)
            {
                _number = 0;
            }
        }

        

        public override void SetupActions()
        {
            base.SetupActions();
        }

        public override List<IUserAction> GetActions()
        {
            return base.GetActions();
        }

        public void Create(string name, int step, TypeObjectProduction objectProduction)
        {
            throw new System.NotImplementedException();
        }
    }
}
