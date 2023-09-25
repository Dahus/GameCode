using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Grid;
using Game.GlobalData;
using Game.GridSystem;
using Game.BuildingSystem;
using Game.UnitsSystem;
using Game.UI;

namespace Game.Player
{
    public class Observer : MonoBehaviour
    {
        public static Observer instance;
        [SerializeField] private GameObject _hexagon;
        [SerializeField] private Color _prewColor;
        
        [SerializeField] private MoveObserver _moveObserver;
        [SerializeField] private PlayerObserver _playerObserver;
        [SerializeField] private ActionObserver _actionObserver;
        [SerializeField] private ChitObserver _chitObserver;

        [SerializeField] private GameObserver _gameObserver;
        [SerializeField] private StateObserver _stateObserver = StateObserver.Standart;

        [SerializeField] private ObserverUpdateUI _uiObserver;

        [SerializeField] private UIButton uiButton; 

        void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }
        }

        public GameObject GetHexagon()
        {
            return _hexagon;
        }

        public void SetHexagon(GameObject hex)
        {   
            _hexagon = hex;
        }

        public void LightHex()
        {
            _prewColor = _hexagon.GetComponent<SpriteRenderer>().color;
            _hexagon.GetComponent<SpriteRenderer>().color= new Color(_prewColor.r*0.8f,_prewColor.g*0.8f,_prewColor.b*0.8f,1);
        }

        public void OffLightHex()
        {
            if (_hexagon != null)
            {
                _hexagon.GetComponent<SpriteRenderer>().color = _prewColor;
            }
        }
        public void SetStateObserver(StateObserver state)
        {
            _stateObserver = state;
        }

        public StateObserver GetStateObserver()
        {
            return _stateObserver;
        }

        public void StartAttack()
        {
            _stateObserver = StateObserver.AttackObserver;
        }

        public void SetUIButton(UIButton uIButton) => uiButton = uIButton;

        public UIButton GetUIButton() => uiButton;


        public void ChooseButtonInUIButton(int number) => uiButton.SetupChooseButtons(number); 



        public bool Check(GridHex hex)
        {

            var objects = hex.ObjectsInHex;


            if (objects.Length > 0) 
            {
                for(int i = 0; i < objects.Length; i++) 
                {
                    if(objects[i].TryGetComponent(out AbstractBuild _))
                    {
                        return false;
                    }
                    if (objects[i].TryGetComponent(out AbstractUnit _))
                    {
                        return false;
                    }
                }
            }
           /* Dictionary<string, GameObject> dict = hex.GetObjectsInHexagon();
            Debug.Log(dict.Count);
            if (dict.Count > 0)
            {
                for (int i = 0; i < dict.Count; i++)
                {
                    if (dict.ContainsKey(ConstantDictionaryObjectIHex.BUILD))
                    {
                        Debug.Log("not building");
                        return false;
                    }
                    if (dict.ContainsKey(ConstantDictionaryObjectIHex.UNIT))
                    {
                        Debug.Log("not unit");
                        return false;
                    }
                }
            }
           */
            return true;
        }

        public MoveObserver GetMoveObserver()
        {
            return _moveObserver;
        }

        public PlayerObserver GetPlayerObserver() 
        {
            return _playerObserver;
        }

        public ChitObserver GetChitObserver() => _chitObserver;

        public ActionObserver GetActionObserver() => _actionObserver;

        public ObserverUpdateUI GetObserverUpdateUI() => _uiObserver;

        public GameObserver GetGameObserver() => _gameObserver;
    }
    
    public enum StateObserver
    {
        Standart,
        MoveObserver,
        AttackObserver,
        ActionObserver,
        ChitObserver
    }
}
