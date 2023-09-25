using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GridSystem;
using Game.Player;
using Game.UnitsSystem;
using Game.GlobalData;
using Game.BuildingSystem;

public class WavedAlgorithm : MonoBehaviour
{
    [SerializeField] List<GridHex> neighbourPoints = new List<GridHex>();
    [SerializeField] Color currentColor;

    public List<GridHex> SearchHexesWithNotWalkalbe(GridHex start, int radius)
    {
        if (radius <= 1)
        {
            return start.NeighbourArray;
        }
        int iterationRange = 1;
        List<GridHex> neigbourArray = new List<GridHex>();
        neigbourArray.AddRange(start.NeighbourArray);
        while (iterationRange != radius)
        {
            List<GridHex> temp = new List<GridHex>();
            foreach (GridHex hex in neigbourArray)
            {
                foreach (GridHex neigbour in hex.NeighbourArray)
                {
                    if (!neigbourArray.Contains(neigbour) && neigbour != start && !temp.Contains(neigbour))
                        temp.Add(neigbour);
                }
            }
            neigbourArray.AddRange(temp);
            iterationRange++;
        }
        
        return neigbourArray;
    }

    public List<GridHex> SearchHexesWithoutNotWalkalbe(GridHex start, int radius)
    {
        if (radius <= 1)
        {
            return start.NeighbourArray;
        }
        int iterationRange = 1;
        List<GridHex> neigbourArray = new List<GridHex>();
        neigbourArray.AddRange(start.NeighbourArray);
        while (iterationRange != radius)
        {
            List<GridHex> temp = new List<GridHex>();
            foreach (GridHex hex in neigbourArray)
            {
                foreach (GridHex neigbour in hex.NeighbourArray)
                {
                    if (!neigbourArray.Contains(neigbour) && neigbour != start && neigbour.HexInfoPathfinder.Walkable && !temp.Contains(neigbour))
                        temp.Add(neigbour);
                }
            }
            neigbourArray.AddRange(temp);
            iterationRange++;
        }

        return neigbourArray;
    }
}
