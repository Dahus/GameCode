using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.GlobalSystems;
public class ObserverUpdateUI : MonoBehaviour
{
   

    public void UpdateQueueLogicUI()
    {
        EntityLocator.instance.GetButtonLogic().UpdateQueueLogicManager();
    }
}
