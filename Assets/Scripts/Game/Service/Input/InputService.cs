using System;
using Game.UI.Game.Mediator;
using UnityEngine;

namespace Game.Service.Input
{
    public class InputService : IInputService
    {
        public event Action<Vector2> OnPressed;
        public event Action<Vector2> OnClick;

        private bool _pressed = false;
        
        public void AddProvider(IInputProvider provider)
        {
            provider.OnPressed += OnProviderPressed;
            provider.OnClick += OnProviderClick;
        }
        
        public void RemoveProvider(IInputProvider provider)
        {
            provider.OnPressed -= OnProviderPressed;
            provider.OnClick -= OnProviderClick;
        }

        private void OnProviderClick(Vector2 inputPosition)
        {
            OnClick?.Invoke(inputPosition);
        }

        private void OnProviderPressed(Vector2 inputPosition)
        {
            OnPressed?.Invoke(inputPosition);
        }
    }
}