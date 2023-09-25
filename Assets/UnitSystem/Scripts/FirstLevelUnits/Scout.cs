using Game.UnitsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : AbstractUnit
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
