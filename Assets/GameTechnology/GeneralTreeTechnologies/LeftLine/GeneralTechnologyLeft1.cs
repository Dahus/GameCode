using Game.Economics;
using Game.GlobalData;
using Game.Player;


using System.Collections.Generic;
using UnityEngine;


public class GeneralTechnologyLeft1 : AbstractTechnology
{

    //Warrobot is open
    public override void SetupTechnology(string name, int countOfMaves, DictionaryResource resource, string predecessorName1, string predecessorName2, AbstractTechnologyTree treeTechnologies)
    {
        necessaryPreviousTechnologies = new List<AbstractTechnology>(2);
        SetNameTechnology(name);
        SetCountOfMovesToStudyTechnology(countOfMaves);
        SetResourceTechnology(resource);
    }

    

    //нужна факторка  Это WarRobot
    public override void CallImpactObject(PlayerManager player)
    {
       var factory = player.GetFactoryUnits();
       factory.OpenUnit(ConstantNameUnit.WARROBOT); 
    }



}
