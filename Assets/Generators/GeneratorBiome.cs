using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Game.GridSystem;
namespace Game.Generators
{
    [Serializable]
    public class GeneratorBiome
    {

        [SerializeField] private int _dotForBiome;
        // [SerializeField] private int 
        [SerializeField] private int _countBiomeForParty;
        [SerializeField] private List<ListHexes> allHexes = new List<ListHexes>();
        [SerializeField] private List<ListHexes> allHexesWithoutSmallRegion = new List<ListHexes>();
        [SerializeField] private int countStartDot = 0;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private List<GridHex> startHexs;
        [SerializeField] private int[,] _array;
        [SerializeField] private int _min, _max;
        [SerializeField] private int _coef;
        [SerializeField] private int _countBiome;
        [SerializeField] private Color[] color;
        [SerializeField] private int _countGrowth;
        [SerializeField] private int _countGrowth2;
        [SerializeField] private int[] _arrayGrowthBiome;
        [SerializeField] private List<GridHex> _hexBiome1 = new List<GridHex>();
        [SerializeField] private List<GridHex> _hexBiome2 = new List<GridHex>();
        [SerializeField] private List<GridHex> _hexBiome3 = new List<GridHex>();
        [SerializeField] private List<GridHex> _hexBiome4 = new List<GridHex>();
        [SerializeField] private List<GridHex> _hexBiome5 = new List<GridHex>();
        [SerializeField] private List<GridHex> startHexs2 = new List<GridHex>();


        [SerializeField] private List<GridHex> _biomeCrystal = new List<GridHex>();
        [SerializeField] private List<GridHex> _biomeMetal = new List<GridHex>();
        [SerializeField] private List<GridHex> _biomeBattlefield = new List<GridHex>();
        [SerializeField] private List<GridHex> _biomeStoneDesert = new List<GridHex>();
        [SerializeField] private List<GridHex> _biomeSwamp = new List<GridHex>();

        // Start is called before the first frame update
        private GridHex[,] grid;




        public void DistributingDots()
        {
            _countBiomeForParty = _dotForBiome;
            grid = GameGrid.Grid;
            startHexs = new List<GridHex>();
            _array = new int[_size.x, _size.y];

            for (int i = 0; i < _dotForBiome + _countBiome; i++)
            {
                allHexes.Add(new ListHexes(i));
                allHexesWithoutSmallRegion.Add(new ListHexes(i));
            }


            for (int i = 0; i < _size.x; i++)
            {
                for (int j = 0; j < _size.y; j++)
                {
                    grid[i, j].BiomeNumber = -1;
                    // _array[i, j] = -1;
                }
            }
            for (int i = 0; i < _size.x; i++)
            {
                for (int j = 0; j < _size.y; j++)
                {
                    var rand = UnityEngine.Random.Range(_min, _max);
                    if (rand < _coef)
                    {
                        grid[i, j].BiomeNumber = _dotForBiome % color.Length;
                        startHexs.Add(grid[i, j]);
                        startHexs2.Add(grid[i, j]);
                        grid[i, j].NumberDot = _dotForBiome;
                        allHexes[_dotForBiome-1].Add(grid[i, j]);
                        countStartDot++;
                        //_array[i, j] = _dotForBiome % color.Length;
                        _dotForBiome--;
                        if (_dotForBiome <= 0)
                        {
                            break;
                        }
                    }

                }
                if (_dotForBiome <= 0)
                {
                    break;
                }
            }
            _dotForBiome = _countBiome;
            for (int i = _size.x / 2; i < _size.x; i++)
            {
                for (int j = 0; j < _size.y; j++)
                {
                    var rand = UnityEngine.Random.Range(_min, _max);
                    if (rand < _coef)
                    {
                        grid[i, j].BiomeNumber = _dotForBiome % color.Length;
                        startHexs.Add(grid[i, j]);
                        startHexs2.Add(grid[i, j]);
                        grid[i, j].NumberDot = _dotForBiome + _countBiomeForParty;
                        allHexes[_dotForBiome + _countBiomeForParty - 1].Add(grid[i, j]);
                        countStartDot++;
                        //_array[i, j] = _dotForBiome % color.Length;
                        _dotForBiome--;
                        if (_dotForBiome <= 0)
                        {
                            break;
                        }
                    }
                }
                if (_dotForBiome <= 0)
                {
                    break;
                }
            }

            //Log();
            //GrowthBiome();

            //DestroySmallRegion();
            GrowthBiome2();
            DestroySmallBiome(25);
            UpdateCountListHexes(allHexesWithoutSmallRegion);
            Colorize();
           // DebugLogger();
        }

        /*     public void GrowthBiome()
             {
                 int countNullBiom = 100;
                 List<int> visited = new List<int>();
                 while (countNullBiom != 0)
                 {
                     countNullBiom = 0;
                     visited.Clear();
                     for (int i = 0; i < _arrayGrowthBiome.Length; i++)
                     {
                         _arrayGrowthBiome[i] = 0;
                     }
                     for (int i = 0; i < _size.x; i++)
                     {
                         for (int j = 0; j < _size.y; j++)
                         {
                             if (grid[i, j].BiomeNumber != -1 && !grid[i, j].IsUseGeneration && !visited.Contains((grid[i, j].BiomeNumber)))
                             {
                                 grid[i, j].GrowthBiome();
                                 _arrayGrowthBiome[grid[i, j].GetNumberBiome()]++;
                                 if (_arrayGrowthBiome[grid[i, j].GetNumberBiome()] >= _countGrowth)
                                 {
                                     visited.Add(grid[i, j].GetNumberBiome());
                                 }
                                 //visited.Add(_grid[i, j].GetNumberBiome());
                             }
                             if (grid[i, j].GetNumberBiome() == -1)
                             {
                                 countNullBiom++;
                             }
                         }
                     }
                 }
             }
           */

        public void DebugLogger()
        {
            foreach (var hex in startHexs2)
            {
                Debug.LogError(hex.name + " " + hex.NumberDot);
            }
        }
        public void GrowthBiome2()
        {
            //for(int k=0;k<210;k++)
            while (startHexs.Count != 0)
            {
                var count = startHexs.Count;
                for (int i = 0; i < count; i++)
                {
                    var hex = startHexs[0];
                    GrowthBiome(hex);
                    var neighbours = hex.NeighbourArray.Where(h => h.BiomeNumber == hex.BiomeNumber && h.IsUseGeneration == false);
                    foreach (var neighbour in neighbours)
                    {
                        AddDots(neighbour);
                    }
                    startHexs.Remove(hex);
                }
                TakeDots(_hexBiome1);
                TakeDots(_hexBiome2);
                TakeDots(_hexBiome3);
                TakeDots(_hexBiome4);
                TakeDots(_hexBiome5);
            }
        }
        public void Colorize()
        {
            for (int i = 0; i < _size.x; i++)
            {
                for (int j = 0; j < _size.y; j++)
                {

                    var currentNumber = grid[i, j].BiomeNumber;
                    if (currentNumber != -1)
                    {
                        grid[i, j].BiomType = IdentificationBiome(currentNumber);
                        var mat = grid[i, j].gameObject.GetComponent<Renderer>().material;
                        mat.SetTexture("_BiomTex", HexTextureContainer.GetBiomTex(grid[i, j].BiomType));
                        // var mod = currentNumber % color.Length;
                        // grid[i, j].HighLite(color[mod]);
                        // grid[i, j].SetColor(2, color[mod]);
                    }
                    else
                    {
                        // grid[i, j].SetColor(2, Color.white);
                    }
                }

            }
        }

        public void TakeDots(List<GridHex> listBiome)
        {
            var count = listBiome.Count;
            if (count > _countGrowth2)
            {
                count = _countGrowth2;

            }
            for (int i = 0; i < count; i++)
            {
                if (listBiome.Count > 0)
                {
                    var rand = UnityEngine.Random.Range(0, listBiome.Count);
                    var hex = listBiome[rand];
                    listBiome.Remove(hex);
                    startHexs.Add(hex);
                }
            }
        }

        public void AddDots(GridHex hex)
        {

            switch (hex.BiomeNumber)
            {
                case 0:
                    _hexBiome1.Add(hex);
                    break;
                case 1:
                    _hexBiome2.Add(hex);
                    break;
                case 2:
                    _hexBiome3.Add(hex);
                    break;
                case 3:
                    _hexBiome4.Add(hex);
                    break;
                case 4:
                    _hexBiome5.Add(hex);
                    break;
            }
        }

        public void GrowthBiome(GridHex gridhex)
        {
            gridhex.IsUseGeneration = true;
            foreach (GridHex hex in gridhex.NeighbourArray)
            {
                if (hex.BiomeNumber == -1)
                {
                    hex.BiomeNumber = gridhex.BiomeNumber;
                    hex.NumberDot = gridhex.NumberDot;
                    allHexes[gridhex.NumberDot - 1].Add(hex);
                }
            }
        }

        public void DestroySmallBiome(int countHex)
        {

            var listRemoved = new List<GridHex>();
            int numberDotRemoved;
            int counter = 0;
            foreach (var listHexes in allHexes)
            {
                if (countHex <= listHexes.listhexs.Count)
                {
                    counter++;
                    continue;
                }


                listRemoved.Clear();
                foreach (var hex in listHexes.listhexs)
                {
                    listRemoved.Add(hex);
                }
                int testCounter = 0;
                numberDotRemoved = listRemoved[1].NumberDot;
                do
                {
                    foreach (var hex in listRemoved)
                    {
                        testCounter = 0;
                        if (hex.NumberDot == numberDotRemoved)
                        {
                            testCounter++;
                        }

                        foreach (var neighbour in hex.NeighbourArray)
                        {
                            if (neighbour.NumberDot != numberDotRemoved)
                            {
                                hex.NumberDot = neighbour.NumberDot;
                                hex.BiomeNumber = neighbour.BiomeNumber;
                                break;
                            }
                        }
                    }

                }
                while (testCounter != 0);
                counter++;
              
            }


            int number;
            for (int i = 0; i < _size.x; i++)
            {
                for (int j = 0; j < _size.y; j++)
                {
                    number = grid[i, j].NumberDot;
                    allHexesWithoutSmallRegion[number - 1].Add(grid[i, j]);
                    allHexesWithoutSmallRegion[number - 1].biom = IdentificationBiome(grid[i, j].BiomeNumber);
                    AddBiomeList(grid[i, j].BiomeNumber, grid[i, j]);
                }
            }
        }

        public List<ListHexes> UpdateCountListHexes(List<ListHexes> list)
        {
            var count = list.Count;

            for (int i = count - 1; i >= 0; i--)
            {
                if (list[i].listhexs.Count == 0)
                {
                    list.RemoveAt(i);
                }
            }
            return list;
        }


        public List<ListHexes> GetListHexes() => allHexesWithoutSmallRegion;


        public void AddBiomeList(int numberBiome, GridHex hex)
        {
            switch (numberBiome)
            {
                case 0:
                    _biomeMetal.Add(hex);
                    break;

                case 1:
                    _biomeCrystal.Add(hex);
                    break;

                case 2:
                    _biomeBattlefield.Add(hex);
                    break;

                case 3:
                    _biomeStoneDesert.Add(hex);
                    break;

                case 4:
                    _biomeSwamp.Add(hex);
                    break;

            }
        }


        public BiomType IdentificationBiome(int numberBiome)
        {
            switch (numberBiome)
            {
                case 0:
                    return BiomType.MetalForest;

                case 1:
                    return BiomType.CrystalValley;

                case 2:
                    return BiomType.Battlefield;

                case 3:
                    return BiomType.StoneDesert;

                case 4:
                    return BiomType.Swamp;

            }
            return BiomType.MetalForest;
        }

    }

    [System.Serializable]
    public class ListHexes
    {
        public List<GridHex> listhexs = new List<GridHex>();
        public int number;
        public BiomType biom;

        public ListHexes(int numb)
        {
            number = numb;
        }
        public void Add(GridHex hex)
        {
            listhexs.Add(hex);
        }

        

    }
}
