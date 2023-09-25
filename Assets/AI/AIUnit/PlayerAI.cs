using Game.GlobalSystems;
using Game.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAI : AbstractAI
{
    public bool EndStepPlayer { get; set; }
    public void SetupBot(PlayerManager player, string name, StepManager manager, Coefficients coefficients)
    {
        SetInfoPlayer(player);
        SetBotName(name);
        SetStepManager(manager);
        SetCoefficients(coefficients);
        SetupActionsAI();
        EndStepPlayer = false;
    }
    
    public void PerfomrAnAction()
    {
        int i = 0;
        do
        {
            if (EndStepPlayer == true)
                break;
            IUserAction? nextAction = PredictOptimalAction(GetAvaibleActions(), 1);
            if (nextAction == default)
                break;
            nextAction.Execute();
            i++;
        } while (i < 100);
    }

    private List<IUserAction> GetAvaibleActions()
    {
        List<IUserAction> allActions = new List<IUserAction>();
        allActions.AddRange(PlayerActions);
        allActions.AddRange(GetAllActions());
        return allActions;
    }

    private IUserAction? PredictOptimalAction(List<IUserAction> actions, int depth, List<IUserAction>? path = null)
    {
        List<IUserAction> maxPath = default;
        foreach (IUserAction action in actions)
        {
            var nextAction = CalculatePath(actions, depth, ref path, action);
            if (nextAction == default)
                continue;
            if (CalculatePathCost(maxPath) < CalculatePathCost(path))
                maxPath = new List<IUserAction>(path);
            path.Remove(nextAction);
        }

        return maxPath?.FirstOrDefault();
    }

    private IUserAction? CalculatePath(List<IUserAction> actions, int depth, ref List<IUserAction>? path, IUserAction action)
    {
        depth -= 1;
        path ??= new List<IUserAction>();
        path.Add(action);
        if (depth == 0)
            return action;


        var nextAvailable = new List<IUserAction>(actions);
        for (var i = nextAvailable.Count - 1; i >= 0; i--)
        {
            if (path.Contains(nextAvailable[i]))
                nextAvailable.RemoveAt(i);
        }
        nextAvailable.AddRange(action.GetNextActions());

        List<IUserAction> maxPath = new List<IUserAction>(path);
        foreach (IUserAction nextAction in nextAvailable)
        {
            var overNextAction = CalculatePath(actions, depth, ref path, nextAction);
            if (overNextAction == default)
                continue;
            if (CalculatePathCost(maxPath) < CalculatePathCost(path))
                maxPath = new List<IUserAction>(path);
            path.Remove(overNextAction);
        }
        return maxPath?.LastOrDefault();
    }

    private float CalculatePathCost(List<IUserAction>? userActions)
    {
        if (userActions == null)
            return float.MinValue;
        var sum = new Coefficients();
        foreach (IUserAction userAction in userActions)
        {
            var multCoeff = Coefficients.MulttiplyCoeff(userAction.Coefficients, Coefficients);
            sum = Coefficients.AdditionCoeff(sum, multCoeff);
        }

        return sum.GetMaxValue();
    }

}
