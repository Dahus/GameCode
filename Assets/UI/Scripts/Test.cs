using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void OnEnable () {
        EventManager.Subscribe ("event_name", MyFunction);
        EventManager.Subscribe("event_name1", Test1);
    }

    void OnDisable () {
        EventManager.Unsubscribe ("event_name", MyFunction);
        EventManager.Subscribe("event_name1", Test1);
        
    }


    private void Start()
    {
        EventManager.SendEvent ("event_name", "param_string", 1, 2);
        EventManager.SendEvent("event_name1",4,5);// вызов события
    }

    void MyFunction (object[] parameters) {
        Debug.Log (parameters.Length); // количество параметров -> 3 в примере
        Debug.Log (parameters[1]);        // выведет -> 1 
    }

    void Test1(object[] parameters)
    {
        int t1 = (int)parameters[0];
        int t2 = (int)parameters[1];
        Debug.Log(t1 + t2);
    }
}
