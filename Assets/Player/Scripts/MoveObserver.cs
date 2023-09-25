using System.Collections;
using System.Collections.Generic;
using Game.GridSystem;
using Game.UnitsSystem;
using UnityEngine;

namespace Game.Player
{
    public class MoveObserver : MonoBehaviour
    {
        [SerializeField] private GridHex _endHex;
        [SerializeField] private AbstractUnit _unit;

        public void SetEndHex(GridHex endHex)
        {
                _endHex = endHex;
                _unit.Movement(endHex);
                Observer.instance.SetStateObserver(StateObserver.Standart);
        }

        
        public void SetUnit(AbstractUnit unit)
        {
            Debug.LogError("Добавил юнита");
            _unit = unit;
        }

    }
}
