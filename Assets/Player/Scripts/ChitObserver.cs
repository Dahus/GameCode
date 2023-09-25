using Game.GridSystem;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Game.BuildingSystem;
using Game.UnitsSystem;
using Game.GlobalData;
using Game.Player;

public class ChitObserver : MonoBehaviour
{
    [SerializeField] private GridHex chooseHex;
    [SerializeField] private FactoryForUnits factoryUnits;
    [SerializeField] private FactoryForBuild factoryBuild;

    [SerializeField] private PlayerManager chiterPlayer;
    [SerializeField] private GameObject chitPanel;

    public void SetChooseHex(GridHex hex)=> chooseHex = hex;
   

    public void CreateDron()
    {
         factoryUnits.CreateUnitChit(ConstantNameUnit.SNIPERDRONE, chooseHex.gameObject);
    }

    public void CloseChitPanel() => chitPanel.SetActive(false);

    public void OpenChitPanel() => chitPanel.SetActive(true);

    public void OnObserverChit()=> Observer.instance.SetStateObserver(StateObserver.ChitObserver);
    public void OffObserverChit()=>Observer.instance.SetStateObserver(StateObserver.Standart);

    public void SetFactoryUnits(FactoryForUnits fact) => factoryUnits = fact;

    public void SetFactoryBuilds(FactoryForBuild fact) => factoryBuild = fact;



}
