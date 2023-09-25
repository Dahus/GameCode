using Game.UnitsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GridSystem
{
    public interface IHexObserver
    {
        /// <summary>
        /// ����� ���������� ���� ������. � ������ ������� ����� ������� ���������� �� ������ ������, � ����� �������� �� �����. 
        /// </summary>
        void ObservationArea();
        /// <summary>
        /// ����� ���������� ��� ����������� ����������� (������ ��� �����). 
        /// </summary>
        void TermonationObservation();
    }
    [Serializable]
    public struct HexObserveData
    {
        public HexObserveStatus observeStatus;
        public GameObject hexLink;
        public GridHexInfo gridInfo;
        public List<GameObject> objectsInHex;
        public List<IHexObserver> observers;
        public List<GameObject> observersHex;

        public HexObserveData(GameObject hexLink, GridHexInfo gridInfo, List<GameObject> objectsInHex) : this()
        {
            this.hexLink = hexLink;
            this.gridInfo = gridInfo;
            this.objectsInHex = objectsInHex;
            observers = new List<IHexObserver>();
            observersHex = new List<GameObject>();
            observeStatus = HexObserveStatus.NoneData;
        }
        public HexObserveData(HexObserveStatus observeStatus, GameObject hexLink, GridHexInfo gridInfo,
            List<GameObject> objectsInHex) : this(hexLink, gridInfo, objectsInHex)
        {
            this.observeStatus = observeStatus;
        }

        public HexObserveData(HexObserveStatus observeStatus, GameObject hexLink, GridHexInfo gridInfo,
            List<GameObject> objectsInHex, List<IHexObserver> observers) : this(observeStatus, hexLink, gridInfo, objectsInHex)
        {
            this.observers = observers;
        }
        public HexObserveData CloneData() => new HexObserveData(hexLink, gridInfo, objectsInHex);

    }

    public enum HexObserveStatus
    {
        NoneData,
        FogOfWar,
        Observed
    }
    [Serializable]
    public class ObserveMap
    {
        private HexObserveData[,] mapObserveData;

        public HexObserveData[,] HexObserveData { get => mapObserveData; }

        /// <summary>
        /// ����� ���������� �������� � ������ ���� ��� �������� ����� ��� ������� ������.
        /// </summary>
        /// <param name="mapSize">������� ��������������� �����</param>
        public void Setup(Vector2Int mapSize)
        {
            mapObserveData = new HexObserveData[mapSize.x, mapSize.y];
            for (int i = 0; i < mapObserveData.GetLength(0); i++)
                for (int j = 0; j < mapObserveData.GetLength(1); j++)
                {
                    ref var cell = ref mapObserveData[i, j];
                    cell.observeStatus = HexObserveStatus.NoneData;
                    cell = new HexObserveData(null, null, null);
                }
        }
        /// <summary>
        /// ����� ���������� ��� �������� ������ �����������, � ����� ��� ��������� ���� ���������� (����� ����������� ���������� �� ������� ��������)
        /// </summary>
        /// <param name="newData">������ ��������� �� ������� ����� (pos) � ���������������� ���������� � ����� (hexObserveData)</param>
        /// <param name="source">������ ������� ��������� ���� (�� ���� ��� �� �������� ���������� ���� �����)</param>
        public void ObservationHex((Vector2Int pos, HexObserveData hexInfo)[] newData, IHexObserver source)
        {
            for (int i = 0; i < newData.Length; i++)
            {
                var pos = newData[i].pos;
                var newhexInfo = newData[i].hexInfo;
                var cell = new HexObserveData(HexObserveStatus.Observed, newhexInfo.hexLink, newhexInfo.gridInfo, newhexInfo.objectsInHex);

                cell.observers.Add(source);
                foreach (var observer in mapObserveData[pos.x, pos.y].observers)
                {
                    if (!cell.observers.Contains(observer))
                    {
                        cell.observers.Add(observer);
                    }
                }
                mapObserveData[pos.x, pos.y] = cell;
            }
        }
        /// <summary>
        /// ����� ���������� ��� �������� ������ �����������, � ����� ��� ��������� ���� ���������� (����� ����������� ���������� �� ������� ��������)
        /// </summary>
        /// <param name="hexesPos">������� ����� � ������� ���������� ����������</param>
        /// <param name="source">������ ������� �������� ��������� ���� (�� ���� ��� �� �������� ���������� ���� �����)</param>
        public void TerminationObservationHex(Vector2Int[] hexesPos, IHexObserver source)
        {
            for (int i = 0; i < hexesPos.Length; i++)
            {
                var pos = hexesPos[i];
                ref var cell = ref mapObserveData[pos.x, pos.y];
                cell.observers.Remove(source);
                if (cell.observers.Count > 0)
                {
                    continue;
                }

                cell.observeStatus = HexObserveStatus.FogOfWar;
                var objectsInHex = cell.objectsInHex;
                var obectsToRemove = new List<GameObject>();
                for (int j = 0; j < objectsInHex.Count; j++)
                {
                    if (objectsInHex[j].TryGetComponent(out AbstractUnit _))
                    {
                        obectsToRemove.Add(objectsInHex[j]);
                    }
                }
                for (int j = 0; j < obectsToRemove.Count; j++)
                {
                    objectsInHex.Remove(obectsToRemove[j]);
                }
                mapObserveData[pos.x, pos.y].objectsInHex = objectsInHex;
            }
        }
    }
}