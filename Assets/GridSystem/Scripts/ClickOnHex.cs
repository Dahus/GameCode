using System;
using System.Collections;
using System.Collections.Generic;
using Game.BuildingSystem;
using Game.Events;
using Game.GlobalData;
using Game.Grid;
using Game.GridSystem;
using Game.UI;
using Game.UnitsSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Player
{
    public class ClickOnHex : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ButtonLogicManager _buttonLogicManager;
        [SerializeField] private UIParametresLogic _uiParametres;
        [SerializeField] private GameObject _currentObjectInHex;
        [SerializeField] private PlayerManager _playerManager;
        private GridHex _hex;


        public void Setup(ButtonLogicManager buttonLogicManager, UIParametresLogic uIParametres)
        {
            _buttonLogicManager = buttonLogicManager;
            _uiParametres = uIParametres;
            _hex = GetComponent<GridHex>();
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            Observer.instance.OffLightHex();
            Observer.instance.SetHexagon(gameObject);
            Observer.instance.LightHex();
            _buttonLogicManager.OffButton();
            _buttonLogicManager.OffQueueButtons();
            _playerManager = Observer.instance.GetPlayerObserver().GetPlayerManager();

            //Debug.LogError(_hex.PositionInGrid.x + " : " + _hex.PositionInGrid.y);
            //Debug.LogError(_playerManager.GetObserverMap().HexObserveData[_hex.PositionInGrid.x, _hex.PositionInGrid.y].observers.Count);

            if (Observer.instance.GetStateObserver() == StateObserver.ChitObserver)
            {
                if (_hex.ObjectsInHex.Length <= 0)
                {
                    if (_hex.HexInfoPathfinder.Walkable)
                    {
                        Observer.instance.GetChitObserver().SetChooseHex(_hex);
                    }
                }
            }

            if (Observer.instance.GetStateObserver() == StateObserver.Standart)
            {
                if (_hex.ObjectsInHex.Length <= 0)
                {
                    if (_hex.HexInfoPathfinder.Walkable)
                    {
                        if (_hex.TryGetComponent(out UIButtonHex buttonHex))
                        {
                            buttonHex.Player = _playerManager;
                            buttonHex.Setup();
                            _buttonLogicManager.SetUIButton(buttonHex, _hex.gameObject);
                            _buttonLogicManager.SetUIInfo(_hex.GetComponent<UIInfoHex>());
                            Observer.instance.SetUIButton(buttonHex);
                        }
                    }
                }
                else
                {
                    // Dictionary<string, GameObject> objectsInHex = _hex.GetObjectsInHexagon();
                    var objectsInHex = _hex.ObjectsInHex;
                    
                    string name = "";
                    _currentObjectInHex = objectsInHex[objectsInHex.Length-1];

                    if (_currentObjectInHex.TryGetComponent(out AbstractBuild build))
                    {
                        name = build.GetName();
                        _playerManager = build.GetPlayerManager();
                        if (_currentObjectInHex.TryGetComponent(out UIButtonBuild buttonBuild))
                        {
                            buttonBuild.Player = _playerManager;
                            buttonBuild.Setup();
                            _buttonLogicManager.SetUIButton(buttonBuild, _hex.gameObject);
                            Observer.instance.SetUIButton(buttonBuild);
                        }
                        if(build.TryGetComponent(out AbstractCreateUnitBuild buildForUnits))
                        {
                            _buttonLogicManager.SetupQueueLogicManager(buildForUnits);
                        }
                    }

                    if (_currentObjectInHex.TryGetComponent(out AbstractUnit unit))
                    {
                        name = unit.GetName();
                        _playerManager = unit.GetPlayerManager();
                        if (_currentObjectInHex.TryGetComponent(out UIButtonUnit buttonUnit))
                        {
                            buttonUnit.Player = _playerManager;
                            buttonUnit.Setup();
                            _buttonLogicManager.SetUIButton(buttonUnit, _hex.gameObject);
                            Observer.instance.SetUIButton(buttonUnit);
                        }
                       
                    }
                    //Debug.LogError("сейчас" + _currentObjectInHex.name);
                    //_uiParametres.UpdateInfo(_currentObjectInHex);
                    if (Observer.instance.GetPlayerObserver().GetPlayerManager() == _playerManager)
                    {

                      //  _buttonLogicManager.UpdateButton();


                    }
                    else
                    {
                        //  _buttonLogicManager.OFFButton();
                    }
                }
            }

            if (Observer.instance.GetStateObserver() == StateObserver.AttackObserver)
            {
                Observer.instance.GetPlayerObserver().SetFightHex(gameObject.GetComponent<GridHex>());
            }

            if (Observer.instance.GetStateObserver() == StateObserver.MoveObserver)
            {
                Observer.instance.GetMoveObserver().SetEndHex(gameObject.GetComponent<GridHex>());
            }
            if (Observer.instance.GetStateObserver() == StateObserver.ActionObserver)
            {
                Observer.instance.GetActionObserver().Execute(gameObject.GetComponent<GridHex>());
            }

            if (Observer.instance.GetPlayerObserver().GetPlayerManager() == _playerManager)
            {

                //_buttonLogicManager.UpdateButton();
            }
            else
            {
                _buttonLogicManager.OffButton();
            }
        }
        /* public void OnPointerClick(PointerEventData pointerEventData)
         {
             // var count = gameObject.GetComponent<UIInHex>().InfoHex();
             Observer.instance.OffLightHex();
             Observer.instance.SetHexagon(gameObject);
             Observer.instance.LightHex();
             Observer.instance.NullButtonAttack();


             if (Observer.instance.GetStateObserver() == StateObserver.Standart)
             {
                 if (_hex.CountObjectInHex() <= 0)
                 {
                     if (_hex.Walkable)
                     {
                         EventLocator.instance.SetObject(_hex.gameObject);
                         _choosedObject = EventLocator.instance.GetFunctionHexChoosedObject();
                        // _buttonLogicManager.SetChoosedObject(_choosedObject, _hex.gameObject);
                        // _buttonLogicManager.UpdateButton();
                         _uiParametres.SetModelForParametres(_choosedObject.GetComponent<ModelForParametresHex>());
                         _uiParametres.UpdateInfo(_hex.gameObject);
                     }
                     else _buttonLogicManager.OFFButton();   
                 }
                 else
                 {
                     Dictionary<string, GameObject> objectsInHex = _hex.GetObjectsInHexagon();

                     int count = 0;
                     string name = "";
                     foreach (var obj in objectsInHex)
                     {
                         _currentObjectInHex = obj.Value;
                     }

                     if (_currentObjectInHex.TryGetComponent(out AbstractBuild build))
                     {
                         name = build.GetName();
                         _playerManager = build.GetPlayerManager();
                         EventLocator.instance.SetObject(build.gameObject);
                         //_choosedObject = EventLocator.instance.GetFunctionBuildByName(name);
                        // _buttonLogicManager.SetChoosedObject(_choosedObject, _hex.gameObject);
                       //  _uiParametres.SetModelForParametres(_choosedObject.GetComponent<ModelForParametresBuild>());

                     }

                     if (_currentObjectInHex.TryGetComponent(out AbstractUnit unit))
                     {
                         name = unit.GetName();
                         _playerManager = unit.GetPlayerManager();
                         EventLocator.instance.SetObject(unit.gameObject);
                         _choosedObject = EventLocator.instance.GetFunctionUnitsByName(name, unit);
                        // _buttonLogicManager.SetChoosedObject(_choosedObject, _hex.gameObject);
                         _uiParametres.SetModelForParametres(_choosedObject.GetComponent<ModelForParametresUnit>());
                     }
                     Debug.LogError("сейчас" + _currentObjectInHex.name);
                     _uiParametres.UpdateInfo(_currentObjectInHex);
                     if (Observer.instance.GetPlayerObserver().GetPlayerManager() == _playerManager)
                     {

                         _buttonLogicManager.UpdateButton();
                     }
                     else
                     {
                         _buttonLogicManager.OFFButton();
                     }
                 }


             }

             if (Observer.instance.GetStateObserver() == StateObserver.AttackObserver)
             {
                 Observer.instance.GetPlayerObserver().SetFightHex(gameObject.GetComponent<GridHex>());
             }

             if (Observer.instance.GetStateObserver() == StateObserver.MoveObserver)
             {
                 Observer.instance.GetMoveObserver().SetEndHex(gameObject.GetComponent<GridHex>());
             }

             //GameGrid.GetNeighbours(_hex.transform.position.x, _hex.transform.position.y);

             //_buttonLogicManager.SetChoosedObject(_choosedObject);
             //_buttonLogicManager.UpdateButton();
         }
         */


    }
}