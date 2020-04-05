using System;
using Game.Unit.View;
using UnityEngine;

namespace Game.Unit.Strategy
{
    public interface IStrategy : IDisposable
    {
        event Action OnDestroy;
        void Initialize(UnitView unitView, Transform container);
    }
}