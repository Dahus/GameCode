using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.UpgradeSystem;
using Game.UnitsSystem;
using Game.Player;
using Game.FightSystem;
namespace Game.UI
{
    public class UIButtonStalker : UIButtonUnit
    {
        private UpgradeUnitHealth _upgradeUnitHealth;

        private void Start()
        {
            _unit = GetComponent<AbstractUnit>();
            _upgradeUnitHealth = GetComponent<UpgradeUnitHealth>();
        }

        public override void Setup()
        {
            ClearActions();
            AddActions(Attack);
            AddActions(Move);
            AddActions(UpgradeHealth);
        }
        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            buttons[0].onClick.AddListener(Move);
            buttons[1].onClick.AddListener(Attack);
            buttons[2].onClick.AddListener(UpgradeHealth);
         
        }
        private void Attack()
        {
            if (_unit.FightActionPoints < _unit.ActionPoints)
            {
                Observer.instance.GetPlayerObserver().SetAttackObject(_unit.gameObject);
                Observer.instance.StartAttack();
                _unit.GetComponent<IAttack>().GetReadyAttack();
            }
            else
            {
                Debug.LogError("Не хватает энергии");
            }
        }

        private void UpgradeHealth()
        {
            _upgradeUnitHealth.MakeUpgrade();
        }
        private void Move()
        {
            Observer.instance.GetMoveObserver().SetUnit(_unit);
            Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        }
    }
}
