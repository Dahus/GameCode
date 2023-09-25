using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GridSystem;
public class PathfindingVitalic : MonoBehaviour
{


    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static PathfindingVitalic Instance { get; private set;}

    private GridHex[,] _grid;

    private List<GridHex> _openList;
    private List<GridHex> _closedList;

    public void Setup(int width, int height, float cellSize) 
    {
        Instance = this;
        _grid = GameGrid.Grid;
    }


  /*  public List<GridHex> FindPath(GridHex start, GridHex end) 
    {
    
    }*/
}
