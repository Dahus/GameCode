using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;

public class GameObserver : MonoBehaviour
{
  

    public void CCDestroy(PlayerManager player)
    {
        Debug.Log(player);
    }


    public bool CheckUnitTypeTwo(PlayerManager player)
    {
        foreach(var unit in player.GetPlayerUnits())
        {
            if (unit.GetLevel() == 2)
            {
                return true;
            }
        }
        return false;
    }
}
