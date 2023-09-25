using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

namespace Game.BuildingSystem
{
    public interface IBuild
    {
        public void DestroyBuilding();

        public void UpgradeBuilding(Dictionary<string, int> res);

        public Dictionary<string, int> DisassemblyBuilding();

        public bool BuildBuilding(GameObject place, PlayerManager player);
    }
}