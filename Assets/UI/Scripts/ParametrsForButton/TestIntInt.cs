using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIntInt : IReturnObjects
{
    public int t;
    public int t1;


    public object[] ReturnObjects()
    {
        return new object[] { t,t1 };
    }
}
