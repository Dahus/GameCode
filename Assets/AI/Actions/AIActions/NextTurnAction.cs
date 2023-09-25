using Game.Player;
using Game.UnitsSystem;
using System.Collections.Generic;

public class NextTurnAction : AbstractAction
{
    private readonly PlayerAI _player;

    public NextTurnAction(PlayerManager playerAI)
    {
        _player = playerAI.gameObject.GetComponent<PlayerAI>();
    }

    public override Coefficients Coefficients => GetCoefficients();

    protected override Coefficients GetCoefficients()
    {
        return new Coefficients(10, 10, 10);
    }

    public override List<IUserAction> GetNextActions()
    {
        return new List<IUserAction>();
    }

    protected override void OnExecute()
    {
        _player.EndStepPlayer = true;
    }
}
