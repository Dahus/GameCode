using Game.Economics;
using Game.GlobalSystems;
using Game.GlobalData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.TechnologyBuild
{
    public class StorageTechnologyBuild : MonoBehaviour
    {
        [SerializeField] private List<AbstractTechnologyBuild> _allBuildTechnology;
        public void SetupAllTechnologies()
        {

            _allBuildTechnology = new List<AbstractTechnologyBuild>();
            TechnologyIncreaseDamageRobots1 technologyIncreaseDamageRobots1 = new TechnologyIncreaseDamageRobots1();
            _allBuildTechnology.Add(technologyIncreaseDamageRobots1);

            TechnologyIncreaseDamageRobots2 technologyIncreaseDamageRobots2 = new TechnologyIncreaseDamageRobots2();
            _allBuildTechnology.Add(technologyIncreaseDamageRobots2);

            TechnologyIncreaseSpeedRobots1 technologyIncreaseSpeedRobots1 = new TechnologyIncreaseSpeedRobots1();
            _allBuildTechnology.Add(technologyIncreaseSpeedRobots1);

            TechnologyIncreaseSpeedRobots2 technologyIncreaseSpeedRobots2 = new TechnologyIncreaseSpeedRobots2();
            _allBuildTechnology.Add(technologyIncreaseSpeedRobots2);

            TechnologyIncreaseStrengthRobots1 technologyIncreaseStrengthRobots1 = new TechnologyIncreaseStrengthRobots1();
            _allBuildTechnology.Add(technologyIncreaseStrengthRobots1);

            TechnologyIncreaseStrengthRobots2 technologyIncreaseStrengthRobots2 = new TechnologyIncreaseStrengthRobots2();
            _allBuildTechnology.Add(technologyIncreaseStrengthRobots2);


            var csvReader = EntityLocator.instance.GetCSVReaderUpgradeResource();
            technologyIncreaseDamageRobots1.Setup(csvReader.GetAbstractTechnology(ConstantNameTechnologyBuild.DAMAGEROBOTS1));
            technologyIncreaseDamageRobots2.Setup(csvReader.GetAbstractTechnology(ConstantNameTechnologyBuild.DAMAGEROBOTS2));
            technologyIncreaseSpeedRobots1.Setup(csvReader.GetAbstractTechnology(ConstantNameTechnologyBuild.SPEEDROBOTS1));
            technologyIncreaseSpeedRobots2.Setup(csvReader.GetAbstractTechnology(ConstantNameTechnologyBuild.SPEEDROBOTS2));
            technologyIncreaseStrengthRobots1.Setup(csvReader.GetAbstractTechnology(ConstantNameTechnologyBuild.STRENGTHROBOTS1));
            technologyIncreaseStrengthRobots2.Setup(csvReader.GetAbstractTechnology(ConstantNameTechnologyBuild.STRENGTHROBOTS2));

        }

        public List<AbstractTechnologyBuild> GetAbstractTechnologyBuilds() => _allBuildTechnology;
        
        public DictionaryResource GetResource(string name)
        {
            DictionaryResource res = new DictionaryResource();
            foreach (var Tech in _allBuildTechnology)
            {
                if (Tech.GetName() == name)
                {
                    res = Tech.GetDictionaryResource();
                }
            }
            return res;
        }

        public AbstractTechnologyBuild GetTechnologyBuild(string name)
        {
            AbstractTechnologyBuild currentTechnology = new AbstractTechnologyBuild();
            foreach (var Tech in _allBuildTechnology)
            {
                if (Tech.GetName() == name)
                {
                    currentTechnology = Tech;
                }
            }
            return currentTechnology;
        }




    }
}
