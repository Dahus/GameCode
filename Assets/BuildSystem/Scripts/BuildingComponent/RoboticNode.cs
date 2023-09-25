using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;
using Game.GlobalData;
using Game.UnitsSystem;

namespace Game.BuildingSystem
{
    public class RoboticNode : AbstractCreateUnitBuild
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

        public override void SetupActions()
        {
            actionsBuilds = new List<IUserAction>();
            var actionCreateUnit = new CreateUnitAction(Observer.instance.GetPlayerObserver().GetPlayerManager(), this, ConstantNameUnit.SNIPERDRONE);
            actionsBuilds.Add(actionCreateUnit);
        }

        public override List<IUserAction> GetActions()
        {
            return actionsBuilds;
        }
    }
}
