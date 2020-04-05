using Core.Binder;
using Core.Dispatcher;
using Game.Service;
using Game.Signals;
using Game.UI.Base;
using Game.UI.Game.Mediator;
using Game.UI.MainMenu.View;

namespace Game.UI.MainMenu.Mediator
{
    public class MainMenuMediator : BaseMediator<MainMenuScreen>
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        
        [Inject]
        public IUserService UserService { get; set; }
        
        protected override void OnEntry()
        {
            base.OnEntry();
            Subscribe();
        }

        protected override void OnExit()
        {
            base.OnExit();
            UnSubscribe();
        }

        private void OnStartClick()
        {
            Dispatcher.Fire(new StartGameSignal());
        }
        
        private void OnLoadClick()
        {
            UserService.LoadData();
            View.UpdateData();
        }

        private void OnSaveClick()
        {
            UserService.SaveData();
        }
        
        private void OnSelectPlanet(string id)
        {
            UserService.ChangePlanet(id);
        }
        
        private void Subscribe()
        {
            View.OnStartButtonClick += OnStartClick;
            View.OnSaveClick += OnSaveClick;
            View.OnLoadClick += OnLoadClick;
            View.OnSelectPlanet += OnSelectPlanet;
        }

        private void UnSubscribe()
        {
            View.OnStartButtonClick -= OnStartClick;
            View.OnSaveClick -= OnSaveClick;
            View.OnLoadClick -= OnLoadClick;
            View.OnSelectPlanet -= OnSelectPlanet;
        }
    }
}