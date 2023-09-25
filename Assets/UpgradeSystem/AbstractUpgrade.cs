using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Economics;
using Game.Player;
using Game.GlobalSystems;
namespace Game.UpgradeSystem
{
    public abstract class AbstractUpgrade : MonoBehaviour
    {

        [SerializeField] protected string nameUpgrade;
        protected int _currentLevelUpgrade;
        protected int _maxLevelUpgrade;

        protected List<DictionaryResource> _listResourceUpgrades;
        protected Dictionary<int, bool> _dictionaryIsOpen = new Dictionary<int, bool>();

        public void Setup(int maxlevel, List<DictionaryResource> listResource)
        {
            _maxLevelUpgrade = maxlevel;
            _listResourceUpgrades = listResource;
            for (int i = 0; i < maxlevel; i++)
            {
                _dictionaryIsOpen.Add(i + 1, false);
            }
        }
        public bool GetValueDictionaryIsOpen(int key) => _dictionaryIsOpen[key];

        public void OpenUpgrade(int key) => _dictionaryIsOpen[key] = true;

        public bool CheckResource()
        {
            var res = Observer.instance.GetPlayerObserver().GetPlayerManager().GetResources();
            return DictionaryResource.CheckAvailabilityResource(res, EntityLocator.instance.GetCostUpgradeLocator().GetValue(nameUpgrade));
        }

        public abstract void MakeUpgrade();

    }
}