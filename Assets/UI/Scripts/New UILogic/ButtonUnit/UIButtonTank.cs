using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Player;
using Game.UnitsSystem;
using Game.FightSystem;
using Game.UpgradeSystem;

namespace Game.UI
{
    public class UIButtonTank : UIButtonUnit
    {
        private UpgradeUnitHealth _upgradeUnitHealth;
        private UpgradeUnitDamage _upgradeUnitDamage;
        private UpgradeUnitSpeed _upgradeUnitSpeed;
        private UpgradeObserver _upgradeObserver;

        private void Start()
        {
            _unit = GetComponent<AbstractUnit>();
            _upgradeUnitHealth = GetComponent<UpgradeUnitHealth>();
            _upgradeUnitDamage = GetComponent<UpgradeUnitDamage>();
            _upgradeUnitSpeed = GetComponent<UpgradeUnitSpeed>();
            _upgradeObserver = GetComponent<UpgradeObserver>();
        }
        public override void Setup()
        {
            ClearActions();
            AddActions(Attack);
            AddActions(Move);
            AddActions(UpgradeHealth);
            AddActions(UpgradeDamage);
            AddActions(UpgradeEnergy);
            AddActions(UpgradeUnit);
            
        }

        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            buttons[0].onClick.AddListener(Move);
            texts[0].text = "������";
            buttons[1].onClick.AddListener(Attack);
            texts[1].text = "���������";
            buttons[2].onClick.AddListener(UpgradeHealth);
            texts[2].text = "��������";
            buttons[3].onClick.AddListener(UpgradeDamage);
            texts[3].text = "����";
            buttons[4].onClick.AddListener(UpgradeEnergy);
            texts[4].text = "�������";
            buttons[5].onClick.AddListener(UpgradeUnit);
            texts[5].text = "����";
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
                Debug.LogError("�� ������� �������");
            }
        }

        private void UpgradeHealth()
        {
            _upgradeUnitHealth.MakeUpgrade();
        }

        private void UpgradeDamage()
        {
            _upgradeUnitDamage.MakeUpgrade();
        }

        private void UpgradeEnergy()
        {
            _upgradeUnitSpeed.MakeUpgrade();
        }
        private void UpgradeUnit()
        {
            if (_upgradeObserver.CheckListUpgrade())
            {
                _unit.UpgradeUnit();
            }
            else Debug.LogError("�� ������� ��������");
        }

        private void Move()
        {
            Observer.instance.GetMoveObserver().SetUnit(_unit);
            Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        }
    }
}
