using System.Collections;
using System.Collections.Generic;
using Game.UI;
using UnityEngine;

namespace Game.GridSystem
{
    [RequireComponent(typeof(GameGrid))]
    public class GridBuilder : MonoBehaviour
    {
        [InspectorName("Размеры")][SerializeField] private Vector2Int GridSize;
        [InspectorName("Просветы")][SerializeField] private float Clearence;
        [InspectorName("Гекс")][SerializeField] private GameObject HexPrefab;


        public void SetGridSize(Vector2Int gridSize)
        {
            GridSize = gridSize;
        }
        
        
        public void CreateGrid(ButtonLogicManager buttonLogicManager, UIParametresLogic uIParametresLogic)
        {
            GameGrid.GridCreate(new GridSetting(GridSize,Clearence,HexPrefab), buttonLogicManager, uIParametresLogic);
        }
        
    }
}
