using System;
using UnityEngine;

namespace Game.Unit.Mediator
{
    public interface IGravityApplicable
    {
        event Action OnRelease;
        string Id { get; }
        void ApplyGravity(Vector3 force);
        float Mass { get; }
        Vector3 Position { get; }
    }
}