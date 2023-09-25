using System.Collections;
using System.Collections.Generic;
using Game.GlobalData;
using UnityEngine;
using UnityEngine.UI;
using Game.Player;

namespace Game.UI
{
    public class UIResource : MonoBehaviour
    {
        [SerializeField] private Text _textEnergyCristal;
        [SerializeField] private Text _textWorkers;
        [SerializeField] private Text _textCredits;
        [SerializeField] private Text _textMetal;
        [SerializeField] private Text _textNanoStructure;

        public void UpdateResource(ResourcePlayer resource)
        {
            _textEnergyCristal.text = resource.GetResourceDictionary().GetValue(ConstantNameResource.ENERGY_CRISTAL).ToString();
            _textWorkers.text = resource.GetResourceDictionary().GetValue(ConstantNameResource.WORKERS).ToString() + "/" + resource.GetMaxWorkers().ToString();
            _textCredits.text = resource.GetResourceDictionary().GetValue(ConstantNameResource.CREDITS).ToString();
            _textMetal.text = resource.GetResourceDictionary().GetValue(ConstantNameResource.METAL).ToString();
            _textNanoStructure.text = resource.GetResourceDictionary().GetValue(ConstantNameResource.NANO_STRUCTURE).ToString();
        }
    }
}
