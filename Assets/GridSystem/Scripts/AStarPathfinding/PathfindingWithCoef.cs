using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.GridSystem;


public class PathfindingWithCoef : MonoBehaviour
{

    public class Node
    {
        public GridHex hex { get; set; }
        public Node prevNode { get; set; }

        public int countHexesPrev;
    }

    [SerializeField] private List<GridHex> _toSearch;
    [SerializeField] private List<GridHex> _processed;
    [SerializeField] private List<GridHex> _result;

    [SerializeField] private Node _start = new Node();
    [SerializeField] private Node _end = new Node();

    //public List<GridHex> FindPath(GridHex startPoint, GridHex _endPoint)
    //{

    //    _start = startPoint;
    //    _end = _endPoint;
    //    _toSearch = new List<GridHex> { startPoint };
    //    _processed = new List<GridHex>();
    //    _result = new List<GridHex>();
    //    float summa = 0;
    //    var currentHex = _toSearch[0];
    //    while (currentHex != _endPoint)
    //    {
    //        currentHex = _toSearch[0];

    //        foreach (var t in _toSearch)
    //        {
    //            bool flag1 = currentHex.GetCoefPatency() * Vector2.Distance(currentHex.transform.position, _endPoint.transform.position) > t.GetCoefPatency() * Vector2.Distance(t.transform.position, _endPoint.transform.position);
    //            if (flag1)
    //            {
    //                currentHex = t;
    //            }
    //        }

    //        _processed.Add(currentHex);
    //        _toSearch.Remove(currentHex);

    //        foreach (var neighbor in currentHex.NeighbourArray.Where(t => t.Walkable && !_processed.Contains(t)))
    //        {
    //            var inSearch = _toSearch.Contains(neighbor);

    //            var costToNeighbor = Vector2.Distance(neighbor.transform.position, _endPoint.transform.position) + neighbor.GetCoefPatency();

    //            var cost = Vector2.Distance(currentHex.transform.position, _endPoint.transform.position) + currentHex.GetCoefPatency();

    //            if (!inSearch || costToNeighbor < cost)
    //            {
    //                _toSearch.Add(neighbor);
    //            }

    //        }
    //    }

    //    GridHex prevHex = _processed[_processed.Count - 1];
    //    _result.Add(prevHex);

    //    foreach (var hex in prevHex.NeighbourArray)
    //    {
    //        bool flag1 = prevHex.GetCoefPatency() * Vector2.Distance(prevHex.transform.position, startPoint.transform.position) > hex.GetCoefPatency() * Vector2.Distance(hex.transform.position, startPoint.transform.position);
    //        if (flag1 && _processed.Contains(hex))
    //        {
    //            currentHex = hex;
    //        }
    //    }

    //    _result.Add(currentHex);
    //    while (currentHex != startPoint)
    //    {
    //        float distPrev = Vector2.Distance(prevHex.transform.position, startPoint.transform.position) * prevHex.GetCoefPatency();

    //        GridHex nextHex = currentHex;
    //        GridHex tempHex = currentHex;
    //        foreach (var hex in currentHex.NeighbourArray)
    //        {
    //            if (_processed.Contains(hex))
    //            {
    //                if (distPrev > Vector2.Distance(startPoint.transform.position, hex.transform.position) * hex.GetCoefPatency() && !_result.Contains(hex))
    //                {
    //                    distPrev = Vector2.Distance(startPoint.transform.position, hex.transform.position) * hex.GetCoefPatency();
    //                    nextHex = hex;
    //                }
    //                if (!_result.Contains(hex))
    //                    tempHex = hex;
    //            }
    //        }

    //        if (nextHex == currentHex)
    //        {
    //            nextHex = tempHex;
    //        }
    //        prevHex = currentHex;
    //        currentHex = nextHex;
    //        if (_result.Contains(currentHex))
    //        {
    //            _processed.Remove(currentHex);
    //            _result.Remove(currentHex);
    //            currentHex = _result[_result.Count - 1];
    //            prevHex = _result[_result.Count - 2];
    //            continue;
    //        }

    //        _result.Add(currentHex);
    //    }


    //     List<GridHex> _result2 = new List<GridHex>();

    //    for(int i = _result.Count - 1; i >= 0; i--)
    //    {
    //        _result2.Add(_result[i]);
    //    }
    //    foreach(var hex in _result)
    //    {
    //        Debug.Log(hex.name);
    //        hex.GetComponent<SpriteRenderer>().color = Color.blue;
    //    }

    //    return _result2;
    //}

    public void TestPathfinder()
    {
        if (_start != null && _end != null)
            FindHexPath(_start.hex, _end.hex);
        //FindPath(_start, _end);

        if (_start != null)
        {
            foreach (var neighbor in _start.hex.NeighbourArray.OrderBy(t => t.HexInfoPathfinder.GetCoefPatency()).Where(t => t.HexInfoPathfinder.Walkable && !_processed.Contains(t)))
            {
                Debug.LogError(neighbor.HexInfoPathfinder.GetCoefPatency());
            }
        }

    }

    public List<GridHex> FindHexPath(GridHex start, GridHex end) // ����� ��� ��� � ���� ������. ��������� ���������� � ��� � ����� ����� ��������� � � ����� � �������
    {
        _start.hex = start;
        _end.hex = end;
        List<Node> openSet = new List<Node>(); // �� ������ ������� �� ��������
        List<Node> closeSet = new List<Node>(); // �� ������ ������� ����������
        openSet.Add(_start); // ��� ��� �����

        float dist1 = GetDistanceToHexes(_start.hex, _end.hex);
        while (openSet.Count > 0) // ������� �������. ����� ����������� ���� isOpenSet �������� false
        {
            Node currentNode = openSet[0]; // ��� ����� � �������� ����� ��������, �������� �����
            
         
            for (int i = 1; i < openSet.Count; i++) // ������ ��� ���� �� ���������. //������������ ������� � ������������� � ������� ����� ����� � ������
            {
                float distCurrentToEnd = GetDistanceToHexes(currentNode.hex, _end.hex);

                float openSetCost = DistanceCoef(_end.hex, openSet[i].hex, _start.hex);
                if (openSetCost < dist1 || (openSetCost == dist1 && GetDistanceToHexes(openSet[i].hex, _end.hex) < distCurrentToEnd)) // ������� ����� �� ������� ����� ����� � �����
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode); // ������ ����� ��� ��������� ���
            closeSet.Add(currentNode); // ���������� ������������� �����

            if (currentNode.hex == _end.hex) // ��������, ��� ���� node � ������ ������ �������� ��������� ����
            {
                var path = GetPath(currentNode);
                foreach (var hex in path)
                {
                    hex.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                return path;
            }

            #region �������� �������
            List<Node> neighbourNodes = new List<Node>();
            foreach (GridHex hex in currentNode.hex.NeighbourArray)
            {
                Node node = new Node();
                node.hex = hex;
                neighbourNodes.Add(node);
            }
            #endregion

            Debug.LogError(neighbourNodes.Count);

            foreach (Node node in neighbourNodes) // �������� ���� ������� ������ ����
            {
                #region �������� �� ���������� (�� ���� ��� ��� ���-��)
                if (!node.hex.HexInfoPathfinder.Walkable || IsContainsNodes(node, closeSet)) // ��������� ��������� �� ����� � ���� �� �� � ���. ���� ����, �� ���������� ���. ���� �� ����, ����� ������
                    continue;
                #endregion
                /*var distCurToEnd = GetDistanceToHexes(currentNode.hex, _end.hex);
                var distStartToEnd = GetDistanceToHexes(currentNode.hex, _start.hex);
                var dist = DistanceCoef(distCurToEnd, distStartToEnd);*/

                /*var distNodeToCurrentNode = GetDistanceToHexes(currentNode.hex, node.hex);
                var newGCost = dist1 + distNodeToCurrentNode;*/


                bool isOpenSet = IsContainsNodes(node, openSet); // �������
                if (!isOpenSet) // �������� ���������� ����� ��������� ������ �� �� ����� ������
                {
                    node.prevNode = currentNode;
                    openSet.Add(node);
                    continue;
                }
                /*
                if (newGCost < distNodeToCurrentNode)
                {
                    Debug.Log("��");
                    var newHexCount = node.countHexesPrev;
                    node.prevNode = currentNode;
                    node.countHexesPrev = newHexCount + 1;
                    if (!isOpenSet)
                    {
                        openSet.Add(node);
                    }
                }*/

                
                if (dist1 < 0) // ���������� ����� ���������� ����� � prevNode
                {
                    Debug.LogError(dist1);
                    int newHexCount = node.countHexesPrev;
                    node.prevNode = currentNode;
                    node.countHexesPrev = newHexCount + 1;
                    /*
                    if (!isOpenSet)
                    {
                        openSet.Add(node);
                    }
                    */
                }

                else if (dist1 == 0 && currentNode.countHexesPrev > node.countHexesPrev) // ���������� ����� ���������� ����� � prevNode
                {
                    node.prevNode = currentNode;
                    node.countHexesPrev = currentNode.countHexesPrev;
                    /*
                    if (!isOpenSet)
                    {
                        openSet.Add(node);
                    }
                    */
                }
            }
        }

        return null;
    }

    private float GetDistanceToHexes(GridHex current, GridHex startOrEnd)
    {
        return Vector2.Distance(current.gameObject.transform.position, startOrEnd.gameObject.transform.position) + current.HexInfoPathfinder.GetCoefPatency(); // GetCoefPatency - �����, ������� ��������� ������������
    }

    private float DistanceCoef(float distanceCurToEnd, float distanceStatrToCur)
    {
        return distanceCurToEnd + distanceStatrToCur;
    }
    private float DistanceCoef(GridHex startHex, GridHex currentHex, GridHex endHex)
    {
        var distanceCurToEnd = GetDistanceToHexes(currentHex, endHex);
        var distanceStartToCur = GetDistanceToHexes(startHex, currentHex);
        return distanceCurToEnd + distanceStartToCur;
    }

    private List<GridHex> GetPath(Node node)
    {
        HashSet<GridHex> path = new HashSet<GridHex>();

        while (node.hex != _start.hex)
        {
            path.Add(node.hex);
            node = node.prevNode;
        }

        List<GridHex> pathList = new List<GridHex>();
        pathList.AddRange(path);
        pathList.Add(_start.hex);
        pathList.Reverse();
        return pathList;
    }
    private bool IsContainsNodes(Node hex, List<Node> nodes) // ��������, ��������� �� �� � �������� �����
    {
        foreach (Node node in nodes)
        {
            if (node.hex == hex.hex)
            {
                return true;
            }
        }
        return false;
    }
}


