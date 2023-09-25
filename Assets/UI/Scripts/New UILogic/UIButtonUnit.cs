using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.GridSystem;
using Game.UnitsSystem;

namespace Game.UI
{
    public class UIButtonUnit : UIButton
    {
        [SerializeField] private GridHex _hex;
        [SerializeField] protected AbstractUnit _unit;



        public override void UpdateButton(List<Button> buttons,List<Text> texts)
        {


            
        }


    }
}
