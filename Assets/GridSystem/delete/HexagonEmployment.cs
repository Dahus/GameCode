using UnityEngine;

namespace Game.Grid
{
    public class HexagonEmployment : MonoBehaviour
    {
        [SerializeField] private EnumStateHexagonEmployment _stateEmployment = EnumStateHexagonEmployment.None;

        public void UpdateState(EnumStateHexagonEmployment state)
        {
            _stateEmployment = state;
        }


        public EnumStateHexagonEmployment GetStateEmployment()
        {
            return _stateEmployment;
        }
    }
}