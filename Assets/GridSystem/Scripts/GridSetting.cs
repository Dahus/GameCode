using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSetting
{
    public Vector2Int Size = Vector2Int.zero;
    public float Сlearance = 0;
    public GameObject HexPrefab = null;

    public GridSetting(Vector2Int size, float сlearance, GameObject hexPrefab)
    {
        Size = size;
        Сlearance = сlearance;
        HexPrefab = hexPrefab;
    }
}
