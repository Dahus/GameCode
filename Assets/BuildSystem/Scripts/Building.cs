using System.Collections.Generic;
using Game.Economics;
using UnityEngine;
using Game.GlobalData;
using Game.GlobalSystems;
using Game.Player;

//[Serializable]
namespace Game.BuildingSystem
{
    public class Building : MonoBehaviour, ITypeObjInHex
    {
        [SerializeField] private int _lifeBuild;

        [SerializeField] private int _timeBuild;

        [SerializeField] private string _typeObj = ConstantDictionaryObjectIHex.BUILD;

        [SerializeField] private bool _active = true;

        [SerializeField] private BuildData _buildData;

        

        //private DictionaryResource _resourceBuild;


        private void Awake()
        {
            //_lifeBuild = _buildData.GetLifeBuild();
            _timeBuild = _buildData.GetTimeBuilding();
        }

        public string GetTypeObjInHex()
        {
            return _typeObj;
        }

        public string GetName()
        {
            return _buildData.GetName();
        }
       
        
    }
}