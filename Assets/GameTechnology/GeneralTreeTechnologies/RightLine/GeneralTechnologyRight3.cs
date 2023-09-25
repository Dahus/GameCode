using Game.Economics;
using Game.Player;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTechnologyRight3 : AbstractTechnology
{
    // RoboticFactory is open
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
        var checkTechnology1 = player.GetPlayerTechnologies().GetTechnology(GetPreviousTechnologies1().GetNameTechnology());
        if (checkTechnology1 == null)
        {
            Debug.LogError("Технология " + GetPreviousTechnologies1().GetNameTechnology() + " не изучена.");
            return;
        }
        base.Operation(player);
    }
}
