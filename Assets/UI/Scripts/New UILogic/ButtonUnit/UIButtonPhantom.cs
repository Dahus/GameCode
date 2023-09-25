using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Game.Player;
using Game.UnitsSystem;
using Game.FightSystem;
using Game.UpgradeSystem;
using Game.GlobalData;

namespace Game.UI
{
    public class UIButtonPhantom : UIButtonUnit
    {
        private UpgradeUnitHealth _upgradeUnitHealth;
        private UpgradeUnitDamage _upgradeUnitDamage;
        private UpgradeUnitSpeed _upgradeUnitSpeed;


        private void Start()
        {
            _unit = GetComponent<AbstractUnit>();
            _upgradeUnitHealth = GetComponent<UpgradeUnitHealth>();
            _upgradeUnitDamage = GetComponent<UpgradeUnitDamage>();
            _upgradeUnitSpeed = GetComponent<UpgradeUnitSpeed>();
        }

        public override void Setup()
        {
            ClearActions();
            AddActions(Attack);
            AddActions(Move);
            AddActions(UpgradeHealth);
            AddActions(UpgradeDamage);
            AddActions(UpgradeSpeed);

        }

        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            buttons[0].onClick.AddListener(Move);
            texts[0].text = "����";
            buttons[1].onClick.AddListener(Attack);
            texts[1].text = "���������";
            buttons[2].onClick.AddListener(UpgradeHealth);
            texts[2].text = "��������";
            buttons[3].onClick.AddListener(UpgradeDamage);
            texts[3].text = "����";
            buttons[4].onClick.AddListener(UpgradeSpeed);
            texts[4].text = "�������";
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
            if (Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.STRENGTHROBOTS1))
            {
                _upgradeUnitHealth.MakeUpgrade();
            }
        }

        private void UpgradeDamage()
        {
            if (Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.DAMAGEROBOTS1))
                _upgradeUnitDamage.MakeUpgrade();
        }

        private void UpgradeSpeed()
        {
            if (Player.GetPlayerTechnologyBuild().CheckTechnology(ConstantNameTechnologyBuild.SPEEDROBOTS1))
                _upgradeUnitSpeed.MakeUpgrade();
        }


        private void Move()
        {
            Observer.instance.GetMoveObserver().SetUnit(_unit);
            Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        }
    }

}