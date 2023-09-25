using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Game.GlobalSystems;
using Game.GlobalData;

namespace Game.UI
{
    public class PanelInfoTechnologies : MonoBehaviour
    {
        [SerializeField] private Text nameTechnologies;
        [SerializeField] private Text countStep;
        [SerializeField] private Text countMetal;
        [SerializeField] private Text countCrystals;
        [SerializeField] private Text countNanostructs;
        [SerializeField] private Text countCredits;
        [SerializeField] private GameObject panel;
        [SerializeField] private CSVReaderTechnology csvReaderTechnology;
        [SerializeField] private TechnologyInfo technology;


        private void Start()
        {
            csvReaderTechnology = EntityLocator.instance.GetCSVReaderTechnology();
        }
        public void OnPanel(int id)
        {
            technology = csvReaderTechnology.SearchTechnologies(id);
            UpdateInfo();
            panel.SetActive(true);
        }

        public void UpdateInfo()
        {
            nameTechnologies.text = technology.name;
            countStep.text = technology.countOfMovesToStudy.ToString();
            countCredits.text = technology.resource.GetValue(ConstantNameResource.CREDITS).ToString();
            countMetal.text = technology.resource.GetValue(ConstantNameResource.METAL).ToString();
            countCrystals.text = technology.resource.GetValue(ConstantNameResource.ENERGY_CRISTAL).ToString();
            countNanostructs.text = technology.resource.GetValue(ConstantNameResource.NANO_STRUCTURE).ToString();
        }

        public void OffPanel()
        {
            panel.SetActive(false);
        }
    }
}
