using Game.GridSystem;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public static class ColorizeHexes
{
    public static void Colorize(PlayerManager player)
    {
        var map = player.GetObserverMap().HexObserveData;
        var grid = GameGrid.Grid;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                var hex = grid[i, j];
                var objInHex = hex.ObjectsInHex;
                foreach (var obj in objInHex)
                {
                    var objRenderers = obj.GetComponentsInChildren<Renderer>();
                    foreach (var renderer in objRenderers)
                    {
                        renderer.enabled = map[i, j].observeStatus == HexObserveStatus.Observed;
                    }
                }
                var color = hex.GetComponent<SpriteRenderer>().material;
                color.SetInt("_VisibilityType", 2 /*(int)map[i, j].observeStatus*/);
            }
        }
    }

    public static void Colorize(List<GridHex> hexes, PlayerManager player)
    {
        var map = player.GetObserverMap().HexObserveData;
        foreach (var hex in hexes)
        {
            var color = hex.GetComponent<SpriteRenderer>().material;
            var pos = hex.PositionInGrid;
            color.SetInt("_VisibilityType", 2 /*(int)map[pos.x, pos.y].observeStatus*/);
            var objInHex = hex.ObjectsInHex;
            foreach (var obj in objInHex)
            {
                var objRenderers = obj.GetComponentsInChildren<Renderer>();
                foreach (var renderer in objRenderers)
                {
                    renderer.enabled = map[pos.x, pos.y].observeStatus == HexObserveStatus.Observed;
                }
            }
        }
    }
}
