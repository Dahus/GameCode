using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Game.GlobalSystems;
using Game.GridSystem;

namespace Game.Generators
{
    [Serializable]
    public class GeneratorResource
    {

       // Dictionary<int, List<GridHex>> allRegions = new Dictionary<int, List<GridHex>>();
        //[SerializeField] private List<ListHexes> allHexes = new List<ListHexes>();
        [SerializeField] private List<ListHexes> hexes = new List<ListHexes>();
        GridHex[,] grid;
        [SerializeField] private List<GridHex> hexResources;

        [SerializeField] private BiomeParameters metalForest;
        [SerializeField] private BiomeParameters crystalValley;
        [SerializeField] private BiomeParameters battlefield;
        [SerializeField] private BiomeParameters stoneDesert;
        [SerializeField] private BiomeParameters swomp;




        public void Setup(int count)
        {
            //CreateDictionaryRegions((int)GameGrid.GridSize.x, (int)GameGrid.GridSize.y);
            hexes = EntityLocator.instance.GetStorageGenerators().GetListHexes();
            ResourceAllocation(count);
            ResourceSaturation();
        }
        public void CreateDictionaryRegions(int width, int height)
        {
          


/*            for (int i = 0; i < 100; i++) 
            {
                //allHexes.Add(new ListHexes(i));
            }
            grid = GameGrid.Grid;
            int numberDot;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    numberDot = grid[i, j].NumberDot;
                    if (!allRegions.ContainsKey(numberDot))
                    {
                        allRegions.Add(numberDot, new List<GridHex>());
                        hexes.Add(new ListHexes(numberDot));
                       // allHexes.Add(new ListHexes(numberDot));
                    }
                    allRegions[numberDot].Add(grid[i, j]);
                   
                   // allHexes[numberDot].Add(grid[i,j]);
                }
            }
            Debug.LogError("Количество  регионов " + allRegions.Count);*/
        }



        public void ResourceAllocation(int countVeins)
        {
            hexResources = new List<GridHex>();
            int countHex;
            int rand;
            int number = 0;
            foreach (var listHex in hexes)
            {
                countHex = listHex.listhexs.Count;
                for (int i = 0; i < countVeins; i++)
                {
                    rand = UnityEngine.Random.Range(0, countHex);
                    if (!hexResources.Contains(listHex.listhexs[rand]))
                    {
                        hexResources.Add(listHex.listhexs[rand]);
                        hexes[number].Add(listHex.listhexs[rand]);
                    }
                }
                number++;
            }
        }

        public void ResourceSaturation()
        {
            int rand;
            foreach (var hex in hexResources)
            {
                hex.ResourceType = ResourceType.Crystal;
                hex.ResourceHex.SetupResource(0, 0, 0, 1000, 0);
                /*rand = UnityEngine.Random.Range(0, 2);
                switch (rand)
                {
                    case 0:
                        hex.ResourceType = ResourceType.Metal;
                        hex.ResourceHex.SetupResource(1000, 0, 0, 0, 0);
                        break;
                    case 1:
                        hex.ResourceType = ResourceType.Crystal;
                        hex.ResourceHex.SetupResource(0, 0, 0, 1000, 0);
                        break;
                    case 2:
                        hex.ResourceType = ResourceType.Nanostruct;
                        hex.ResourceHex.SetupResource(0, 0, 0, 0, 1000);
                        break;
                }
                */
            }
        }



    }
    [System.Serializable]
    public struct BiomeParameters
    {
        public float coefBiome;
        public float coefMetall;
        public float coefCrystal;
        public float coefWreckage;
        public float hexCountToVein;

    }
   
}
