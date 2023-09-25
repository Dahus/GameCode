using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GridSystem;
using Game.Player;

public class AddAStar : MonoBehaviour
{
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private GridHex start = null;
    [SerializeField] private GridHex  goal = null;
    [SerializeField] private int counter=0;

    void Update()
    {
        if (counter == 0 && Observer.instance.GetHexagon()!=null)
        {
            start = Observer.instance.GetHexagon().GetComponent<GridHex>();
        }

        if (start != null && counter == 0)
        {
            counter = 1;
        }

        if (counter == 1 && Observer.instance.GetHexagon().GetComponent<GridHex>() != start && Observer.instance.GetHexagon() != null)
        {
            goal = Observer.instance.GetHexagon().GetComponent<GridHex>();
        }

        if (goal != null && goal != start && counter == 1)
        {
            pathfinding.FindPath(start, goal);
            counter = 2;
        }
    }
}
