using Game.BuildingSystem;
using Game.Player;
using Game.UnitsSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitAction : AbstractAction
{
    private PlayerManager _player;
    private AbstractCreateUnitBuild _build;
    private string _nameUnit;

    public CreateUnitAction(PlayerManager player, AbstractCreateUnitBuild build, string nameUnit)
    {
        _player = player;
        _build = build;
        _nameUnit = nameUnit;
    }

    public override Coefficients Coefficients => GetCoefficients();

    public override List<IUserAction> GetNextActions()
    {
        return new List<IUserAction>();
    }

    protected override Coefficients GetCoefficients()
    {
        if (!(_player.GetPlayerUnits().Count == 0) || !(_build.GetCountUnitInProduction() == 0))
        {
            return new Coefficients(0, 0, 0);
        }
        return new Coefficients(100, 100, 100);
    }

    protected override void OnExecute()
    {
        var unit = _player.GetFactoryUnits().SearchUnit(_nameUnit);
        _build.Create(unit.GetName(), unit.GetUnitsData().GetTimeCreateUnit(), TypeObjectProduction.Unit, unit.GetUnitsData().GetIcon());
    }
}
