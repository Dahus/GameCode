using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.GridSystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ReliefAndBiom", order = 1)]
    public class ReliefAndBiom : ScriptableObject
    {
        public Sprite SpriteHex;
        public BiomType BiomHex;
        public int TypeBiom;
        public int TypeRelief;
        //public int coefResource;
    }
}