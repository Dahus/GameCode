using System;

namespace Game.Grid
{
    [Serializable]
    public class HexType
    {
        public enum hexType
        {
            Grass,
            Desert,
            HexResource
        }

        public sbyte dificultHex;
    }
}