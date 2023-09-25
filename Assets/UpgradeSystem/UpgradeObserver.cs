using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.UnitsSystem;
using Game.GlobalSystems;
using Game.Economics;
using Game.Player;
namespace Game.UpgradeSystem
{
    public class UpgradeObserver : MonoBehaviour
    {
        [SerializeField] private List<AbstractUpgrade> _listUpgrade;
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private string nameUpgrade;
        private bool _IsCanUpgrade = false;
        public bool CheckListUpgrade()
        {
            _IsCanUpgrade = false;
            for (int i = 0; i < _listUpgrade.Count; i++)
            {
                _IsCanUpgrade = _listUpgrade[i].GetValueDictionaryIsOpen(currentLevel);
            }
            if (_IsCanUpgrade)
            {
                _IsCanUpgrade = CheckResource();
                if (_IsCanUpgrade)
                {
                    var res = GetComponent<AbstractUnit>().GetPlayerManager().GetResources();
                    DictionaryResource.SubstractionResourcePlayer(res, EntityLocator.instance.GetCostUpgradeLocator().GetValue(nameUpgrade));
                    EntityLocator.instance.GetUIResource().UpdateResource(res);
                }
            }
            return _IsCanUpgrade;
        }

        public bool CheckResource()
        {
            var res = Observer.instance.GetPlayerObserver().GetPlayerManager().GetResources();
            return DictionaryResource.CheckAvailabilityResource(res, EntityLocator.instance.GetCostUpgradeLocator().GetValue(nameUpgrade));
        }

    }
}