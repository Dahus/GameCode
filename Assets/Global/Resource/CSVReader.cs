using System;
using Game.Economics;
using Game.GlobalData;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset[] textAssetData;
    [SerializeField] private ResourceBuild[] resourceBuilds;
    [SerializeField] private ResourceUnit[] resourceUnits;


    public DictionaryResource GetResourceBuild(string key)
    {
        for (int i = 0; i < resourceBuilds.Length; i++)
        {
            if (resourceBuilds[i].Name == key)
            {
                return resourceBuilds[i].Resource;
            }
        }

        return null;
    }

    public DictionaryResource GetResourceUnit(string key)
    {
        for (int i = 0; i < resourceUnits.Length; i++)
        { 
            if (resourceUnits[i].Name == key)
            {
                return resourceUnits[i].Resource;
            }
        }

        return null;
    }


    private void Start()
    {
        ReadCSVBuild(textAssetData[0]);
        ReadCSVUnit(textAssetData[1]);
    }

    private void ReadCSVBuild(TextAsset assetRead)
    {
        string[] data = assetRead.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 6 - 1;
        resourceBuilds = new ResourceBuild[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            resourceBuilds[i] = new ResourceBuild();
            resourceBuilds[i].Name = data[6 * (i + 1)];
            int metal = int.Parse(data[6 * (i + 1) + 1]);
            int credits = int.Parse(data[6 * (i + 1) + 2]);
            int workers = int.Parse(data[6 * (i + 1) + 3]);
            int crystal = int.Parse(data[6 * (i + 1) + 4]);
            int nanoStructure = int.Parse(data[6 * (i + 1) + 5]);
            resourceBuilds[i].Resource = new DictionaryResource();
            resourceBuilds[i].Resource.SetupResource(metal, credits, workers, crystal, nanoStructure);
        }
    }

    private void ReadCSVUnit(TextAsset assetRead)
    {
        string[] data = assetRead.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 6 - 1;
        resourceUnits = new ResourceUnit[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            resourceUnits[i] = new ResourceUnit();
            resourceUnits[i].Name = data[6 * (i + 1)];
            int metal = int.Parse(data[6 * (i + 1) + 1]);
            int credits = int.Parse(data[6 * (i + 1) + 2]);
            int workers = int.Parse(data[6 * (i + 1) + 3]);
            int crystal = int.Parse(data[6 * (i + 1) + 4]);
            int nanoStructure = int.Parse(data[6 * (i + 1) + 5]);
            resourceUnits[i].Resource = new DictionaryResource();
            resourceUnits[i].Resource.SetupResource(metal, credits, workers, crystal, nanoStructure);
        }
    }
}