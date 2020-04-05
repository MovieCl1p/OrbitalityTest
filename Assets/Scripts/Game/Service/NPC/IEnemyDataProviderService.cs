using System.Collections.Generic;
using Game.Service.Data;

namespace Game.Service.NPC
{
    public interface IEnemyDataProviderService
    {
        List<UserData> GetEnemies();
    }
}