using Core;
using Core.Binder;
using Game.Service.Data;
using Game.UI.Game.View.Unit.Data;
using UnityEngine;

namespace Game.Unit.Data
{
    public class UnitDataMapper : IUnitDataMapper
    {
        public UnitDataContext MapData(UserData userData)
        {
            var resourceCache = BindManager.GetInstance<IResourceCache>();

            var planet = resourceCache.GameConfig.GetPlanet(userData.PlanetId);
            var rocket = resourceCache.GameConfig.GetRocket(userData.RocketId);
            
            return new UnitDataContext(planet, rocket);
        }
    }
}