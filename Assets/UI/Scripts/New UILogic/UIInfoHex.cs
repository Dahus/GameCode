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
            activText[0].text = "�������� ������";
            activText[1].text = "��� �������";
            activText[2].text = "���������� �������";
            activText[3].text = "��� ������";
           
        }
    }
}