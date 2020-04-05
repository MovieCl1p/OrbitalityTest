using System.Collections.Generic;
using System.Linq;
using Core.Binder;
using Core.Dispatcher;
using Game.Service;
using Game.Service.Data;
using Game.Service.NPC;
using Game.Signals;
using Game.UI.Base;
using Game.UI.Game.View;
using Game.UI.Game.View.Unit.Data;
using Game.Unit.Data;
using Game.Unit.Mediator;
using Game.Unit.Strategy;
using UnityEngine;

namespace Game.UI.Game.Mediator
{
    public class GameMediator : BaseMediator<GameScreen>
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject] 
        public IUnitDataMapper UnitDataMapper { get; set; }
        
        [Inject] 
        public IEnemyDataProviderService EnemyProvider { get; set; }
        
        [Inject] 
        public IDispatcher Dispatcher { get; set; }
        
        [Inject] 
        public IEffectService EffectService { get; set; }
        
        private List<UnitMediator> _units = new List<UnitMediator>();

        private int _timeK = -1;
        protected override void OnEntry()
        {
            base.OnEntry();
            
            View.OnPauseClick += OnPauseClick;
            View.OnRestartClick += OnRestartClick;
            View.OnMainMenuClick += OnMainMenuClick;
            
            UpdateData();
            CreateEnemies();
            
            EffectService.ShowEffect(true);
        }

        private void OnMainMenuClick()
        {
            Time.timeScale = 1;
            Dispatcher.Fire(new OpenMainMenuSignal());
        }

        private void OnRestartClick()
        {
            Time.timeScale = 1;
            Dispatcher.Fire(new OpenMainMenuSignal());
        }

        private void OnPauseClick()
        {
            Time.timeScale += _timeK;
            _timeK *= -1;
        }

        private void UpdateData()
        {
            UserData userData = UserService.GetUserData();
            UnitDataContext dataContext = UnitDataMapper.MapData(userData);
            dataContext.IsPlayer = true;
            
            var unitMediator = new UnitMediator<PlayerStrategy>();
            unitMediator.Entry(dataContext, View.UnitContent);
            unitMediator.UnitDestoyed += OnUnitDestroyed;
            
            _units.Add(unitMediator);

            View.SetDataContext(userData);
        }
        
        private void CreateEnemies()
        {
            var enemies = EnemyProvider.GetEnemies();
            
            for (int i = 0; i < enemies.Count; i++)
            {
                UnitDataContext enemyDataContext = UnitDataMapper.MapData(enemies[i]);
                enemyDataContext.StartPosition = Random.Range(0.0f, 1.0f);
                
                var enemy = new UnitMediator<EnemyStrategy>();
                enemy.Entry(enemyDataContext, View.UnitContent);
                enemy.UnitDestoyed += OnUnitDestroyed;
                
                _units.Add(enemy);
            }
        }

        private void OnUnitDestroyed(UnitMediator unit)
        {
            unit.UnitDestoyed -= OnUnitDestroyed;
            unit.Exit();
            _units.Remove(unit);

            if (!unit.DataContext.IsPlayer)
            {
                UserService.AddScore();    
            }
            
            CheckGameEnd();
        }

        private void CheckGameEnd()
        {
            if (_units.Count <= 1)
            {
                ShowEndScreen();
                return;
            }

            if (!_units.Any(x => x.DataContext.IsPlayer))
            {
                ShowEndScreen();
            }
        }

        private void ShowEndScreen()
        {
            EffectService.ShowEffect(false);
            Time.timeScale = 0;
            View.ShowFinishScreen();
        }

        protected override void OnExit()
        {
            EffectService.ShowEffect(false);
            
            for (int i = 0; i < _units.Count; i++)
            {
                _units[i].Exit();
            }
            
            _units.Clear();
            
            View.OnPauseClick -= OnPauseClick;
            View.OnRestartClick -= OnRestartClick;
            View.OnMainMenuClick -= OnMainMenuClick;
            
            base.OnExit();
        }
    }
}