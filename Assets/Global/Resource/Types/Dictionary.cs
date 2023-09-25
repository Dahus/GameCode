using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Types
{
    [Serializable]
    public class Dictionary<KeyType, ValueType>
    {
        [SerializeField] private List<KeyValuePair<KeyType, ValueType>> _item;

        public Dictionary(List<KeyValuePair<KeyType, ValueType>> item)
        {
            _item = item;
        }

        public Dictionary()
        {
            _item = new List<KeyValuePair<KeyType, ValueType>>();
        }

        public List<KeyValuePair<KeyType, ValueType>> Item
        {
            get => _item;
        }

        public void Add(KeyValuePair<KeyType, ValueType> newItem) => _item.Add(newItem);
        public void Add(KeyType key, ValueType value) => _item.Add((new KeyValuePair<KeyType, ValueType>(key, value)));
        public void Remove(int index) => _item.RemoveAt(index);
        public void Remove(KeyValuePair<KeyType, ValueType> item) => _item.Remove(item);

        public KeyValuePair<KeyType, ValueType> GetField(string key)
        {
            for (int i = 0; i < Item.Count; i++)
            {
                if (Item[i].Key.Equals(key))
                    return Item[i];
            }

            return null;
        }
        public void SetField(KeyValuePair<KeyType, ValueType> item)
        {
            for (int i = 0; i < Item.Count; i++)
            {
                if (Item[i].Key.Equals(item.Key))
                    Item[i].Value = item.Value;
            }
        }
    }
}