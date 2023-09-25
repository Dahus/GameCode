using System.Collections;
using System.Collections.Generic;
using Game.FightSystem;
using UnityEngine;

using Game.Player;

namespace Game.UnitsSystem
{
    public class SniperDrone : AbstractUnit
    {

        [SerializeField] private AbstractUnit _unitUpgrade;
        [SerializeField] private Patron _patron;

        public override void Setup(GameObject place, PlayerManager player, PathfindingWithCoef pathfinding)
        {
            _typeAttack = TypeAttack.DistanceFight;
            //SetupActions();
            base.Setup(place, player, pathfinding);
        }

        public override void CreatePatron(float damage, IDamage objectDamage)
        {
            var patron = Instantiate(_patron, transform.position, Quaternion.identity);
            patron.Setup(_damageDistance, Observer.instance.GetHexagon(), objectDamage);
            ResetActionPoints();
            CancelAttack();
        }

        public override void SetupActions()
        {
            _userActions = new List<IUserAction>();
            var actionMove = new MoveUnitAction(_player, this);
            _userActions.Add(actionMove);
        }

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
