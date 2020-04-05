using Factories;
using Game.Unit.View;
using UnityEngine;

namespace Game.Factories.Unit
{
    public interface IUnitFactory : IFactory
    {
        IUnitView GetUnit(Transform parent);

        RocketView GetRocket(Transform parent);
    }
}