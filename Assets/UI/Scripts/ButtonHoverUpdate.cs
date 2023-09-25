using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Game.GlobalSystems;
using Game.Player;

public class ButtonHoverUpdate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PanelInfoForButton hoverPanel;
    private RectTransform panelRectTransform;
    [SerializeField] private int number;

    private void Start()
    {
        panelRectTransform = hoverPanel.GetComponent<RectTransform>();
        hoverPanel.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverPanel.PanelOn();
        Observer.instance.ChooseButtonInUIButton(number);
        //hoverPanel.transform.position = gameObject.transform.position;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.PanelOff();
    }


    
   

    
}
