using System;

namespace Game.Types
{
    [Serializable]
    public class KeyValuePair<KeyType, ValueType>
    {
        public KeyType Key;
        public ValueType Value;

        public KeyValuePair(KeyType key, ValueType value)
        {
            Key = key;
            Value = value;
        }
    }
}

