using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TestInt : IReturnObjects
{
    public int t;
    

    public object[] ReturnObjects()
    {
        
        return new object[] { t };
    }
}
