using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.TechnologyBuild
{
    [Serializable]
    public class PlayerTechnologyBuild
    {
        [SerializeField] private List<AbstractTechnologyBuild> _technologies;


        public PlayerTechnologyBuild()
        {
            _technologies = new List<AbstractTechnologyBuild>();
        }

        public void AddTechnology(AbstractTechnologyBuild technology)
        {
            _technologies.Add(technology);
            technology.Perform();
        }

        public bool CheckTechnology(string name)
        {
            bool flag = false;
            foreach (var Tech in _technologies)
            {
                if (Tech.GetName() == name)
                {
                    flag = true;
                }
            }
            return flag;

        }

    }
}
