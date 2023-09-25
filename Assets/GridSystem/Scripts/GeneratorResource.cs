using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Economics;
using Game.Grid;

namespace Game.GridSystem
{
    public class GeneratorResource : MonoBehaviour
    {
       

        [SerializeField] private int _coefMetal;
        [SerializeField] private int _coefCrystal;
        [SerializeField] private int _coefNanostruct;
        [SerializeField] private int _countResource;
        [SerializeField] private Color _colorMetal;
        [SerializeField] private Color _colorCristal;
        [SerializeField] private Color _colorNanostruct;
        [SerializeField] private Color _currentColor;
        [SerializeField] private ResourceType _currentResourceType;
        public void CreateGridResource()
        {
            for (int i = 0; i < GameGrid.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < GameGrid.Grid.GetLength(1); j++)
                {
                    GameGrid.Grid[i, j].ResourceHex = CreateResourceInHex(GameGrid.Grid[i, j].ResourceHex);
                    //GameGrid.Grid[i, j].SetColorHex(GetColor());
                    // Debug.LogError(GameGrid.Grid[i, j].ResourceType);
                    GameGrid.Grid[i,j].HexInfo.ResourceType=_currentResourceType;
                }
            }
        }

        private DictionaryResource CreateResourceInHex(DictionaryResource resourceHex)
        {               
            int rand = Random.Range(0, 100);
            if (rand <= 50)
            {
                resourceHex.SetupResource(0, 0, 0, 0, 0);
                _currentColor = Color.white;
                _currentResourceType = ResourceType.None;
            }
            else if (rand > 50 && rand <= 70) // metal
            {
                resourceHex.SetupResource(_countResource, 0, 0, 0, 0);
                _currentColor = Color.gray;
                _currentResourceType = ResourceType.Metal;
            }
            else if (rand > 70 && rand <= 90)
            {
                resourceHex.SetupResource(0, 0, 0, _countResource, 0);
                _currentColor = Color.blue;
                _currentResourceType = ResourceType.Crystal;
            }
            else if (rand > 90)
            {
                resourceHex.SetupResource(0, 0, 0, 0, _countResource);
                _currentColor = Color.red;
                _currentResourceType = ResourceType.Nanostruct;
            }

            return resourceHex;
        }

        private Color GetColor()
        {
            return _currentColor;
        }

        private ResourceType GetResourceType()
        {
            Debug.LogError(_currentResourceType);
            return _currentResourceType;
        }
    }
}