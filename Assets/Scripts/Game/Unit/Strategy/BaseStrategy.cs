using System;
using Core.Binder;
using Game.Factories.Unit;
using Game.Unit.Data;
using Game.Unit.View;
using UnityEngine;

namespace Game.Unit.Strategy
{
    public class BaseStrategy : IStrategy
    {
        public event Action OnDestroy;
        
        protected UnitView UnitView;
        protected IUnitFactory UnitFactory;
        protected Transform Container;
        protected bool Ready;
        
        public virtual void Initialize(UnitView unitView, Transform container)
        {
            UnitView = unitView;
            UnitView.OnDamageTriggered += OnDamageTriggered;
            UnitView.OnCoolDownFinished += CoolDownFinished;
            
            Container = container;
            
            UnitFactory = BindManager.GetInstance<IUnitFactory>();

            Ready = true;
        }
        
        protected virtual void Shoot()
        {
            if (!Ready)
            {
                return;
            }
            
            var rocketView = UnitFactory.GetRocket(Container);
            rocketView.SetContext(new RocketDataContext(UnitView.transform.position, UnitView.Rotation, UnitView.DataContext.Id, UnitView.DataContext.Weapon));

            UnitView.StartCoolDown();
            Ready = false;
        }
        
        protected virtual void CoolDownFinished()
        {
            Ready = true;
        }
        
        protected virtual void OnDamageTriggered(int damage)
        {
            UnitView.DataContext.CurrentHealth.Value -= damage;
            if (UnitView.DataContext.CurrentHealth.Value <= 0)
            {
                OnDestroy?.Invoke();
            }
        }
        
        public virtual void Dispose()
        {   
            UnitView.OnDamageTriggered -= OnDamageTriggered;
        }
    }
}