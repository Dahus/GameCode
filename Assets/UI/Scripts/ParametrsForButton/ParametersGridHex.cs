using System.Collections;
using System.Collections.Generic;
using Game.GridSystem;
using UnityEngine;
using System;
[Serializable] 
public class ParametersGridHex : IReturnObjects
{
    public GridHex gridHex;
  
    public object[] ReturnObjects()
    {
        return new object[] {gridHex};
    }
}
