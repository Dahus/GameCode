using Game.FightSystem;
using Game.Player;
using Game.UnitsSystem;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnitAction : AbstractAction
{
    private PlayerManager _player;
    private GameObject _attackObject;
    private GameObject _damagableObject;
    
    public AttackUnitAction(PlayerManager player, GameObject attackObject, GameObject damagableObject)
    {
        _player = player;
        _attackObject = attackObject;
        _damagableObject = damagableObject;
    }
    public override Coefficients Coefficients => GetCoefficients();

    protected override Coefficients GetCoefficients()
    {
        var attackUnit = _attackObject.GetComponent<AbstractUnit>();
        if (attackUnit.FightActionPoints > attackUnit.ActionPoints)
            return new Coefficients(-10, -10, -10);

        return new Coefficients(15, 15, 15);
    }

    public override List<IUserAction> GetNextActions()
    {
        return new List<IUserAction>();
    }

    protected override void OnExecute()
    {
        var attackUnit = _attackObject.GetComponent<AbstractUnit>();
        if (attackUnit.FightActionPoints > attackUnit.ActionPoints)
            return;
        var fightManager = Observer.instance.GetPlayerObserver().GetFightManager();
        fightManager.SetAttacking(_attackObject);
        fightManager.SetDefencing(_damagableObject);
        fightManager.Fight();
    }
}
