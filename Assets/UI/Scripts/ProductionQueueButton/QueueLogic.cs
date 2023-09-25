using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.BuildingSystem;
using Game.Player;

public class QueueLogic : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private Image[] images;
    [SerializeField] private AbstractCreateUnitBuild createUnitBuild;
    [SerializeField] private Image imageForButtons;
    [SerializeField] private Text textCountStep;
    public void UpdateButton()
    {
        if (Observer.instance.GetPlayerObserver().GetPlayerManager().TryGetComponent<PlayerAI>(out var bot))
            return;
        OffButton();
        SetButtons(createUnitBuild.GetCountUnitInProduction());
    }



    
    

    public void SetCreateUnitBuild(AbstractCreateUnitBuild abstractCreateUnitBuild)
    {
        createUnitBuild = abstractCreateUnitBuild;
        SetButtons(createUnitBuild.GetCountUnitInProduction());
    }

    public void SetButtons(int countButtons)
    {
        imageForButtons.gameObject.SetActive(true);
        for (int i = 0; i < countButtons; i++)
        {
            buttons[i].gameObject.SetActive(true);
            SetupImage(i, createUnitBuild.GetUnitIcon(i));
        }
    }

    public void SetupImage(int id, Sprite sprite) => images[id].sprite = sprite;
   
    
    public void OffButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
            //_uIButton = null;
        }
        imageForButtons.gameObject.SetActive(false);
    }

    public void UpdateTextCountStep(int step)
    {
        textCountStep.text = step.ToString();
    }

    public void Delete(int id)
    {
        createUnitBuild.DeleteObject(id);
    }
}
