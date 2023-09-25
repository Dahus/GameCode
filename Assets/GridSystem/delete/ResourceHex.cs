using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Grid
{
    public class ResourceHex : MonoBehaviour
    {
        [SerializeField] private int _countResource;
        [SerializeField] private ResourceType.typeResource _typeRes;






        public void SetupHexResource(int countResource, ResourceType.typeResource typeRes)
        {
            _countResource = countResource;
            _typeRes = typeRes;

        }


        public int GetResource(int countProdFabricaResource)
        {
            if (_countResource < countProdFabricaResource)
                return _countResource;
            else return countProdFabricaResource;


        }

        public void ProdResource(int countProdFabricaResource)
        {
            _countResource = _countResource - countProdFabricaResource;
            if (_countResource <= 0)
            {
                _countResource = 0;
                _typeRes = ResourceType.typeResource.NullResource;
            }
        }
    }
}
