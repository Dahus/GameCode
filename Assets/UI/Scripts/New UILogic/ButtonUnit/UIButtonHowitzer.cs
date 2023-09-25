using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Player;
using Game.FightSystem;
using Game.UpgradeSystem;
using Game.UnitsSystem;
using Game.GlobalSystems;

namespace Game.UI
{
    public class UIButtonHowitzer : UIButtonUnit
    {

        private UpgradeUnitDamage upgradeUnitDamage;
        private UpgradeDeployedArtiller upgradeDeployed;
        private UpgradeUnitHealth upgradeUnitHealth;
        private UpgradeObserver upgradeObserver;
        private UpgradeUnitSpeed upgradeUnitSpeed;
        private Howitzer howitzer;


        private void Start()
        {
            _unit = GetComponent<AbstractUnit>();
            howitzer = GetComponent<Howitzer>();
            upgradeUnitHealth = GetComponent<UpgradeUnitHealth>();
            upgradeDeployed = GetComponent<UpgradeDeployedArtiller>();
            upgradeUnitDamage = GetComponent<UpgradeUnitDamage>();
            upgradeObserver = GetComponent<UpgradeObserver>();
        }
        public override void Setup()
        {
            ClearActions();

           
            AddActions(Attack);
            AddActions(Move);
            AddActions(Deployed);
            AddActions(UpgradeHealth);
            AddActions(UpgradeDamage);
            AddActions(UpgradeSpeed);
            AddActions(UpgradeDeployed);
            AddActions(UpgradeUnit);
        }
        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            buttons[0].onClick.AddListener(Move);
            texts[0].text = "����";
            buttons[1].onClick.AddListener(Attack);
            texts[1].text = "���������";
            buttons[2].onClick.AddListener(Deployed);
            texts[2].text = "������/����";
            buttons[3].onClick.AddListener(UpgradeHealth);
            texts[3].text = "��������";
            buttons[4].onClick.AddListener(UpgradeDamage);
            texts[4].text = "����";
            buttons[5].onClick.AddListener(UpgradeSpeed);
            texts[5].text = "�������";
            buttons[6].onClick.AddListener(UpgradeDeployed);
            texts[6].text = "��������� ����";
            buttons[7].onClick.AddListener(UpgradeUnit);
            texts[7].text = "����";
        }

        private void Attack()
        {
            if (!howitzer.GetDeployed())
            {
                EntityLocator.instance.GetPanelErrorInfo().UpdateTextError("�� ���������");
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
                EntityLocator.instance.GetPanelErrorInfo().UpdateTextError("�� ������� �������");
            }
        }

        private void Deployed()
        {
            howitzer.Deployed();
        }

        private void UpgradeSpeed()
        {
            upgradeUnitSpeed.MakeUpgrade();
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
        
        private void UpgradeUnit()
        {
            if (upgradeObserver.CheckListUpgrade())
            {
                _unit.UpgradeUnit();
            }
            else
            {
                EntityLocator.instance.GetPanelErrorInfo().UpdateTextError("�� ������� �������");
            }
        }


        private void Move()
        {
            Observer.instance.GetMoveObserver().SetUnit(_unit);
            Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        }
    }
}
