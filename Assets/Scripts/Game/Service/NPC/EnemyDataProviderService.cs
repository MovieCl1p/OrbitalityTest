using System.Collections.Generic;
using Core;
using Core.Binder;
using Game.Service.Data;

namespace Game.Service.NPC
{
    public class EnemyDataProviderService : IEnemyDataProviderService
    {
        public List<UserData> GetEnemies()
        {
            var resourceCache = BindManager.GetInstance<IResourceCache>();
            return resourceCache.GameConfig.GetEnemies();
        }
    }
}