using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.FightSystem;
using Game.GlobalData;
using Game.Player;
using Game.GridSystem;

namespace Game.UnitsSystem
{

    public class Howitzer : AbstractUnit
    {

        [SerializeField] private bool isDeployed = false;
        private bool isUpgradeSpeedDeployed = false;
        [SerializeField] private int _pointsForDeployed;
        [SerializeField] private Patron _patron;
        [SerializeField] private AbstractUnit _unitUpgrade;


        public void Deployed()
        {
            if (_pointsForDeployed > ActionPoints)
                return;

            isDeployed = !isDeployed;
            if (isUpgradeSpeedDeployed)
            {
                SubstractionActionPoints(_pointsForDeployed);
            }
            else
            {
                SubstractionActionPoints(ActionPoints);
            }
        }

        public override void Movement(GridHex end)
        {
            if(!isDeployed)
            base.Movement(end);
        }

        public override void AttackDistance(IDamage objectDamage, float coefModificators)
        {
            if (!isDeployed)
                return;
            var damage = 0f;
            switch (objectDamage.GetTypeObInHex())
            {
                case ConstantDictionaryObjectIHex.UNIT:
                    damage = _damageDistance;
                    break;
                default:
                    damage = _damageForBuild;
                    break;
            }

            var resultCoef = coefModificators * damage;
            CreatePatron(damage + resultCoef, objectDamage);
        }


        public bool GetDeployed() => isDeployed;

        public override void CreatePatron(float damage, IDamage objectDamage)
        {
            var patron = Instantiate(_patron, transform.position, Quaternion.identity);
            patron.Setup(_damageMelee, Observer.instance.GetHexagon(), objectDamage);
            ResetActionPoints();
            CancelAttack();
        }

        public override void UpgradeSpeedDeployd() => isUpgradeSpeedDeployed = true;

       

        public override void UpgradeUnit()
        {
            var hex = _startHex;
            var unit = Instantiate(_unitUpgrade, hex.transform.position, Quaternion.identity);
            unit.transform.SetParent(Player.transform);
            unit.Setup(hex.gameObject, Player, _pathfinding);
            DestroyObject();
        }



    }
}