using UnityEngine;


namespace Game.GridSystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "TextureData", order = 1)]
    public class HexTexturePack : ScriptableObject
    {
        [Header("Biom Textures")]
        public Texture MetalForrestTex;
        public Texture CrystalValleyTex;
        public Texture BattlefieldTex;
        public Texture StoneDesertTex;
        public Texture SwampTex;

        [Header("Relief Textures")]
        public Texture MountainTex;
        public Texture RoughTerrainTex;
        public Texture PlainTex;
        public Texture NaturalTrailsTex;

        [Header("Resources Textures")]
        public Texture Metal;
        public Texture EnergyCrystall;
        public Texture Wreckage;
    }
}