using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUIHexagon : MonoBehaviour
{

    [SerializeField] private int _countButton;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;
    private void Start()
    {
        RandomGenerate();
    }

    public int GetCountButton()
    {
        return  _countButton;
    }
    
    public Sprite GetSprite()
    {
        return  _sprite;
    }
    
    public string GetDescription()
    {
        return  _description;
    }

    public void RandomGenerate()
    {
        _countButton = UnityEngine.Random.Range(0, 9);
    }
}
