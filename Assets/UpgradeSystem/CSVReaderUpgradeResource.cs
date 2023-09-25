using UnityEngine;
using System;

using Game.UpgradeSystem;
using Game.Economics;
using Game.GlobalSystems;
using Game.TechnologyBuild;

public class CSVReaderUpgradeResource : MonoBehaviour
{
    public TextAsset[] textAssetData;
    [SerializeField] private ResourceUpgrade[] resourceUpgrades;
    [SerializeField] private AbstractTechnologyBuild[] technologyBuild;

    public DictionaryResource GetResourceUpgrade(string key)
    {
        for (int i = 0; i < resourceUpgrades.Length; i++)
        {
            if (resourceUpgrades[i].NameUpgrade == key)
            {
                return resourceUpgrades[i].Resource;
            }
        }
        return null;
    }

    public AbstractTechnologyBuild GetAbstractTechnology(string key)
    {
        for(int i = 0; i < technologyBuild.Length; i++)
        {
            if (technologyBuild[i].GetName() == key)
            {
                return technologyBuild[i];
            }
        }
        return null;
    }

    private void Start()
    {
        ReadCSVUpgradeResource(textAssetData[0]);
        EntityLocator.instance.GetCostUpgradeLocator().SetupCostUnitsUpgrade(resourceUpgrades);
        ReadCSVCotsUpgradeBuild(textAssetData[1]);//, out EntityLocator.instance.GetStorageTechnologyBuild().GetAbstractTechnologyBuilds());
    }
    private void ReadCSVUpgradeResource(TextAsset assetRead)
    {
        string[] data = assetRead.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 6 - 1;
        resourceUpgrades = new ResourceUpgrade[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            resourceUpgrades[i] = new ResourceUpgrade();
            resourceUpgrades[i].NameUpgrade = data[6 * (i + 1)];
            int metal = int.Parse(data[6 * (i + 1) + 1]);
            int credits = int.Parse(data[6 * (i + 1) + 2]);
            int workers = int.Parse(data[6 * (i + 1) + 3]);
            int crystal = int.Parse(data[6 * (i + 1) + 4]);
            int nanoStructure = int.Parse(data[6 * (i + 1) + 5]);
            resourceUpgrades[i].Resource = new DictionaryResource();
            resourceUpgrades[i].Resource.SetupResource(metal, credits, workers, crystal, nanoStructure);
        }
    }

    private void ReadCSVCotsUpgradeBuild(TextAsset assetRead)
    {
        string[] data = assetRead.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 7 - 1;
        technologyBuild = new AbstractTechnologyBuild[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            technologyBuild[i] = new AbstractTechnologyBuild();
            string nameUpgrade = data[7 * (i + 1)];
            int metal = int.Parse(data[7 * (i + 1) + 1]);
            int credits = int.Parse(data[7 * (i + 1) + 2]);
            int workers = int.Parse(data[7 * (i + 1) + 3]);
            int crystal = int.Parse(data[7 * (i + 1) + 4]);
            int nanoStructure = int.Parse(data[7 * (i + 1) + 5]);
            int countStep = int.Parse(data[7 * (i + 1) + 6]);
            var resource = new DictionaryResource();
            resource.SetupResource(metal, credits, workers, crystal, nanoStructure);
            technologyBuild[i].Setup(resource,nameUpgrade, countStep);
        }
    }

   

  

}
