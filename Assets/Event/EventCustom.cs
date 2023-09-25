using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class EventCustom : UnityEvent<object[]>
{
    [SerializeField] private string _name;
    private Sprite _sprite;
    public void Setup(string name,Sprite sprite)
    {
        _name = name;
        _sprite = sprite;
    }
    
    public Sprite GetSprite() 
    {
        return _sprite;
    }

    public void Log()
    {
        Debug.Log(_name);
    }

};
