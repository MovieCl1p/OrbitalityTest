using System;
using Core.UI;
using Game.Service.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Game.View
{
    public class FinishScreen : BaseView
    {
        public event Action OnRestartClick;
        public event Action OnMainMenuClick;

        [SerializeField] 
        private Text _score;
        
        [SerializeField] 
        private Button _restartButton;
        
        [SerializeField] 
        private Button _mainMenuButton;

        protected override void Start()
        {
            base.Start();
            
            _restartButton.onClick.AddListener(RestartClick);
            _mainMenuButton.onClick.AddListener(MainMenuClick);
        }

        private void MainMenuClick()
        {
            OnRestartClick?.Invoke();
        }

        private void RestartClick()
        {
            OnMainMenuClick?.Invoke();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show(UserData data)
        {
            _score.text = string.Format("Score: {0}", data.Score.Value);
            gameObject.SetActive(true);
        }

        protected override void OnReleaseResources()
        {
            _restartButton.onClick.RemoveListener(RestartClick);
            _mainMenuButton.onClick.RemoveListener(MainMenuClick);
            base.OnReleaseResources();
        }
    }
}