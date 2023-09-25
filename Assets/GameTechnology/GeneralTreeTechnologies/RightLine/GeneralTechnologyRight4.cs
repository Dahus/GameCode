using Game.Economics;
using Game.Player;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTechnologyRight4 : AbstractTechnology
{   // Minomet is Open 
    public override void SetupTechnology(string name, int countOfMaves, DictionaryResource resource, string predecessorName1, string predecessorName2, AbstractTechnologyTree treeTechnologies)
    {
        necessaryPreviousTechnologies = new List<AbstractTechnology>(2);
        SetNameTechnology(name);
        SetCountOfMovesToStudyTechnology(countOfMaves);
        SetResourceTechnology(resource);
        AddNecessaryPreviousTechnology(treeTechnologies.GetTechnology(predecessorName1));
        AddNecessaryPreviousTechnology(treeTechnologies.GetTechnology(predecessorName2));
    }

    public override void Operation(PlayerManager player)
    {
        var checkTechnology1 = player.GetPlayerTechnologies().GetTechnology(GetPreviousTechnologies1().GetNameTechnology());
        var checkTechnology2 = player.GetPlayerTechnologies().GetTechnology(GetPreviousTechnologies2().GetNameTechnology());
        if (checkTechnology1 == null || checkTechnology2 == null)
        {
            Debug.LogError("Не все технологии предшественники изучены.");
            return;
        }
        base.Operation(player);
    }
}
