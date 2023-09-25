using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ButtonsAreaUI<T0,T1,T2> : MonoBehaviour
    {
        [SerializeField] private MenuButton[] ButtonsObject = new MenuButton[5]; 
        //[SerializeField] private MenuButtonData[] buttons;
       
        public void UpdateButtons(MenuButtonData[] countButtons)
        {
            for (int i = 0; i < countButtons.Length; i++)
            {
                var data = countButtons[i];
                ButtonsObject[data.ID].ButtonUpdate(data);
            }
        }
        private void Start()
        {
            ButtonsObject = GetComponentsInChildren<MenuButton>();
            /*for (int i = 0; i < ButtonsObject.Length; i++)
            {
                ButtonsObject[i].ButtonUpdate(buttons[0]);
            }*/
        }

    }
}
