using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Economics;

namespace Game.UpgradeSystem
{
    public class CostUpgradeLocator : MonoBehaviour
    {

        private Dictionary<string, DictionaryResource> costUnitsUpgrade = new Dictionary<string, DictionaryResource>();
        public void SetupCostUnitsUpgrade(ResourceUpgrade[] costs)
        {
            foreach (var cost in costs)
                costUnitsUpgrade.Add(cost.NameUpgrade, cost.Resource);
        }

        public DictionaryResource GetValue(string name) => costUnitsUpgrade[name];
    }
}
