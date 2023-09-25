using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GridSystem
{
    [Serializable]
    public class GridHexInfo : ICloneable
    {
        public BiomType BiomType;
        public ReliefType ReliefType;
        public ResourceType ResourceType;
        public int id;

        public GridHexInfo() 
        {
            BiomType = BiomType.MetalForest;
            ReliefType = ReliefType.Mountain;
            ResourceType = ResourceType.Crystal;
            id = 10;
        }

        public object Clone()
        {
            return new GridHexInfo
            {
                BiomType = BiomType,
                ReliefType = ReliefType,
                ResourceType = ResourceType
            };
        }
    }
}
