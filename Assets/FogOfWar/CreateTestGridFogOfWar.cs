using System;
using System.Collections;
using System.Collections.Generic;
using Game.GridSystem;
using Game.UI;
using UnityEngine;

public class CreateTestGridFogOfWar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GridBuilder grid;
    [SerializeField] private ButtonLogicManager _buttonLogicManager;


    private void Start()
    {
       // grid.CreateGrid(_buttonLogicManager);
    }
}
