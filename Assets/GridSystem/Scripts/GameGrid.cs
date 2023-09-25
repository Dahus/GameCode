using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Game.UI;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
namespace Game.GridSystem
{
    [RequireComponent(typeof(GridGenerator))]
    public class GameGrid : MonoBehaviour
    {
        private static GameObject _instance;
        private static GridHex[,] _grid;
        private static Vector2 _hexSize;
        private static Vector2 _gridSize;
        public static GridHex[,] Grid
        {
            get => _grid;
            set => _grid = value;
        }
        private void Awake()
        {
            _instance = this.gameObject;
        }
        public static void GridCreate(GridSetting settings, ButtonLogicManager buttonLogicManager, UIParametresLogic uIParametresLogic)
        {
            var boundSize = settings.HexPrefab.GetComponent<Renderer>().bounds.size;
            _hexSize = new Vector2(boundSize.x, boundSize.y);
            _grid = GridGenerator.GenerateGrid(settings, _instance, buttonLogicManager, uIParametresLogic);
            _gridSize = new Vector2(
                _grid[_grid.GetLength(0) - 1, 0].transform.position.x - _grid[0, 0].transform.position.x,
                _grid[0, _grid.GetLength(1) - 1].transform.position.y - _grid[0, 0].transform.position.y);
            Debug.Log(_gridSize);
            Camera.main.GetComponent<CameraControl>().CalculateCameraMovementBorder(_gridSize, _hexSize);
        }
        public static Vector2 GridSize => _gridSize;
        public static Vector2 HexSize => _hexSize;


        public bool CheckGrid()
        {
            if (_grid != null) return true;
            else return false;

        }

        public static void SetGrid(GridHex[,] grid)
        {
            if (_grid.GetLength(0) < 10 && _grid.GetLength(1) < 10) return;
            _grid = grid;
        }

        public static GridHex GetGridHex(Vector2 pos)
        {
            Debug.LogError(pos.x + " x " + pos.y + " y ");
            Vector2Int index = GridGenerator.ConvertPosToIndex(pos);
            return _grid[index.x, index.y];
        }

        private static float padding = 0.04f;
        private static float magicx = (Mathf.Cos(Mathf.PI / 6)) / 2 + padding;
        private static float magicy = (Mathf.Sin(Mathf.PI / 6) + 1) * (padding + 0.5f);
        public static Vector3 MoveLU(Vector3 hex)
        {
            return new Vector3(hex.x - magicx, hex.y + magicy, 15);
        }
        public static Vector3 MoveRU(Vector3 hex)
        {
            return new Vector3(hex.x + magicx, hex.y + magicy, 15);
        }
        public static Vector3 MoveLC(Vector3 hex)
        {
            return new Vector3(hex.x - magicx * 2, hex.y, 15);
        }
        public static Vector3 MoveRC(Vector3 hex)
        {
            return new Vector3(hex.x + magicx * 2, hex.y, 15);
        }
        public static Vector3 MoveLD(Vector3 hex)
        {
            return new Vector3(hex.x - magicx, hex.y - magicy, 15);
        }
        public static Vector3 MoveRD(Vector3 hex)
        {
            return new Vector3(hex.x + magicx, hex.y - magicy, 15);
        }

        public static GridHex GetRandomHexInGrid()
        {
            GridHex hex;
            do
            {
                hex = _grid[(int)Random.Range(0, _gridSize.x), (int)Random.Range(0, _gridSize.y)];
            } while (hex.HexInfoPathfinder.Walkable == false);
            return hex;
        }
    }
}