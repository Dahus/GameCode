using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Game.GlobalData;
using Game.GridSystem;
using Game.Player;
using Game.FightSystem;


namespace Game.UnitsSystem
{
    public class Paratrooper : AbstractUnit, IAction
    {

        [SerializeField] private int radiusJump;
        [SerializeField] private int timeKDJump;
        [SerializeField] private int currentTimeKD = 0;
        [SerializeField] private int pointsJump;
        [SerializeField] private bool isKDJump;
        [SerializeField] private List<GridHex> hexForJump = new List<GridHex>();



        public void Jump()
        {
            Observer.instance.GetActionObserver().SetObjectAction(this);         
        }

        public int GetPointsJump() => pointsJump;
        public bool GetKDJump() => isKDJump;

        public void Execute(GridHex hex)
        {
            hexForJump = _wavedAlgorithm.SearchHexesWithNotWalkalbe(_startHex, radiusJump);
            
            if (hexForJump.Contains(hex) && Observer.instance.Check(hex))
            {
                isKDJump = false;
                _startHex.RemoveObjectInHex(gameObject);
                _startHex = hex;
                _startHex.AddObjectInHex(gameObject);
                gameObject.transform.position = hex.transform.position;
                ActionPoints -= pointsJump;
                SetStateUnit(StateUnit.Nun);
            }
        }

        public override void BeginPlayerTurn()
        {
            base.BeginPlayerTurn();
            currentTimeKD++;
            if (currentTimeKD >= timeKDJump)
            {
                currentTimeKD = 0;
                isKDJump = true;
            }
        }
    }
}
