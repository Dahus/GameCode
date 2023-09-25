using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInHex : MonoBehaviour,IServiceHex
{
    

    [SerializeField] private List<DataUIHexagon> _dataUIHexagons;

   

    public int  InfoHex()
    {
        return _dataUIHexagons[0].GetCountButton();
    }
    public void GetInfoHex()
    {
        throw new System.NotImplementedException();
    }

    public void InitializeList(List<DataUIHexagon> _data)
    {
        _dataUIHexagons=new List<DataUIHexagon>(); 
        _dataUIHexagons = _data;
    }
    
    
    
    
}
