using Game.Economics;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTechnologyLeft2 : AbstractTechnology
{
    //paratrooper is open
    public override void SetupTechnology(string name, int countOfMaves, DictionaryResource resource, string predecessorName1, string predecessorName2, AbstractTechnologyTree treeTechnologies)
    {
        necessaryPreviousTechnologies = new List<AbstractTechnology>(2);
        SetNameTechnology(name);
        SetCountOfMovesToStudyTechnology(countOfMaves);
        SetResourceTechnology(resource);
        AddNecessaryPreviousTechnology(treeTechnologies.GetTechnology(predecessorName1));
    }

    public override void Operation(PlayerManager player)
    {
        var checkTechnology = player.GetPlayerTechnologies().GetTechnology(GetPreviousTechnologies1().GetNameTechnology());
        if (checkTechnology == null)
        {
            Debug.LogError("Технология " + GetNameTechnology() + " не изучена.");
            return;
        }
        base.Operation(player);
    }
}
