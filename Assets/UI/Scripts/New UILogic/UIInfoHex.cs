using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIInfoHex : UIInfo
    {
        public override int CountWaitsText()
        {
            return 4;
        }

        public override void UpdateInfo(List<Text> activText)
        {

            for(int i = 0; i < activText.Count; i++)
            {
                activText[i].gameObject.SetActive(true);
            }
            activText[0].text = "название клетки";
            activText[1].text = "тип ресурса";
            activText[2].text = "количество ресурса";
            activText[3].text = "чья клетка";
           
        }
    }
}