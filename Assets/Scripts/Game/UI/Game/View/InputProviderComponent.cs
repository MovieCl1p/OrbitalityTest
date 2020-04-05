using System;
using Core;
using Core.Binder;
using Game.Service.Input;
using Game.UI.Game.Mediator;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.Game.View
{
    public class InputProviderComponent : BaseMonoBehaviour, IInputProvider, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<Vector2> OnPressed;
        public event Action<Vector2> OnClick;

        private int _frameRate = 5;
        private int _lastFrame;
        private bool _pressed;
        
        protected override void Start()
        {
            base.Start();
            
            var inputService = BindManager.GetInstance<IInputService>();
            inputService.AddProvider(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            OnClick?.Invoke(eventData.position);
        }

        public void Update()
        {
            if (!_pressed)
            {
                return;
            }
            
            var currentFrame = Time.frameCount;
            if (currentFrame >= _lastFrame + _frameRate)
            {
                _lastFrame = currentFrame;
                OnPressed?.Invoke(Input.mousePosition);
            }
        }
    }
}