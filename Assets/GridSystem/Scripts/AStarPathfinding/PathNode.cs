using Game.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    // ���������� ����� �� �����.
    public Transform Position { get; set; }
    // ����� ���� �� ������ (G).
    [SerializeField] public int PathLengthFromStart { get; set; }
    // �����, �� ������� ������ � ��� �����.
    [SerializeField] public PathNode CameFrom { get; set; }
    // ��������� ���������� �� ���� (H).
    [SerializeField] public float HeuristicEstimatePathLength { get; set; }
    // ��������� ������ ���������� �� ���� (F).

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
