using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GlobalData;
using Game.Economics;

namespace Game.Player
{
    public class ResourcePlayer : DictionaryResource
    {
        private int _maxWorkers;
        public void SetupDictionary()
        {
            SetupResource();
            _maxWorkers = _dictionary.GetField(ConstantNameResource.WORKERS).Value;
            _dictionary.GetField(ConstantNameResource.WORKERS).Value = 0;
        }
        public DictionaryResource GetResourceDictionary()
        {
            return this;
        }
        public void DebugLogger()
        {
            Debug.Log(GetValue(ConstantNameResource.METAL));
        }

        public int GetMaxWorkers()
        {
            return _maxWorkers;
        }
        public void SetMaxWorkers(int max)
        {
            _maxWorkers=max;
        }
    }
}