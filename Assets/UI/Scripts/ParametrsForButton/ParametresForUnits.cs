using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametresForUnits : IReturnObjects
{
   public string Name;
   public int Step;
   public GameObject Place;

   public object[] ReturnObjects()
   {
      return new object[] { Name,Step,Place };
   }
}
