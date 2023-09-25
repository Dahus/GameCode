using Game.BuildingSystem;
using Game.GlobalSystems;
using Game.GridSystem;
using Game.Player;
using Game.UnitsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitAction : AbstractAction
{
    private readonly PlayerManager _player;
    private AbstractUnit _unit;
    private GridHex _comePlace;
    private List<IUserAction> nextActions;

    public MoveUnitAction(PlayerManager player, AbstractUnit unit)
    {
        _player = player;
        _unit = unit;
    }

    public override Coefficients Coefficients => GetCoefficients();

    protected override Coefficients GetCoefficients()
    {
        if (_unit.ActionPoints <= 0)
            return new Coefficients(0, 0, 0);
        _comePlace = GameGrid.GetRandomHexInGrid();
        return new Coefficients(12, 12, 12);
    }

    public override List<IUserAction> GetNextActions()
    {
        nextActions = new List<IUserAction>();
        var hexes = EntityLocator.instance.GetWavedAlgorithm().SearchHexesWithNotWalkalbe(_unit.GetPlace(), _unit.GetRadiusDamage());
        foreach (var hex in hexes)
        {
            if (hex.ObjectsInHex.Length == 0)
                continue;

            if (hex.ObjectsInHex[0].TryGetComponent<AbstractUnit>(out var damagableUnit))
                nextActions.Add(new AttackUnitAction(_player, _unit.gameObject, hex.ObjectsInHex[0]));
            else if (hex.ObjectsInHex[0].TryGetComponent<AbstractBuild>(out var damagablebuild))
                nextActions.Add(new AttackUnitAction(_player, _unit.gameObject, hex.ObjectsInHex[0]));
            else if (hex.ObjectsInHex[0].TryGetComponent<CreateStartObject>(out var damagablestartbuild))
                nextActions.Add(new AttackUnitAction(_player, _unit.gameObject, hex.ObjectsInHex[0]));
        }
        return nextActions;
    }

    protected override void OnExecute()
    {
        _unit.Movement(_comePlace);
    }
}