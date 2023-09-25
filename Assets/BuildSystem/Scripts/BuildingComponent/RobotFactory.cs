using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;

namespace Game.BuildingSystem
{
    public class RobotFactory : AbstractCreateUnitBuild
    {
        public override void CancelFunction()
        {
            _isBroken = true;
        }

        public override void BeginPlayerTurn()
        {
            CreatingUnit();
        }
        public override bool CreateBuilding(GameObject place, PlayerManager player)
        {
            SetupFactoryUnits(player.GetFactoryUnits());
            return base.CreateBuilding(place, player);
        }

        public override void CompletePlayerTurn()
        {
            base.CompletePlayerTurn();
        }
    }
}