using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;
using Game.GlobalData;
using Game.Economics;

namespace Game.BuildingSystem
{
    public class RobotFerm : AbstractBuild
    {

        [SerializeField] private int addCountRobots;
      
        public override bool CreateBuilding(GameObject place, PlayerManager player)
        {
           

            player.GetResources().SetMaxWorkers(player.GetResources().GetMaxWorkers() + addCountRobots);
            //DictionaryResource.AddAttributeDictionary(player.GetResources(), countRobots);
            player.UpdateResource();
            base.CreateBuilding(place, player);
            return true;
        }
    }
}
