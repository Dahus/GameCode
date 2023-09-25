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
    public class UIButtonWarRobot : UIButtonUnit
    {
        private UpgradeUnitHealth _upgradeUnitHealth;
        private UpgradeObserver _upgradeObserver;
        private UpgradeUnitDamage _upgradeUnitDamage;
        private UpgradeUnitSpeed _upgradeUnitSpeed;
        

        private  void  Start()
        {
            _unit = GetComponent<AbstractUnit>();
            _upgradeUnitHealth = GetComponent<UpgradeUnitHealth>();
            _upgradeObserver = GetComponent<UpgradeObserver>();
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
            AddActions(UpgradeUnit);

        }

        public override void UpdateButton(List<Button> buttons,List<Text> texts)
        {
            buttons[0].onClick.AddListener(Move);
            texts[0].text = "Идти";
            buttons[1].onClick.AddListener(Attack);
            texts[0].text = "Атаковать";
            buttons[2].onClick.AddListener(UpgradeHealth);
            texts[0].text = "Здоровье";
            buttons[3].onClick.AddListener(UpgradeDamage);
            texts[0].text = "Урон";
            buttons[4].onClick.AddListener(UpgradeSpeed);
            texts[0].text = "Энергия";
            buttons[5].onClick.AddListener(UpgradeUnit);
            texts[0].text = "Юнит";
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

        private void UpgradeUnit() 
        {
            if (_upgradeObserver.CheckListUpgrade())
            {
                _unit.UpgradeUnit();
            }
            else Debug.LogError("Не хватает прокачки");
        }


        private void Move()
        {
            Observer.instance.GetMoveObserver().SetUnit(_unit);
            Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        }
    }
}
