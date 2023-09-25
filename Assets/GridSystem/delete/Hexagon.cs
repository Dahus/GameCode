using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.GlobalData;

//using UnityEngine.Serialization;

namespace Game.Grid
{
    public class Hexagon : MonoBehaviour
    {
        [SerializeField] private int _x;
        [SerializeField] private int _y;
        
        private Dictionary<string,GameObject> _objectInHexagon = new  Dictionary<string,GameObject>();
        [SerializeField] private GameObject _currentObject;

        [SerializeField] private HexType.hexType _typeHex;

        [SerializeField] private ResourceType.typeResource _typeResource;
        public void Setup(int x,int y) 
        {
            _x = x;
            _y = y;
        }

        public void  HexLog()
        {
            Debug.Log("_x " + _x+ " _y "+_y);
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }
        

        public void SetupTypeHex(HexType.hexType typeHex ,ResourceType.typeResource typeResource)
        {
            _typeHex=typeHex;
            _typeResource=typeResource;
        }

        // возвращает слов.
        public Dictionary<string,GameObject> GetObjectInHexagon() 
        {
            return _objectInHexagon;
        }

        public GameObject GetObjectInHexagon( string name)
        {
            _currentObject = _objectInHexagon[name];
            return _currentObject;
        }

        public int CountWords()
        {
            return _objectInHexagon.Count;
        }

        public void DebugWords()
        {
            Debug.Log(_objectInHexagon.ContainsKey(ConstantDictionaryObjectIHex.BUILD));
        }
        

        public void SetObjectInHexagon(GameObject obj, string strType)
        {
            _objectInHexagon.Add(strType,obj);
            Debug.Log("ADD");
        }
            
        public void DeleteSecondObjectInHex(string strType)
        {
            _objectInHexagon.Remove(strType);
        }   

    }
}
