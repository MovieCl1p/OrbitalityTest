using System;
using Core.UI;
using Game.Service.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Game.View
{
    public class GameScreen : BaseView
    {
        public event Action OnPauseClick;
        public event Action OnRestartClick;
        public event Action OnMainMenuClick;
        
        [SerializeField] 
        private Transform _unitContent;
        
        [SerializeField] 
        private Button _pauseButton;

        [SerializeField] 
        private Text _scoreLabel;
        
        [SerializeField] 
        private FinishScreen _finishScreen;

        private UserData _dataContext;
        
        public Transform UnitContent
        {
            get { return _unitContent; }
        }

        protected override void Start()
        {
            base.Start();
            
            _pauseButton.onClick.AddListener(OnPause);
            _finishScreen.OnRestartClick += OnFinishRestartClick;
            _finishScreen.OnMainMenuClick += OnFinishMainMenuClick;
        }

        public void SetDataContext(UserData userData)
        {
            _dataContext = userData;
            _dataContext.Score.Bind(OnScoreChange);    
        }

        private void OnScoreChange(int data)
        {
            _scoreLabel.text = string.Format("Score: {0}", data);
        }

        private void OnFinishMainMenuClick()
        {
            _finishScreen.Hide();
            OnMainMenuClick?.Invoke();
        }

        private void OnFinishRestartClick()
        {
            _finishScreen.Hide();
            OnRestartClick?.Invoke();
        }

        public void ShowFinishScreen()
        {
            _finishScreen.Show(_dataContext);
        }
        
        private void OnPause()
        {
            OnPauseClick?.Invoke();
        }

        protected override void OnReleaseResources()
        {
            _dataContext.Score.UnBind(OnScoreChange);
            _finishScreen.OnRestartClick -= OnFinishRestartClick;
            _finishScreen.OnMainMenuClick -= OnFinishMainMenuClick;
            _pauseButton.onClick.RemoveListener(OnPause);
            base.OnReleaseResources();
        }


        
    }
}