using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Game.GlobalData;
using UnityEngine;
using UnityEngine.UI;
using Game.Player;


public class UIManager : MonoBehaviour
{
    
    
    public Button[] _button;

    public static UIManager instance;
    
    

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    public  void UpdateButtons(int count,GameObject obj)
    {
        for (int i = 0; i < _button.Length; i++)
        {
            _button[i].gameObject.SetActive(false);
            //_button[i].onClick.RemoveListener(Builder.instance.BuildFactory);
            _button[i].onClick.RemoveAllListeners();
        }
        for (int i = 0; i < count; i++)
        {
            _button[i].gameObject.SetActive(true);
        }

        if (count > 1)
        {
            AddFunctionBuild(obj);
            AddFunctionBuildUnit(obj);
        }
    }

    public void AddFunctionBuild(GameObject obj)
    {
        
       // _button[0].onClick.AddListener(Builder.instance.BuildFactory);
       // Observer.instance.SetNameBuild(ConstantNameBuild.CC);
    }

    public void AddFunctionBuildUnit(GameObject obj)
    {
        //_button[1].onClick.AddListener();
        //_button[1].onClick.AddListener(Builder.instance.BuildUnit);
       
        //Observer.instance.SetNameBuild(ConstantNameUnit.WARROBOT);
    }

}
