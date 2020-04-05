using Core;
using Core.Binder;
using Game.Unit.View;
using UnityEngine;

namespace Game.Factories.Unit
{
    public class UnitFactory : IUnitFactory
    {
        private IResourceCache _resourceCache;

        public IUnitView GetUnit(Transform parent)
        {
            Init();
            return _resourceCache.CreateAsset<UnitView>(_resourceCache.PrefabConfig.UnitView, parent);
        }

        public RocketView GetRocket(Transform parent)
        {
            Init();
            return _resourceCache.CreateAsset<RocketView>(_resourceCache.PrefabConfig.RocketView, parent);
        }

        private void Init()
        {
            if (_resourceCache == null)
            {
                _resourceCache = BindManager.GetInstance<IResourceCache>();
            }
        }
    }
}