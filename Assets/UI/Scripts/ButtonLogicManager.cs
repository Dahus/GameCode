using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.BuildingSystem;

namespace Game.UI
{
    public class ButtonLogicManager : MonoBehaviour
    {
       // [SerializeField] private ButtonLogic[] _buttons;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private Text[] _textButtons;
        [SerializeField] private UIButton _uIButton;
        [SerializeField] private ChoosedObject _choosedObject;
        [SerializeField] private List<Button> _activButtons;
        [SerializeField] private List<Text> _activText;
        [SerializeField] private QueueLogic _queueLogicManager;
        [SerializeField] private UIInfoManager _uiInfoManager;
        public void UpdateButton()
        {
            int count = _uIButton.GetCountActions();
            _activButtons.Clear();
            _activText.Clear();
           
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i < count)
                {
                    _buttons[i].gameObject.SetActive(true);
                    _buttons[i].onClick.RemoveAllListeners();
                    _activButtons.Add(_buttons[i]);
                    _activText.Add(_textButtons[i]);
                    _textButtons[i].text = "1";
                }
                else _buttons[i].gameObject.SetActive(false);
            }
        }

       

        public void SetUIButton(UIButton uIButton, GameObject place)
        {
            _uIButton = uIButton;
            _uIButton.Place = place;
            UpdateButton();
            _uIButton.UpdateButton(_activButtons,_activText);

        }

        public void OffButton()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].gameObject.SetActive(false);
                _uIButton = null;
            }
            _uiInfoManager.offText();
        }

        public void SetupQueueLogicManager(AbstractCreateUnitBuild abstractCreateUnitBuild) => _queueLogicManager.SetCreateUnitBuild(abstractCreateUnitBuild);
        public void UpdateQueueLogicManager()=> _queueLogicManager.UpdateButton();

        public void OffQueueButtons() => _queueLogicManager.OffButton();

        public void UpdateTextCountStep(int step) => _queueLogicManager.UpdateTextCountStep(step);

        public void SetUIInfo(UIInfo uIInfo)
        {
            uIInfo.UpdateInfo(_uiInfoManager.GetActiveTexts(uIInfo.CountWaitsText()));
        }

    }
}
