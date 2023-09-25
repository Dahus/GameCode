using UnityEngine;
using Game.GlobalData;
using Game.Types;
using Game.Player;

namespace Game.Economics
{
    public class DictionaryResource
    {
        protected Types.Dictionary<string, int> _dictionary = new Types.Dictionary<string, int>();
        
        public void SetupResource()
        {
            _dictionary = new Types.Dictionary<string, int>();
            _dictionary.Add(ConstantNameResource.METAL, 1000);
            _dictionary.Add(ConstantNameResource.CREDITS, 1000);
            _dictionary.Add(ConstantNameResource.WORKERS, 1000);
            _dictionary.Add(ConstantNameResource.ENERGY_CRISTAL, 1000);
            _dictionary.Add(ConstantNameResource.NANO_STRUCTURE, 1000);
        }


        public int GetValue(string key)
        {
            for (int i = 0; i < _dictionary.Item.Count; i++)
            {
                if (_dictionary.Item[i].Key == key)
                {
                    return _dictionary.Item[i].Value;
                }
            }

            return 0;
        }
         

        public void DebugLoger() 
        {
            Debug.LogError(_dictionary.Item[0].Value + " Метал ");
            Debug.LogError(_dictionary.Item[1].Value + " кридитс ");
            Debug.LogError(_dictionary.Item[2].Value + " workers ");
            Debug.LogError(_dictionary.Item[3].Value + " кристалы ");
            Debug.LogError(_dictionary.Item[4].Value + " нанструктуры");
        }

        public void SetupResource(int metal, int credits, int workers, int crystal, int nanoStructure)
        {
            _dictionary = new Types.Dictionary<string, int>();
            _dictionary.Add(ConstantNameResource.METAL, metal);
            _dictionary.Add(ConstantNameResource.CREDITS, credits);
            _dictionary.Add(ConstantNameResource.WORKERS, workers);
            _dictionary.Add(ConstantNameResource.ENERGY_CRISTAL, crystal);
            _dictionary.Add(ConstantNameResource.NANO_STRUCTURE, nanoStructure);
        }

        public void SetupResource(DictionaryResource resource)
        {
            _dictionary = new Types.Dictionary<string, int>();
            _dictionary.Add(ConstantNameResource.METAL, resource.GetValue(ConstantNameResource.METAL));
            _dictionary.Add(ConstantNameResource.CREDITS, resource.GetValue(ConstantNameResource.CREDITS));
            _dictionary.Add(ConstantNameResource.WORKERS, resource.GetValue(ConstantNameResource.WORKERS));
            _dictionary.Add(ConstantNameResource.ENERGY_CRISTAL, resource.GetValue(ConstantNameResource.ENERGY_CRISTAL));
            _dictionary.Add(ConstantNameResource.NANO_STRUCTURE, resource.GetValue(ConstantNameResource.NANO_STRUCTURE));
        }

        public static void ADDRESOURCECHIT(ResourcePlayer res1)
        {
            for (int i = 0; i < res1._dictionary.Item.Count; i++)
            {
                if (res1._dictionary.Item[i].Key == ConstantNameResource.WORKERS)
                {
                    if (res1._dictionary.Item[i].Value - 1000 < 0)
                    {
                        continue;
                    } else
                    {
                        res1._dictionary.Item[i].Value -= 1000;
                    }
                }
                res1._dictionary.Item[i].Value += 1000;
            }
        }

        public static void Substraction(DictionaryResource res1, DictionaryResource res2)
        {
            for (int i = 0; i < res1._dictionary.Item.Count; i++)
            {
                if (res1._dictionary.Item[i].Value - res2._dictionary.Item[i].Value < 0)
                {
                    return;
                }
                res1._dictionary.Item[i].Value -= res2._dictionary.Item[i].Value;
            }
        }

        public static void SubstractionResourcePlayer(ResourcePlayer res1, DictionaryResource res2)
        {
            for (int i = 0; i < res1._dictionary.Item.Count; i++)
            {
                if (res1._dictionary.Item[i].Key == ConstantNameResource.WORKERS)
                {
                    if (res1._dictionary.Item[i].Value + res2._dictionary.Item[i].Value < res1.GetMaxWorkers())
                    {
                        res1._dictionary.Item[i].Value += res2._dictionary.Item[i].Value;
                    }
                    continue;
                }
                res1._dictionary.Item[i].Value -= res2._dictionary.Item[i].Value;
            }
        }

        public static bool CheckAvailabilityResource(ResourcePlayer res1, DictionaryResource res2)
        {
            for (int i = 0; i < res1._dictionary.Item.Count; i++)
            {
                if (res1._dictionary.Item[i].Key == ConstantNameResource.WORKERS)
                {
                    //Debug.LogError(res1.GetMaxWorkers());
                    if (res1._dictionary.Item[i].Value + res2._dictionary.Item[i].Value > res1.GetMaxWorkers())
                    {
                        return false;
                    }
                    continue;
                }

                if (res1._dictionary.Item[i].Value - res2._dictionary.Item[i].Value < 0)
                    return false;
            }

            return true;
        }

        public static void AddAttributeDictionary(DictionaryResource resourcePlayer,
            params KeyValuePair<string, int>[] resources)
        {
            foreach (var resource in resources)
            {
                var resourceField = resourcePlayer._dictionary.GetField(resource.Key);
                if (resourceField != null)
                {
                    resourceField.Value += resource.Value;
                    resourcePlayer._dictionary.SetField(resourceField);
                }
            }
        }

        public static void SubAttributeDictionary(DictionaryResource resourcePlayer,
            params KeyValuePair<string, int>[] resources)
        {
            foreach (var resource in resources)
            {
                var resourceField = resourcePlayer._dictionary.GetField(resource.Key);
                if (resourceField != null)
                {
                    resourceField.Value -= resource.Value;
                    resourcePlayer._dictionary.SetField(resourceField);
                    
                }
            }
        }
    }
}