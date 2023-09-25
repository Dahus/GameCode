using Game.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GlobalData;


namespace Game.GridSystem
{
    public class HexTextureContainer : MonoBehaviour
    {
        [SerializeField] private HexTexturePack texturePack;
        [Header("Biom Textures")]
        private Texture MetalForrestTex;
        private Texture CrystalValleyTex;
        private Texture BattlefieldTex;
        private Texture StoneDesertTex;
        private Texture SwampTex;

        [Header("Relief Textures")]
        private Texture MountainTex;
        private Texture RoughTerrainTex;
        private Texture PlainTex;
        private Texture NaturalTrailsTex;

        [Header("Resources Textures")]
        private Texture MetalTex;
        private Texture EnergyCrystallTex;
        private Texture Wreckage;

        private static HexTextureContainer instance;

        private void Awake()
        {
            instance = this;
        }
        private void OnDestroy()
        {
            instance = null;
        }
        private void Start()
        {
            MetalForrestTex = texturePack.MetalForrestTex;
            CrystalValleyTex = texturePack.CrystalValleyTex;
            BattlefieldTex = texturePack.BattlefieldTex;
            StoneDesertTex = texturePack.StoneDesertTex;
            SwampTex = texturePack.SwampTex;
            MountainTex = texturePack.MountainTex; 
            RoughTerrainTex = texturePack.RoughTerrainTex;
            PlainTex = texturePack.PlainTex;
            NaturalTrailsTex = texturePack.NaturalTrailsTex;
            MetalTex = texturePack.Metal;  
            EnergyCrystallTex = texturePack.EnergyCrystall;
            Wreckage = texturePack.Wreckage;
        }

        public static Texture GetBiomTex(BiomType type)
        {
            return type switch
            {
                (BiomType.MetalForest) => instance.MetalForrestTex,
                (BiomType.CrystalValley) => instance.CrystalValleyTex,
                (BiomType.StoneDesert) => instance.StoneDesertTex,
                (BiomType.Battlefield) => instance.BattlefieldTex,
                (BiomType.Swamp) => instance.SwampTex,
                _ => null,
            };
        }
        public static Texture GetReliefTex(ReliefType type)
        {
            return type switch
            {
                (ReliefType.Mountain) => instance.MountainTex,
                (ReliefType.RoughTerrain) => instance.RoughTerrainTex,
                (ReliefType.Plain) => instance.PlainTex,
                (ReliefType.NaturalTrails) => instance.NaturalTrailsTex,
                _ => null,
            };
        }
        public static Texture GetResourceTex(ResourceType type)
        {
            return type switch
            {
                (ResourceType.Crystal) => instance.EnergyCrystallTex,
                (ResourceType.Metal) => instance.MetalTex,
                (ResourceType.Nanostruct) => instance.Wreckage,
                _ => null,
            };
        }
    }
}