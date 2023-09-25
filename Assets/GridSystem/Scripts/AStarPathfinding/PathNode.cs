using Game.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    // Координаты точки на карте.
    public Transform Position { get; set; }
    // Длина пути от старта (G).
    [SerializeField] public int PathLengthFromStart { get; set; }
    // Точка, из которой пришли в эту точку.
    [SerializeField] public PathNode CameFrom { get; set; }
    // Примерное расстояние до цели (H).
    [SerializeField] public float HeuristicEstimatePathLength { get; set; }
    // Ожидаемое полное расстояние до цели (F).

    private GridHex current_compCell;

    public float EstimateFullPathLength
    {
        get
        {
            return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
        }
    }


    public GridHex Current_compCell
    {
        get
        {
            return current_compCell;
        }

        set
        {
            current_compCell = value;
        }
    }
}
