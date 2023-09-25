using Game.BuildingSystem;
using Game.GlobalSystems;
using Game.Player;
using Game.UnitsSystem;
using System.Collections.Generic;
using UnityEngine;
using Game.GlobalData;

public abstract class AbstractAI : MonoBehaviour
{
    /// <summary>
    /// AI Predisposition to: Attack, Defense, Accumulation
    /// </summary>
    public Coefficients Coefficients { get; set; }
    [SerializeField] private PlayerManager _player;
    [SerializeField] private string _botName;
    private StepManager _stepManager;
    public List<IUserAction> PlayerActions { get; set; }

    public PlayerManager GetInfoPlayer()
    {
        return _player;
    }
    protected void SetInfoPlayer(PlayerManager playerInfo)
    {
        _player = playerInfo;
    }

    public string GetBotName()
    {
        return _botName;
    }
    protected void SetBotName(string playerName)
    {
        _botName = playerName;
    }

    /// <summary>
    /// Add Tuple coefs
    /// </summary>
    /// <param name="tupleCoef"> AI Predisposition to: Attack, Defense, Accumulation </param>
    public Coefficients GetCoefAI()
    {
        return Coefficients;
    }
    protected void SetCoefficients(Coefficients coefficients)
    {
        Coefficients = coefficients;
    }

    public StepManager GetStepManager()
    {
        return _stepManager;
    }
    protected void SetStepManager(StepManager manager)
    {
        _stepManager = manager;
    }

    public List<AbstractBuild> GetBuildingsPlayer()
    {
        return _player.GetPlayerBuildsWithoutStartBuilds();
    }
    
    public List<AbstractUnit> GetUnitsPlayer()
    {
        return _player.GetPlayerUnits();
    }
    public ResourcePlayer GetResourcePlayer()
    {
        return _player.GetResources();
    }

    public IEnumerable<IUserAction> GetAllActions()
    {
        foreach (AbstractBuild build in GetBuildingsPlayer()) 
            foreach (IUserAction userAction in build.GetActions())
                yield return userAction;

        foreach (AbstractUnit unit in GetUnitsPlayer())
            foreach (IUserAction userAction in unit.GetActions())
                yield return userAction;
    }

    protected void SetupActionsAI()
    {
        PlayerActions = new List<IUserAction>
        {
            new CreateBuildAction(_player, ConstantNameBuild.ROBOTNODE),
            new NextTurnAction(_player),
        };
    } 
}
