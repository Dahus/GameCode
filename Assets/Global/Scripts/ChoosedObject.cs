using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.UI;
public class ChoosedObject : MonoBehaviour
{
    private Dictionary<int, EventCustom> _eventDictionaryForButton;
    private Dictionary<int, IReturnObjects> _dictionaryParametresForButton;
    [SerializeField] private GameObject _currentPlace;
    
   
    public IReturnObjects SearchReturnObject(int id)
    {
        IReturnObjects obj=null;
        foreach (var Object in _dictionaryParametresForButton)
        {
            if (Object.Key == id)
            {
                obj = Object.Value;
            }
        }
        return obj;
    }
    
    public int GetCountEvent()
    {
        return _eventDictionaryForButton.Count;;
    }


    public void AddDictionaryEvent(int id, EventCustom eventCustom)
    {
        _eventDictionaryForButton.Add(id, eventCustom);
        Debug.Log(id+" добавили ивент " );
    }

   
    public void AddDictionaryParameters(int id, IReturnObjects obj)
    {
        _dictionaryParametresForButton.Add(id,obj);
        Debug.Log(id+" добавили параметры" );
    }

    

    public EventCustom GetEventCustom(int id)
    {
        return _eventDictionaryForButton[id];
    }

    public void ClearDictionary()
    {
        _eventDictionaryForButton.Clear();
        _dictionaryParametresForButton.Clear();
    }

    public void Setup()
    {
        _eventDictionaryForButton = new Dictionary<int, EventCustom>();
        _dictionaryParametresForButton = new Dictionary<int, IReturnObjects>();
    }

    public void SetPlace(GameObject place)
    {
        _currentPlace = place;
    }

    public GameObject GetPlace()
    {
        return _currentPlace;
    }
    
 /*   private void Start()
    {
        _eventDictionaryForButton = new Dictionary<int, EventCustom>();
        _dictionaryParametresForButton = new Dictionary<int, IReturnObjects>();
    }
    */
   /* private void Start()
    {
        //Test();
        //_buttonLogic[0].SetEvent(_eventDictionaryForButton[1]);
        //_buttonLogic[0].Subscribe(_eventDictionaryForButton[1]);
        //_buttonLogic[0]._event = _eventDictionaryForButton[1];
        //_buttonLogic[1]._event = _eventDictionaryForButton[2];
        //_buttonLogic[2]._event = _eventDictionaryForButton[3];
        //_buttonLogic[3]._event = _eventDictionaryForButton[4];
    }*/

    public void Test()
    {
        EventCustom test1 = new EventCustom();
        EventCustom test2 = new EventCustom();
        EventCustom test3 = new EventCustom();
        EventCustom test4 = new EventCustom();
        TestInt testInt = new TestInt();
        testInt.t = 5;
        TestIntInt testIntInt = new TestIntInt();
        testIntInt.t = 5;
        testIntInt.t1 = 10;
        TestIntString testIntString = new TestIntString();
        testIntString.str = "привет";
        testIntString.t = 20;

        test1.AddListener(Log1);
        test2.AddListener(Log2);
        test3.AddListener(Log3);
        test4.AddListener(Log4);
        _eventDictionaryForButton = new Dictionary<int, EventCustom>();
        _eventDictionaryForButton.Add(1,test1);
        _eventDictionaryForButton.Add(2,test2);
        _eventDictionaryForButton.Add(3,test3);
        _eventDictionaryForButton.Add(4,test4);
        _dictionaryParametresForButton = new Dictionary<int, IReturnObjects>();
        _dictionaryParametresForButton.Add(1,testInt);
        _dictionaryParametresForButton.Add(2,testIntString);
        _dictionaryParametresForButton.Add(3,testIntInt);
        _dictionaryParametresForButton.Add(4,testInt);
        
    }

    public void Log1(object[] parameters)//int 
    {
       //Debug.Log(parameters.GetValue(0));
       //Debug.Log(parameters[0]);
        ;
        TestInt testInt = (TestInt)parameters[0];
        
         Debug.Log(testInt.t+"кнопка");
    }

    public void Log2(object[] parameters)//int and string
    {


        TestIntString testIntString = (TestIntString)parameters[0];
       
            
        Debug.Log(testIntString.t+" "+testIntString.str+" кнопка");
    }

    public void Log3(object[] parameters)//int and int
    {
        Debug.Log((int)parameters[0]+(int)parameters[1]+" сумма и кнопка");
    }

    public void Log4(object[] parameters)
    {
     Debug.Log("Кнопка122212123");   
    }
    
}
