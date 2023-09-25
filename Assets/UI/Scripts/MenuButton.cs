using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private UnityEvent _event;
        public UnityAction action;
        private Button _button;
        private Text _text;

        public string Name { get => Name; set => Name = value; }
        public Sprite Sprite { get => Sprite; set => Sprite = value; }
        public UnityEvent Event { get => Event; set => Event = value; }
        private void Start()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<Text>();
            _button.onClick.AddListener(() => _event?.Invoke());
        }
        public void ButtonUpdate(string newName, Sprite newSprite, UnityEvent newEvent)
        {
            _name = newName;
            _sprite = newSprite;
            _event = newEvent;
            _text.text = newName;
            _button.GetComponent<Image>().sprite = newSprite;
            _event = newEvent;
        }
        public void ButtonUpdate(MenuButtonData data)
        {
            ButtonUpdate(data.Name, data.Sprite, data.Event);
        }
    }
}
