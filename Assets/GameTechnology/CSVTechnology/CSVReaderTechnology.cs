using System;
using System.Collections.Generic;
using System.Text;
using Game.Economics;
using UnityEngine;

public class CSVReaderTechnology : MonoBehaviour
{
    public TextAsset[] textAssetData;
    [SerializeField] private TechnologyInfo[] _generalTechnologies;
    private GeneralTreeTechnologies _generalTreeTechnologies = new GeneralTreeTechnologies();

    [SerializeField] private TechnologyInfo[] _administratorTechnologies;
    [SerializeField] private TechnologyInfo[] industrialistTechnologies;

    public void SetupTechnologyTrees()
    {

        ReadCSVGeneral(textAssetData[0], out _generalTechnologies);
        _generalTreeTechnologies.SetupTechnologies(_generalTechnologies);

        //ReadCSVAdministrator(textAssetData[1]);

        //ReadCSVIndustrialist(textAssetData[2]);

    }

    private void ReadCSVGeneral(TextAsset assetRead, out TechnologyInfo[] info)
    {
        string[] data = assetRead.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 10 - 1;
        info = new TechnologyInfo[tableSize];
        Debug.LogError(tableSize);
        for (int i = 0; i < tableSize; i++)
        {
            info[i] = new TechnologyInfo();
            info[i].name = data[10 * (i + 1)];
            info[i].countOfMovesToStudy = int.Parse(data[10 * (i + 1) + 1]);
            int metal = int.Parse(data[10 * (i + 1) + 2]);
            int credits = int.Parse(data[10 * (i + 1) + 3]);
            int workers = int.Parse(data[10 * (i + 1) + 4]);
            int crystal = int.Parse(data[10 * (i + 1) + 5]);
            int nanoStructure = int.Parse(data[10 * (i + 1) + 6]);
            info[i].resource = new DictionaryResource();
            info[i].resource.SetupResource(metal, credits, workers, crystal, nanoStructure);
            info[i].predecessorName1 = data[10 * (i + 1) + 7];
            info[i].predecessorName2 = data[10 * (i + 1) + 8];
            info[i].id = int.Parse(data[10 * (i + 1) + 9]);
        }
    }




    public TechnologyInfo SearchTechnologies(int id)
    {
        foreach( var tech in _generalTechnologies)
        {
            if (id == tech.id)
                return tech;
        }

        return null;
    }

    public TechnologyInfo[] GetGeneralTechnologyInfo()
    {
        return _generalTechnologies;
    }

    public List<AbstractTechnology> GetGeneralTechnologyTree()
    {
        return _generalTreeTechnologies.GetTechnologies();
    }

    public TechnologyInfo[] GetAdministratorTechnologyInfo()
    {
        return _administratorTechnologies;
    }
    public TechnologyInfo[] GetIndustrialistTechnologyInfo()
    {
        return industrialistTechnologies;
    }
}
