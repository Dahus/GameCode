using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIntString : IReturnObjects
{
   public int t;
   public string str;


   public object[] ReturnObjects()
   {
      return new object[] { t,str };
   }
}
