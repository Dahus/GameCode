using Game.Economics;
using System;
using UnityEngine;

[Serializable]
public class TechnologyInfo
{
    public string name;
    public int id;
    public int countOfMovesToStudy;
    public DictionaryResource resource;
    public string predecessorName1;
    public string predecessorName2;
}
