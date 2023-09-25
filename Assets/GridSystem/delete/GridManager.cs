using UnityEngine;
using UnityEngine.Events;

namespace Game.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject Hex;
        [SerializeField] private Vector2Int gridSize = new Vector2Int(10 ,10);
        [SerializeField] private float gridOffset = 0.02f;
        [SerializeField] private UnityEvent<Vector2, Vector2> SetMapSize;
        
        public Vector2 hexSize {private set; get;}
        private GameObject[,] GridCells;

        public Vector2 globalSize {private set; get;}

        public void CreateGrid(Vector2Int gridSize) 
        {
            this.gridSize = gridSize;
            GridCells = new GameObject[gridSize.x, gridSize.y];
            hexSize = new Vector2(Hex.GetComponent<Renderer>().bounds.size.x, Hex.GetComponent<Renderer>().bounds.size.y);
            Debug.Log( hexSize.x + " " + hexSize.y);
            for(int x = 0; x < gridSize.x; x++){
                for(int y = 0; y < gridSize.y; y++)
                {      
                    var hex = Instantiate(Hex, this.transform);
                    hex.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                    hex.name = x + " " + y;
                    var X = (x + (y % 2 == 0? 0.5f : 0.0f)) * (hexSize.x + gridOffset);
                    var Y = y * hexSize.y * (3f/4) + gridOffset * y;
                    hex.transform.position = new Vector3(X, Y,0);
                    GridCells[x,y] = hex;
                }
            }  
            globalSize = new Vector2((gridSize.x-1)*hexSize.x + gridOffset*gridSize.x, (gridSize.y-1)*hexSize.y*(3f/4) + gridOffset*gridSize.y) ;
        }
    /// <summary>
    /// Search for the shortest path from one hexagon to another.
    /// </summary>
    /// <param name="hexID">Hexagon coordinates into grids in Vector2(x, y) format.</param>
    /// <returns>Array of coordinates of hexagons-neighbors in grids in Vector2(x, y) format.</returns>
        public Vector2Int[] GetNeighbors(Vector2Int hexID)
        {
            Vector2Int[] neighboars = {
                new Vector2Int(hexID.x, hexID.y-1), 
                new Vector2Int(hexID.x+1, hexID.y-1), 

                new Vector2Int(hexID.x-1, hexID.y), 
                new Vector2Int(hexID.x+1, hexID.y), 

                new Vector2Int(hexID.x, hexID.y+1), 
                new Vector2Int(hexID.x+1, hexID.y+1), 
            };
            return neighboars;
        }  

    /// <summary>
    /// Search for the shortest path from one hexagon to another.
    /// </summary>
    /// <param name="x">Coordinate X hexagon into grids.</param>
    /// <param name="y">Coordinate Y hexagon into grids.</param>
    /// <returns>Array of coordinates of hexagons-neighbors in grids.</returns>
        public Vector2Int[] GetNeighbors(int x, int y)
        {
            return GetNeighbors(new Vector2Int(x,y));
        }

    /// <summary>
    /// Search for the shortest path from one hexagon to another.
    /// </summary>
    /// <param name="currentHex">Coordinates of the hexagon from which the movement begins in the Vector2(x, y) format.</param>
    /// <param name="targetHex">Coordinates of the hexagon into which the movement is carried out in the Vector2(x, y) format.</param>
    /// <returns>Array of coordinates of hexagons entering the path in the direct order.</returns>
        public Vector2Int[] GetPath(Vector2Int currentHex, Vector2Int targetHex)
        {
            // Код поиска пути
            return null;
        }

        /// <summary>
        /// Search for the shortest path from one hexagon to another.
        /// </summary>
        /// <param name="currentX">Coordinate X hexagon hexagon from which the movement begins in the Vector2(x, y) format.</param>
        /// <param name="currentY">Coordinate Y hexagon from which the movement begins in the Vector2(x, y) format.</param>
        /// <param name="targetX">Coordinate X hexagon into which the movement is carried out in the Vector2(x, y) format.</param>
        /// <param name="targetY">Coordinate Y hexagon into which the movement is carried out in the Vector2(x, y) format.</param>
        /// <returns>Array of coordinates of hexagons entering the path in the direct order.</returns>
        public Vector2Int[] GetPath(int currentX, int currentY, int targetX, int targetY) => 
            GetPath(new Vector2Int(currentX, currentY), new Vector2Int(targetX, targetY));
        public GameObject RandomHex => GridCells[Random.Range(0, gridSize.x - 1), Random.Range(0, gridSize.y - 1)];
        public Hexagon GetGridCell(Vector2Int cellPos) => GridCells[cellPos.x, cellPos.y].GetComponent<Hexagon>();
        
    }
}
