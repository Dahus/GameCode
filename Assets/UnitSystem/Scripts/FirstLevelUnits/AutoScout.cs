using Game.UnitsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;
using UnityEditor.Experimental.GraphView;
using Game.GridSystem;
using static UnityEngine.UI.CanvasScaler;
using Game.FightSystem;

public class AutoScout : AbstractUnit
{
    //int[,] endPoint = new int[2,2];
    private GridHex _comePlace;
    private AbstractUnit _unit;

    public override void Setup(GameObject place, PlayerManager player, PathfindingWithCoef pathfinding)
    {
        _typeAttack = TypeAttack.DistanceFight;
        SetupActions();
        base.Setup(place, player, pathfinding);
    }
    /*
    public int[,] genegeneratePoint ()
    {

        for (int y = 0; y < endPoint.GetLength(1); y++)
        {
            for (int x = 0; x < endPoint.GetLength(0); x++)
            {
                endPoint[x, y] = Random.Range(0, 100);
            }
        }

        return endPoint;
    }
    */

    public void headTowardsTheGoal()
    {
        _comePlace = GameGrid.GetRandomHexInGrid();
        _unit.Movement(_comePlace);
    }

    private void Start()
    {
        var startPoint = _startHex;

        //startPoint = new int[];
        //Debug.LogError(genegeneratePoint().GetLength(0) + ": " + endPoint[0, 1] + " " + endPoint[1, 0] + " Массив по индекс. 0: " + endPoint[0,0]);
    }

    public void searchForTheGoal ()
    {
        if (true)
        {
            
        }
        else
        {

        }
    }

    public override void SetupActions()
    {
        _userActions = new List<IUserAction>();
        var actionMove = new MoveUnitAction(_player, this);
        _userActions.Add(actionMove);
    }
}