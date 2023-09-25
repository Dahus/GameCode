using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Player;
using Game.FightSystem;
using Game.UpgradeSystem;
using Game.UnitsSystem;

namespace Game.UI
{
    public class UIButtonRSZO : UIButtonUnit
    {
        private UpgradeUnitDamage upgradeUnitDamage;
        private UpgradeDeployedArtiller upgradeDeployed;
        private UpgradeUnitHealth upgradeUnitHealth;
        private Howitzer howitzer;


        private void Start()
        {
            _unit = GetComponent<AbstractUnit>();
            howitzer = GetComponent<Howitzer>();
            upgradeUnitHealth = GetComponent<UpgradeUnitHealth>();
            upgradeDeployed = GetComponent<UpgradeDeployedArtiller>();
            upgradeUnitDamage = GetComponent<UpgradeUnitDamage>();
        }
        public override void Setup()
        {
            ClearActions();
            AddActions(Attack);
            AddActions(Move);
            AddActions(Deployed);
            AddActions(UpgradeHealth);
            AddActions(UpgradeDamage);
            AddActions(UpgradeDeployed);
        }
        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            buttons[0].onClick.AddListener(Move);
            buttons[1].onClick.AddListener(Attack);
            buttons[2].onClick.AddListener(Deployed);
            buttons[3].onClick.AddListener(UpgradeHealth);
            buttons[4].onClick.AddListener(UpgradeDamage);
            buttons[5].onClick.AddListener(UpgradeDeployed);
        }

        private void Attack()
        {
            if (!howitzer.GetDeployed())
            {
                Debug.LogError("Не развернут");
                return;
            }
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

        private void Deployed()
        {
            howitzer.Deployed();
        }



        private void UpgradeHealth()
        {
            upgradeUnitHealth.MakeUpgrade();
        }

        private void UpgradeDamage()
        {
            upgradeUnitDamage.MakeUpgrade();
        }

        private void UpgradeDeployed()
        {
            upgradeDeployed.MakeUpgrade();
        }

        private void Move()
        {
            Observer.instance.GetMoveObserver().SetUnit(_unit);
            Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        }
    }
}
