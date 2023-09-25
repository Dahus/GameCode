using Game.GridSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private GridHex[,] _grid;

    [SerializeField] private GameObject prefab_pathNode;

    private PathNode[] arrayOfNodes;

    [SerializeField] private int totalVisitedCells; // ����� ���������� ���������� �����

    [SerializeField] private int step; // ����������� ����� �� ������ �� ����

    [SerializeField] int totalCellcount; // ������� �����

    public PathNode[] ArrayOfNodes
    {
        get { return arrayOfNodes; }

        set { arrayOfNodes = value; }
    }

    public GameObject Prefab_pathNode
    {
        get { return prefab_pathNode; }

        set { prefab_pathNode = value; }
    }

    public int TotalVisitedCells
    {
        get { return totalVisitedCells; }

        set { totalVisitedCells = value; }
    }

    public int Step
    {
        get { return step; }

        set { step = value; }
    }

    public int TotalCellcount
    {
        get { return totalCellcount; }

        set { totalCellcount = value; }
    }

    //=======================������===========================

    public void Setup(int x, int y)
    {
        TotalCellcount = x * y;
        ArrayOfNodes = new PathNode[TotalCellcount];
        _grid = GameGrid.Grid;
    }

    private void RemoveAllNodes()
    {
        //BEGIN ������� ���� ������� �����
        var children = new List<GameObject>();
        foreach (Transform cell in this.transform)
        {
            children.Add(cell.gameObject);
        }

        children.ForEach(c => DestroyImmediate(c));
        //END ������� ���� ������� �����
    }

    // �� ���� ����������, � ��������� �������
    public List<GridHex> FindPath(GridHex start, GridHex goal)
    {
        //��� 0. ����������
        RemoveAllNodes();

        //��� 1.
        var closedSet = new Collection<PathNode>();
        var openSet = new Collection<PathNode>();
        TotalVisitedCells = 0;
        Step = 0;

        // ��� 2.
        GameObject obj_pathNode = Instantiate(Prefab_pathNode); //�������� ����
        obj_pathNode.transform.parent = this.transform; //��������� ������ child � Parent
        obj_pathNode.name =
            "Pathnode �" + TotalVisitedCells + ": (" + start.transform.position.x + ", " + start.transform.position.y + ")"; //������ ��� �������

        PathNode startNode = obj_pathNode.GetComponent<PathNode>();
        //start.Label.text = "" + Step + ""; 
        ArrayOfNodes[TotalVisitedCells] = startNode; //������ � ������
        startNode.Position = start.transform;
        startNode.CameFrom = null;
        startNode.PathLengthFromStart = 0;
        startNode.HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal);
        startNode.Current_compCell = start;

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            // ��� 3.
            var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
            // ��� 4.
            if (currentNode.Current_compCell == goal)
            {
                List<GridHex> listOfCells = GetPathForNode(currentNode);
                //foreach (var hex in listOfCells)
                //    hex.GetComponent<SpriteRenderer>().color = Color.black;
                //start.GetComponent<SpriteRenderer>().color = Color.green;
                //goal.GetComponent<SpriteRenderer>().color = new Color(0.2F, 0.3F, 0.4F);
                //start.Label.text = "->BEGIN<-";
                //goal.Label.text = "->GOAL<-";
                return listOfCells;
            }

            // ��� 5.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // ��� 6.

            foreach (var neighbourNode in GetNeighbours(currentNode, goal))
            {
                // ��� 7.
                if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                    continue;
                var openNode = openSet.FirstOrDefault(node =>
                    node.Position == neighbourNode.Position);
                // ��� 8.
                if (openNode == null)
                    openSet.Add(neighbourNode);
                else if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                {
                    // ��� 9.
                    openNode.CameFrom = currentNode;
                    openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                }
            }
        }

        // ��� 10.

        return null;
    }

    private float GetHeuristicPathLength(GridHex from, GridHex to)
    {
        return Math.Abs(from.transform.position.x - to.transform.position.x) +
               Math.Abs(from.transform.position.y - to.transform.position.y);
    }

    private Collection<PathNode> GetNeighbours(PathNode pathNode, GridHex goal)
    {
        Step++;
        var result = new Collection<PathNode>();

        // ��������� ������� �������� �������� �� ������� ������.
        //GridHex[] neighbourPoints = new GridHex[6];
        //Debug.LogError(pathNode.Current_compCell.NeighbourArray.Length);
        List<GridHex> neighbourPoints = pathNode.Current_compCell.NeighbourArray;
        /* neighbourPoints[0] = pathNode.Current_compCell.NeighbourArray[0];
         neighbourPoints[1] = pathNode.Current_compCell.NeighbourArray[1];
         neighbourPoints[2] = pathNode.Current_compCell.NeighbourArray[2];
         neighbourPoints[3] = pathNode.Current_compCell.NeighbourArray[3];
         neighbourPoints[4] = pathNode.Current_compCell.NeighbourArray[4];
         neighbourPoints[5] = pathNode.Current_compCell.NeighbourArray[5];*/
        foreach (var neighbour in neighbourPoints)
        {
            if (!neighbour) //���� ���������� �������� ������
                continue;
            if (!neighbour.HexInfoPathfinder.Walkable)
            {
                //���� ����� ������ �� ���
                continue;
            }

            // ��������� ������ ��� ����� ��������.
            TotalVisitedCells++;
            if (TotalVisitedCells == TotalCellcount) //���� ������� ����� �����, �� ��������� ������!
            {
                //���� ����� ������ �� ���
                continue;
            }

            GameObject obj_pathNode = Instantiate(Prefab_pathNode); //�������� ����
            obj_pathNode.transform.parent = this.transform; //��������� ������ child � Parent
            obj_pathNode.name = "Pathnode �" + TotalVisitedCells + ": (" + neighbour.transform.position.x + ", " + neighbour.transform.position.y +
                                ")"; //������ ��� �������

            PathNode neighbourNode = obj_pathNode.GetComponent<PathNode>();
            ArrayOfNodes[TotalVisitedCells] = neighbourNode; //������ � ������  
            neighbourNode.Position = neighbour.transform;
            neighbourNode.CameFrom = pathNode;
            neighbourNode.PathLengthFromStart =
                pathNode.PathLengthFromStart + 1; // neighbourNode.GetDistanceBetweenNeighbours();
            neighbourNode.HeuristicEstimatePathLength = GetHeuristicPathLength(neighbour, goal);
            neighbourNode.Current_compCell = neighbour;

            result.Add(neighbourNode);
            //neighbour.GetComponent<SpriteRenderer>().color = Color.magenta;
            //neighbour.Label.text = "" + Step + "";
        }

        return result;
    }

    private List<GridHex> GetPathForNode(PathNode pathNode)
    {
        var result = new List<GridHex>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Current_compCell);
            currentNode = currentNode.CameFrom;
        }

        result.Reverse();
        return result;
    }
}