using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Game.BuildingSystem;
using Game.FightSystem;
using Game.GridSystem;
using Game.UnitsSystem;
using Game.Economics;
using Game.GlobalSystems;
using Game.UI;

namespace Game.Player
{
    public class PlayerObserver : MonoBehaviour
    {
        [SerializeField] private PlayerManager _currentPlayer;
        [SerializeField] private GameObject _currentObjectInHex;
        [SerializeField] private GridHex _hexAttack;

        [SerializeField] private FightManager _fightManager;

        private GameObject _attackObject;
        private GameObject _damageObject;

        private IAttack _attack;

        private int counterUnits = 0;
        private int counterBuilds = 0;

        //[SerializeField] private GameObject _objectActive;

        public PlayerManager GetPlayerManager()
        {
            return _currentPlayer;
        }

        public void SetPlayerManager(PlayerManager player)
        {
            _currentPlayer = player;
        }


        public void CancelFight()
        {
            if (_attackObject != null)
                _attackObject.GetComponent<IAttack>().CancelAttack();
        }

        public void SetFightHex(GridHex hexAttack)
        {
            Debug.LogError("Попал");
            _hexAttack = hexAttack;

            bool flag = SearchIDamageObject(hexAttack, out _damageObject);
            if (flag)
            {
                _fightManager.SetAttacking(_attackObject);
                _fightManager.SetDefencing(_damageObject);
                _fightManager.Fight();
                Observer.instance.SetStateObserver(StateObserver.Standart);
            }
            else
            {
                Debug.LogError("Промазал, атака отключается");
                _attackObject.GetComponent<IAttack>().CancelAttack();
                Observer.instance.SetStateObserver(StateObserver.Standart);
            }
        }

        public void SetAttackObject(GameObject attackObject)
        {
            _attackObject = attackObject;
        }

        private bool SearchIDamageObject(GridHex hex, out GameObject damage)
        {


            if (hex.ObjectsInHex.Length <= 0)
            {
                damage = null;
                return false;
            }
            else
            {
                var objectsInHex = hex.ObjectsInHex;

                foreach (var obj in objectsInHex)
                {
                    _currentObjectInHex = obj;

                }
                if (_attackObject.GetComponent<IAttack>().objectForAttack.Contains(_currentObjectInHex))
                {
                    damage = _currentObjectInHex;
                    return true;
                }



                /*   if (damage.GetComponent<IDamage>().Player == Observer.instance.GetPlayerObserver().GetPlayerManager())
                   {
                       damage = null;
                       return false;
                   }*/
            }
            damage = null;
            return false;
        }

        public void AddRes()
        {
            DictionaryResource.ADDRESOURCECHIT(_currentPlayer.GetResources());
            _currentPlayer.UpdateResource();
        }

        public void NextUnit()
        {
            if (_currentPlayer.GetPlayerUnits().Count == 0)
                return;
            var unit = _currentPlayer.GetPlayerUnits()[counterUnits];
            Camera.main.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y, -10);
            counterUnits++;
            if (counterUnits >= _currentPlayer.GetPlayerUnits().Count)
                counterUnits = 0;

            var buttonLogicManager = EntityLocator.instance.GetButtonLogic();
            buttonLogicManager.OffButton();
            if (unit.TryGetComponent(out UIButtonUnit buttonUnit))
            {
                buttonUnit.Player = _currentPlayer;
                buttonUnit.Setup();
                buttonLogicManager.SetUIButton(buttonUnit, unit.GetPlace().gameObject);
            }
        }
        public void NextBuild()
        {
            if (_currentPlayer.GetPlayerBuilds().Count == 0)
                return;
            var build = _currentPlayer.GetPlayerBuilds()[counterBuilds];
            Camera.main.transform.position = new Vector3(build.transform.position.x, build.transform.position.y, -10);
            counterBuilds++;
            if (counterBuilds >= _currentPlayer.GetPlayerBuilds().Count)
                counterBuilds = 0;

            var buttonLogicManager = EntityLocator.instance.GetButtonLogic();
            buttonLogicManager.OffButton();
            if (build.TryGetComponent(out UIButtonBuild buttonBuild))
            {
                buttonBuild.Player = _currentPlayer;
                buttonBuild.Setup();
                buttonLogicManager.SetUIButton(buttonBuild, build.GetPlace().gameObject);
            }
        }

        public FightManager GetFightManager()
        {
            return _fightManager;
        }
    }
}