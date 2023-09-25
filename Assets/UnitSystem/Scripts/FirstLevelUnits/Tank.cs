using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.UnitsSystem
{
    public class Tank : AbstractUnit
    {

        [SerializeField] private AbstractUnit _unitUpgrade;

        public override void SetupActions()
        {

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
