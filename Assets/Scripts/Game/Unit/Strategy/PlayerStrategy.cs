using System;
using Core.Binder;
using Game.Factories.Unit;
using Game.Service.Input;
using Game.Unit.Data;
using Game.Unit.View;
using UnityEngine;

namespace Game.Unit.Strategy
{
    public class PlayerStrategy : BaseStrategy
    {
        private IInputService _inputService;
        

        public override void Initialize(UnitView unitView, Transform container)
        {
            base.Initialize(unitView, container);
            
            _inputService = BindManager.GetInstance<IInputService>();
            
            _inputService.OnClick += OnClick;
            _inputService.OnPressed += OnTouchUpdate;
            UnitView.OnCoolDownFinished += CoolDownFinished;
        }

        private void OnClick(Vector2 touchPosition)
        {
            Shoot();
        }
        
        private void OnTouchUpdate(Vector2 touchPosition)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 0));
            
            float angleRad = Mathf.Atan2(mousePosition.y - UnitView.transform.position.y, mousePosition.x - UnitView.transform.position.x);
            var angle = (180 / Mathf.PI) * angleRad;
            UnitView.Rotate(angle);
        }
        
        public override void Dispose()
        {   
            base.Dispose();
            _inputService.OnClick -= OnClick;
            _inputService.OnPressed -= OnTouchUpdate;
            UnitView.OnCoolDownFinished -= CoolDownFinished;
        }
    }
}