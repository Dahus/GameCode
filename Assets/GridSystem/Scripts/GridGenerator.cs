using Game.Economics;
using Game.Grid;
using Game.Player;
using Game.UI;
using UnityEngine;

namespace Game.GridSystem
{
    public class GridGenerator : MonoBehaviour
    {
        private static GameObject _hexPrefab = null;
        private static GameObject _gridObject;
        private static Vector2 _hexSize;
        private static Vector2Int _gridSize = Vector2Int.zero;
        private static float _gridOffset = 0;

        public static GridHex[,] GenerateGrid(GridSetting settings, GameObject gridObject,
            ButtonLogicManager buttonLogicManager, UIParametresLogic uIParametresLogic)
        {
            SetSettings(settings, gridObject);
            GridHex[,] grid = new GridHex[settings.Size.x, settings.Size.y];
            grid = HexMatrixGenerate(grid, buttonLogicManager, uIParametresLogic);
            //grid = ReliefGenerate(grid);
            //grid = PatencyGenerate(grid);
            //grid = ResourceGenerate(grid);
            return grid;
        }

        public static Vector2Int ConvertPosToIndex(Vector2 pos)
        {
            int y = (int)(pos.y / (_hexSize.y * 0.75f + _gridOffset) + 0.5f);
            int x = (int)(pos.x / (_hexSize.x + _gridOffset) - (y % 2 == 0 ? 0.5f : 0.0f) + 0.5f);
            return new Vector2Int(x, y);
        }

        public static Vector2 ConvertIndexToPos(Vector2Int index)
        {
            return new Vector2(
                (index.x + (index.y % 2 == 0 ? 0.5f : 0.0f)) * (_hexSize.x + _gridOffset),
                index.y * (_hexSize.y * 0.75f + _gridOffset)
            );
        }

        private static void SetSettings(GridSetting settings, GameObject gridObject)
        {
            _gridSize = settings.Size;
            _gridOffset = settings.Сlearance;
            _hexPrefab = settings.HexPrefab;
            _gridObject = gridObject;
            var boundSize = _hexPrefab.GetComponent<Renderer>().bounds.size;
            _hexSize = new Vector2(boundSize.x, boundSize.y);
        }

        private static GridHex[,] HexMatrixGenerate(GridHex[,] grid, ButtonLogicManager _logicManager, UIParametresLogic uIParametresLogic)
        {
            bool offset = false;
            Vector3 horizon = new Vector3(0, 0, 15);
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector3 current = new Vector3(horizon.x, horizon.y, horizon.z);
                for (int x = 0; x < _gridSize.x; x++)
                {
                    var gameObject = Instantiate(_hexPrefab, _gridObject.transform);
                    var hex = gameObject.GetComponent<GridHex>();

                    hex.name = $"({x}, {y})";
                    hex.transform.position = current;
                    hex.PositionInGrid = new Vector2Int(x, y);

                    grid[x, y] = hex;
                    if (x > 0)
                    {
                        GridHex t = grid[x - 1, y];
                        t.NeighbourArray.Add(hex);
                        hex.NeighbourArray.Add(t);
                    }
                    if (y > 0)
                    {
                        GridHex h = grid[x, y - 1];
                        h.NeighbourArray.Add(hex);
                        hex.NeighbourArray.Add(h);

                        int tx = x + (offset ? 1 : -1);
                        if (tx >= 0 && tx < _gridSize.x)
                        {
                            GridHex t = grid[tx, y - 1];
                            t.NeighbourArray.Add(hex);
                            hex.NeighbourArray.Add(t);
                        }
                    }


                    hex.GetComponent<ClickOnHex>().Setup(_logicManager, uIParametresLogic);
                    
                    current = GameGrid.MoveRC(current);
                }
                horizon = offset ? GameGrid.MoveLU(horizon) : GameGrid.MoveRU(horizon);
                offset = !offset;
            }

            offset = false;
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {

                }
            }

            return grid;
        }

        private static GridHex[,] ReliefGenerate(GridHex[,] grid)
        {
            return grid;
        }

        private static GridHex[,] PatencyGenerate(GridHex[,] grid)
        {
            return grid;
        }

        private static GridHex[,] ResourceGenerate(GridHex[,] grid)
        {
            return grid;
        }


    }
}