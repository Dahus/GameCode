using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Game.BuildingSystem;
using Game.Events;
using Game.Player;
using Game.UI;
using Game.Market;


namespace Game.GlobalSystems
{
    public class StepManager : MonoBehaviour
    {
        [SerializeField] private ButtonLogicManager _uiManager;
        private List<PlayerManager> _playerQueue = new List<PlayerManager>();
        private PlayerManager _currentPlayer;
        [SerializeField] private UITechnologies uiTech;
        [SerializeField] private UIMarket uiMarket;
        [SerializeField] private Image _imageIcon;
        [SerializeField] private GameObject _panelUIInfo;


        private bool IsCoroutine = false;

        public void IsCoroutineTrue()
        {
            IsCoroutine = true;
            UpdatePlayer();
        }
        public void AddInQueue(PlayerManager player)
        {
            _playerQueue.Add(player);
        }

        public void SetupStartPlayer()
        {
            SetCurrentPlayer(_playerQueue[_playerQueue.Count - 1]);
        }

        public void NextPlayer()
        {
            SetCurrentPlayer(_currentPlayer);
         /*   if (_currentPlayer.gameObject.TryGetComponent(out PlayerAI bot))
            {
                _panelUIInfo.active = false;
                bot.PerfomrAnAction();
                bot.EndStepPlayer = false;
                SetCurrentPlayer(_currentPlayer);
            }*/
        }

        private void SetCurrentPlayer(PlayerManager player)
        {
            uiMarket.CloseTrade();
            _currentPlayer = player.GetNextPlayer();
            uiMarket.SetPlayer(_currentPlayer);
            uiTech.UpdatePlayer(_currentPlayer);
            uiTech.SetupUIButtonTechnologies();
            _currentPlayer.CanEndStep = false;
            _uiManager.OffButton();
            _uiManager.OffQueueButtons();
            _currentPlayer.UpdateResource();
            Observer.instance.SetHexagon(null);
            _imageIcon.sprite = _currentPlayer.GetIcon();
            Observer.instance.GetPlayerObserver().SetPlayerManager(_currentPlayer);
            Observer.instance.GetPlayerObserver().CancelFight();
            Observer.instance.SetStateObserver(StateObserver.Standart);
            _currentPlayer.NewStep();

            var objectComandCenter = _currentPlayer.GetPlayerBuilds()[0];
            if (!_currentPlayer.TryGetComponent<PlayerAI>(out var bot))
                Camera.main.transform.position = new Vector3(objectComandCenter.transform.position.x, objectComandCenter.transform.position.y, -10);
            ColorizeHexes.Colorize(_currentPlayer);
            if (!_currentPlayer.gameObject.TryGetComponent(out PlayerAI playerAI))
            {
                _panelUIInfo.SetActive(true);
            }
        }

        public List<PlayerManager> GetPlayers()
        {
            return _playerQueue;
        }

        public void UpdatePlayer()
        {
            //StartCoroutine(EndStep());
            // if (_currentPlayer != null)
            //    _currentPlayer.EndStep();

            if (!IsCoroutine)
            {
                _currentPlayer.EndStep();
            }
            //NextPlayer();
            else
            {
                if (_currentPlayer.CheckEndStepAllObject())
                {
                    NextPlayer();
                    IsCoroutine = false;
                }

                else
                {
                    StartCoroutine(EndStep());
                }
            }
        }
        IEnumerator EndStep()
        {
            yield return new WaitForSeconds(0.5f);
            UpdatePlayer();
          //  if (_currentPlayer.CheckEndStepAllObject())
            //    NextPlayer();
        }



        public PlayerManager GetCurrentPlayer()
        {
            return _currentPlayer;
        }
    }
}
