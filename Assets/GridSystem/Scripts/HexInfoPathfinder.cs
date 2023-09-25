using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.GridSystem
{
    [Serializable]
    public class HexInfoPathfinder : IPatency
    {
        [SerializeField] private bool walkable = true;
        private int _modPatency;
        private float _coefPatency;


        public int GetModPatency() => _modPatency;

        public int SetModPatency(int mod) => _modPatency = mod;

        public float GetCoefPatency() => _coefPatency;
        public void SetCoefPatency(float coef) => _coefPatency = coef;
        public bool Walkable { get => walkable; set => walkable = value; }
    }
}
