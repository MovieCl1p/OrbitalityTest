using System;
using UnityEngine;

namespace Game.UI.Game.Mediator
{
    public interface IInputProvider
    {
        event Action<Vector2> OnPressed;
        event Action<Vector2> OnClick;
    }
}