using System;
using Game.UI.Game.Mediator;
using UnityEngine;

namespace Game.Service.Input
{
    public interface IInputService
    {
        event Action<Vector2> OnPressed;
        event Action<Vector2> OnClick;

        void AddProvider(IInputProvider provider);
        void RemoveProvider(IInputProvider provider);
    }
}