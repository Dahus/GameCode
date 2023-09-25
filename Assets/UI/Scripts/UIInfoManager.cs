using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Game.Economics;

namespace Game.UI
{
    public class UIInfoManager : MonoBehaviour
    {
        [SerializeField] private Text[] texts;
        [SerializeField] private List<Text> activeTexts = new List<Text>();
        [SerializeField] private PanelInfoForButton panel;


        public List<Text> GetActiveTexts(int count)
        {
            activeTexts.Clear();
            for (int i = 0; i < count; i++)
            {
                activeTexts.Add(texts[i]);
            }
            return activeTexts;
        }

        public void offText()
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].gameObject.SetActive(false);
            }
           
        }


        public void SetupPanelInfoForButton(int number)
        {

        }

        public void UpdatePanelResource(string name,DictionaryResource resource)
        {
            panel.UpdateResource(resource);
            panel.UpdateName(name);        
        }

        public void UpdatePanelStep(string name, int step)
        {
            panel.UpdateCountStep(step);
            panel.UpdateName(name);
        }

        public void UpdatePanerResourceAndStep(string name, DictionaryResource resource, int step)
        {
            panel.UpdateResource(resource);
            panel.UpdateCountStep(step);
            panel.UpdateName(name);
        }

      //  public void UpdateIcon(Sprite sprite) => imageIcon.sprite = sprite;


     //   public Image GetImageIcon() => imageIcon;

      

    }
}
