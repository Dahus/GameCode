using Game.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class DiagramaVoronoi : MonoBehaviour
{
    [SerializeField] private int _dotForBiome;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private List<GridHex> _startHexs;
    [SerializeField] private int[,] _array;
    [SerializeField] private int _min, _max;
    [SerializeField] private int _coef;
    [SerializeField] private Text _textTest;
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

    private GridHex[,] _grid;
}
/*
    public void DistributingDots()
    {
        _grid = GameGrid.Grid;
        _startHexs = new List<GridHex>();
        _array = new int[_size.x, _size.y];

        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                _grid[i, j].SetNumberBiome(-1);
                _array[i, j] = -1;
            }
        }
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                var rand = Random.Range(_min, _max);
                if (rand < _coef)
                {
                    _grid[i, j].SetNumberBiome(_dotForBiome % color.Length);
                    _startHexs.Add(_grid[i, j]);
                    _array[i, j] = _dotForBiome % color.Length;
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
                var rand = Random.Range(_min, _max);
                if (rand < _coef)
                {
                    _grid[i, j].SetNumberBiome(_dotForBiome % color.Length);
                    _startHexs.Add(_grid[i, j]);
                    _array[i, j] = _dotForBiome % color.Length;
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
        Log();
        //GrowthBiome();

        //DestroySmallRegion();
        GrowthBiome2();
        Colorize();
    }

    public void GrowthBiome()
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
                    if (_grid[i, j].GetNumberBiome() != -1 && !_grid[i, j].GetUseBiome() && !visited.Contains((_grid[i, j].GetNumberBiome())))
                    {
                        _grid[i, j].GrowthBiome();
                        _arrayGrowthBiome[_grid[i, j].GetNumberBiome()]++;
                        if (_arrayGrowthBiome[_grid[i, j].GetNumberBiome()] >= _countGrowth)
                        {
                            visited.Add(_grid[i, j].GetNumberBiome());
                        }
                        //visited.Add(_grid[i, j].GetNumberBiome());
                    }
                    if (_grid[i, j].GetNumberBiome() == -1)
                    {
                        countNullBiom++;
                    }
                }
            }
        }
    }

    public void GrowthBiome2()
    {
        //for(int k=0;k<210;k++)
        while (_startHexs.Count != 0)
        {
            var count = _startHexs.Count;
            for (int i = 0; i < count; i++)
            {
                var hex = _startHexs[0];
                hex.GrowthBiome();
                var neighbours = hex.NeighbourArray.Where(h => h.GetNumberBiome() == hex.GetNumberBiome() && h.GetUseBiome() == false);
                foreach (var neighbour in neighbours)
                {
                    AddDots(neighbour);
                }
                _startHexs.Remove(hex);
            }
            TakeDots(_hexBiome1);
            TakeDots(_hexBiome2);
            TakeDots(_hexBiome3);
            TakeDots(_hexBiome4);
            TakeDots(_hexBiome5);
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
                var rand = Random.Range(0, listBiome.Count);
                var hex = listBiome[rand];
                listBiome.Remove(hex);
                _startHexs.Add(hex);
            }
        }
    }

    public void AddDots(GridHex hex)
    {

        switch (hex.GetNumberBiome())
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

    public void DestroySmallRegion()
    {
        foreach (GridHex hex in _startHexs)
        {
            if (!hex.CheckNeighbourBiome())
            {
                hex.SetNumberBiome(hex.GetRandomNeighbour().GetNumberBiome());
            }
        }
    }


    public void Colorize()
    {
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                var currentNumber = _grid[i, j].GetNumberBiome();
                if (currentNumber != -1)
                {
                    var mod = currentNumber % color.Length;
                    _grid[i, j].SetColorHex(color[mod]);
                }
                else
                {
                    _grid[i, j].SetColorHex(Color.white);
                }
            }

        }
    }
    public void Log()
    {
        _textTest.text = "";
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                _textTest.text = _textTest.text + _array[i, j] + " ";
            }
            _textTest.text = _textTest.text + "/n";
        }
    }
}
  */