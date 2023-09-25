using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Generators
{

    public class StorageGenerators : MonoBehaviour
    {
        [SerializeField] private GeneratorRelief generatorRelief;
        [SerializeField] private GeneratorNoise generatorNoise;
        [SerializeField] private GeneratorBiome generatorBiome;
        [SerializeField] private GeneratorResource generatorResource;
        [SerializeField] private StateMap currentStateMap;

        public void StartGenerate()
        {
            //generatorRelief.CreateRelief(generatorNoise);
            generatorBiome.DistributingDots();
            generatorResource = new GeneratorResource();
            generatorResource.Setup(5);
            //currentStateMap = StateMap.Biome;
            //SwitchColorMap();
        }

        public void SwitchColorMap() 
        {
            switch (currentStateMap)
            {
                case StateMap.Relief:
                 //   generatorBiome.Colorize();
                    currentStateMap = StateMap.Biome;
                    break;
                case StateMap.Biome:
                 //   generatorRelief.Colorize();
                    currentStateMap = StateMap.Relief;
                    break;
                
            }
        }

        public List<ListHexes> GetListHexes() => generatorBiome.GetListHexes();

        public void SwitchMapRelief() 
        {
            
        }
        
    }

    public enum StateMap 
    {
        Relief,
        Biome,
        Resource
    }
}
