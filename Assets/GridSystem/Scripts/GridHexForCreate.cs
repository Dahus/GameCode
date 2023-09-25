using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GridSystem
{
    public class GridHexForCreate : MonoBehaviour
    {
        [SerializeField] private int _idRegion = -1;

        [SerializeField] private int _numberRelief = -1;


       

        public int GetIdRegion()
        {
            return _idRegion;
        }

        public void SetIdRegion(int idRegion)
        {
            _idRegion = idRegion;
        }

        public int GetNumberRelief()
        {
            return _numberRelief;
        }

        public void SetNumberRelief(int numberRelief)
        {
            _numberRelief = numberRelief;
        }


        
    }
}
