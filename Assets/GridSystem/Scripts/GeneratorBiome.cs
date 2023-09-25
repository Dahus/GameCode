using System.Collections;
using System.Collections.Generic;
using Game.GridSystem;
using UnityEditor;
using UnityEngine;
using System;
using Game.Generators;
namespace Game.GridSystem
{
  /*  [Serializable]
    public struct TerrainLevel
    {
        public string name;
        public int numberRelief;
        public float height;
        public Color colorRelief;
    }*/
    [Serializable]
    public struct Biome
    {
        public string name;
        public int numberBiome;
        public float height;
        public Color colorBiome;
    }

    public class GeneratorBiome : MonoBehaviour
    {
        [SerializeField] public int width;
        [SerializeField] public int height;
        [SerializeField] public float scale;

        [SerializeField] public int octaves;
        [SerializeField] public float persistence;
        [SerializeField] public float lacunarity;

        [SerializeField] public int seed;
        [SerializeField] public Vector2 offset;


        [SerializeField] public List<TerrainLevel> terrainLevel = new List<TerrainLevel>();


       
        [SerializeField] public float scaleBiome;

        [SerializeField] public int octavesBiome;
        [SerializeField] public float persistenceBiome;
        [SerializeField] public float lacunarityBiome;
        [SerializeField] public Vector2 offsetBiome;

        [SerializeField] public List<Biome> biome = new List<Biome>();

        private ReliefType currentRelief = ReliefType.NaturalTrails;

        private GeneratorNoise _generatorNoise;
        private GridHex[,] _grid;
        private Color _currentColor;
        
        private int _currentNumberRelief;
        private SpriteRenderer _spriteRenderer;
        private List<GridHex> _neighbours = new List<GridHex>();
        private Vector2 _pos;

        private int _reliefNumber;
        private int[] _countNumberRelief = new int[4];
        private int _maxCountNumberRelief = 0;

        private void Awake()
        {
            _generatorNoise = GetComponent<GeneratorNoise>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        public void CreateRelief()
        {
            float[] noiseMap = _generatorNoise.GenerationNoise(width, height, seed, scale, octaves, persistence, lacunarity, offset);
            ApplyColorMap(width, height, GenerateColorMapRelief(noiseMap));
            _grid = GameGrid.Grid;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _currentColor = terrainLevel[terrainLevel.Count - 1].colorRelief;
                    foreach (var level in terrainLevel)
                    {
                        if (noiseMap[i + j * width] < level.height)
                        {
                            _currentColor = level.colorRelief;
                            _currentNumberRelief = level.numberRelief;
                            break;
                        }
                    }
                    _grid[i, j].GetComponent<SpriteRenderer>().color = _currentColor;
                   // _grid[i, j].RememberColor(_currentColor);
                   // _grid[i, j].GetComponent<GridHexForCreate>().SetNumberRelief(_currentNumberRelief);
                    _grid[i, j].HexInfoPathfinder.SetCoefPatency(_currentNumberRelief); //тестируем
                    if (_currentNumberRelief == 0)
                    {
                        _grid[i, j].HexInfoPathfinder.Walkable = false;
                    }
                    _grid[i, j].HexInfoPathfinder.SetCoefPatency(_currentNumberRelief);
                }
            }
        }


        public void CreateBiome()
        {
            float[] noiseMap = _generatorNoise.GenerationNoise(width, height, seed, scaleBiome, octavesBiome, persistenceBiome, lacunarityBiome, offsetBiome);
            //ApplyColorMap(width, height, GenerateColorMapBiome(noiseMap));
            _grid = GameGrid.Grid;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _currentColor = biome[biome.Count - 1].colorBiome;
                    foreach (var level in biome)
                    {
                        if (noiseMap[i + j * width] < level.height)
                        {
                            _currentColor = level.colorBiome;
                            //_currentNumberRelief = level.numberRelief;
                            break;
                        }
                    }
                    _grid[i, j].GetComponent<SpriteRenderer>().color = _currentColor;
                    //_grid[i, j].RememberColor(_currentColor);
                    // _grid[i, j].GetComponent<GridHexForCreate>().SetNumberRelief(_currentNumberRelief);
                }
            }
        }

            


        private void ApplyColorMap(int width, int height, Color[] colors)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;
            texture.SetPixels(colors);
            texture.Apply();

            _spriteRenderer.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f); ;
        }

        private Color[] GenerateColorMapRelief(float[] noiseMap)
        {
            Color[] colorMap = new Color[noiseMap.Length];
            for (int i = 0; i < noiseMap.Length; i++)
            {
                // Ѕазовый цвет с самым высоким диапазоном значений
                colorMap[i] = terrainLevel[terrainLevel.Count - 1].colorRelief;
                foreach (var level in terrainLevel)
                {
                    // ≈сли шум попадает в более низкий диапазон, то используем его
                    if (noiseMap[i] < level.height)
                    {
                        colorMap[i] = level.colorRelief;
                        break;
                    }
                }
            }

            return colorMap;
        }

        private Color[] GenerateColorMapBiome(float[] noiseMap)
        {
            Color[] colorMap = new Color[noiseMap.Length];
            for (int i = 0; i < noiseMap.Length; i++)
            {
                // Ѕазовый цвет с самым высоким диапазоном значений
                colorMap[i] = biome[biome.Count - 1].colorBiome;
                foreach (var level in biome)
                {
                    // ≈сли шум попадает в более низкий диапазон, то используем его
                    if (noiseMap[i] < level.height)
                    {
                        colorMap[i] = level.colorBiome;
                        break;
                    }
                }
            }

            return colorMap;
        }





        /*public void CreateBiome(Vector2Int vector, Pathfinding pathfinding)
        {
            grid = GameGrid.Grid;
            _pathfinding = pathfinding;
            _vector2Int = vector;
            _countHex = _vector2Int.x * _vector2Int.y;
            for (int i = 0; i < _propertyBiome.Count; i++)
            {
                _countHexInBiome.Add((int)(_countHex * _propertyBiome[i]) / 100);
            }

            int count = 0;
            foreach (var hexBiom in _countHexInBiome)
            {
                count += hexBiom;
            }

            int hexRaznitsa = _countHex - count;

            _countHexInBiome[countRegion - 1] += hexRaznitsa;
            while (_countHex != 0)
            {
                if (k != countRegion)
                    for (int i = 0; i < _vector2Int.x; i++)
                    {
                        for (int j = 0; j < _vector2Int.y; j++)
                        {
                            if (grid[j, i].GetComponent<GridHexForCreate>().GetIdRegion() == -1)
                            {
                                grid[j, i].GetComponent<GridHexForCreate>().SetIdRegion(k);
                                grid[j, i].GetComponent<SpriteRenderer>().color = colors[k];
                                _countHexInBiome[k]--;
                                _countHex--;
                            }

                            Debug.LogError("Test");
                            if (_flag)
                            {
                                if (Random.Range(0, 100) < 15)
                                    i = DownGenerate(i);
                            }
                            else
                            {
                                if (Random.Range(0, 100) < 15)
                                    i = UpGenerate(i);
                            }


                            if (_countHexInBiome[k] == 0)
                            {
                                k++;
                            }
                        }
                    }

                if (k == countRegion)
                {
                    break;
                }
            }
        }

        private int DownGenerate(int i)
        {
            Debug.LogError("Test1");
            if (i == _vector2Int.x - 1)
            {
                _flag = false;
            }

            if (i < _vector2Int.x)
            {
                i++;
                return i;
            }

            return i;
        }

        private int UpGenerate(int i)
        {
            if (i == 1)
            {
                _flag = false;
            }

            if (i < _vector2Int.x)
            {
                i--;
                return i;
            }

            return i;
        }*/
        //private void RegionsInBiome()
        //{

        //}


        /*     private void CreateRegion(int idRegion, Color currentColor)
             {
                 hex = grid[Random.Range(0, _vector2Int.x), Random.Range(0, _vector2Int.y)];
                 if (hex.GetComponent<GridHexForCreate>().GetIdRegion() != -1)
                 {
                     CreateRegion(idRegion,currentColor);
                 }
     
                 _hexsForCreateBorder = _pathfinding.GetComponent<WavedAlgorithm>().SearchAllHexesForAction(hex, 10);
                
                 
                 for (int i = 0; i < _hexsForCreateBorder.Count; i++)
                 {   
                     _hexsForCreateBorder[i].GetComponent<GridHexForCreate>().SetIdRegion(idRegion);
                     _hexsForCreateBorder[i].GetComponent<SpriteRenderer>().color = currentColor;
                 }
             }
             */

        /*  
        private void AlgorithmBorders(int idRegion, Color currentColor)
        {
            hex = grid[Random.Range(0, _vector2Int.x), Random.Range(0, _vector2Int.y)];
            if (hex.GetComponent<GridHexForCreate>().GetIdRegion() != -1)
            {
                AlgorithmBorders(idRegion,currentColor);
            }
            
            
            bool flag;
            for (int k = 0; k < 8; k++)
            {
                Vector2Int current =
                    GridGenerator.ConvertPosToIndex(new Vector2(hex.transform.position.x, hex.transform.position.y));

                flag = true;
                while (flag)
                {
                    flag = false;
                    int rand1 = Random.Range(0, 10);
                    int rand2 = Random.Range(0, 10);

                    current.x += rand1;
                    current.y += rand2;


                    if (current.x >= _vector2Int.x || current.y >= _vector2Int.y)
                    {
                        flag = true;
                    }

                    if (current.x < 0 || current.y < 0)
                    {
                        flag = true;
                    }
                }

                GridHex endhex = grid[current.x, current.y];
                _hexsForCreateBorder = _pathfinding.FindPath(hex, endhex);
                hex = endhex;
                for (int i = 0; i < _hexsForCreateBorder.Count; i++)
                {   
                    _hexsForCreateBorder[i].GetComponent<GridHexForCreate>().SetIdRegion(idRegion);
                    _hexsForCreateBorder[i].GetComponent<SpriteRenderer>().color = currentColor;
                }
                
            }
            
        }
        */
    }
}