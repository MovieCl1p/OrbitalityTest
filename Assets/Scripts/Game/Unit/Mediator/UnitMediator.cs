using System;
using Core.Binder;
using Game.Factories.Unit;
using Game.UI.Game.View.Unit.Data;
using Game.Unit.Strategy;
using Game.Unit.View;
using UnityEngine;

namespace Game.Unit.Mediator
{
    public class UnitMediator
    {
        public event Action<UnitMediator> UnitDestoyed;
        
        public UnitDataContext DataContext;
        protected Transform UnitContainer;
        
        public virtual void Entry(UnitDataContext dataContext, Transform container)
        {
            UnitContainer = container;
            DataContext = dataContext;
        }

        public virtual void Exit()
        {
        }
        
        protected void OnUnitDestroyed()
        {
            UnitDestoyed?.Invoke(this);
        }
    }
    
    public class UnitMediator<T> : UnitMediator where T : IStrategy, new()
    {
        private IUnitFactory _unitFactory;
        private T _strategy;
        private UnitView _unitView;
        
        public override void Entry(UnitDataContext dataContext, Transform container)
        {
            base.Entry(dataContext, container);

            _unitFactory = BindManager.GetInstance<IUnitFactory>();
            
            _unitView = CreateView(container);
            _unitView.SetContext(dataContext);

            ApplyStrategy<T>();
        }

        public void ApplyStrategy<TStrategy>() where TStrategy : T, new()
        {
            if (_strategy != null)
            {
                _strategy.OnDestroy -= OnUnitDestroyed;
                _strategy.Dispose();
            }
            
            _strategy = new TStrategy();
            _strategy.Initialize(_unitView, UnitContainer);
            _strategy.OnDestroy += OnUnitDestroyed;
        }

        public override void Exit()
        {
            base.Exit();
            if (_strategy != null)
            {
                _strategy.OnDestroy -= OnUnitDestroyed;
                _strategy.Dispose();
            }

            GameObject.Destroy(_unitView.gameObject);
        }

        private UnitView CreateView(Transform container)
        {
            return _unitFactory.GetUnit(container) as UnitView;
        }
    }
}