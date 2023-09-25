using Game.Economics;
using Game.Player;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTechnologyRight1 : AbstractTechnology
{
    //Wall is open
    public override void SetupTechnology(string name, int countOfMaves, DictionaryResource resource, string predecessorName1, string predecessorName2, AbstractTechnologyTree treeTechnologies)
    {
        necessaryPreviousTechnologies = new List<AbstractTechnology>(2);
        SetNameTechnology(name);
        SetCountOfMovesToStudyTechnology(countOfMaves);
        SetResourceTechnology(resource);
    }
}
