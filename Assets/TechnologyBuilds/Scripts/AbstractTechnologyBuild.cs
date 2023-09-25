using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Game.Economics;

namespace Game.TechnologyBuild
{
    [Serializable]
    public class AbstractTechnologyBuild
    {
        private DictionaryResource _resourceTechnology;
        [SerializeField] private string _nameTechnology;
        [SerializeField] private int _countOfMovesToStudy = 0;
        [SerializeField] private Sprite icon;

        public void Setup(DictionaryResource resourceTechnology, string nameTechnology, int countMoveToStudy)
        {
            _resourceTechnology = resourceTechnology;
            _nameTechnology = nameTechnology;
            _countOfMovesToStudy = countMoveToStudy;
        }

        public void Setup(AbstractTechnologyBuild abstractTechnologyBuild)
        {
            _resourceTechnology = abstractTechnologyBuild.GetDictionaryResource();
            _nameTechnology = abstractTechnologyBuild.GetName();
            _countOfMovesToStudy = abstractTechnologyBuild.GetCountMoves();
        }
        public DictionaryResource GetDictionaryResource() => _resourceTechnology;

        public string GetName() => _nameTechnology;

        public int GetCountMoves() => _countOfMovesToStudy;

        public Sprite GetIcon() => icon;

        public void SetIcon(Sprite sprite) => icon = sprite;

        public void Log()
        {
            _resourceTechnology.DebugLoger();
        }

        public virtual void Perform()
        {
            Debug.LogError("Технология добавилась");
        }

    }

    public class TechnologyIncreaseStrengthRobots1 : AbstractTechnologyBuild { }

    public class TechnologyIncreaseStrengthRobots2 : AbstractTechnologyBuild { }

    public class TechnologyIncreaseDamageRobots1 : AbstractTechnologyBuild { }

    public class TechnologyIncreaseDamageRobots2 : AbstractTechnologyBuild { }

    public class TechnologyIncreaseSpeedRobots1 : AbstractTechnologyBuild { }

    public class TechnologyIncreaseSpeedRobots2 : AbstractTechnologyBuild { }


}