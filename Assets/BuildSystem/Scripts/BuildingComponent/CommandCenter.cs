using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;
using Game.GlobalData;
using Game.Grid;
using Game.GridSystem;
using Game.GlobalSystems;
using Game.UnitsSystem;
namespace Game.BuildingSystem
{
    public class CommandCenter : AbstractCreateUnitBuild, IAttackBuildingFunction
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
            SetupActions();
            SetupFactoryUnits(player.GetFactoryUnits());
            return base.CreateBuilding(place, player);
        }
        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void SetupActions()
        {
            base.SetupActions();
        }

        public override List<IUserAction> GetActions()
        {
            return actionsBuilds;
        }
    }
}