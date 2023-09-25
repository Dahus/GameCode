using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametresForBuilding : IReturnObjects
{
    public string Name;

    public object[] ReturnObjects()
    {
        return new object[] { Name };
    } 
}

