using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Game.Player;


namespace Game.UI
{
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private GameObject _place;
        [SerializeField] private PlayerManager _player;
        [SerializeField] private int _currentId;
        [SerializeField] private List<UnityAction> _listActions = new List<UnityAction>();

        public GameObject Place { get => _place; set => _place = value; }
        public PlayerManager Player { get => _player; set => _player = value; }

        public int CurrentId { get => _currentId; set => _currentId = value; }

        public void AddActions(UnityAction unityAction)
        {
            _listActions.Add(unityAction);
        }

        public void ClearActions() => _listActions.Clear();
        public int GetCountActions() => _listActions.Count;
        public List<Sprite> GetSprites() => _sprites;

        public virtual void Setup() { }

        public virtual void SetupChooseButtons(int number) { }

     

        public virtual void UpdateButton(List<Button> buttons, List<Text> text) { }
    }
}
