using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UnitsSystem;
public interface ICreateBuildingFunction : IBuildingFunction
{
     void Create(string name,int step, TypeObjectProduction objectProduction, Sprite icon);

    bool CheckHex();

     //List<UnitInProduction> _units { get; set; }
        
    //int Number { get; set; }
        
}


