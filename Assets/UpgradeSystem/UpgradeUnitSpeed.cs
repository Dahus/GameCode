using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.UnitsSystem;
using Game.Economics;
using Game.GlobalSystems;


namespace Game.UpgradeSystem
{
    public class UpgradeUnitSpeed : AbstractUpgrade
    {
        [SerializeField] private AbstractUnit _unit;
        [SerializeField] private int _addSpeed;
        public override void MakeUpgrade()
        {
            if (_currentLevelUpgrade < _maxLevelUpgrade)
            {
                if (CheckResource())
                {
                    _currentLevelUpgrade++;
                    OpenUpgrade(_currentLevelUpgrade);
                    _unit.UpgradeActionPoint(_addSpeed);
                    var res = _unit.GetPlayerManager().GetResources();
                    DictionaryResource.SubstractionResourcePlayer(res, EntityLocator.instance.GetCostUpgradeLocator().GetValue(nameUpgrade));
                    EntityLocator.instance.GetUIResource().UpdateResource(res);
                }
            }
        }

        private void Start()
        {
            _unit = GetComponent<AbstractUnit>();
            Setup(1, null);
        }
    }
}
