using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using Game.Economics;
using Game.GlobalData;
public class PanelInfoForButton : MonoBehaviour
{
    [SerializeField] private GameObject allPanel;
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textCostStep;
    [SerializeField] private GameObject panelResurce;
    [SerializeField] private Text textCristal;
    [SerializeField] private Text textMetals;
    [SerializeField] private Text textWorkers;
    [SerializeField] private Text textNanostructure;
    [SerializeField] private Text textCredits;


    public void UpdateResource(DictionaryResource resource)
    {
        panelResurce.SetActive(true);
        textCristal.text = resource.GetValue(ConstantNameResource.ENERGY_CRISTAL).ToString();
        textMetals.text = resource.GetValue(ConstantNameResource.METAL).ToString();
        textWorkers.text = resource.GetValue(ConstantNameResource.WORKERS).ToString();
        textNanostructure.text = resource.GetValue(ConstantNameResource.NANO_STRUCTURE).ToString();
        textCredits.text = resource.GetValue(ConstantNameResource.CREDITS).ToString();
    }

    public void UpdateName(string name)
    {
        textName.text = name;
    }

    public void UpdateCountStep(int step)
    {
        textCostStep.gameObject.SetActive(true);
        textCostStep.text = step.ToString();
    }

    public void PanelOff()
    {
        textCostStep.gameObject.SetActive(false);
        panelResurce.SetActive(false);
        allPanel.SetActive(false);
    }

    public void PanelOn()
    {
        allPanel.SetActive(true);
    }

}

