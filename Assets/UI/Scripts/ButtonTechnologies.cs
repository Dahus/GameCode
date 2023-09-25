using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Game.GlobalSystems;


namespace Game.UI
{
    public class ButtonTechnologies : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        private PanelInfoTechnologies panel;
        [SerializeField] private int id;
        [SerializeField] private Image icon;
        [SerializeField] private Image mask;
        private void Start()
        {
            panel = EntityLocator.instance.GetPanelInfoTechnologies();
            panel.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            panel.OnPanel(id);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            panel.OffPanel();
        }
    }
}
