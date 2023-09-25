using Game.Economics;
using Game.Player;
using Game.GlobalData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTechnologyCenter2 : AbstractTechnology
{

    //SNIPERDRONE is open
    public override void SetupTechnology(string name, int countOfMaves, DictionaryResource resource, string predecessorName1, string predecessorName2, AbstractTechnologyTree treeTechnologies)
    {
        necessaryPreviousTechnologies = new List<AbstractTechnology>();
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
            Debug.LogError("Не все технологии предшественники изучены.");
            return;
        }
        base.Operation(player);
    }

    public override void CallImpactObject(PlayerManager player)
    {
        var factory = player.GetFactoryUnits();
        factory.OpenUnit(ConstantNameUnit.SNIPERDRONE);
    }
}
