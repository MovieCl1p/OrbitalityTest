using Core;
using Game.Unit.Data;
using Game.Unit.View;
using UnityEngine;

namespace Game.Unit.Strategy
{
    public class EnemyStrategy : BaseStrategy, IUpdateHandler
    {
        private float _decisionDelay;
        private float _currentTime;
        public bool IsUpdating { get; set; }
        public bool IsRegistered { get; set; }
        
        public override void Initialize(UnitView unitView, Transform container)
        {
            base.Initialize(unitView, container);

            _decisionDelay = Random.Range(unitView.DataContext.Weapon.Cooldown,
                unitView.DataContext.Weapon.Cooldown + 10);
            
            UpdateNotifier.Instance.Register(this);
        }
        
        public void OnUpdate()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _decisionDelay)
            {
                UnitView.Rotate(Random.Range(0, 360));
                Shoot();
                _currentTime = 0;
            }
        }

        public override void Dispose()
        {
            UpdateNotifier.Instance.UnRegister(this);
            base.Dispose();
        }
    }
}