using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Game.GridSystem;
namespace Game.Generators
{
    [Serializable]
    public class GeneratorRelief
    {


        public ParametresNoise parametresNoise;

        public List<TerrainLevel> terrainLevel;
        public Color currentColor;
        public int currentNumberRelief;
        GridHex[,] grid;


        public void SetupCoefNoise(ParametresNoise parametres)
        {
            parametresNoise.width = parametres.width;
            parametresNoise.height = parametres.height;
            parametresNoise.seed = parametres.seed;
            parametresNoise.scale = parametres.scale;
            parametresNoise.octaves = parametres.octaves;
            parametresNoise.persistence = parametres.persistence;
            parametresNoise.lacunarity = parametres.lacunarity;
            parametresNoise.offset = parametres.offset;
        }






        public void CreateRelief(GeneratorNoise generatorNoise)
        {
            float[] noiseMap = generatorNoise.GenerationNoise(parametresNoise.width, parametresNoise.height, parametresNoise.seed, parametresNoise.scale,
                parametresNoise.octaves, parametresNoise.persistence, parametresNoise.lacunarity, parametresNoise.offset);
            //ApplyColorMap(width, height, GenerateColorMapRelief(noiseMap));
            grid = GameGrid.Grid;
            for (int i = 0; i < parametresNoise.width; i++)
            {
                for (int j = 0; j < parametresNoise.height; j++)
                {
                    currentColor = terrainLevel[terrainLevel.Count - 1].colorRelief;
                    foreach (var level in terrainLevel)
                    {
                        if (noiseMap[i + j * parametresNoise.width] < level.height)
                        {
                            currentColor = level.colorRelief;
                            currentNumberRelief = level.numberRelief;
                            break;
                        }
                    }
                    grid[i, j].HexInfoPathfinder.SetCoefPatency(currentNumberRelief);
                    grid[i, j].ReliefNumber = currentNumberRelief;
                    grid[i, j].ReliefType = IdentificationRelief(currentNumberRelief);
                    var mat = grid[i, j].gameObject.GetComponent<Renderer>().material;
                    mat.SetTexture("_ReliefTex", HexTextureContainer.GetReliefTex(grid[i, j].ReliefType));
                    
                    if (currentNumberRelief == 0)
                    {
                        grid[i, j].HexInfoPathfinder.Walkable = false;
                    }
                    grid[i, j].HexInfoPathfinder.SetCoefPatency(currentNumberRelief);
                }
            }
        }

        public void Colorize()
        {
            for (int i = 0; i < parametresNoise.width; i++)
            {
                for (int j = 0; j < parametresNoise.height; j++)
                {
                    grid[i, j].HighLite(terrainLevel[grid[i, j].ReliefNumber].colorRelief);
                }
            }
        }

        public ReliefType IdentificationRelief(int numberRelief)
        {
            switch (numberRelief)
            {
                case 0:
                    return ReliefType.Mountain;
                case 1:
                    return ReliefType.NaturalTrails;
                case 2:
                    return ReliefType.Plain;
                case 3:
                    return ReliefType.RoughTerrain;


            }
            return ReliefType.Mountain;
        }
    }

    [Serializable]
    public struct TerrainLevel
    {
        public string name;
        public int numberRelief;
        public float height;
        public Color colorRelief;
    }
    [Serializable]
    public struct ParametresNoise
    {
        public int width;
        public int height;
        public float scale;

        public int octaves;
        public float persistence;
        public float lacunarity;

        public int seed;
        public Vector2 offset;
    }
}
