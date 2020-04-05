using System;
using System.Collections.Generic;
using Core;
using Core.Binder;
using Core.Dispatcher;
using DefaultNamespace.Commands;
using Game.Signals;
using Game.UI.Base;
using Game.UI.Game.Mediator;
using Game.UI.MainMenu.Mediator;

namespace Game
{
    public class AppRoot : SingletonBaseMonoBehaviour<AppRoot>
    {
        private IDispatcher _dispatcher;
        private Dictionary<Type, BaseMediator> _mediators = new Dictionary<Type, BaseMediator>();
        private BaseMediator _activeMediator;
        
        protected override void Start()
        {
            base.Start();
            
            InitializeMediators();
            
            StartCommand startCommand = new StartCommand(OnReadyToPlay);
            startCommand.Execute();

            _dispatcher = BindManager.GetInstance<IDispatcher>();
            _dispatcher.Subscribe<StartGameSignal>(OpenGameScreen);
            _dispatcher.Subscribe<OpenMainMenuSignal>(OpenMainMenuScreen);
        }

        private void InitializeMediators()
        {
            _mediators.Add(typeof(MainMenuMediator), new MainMenuMediator());
            _mediators.Add(typeof(GameMediator), new GameMediator());
        }

        private void OpenGameScreen(StartGameSignal signal)
        {
            _activeMediator?.Exit();
            _activeMediator = GetMediator<GameMediator>();
            _activeMediator.Entry();
        }
        
        private void OpenMainMenuScreen(OpenMainMenuSignal signal)
        {
            _activeMediator?.Exit();
            _activeMediator = GetMediator<MainMenuMediator>();
            _activeMediator.Entry();
        }

        private T GetMediator<T>() where T : BaseMediator
        {
            if (!_mediators.ContainsKey(typeof(T)))
            {
                return default(T);
            }

            return _mediators[typeof(T)] as T;
        }

        private void OnReadyToPlay()
        {
            OpenMainMenuScreen(null);
        }

        protected override void OnReleaseResources()
        {
            _dispatcher.Unsubscribe<StartGameSignal>(OpenGameScreen);
            _dispatcher.Unsubscribe<OpenMainMenuSignal>(OpenMainMenuScreen);
            base.OnReleaseResources();
        }
    }
}