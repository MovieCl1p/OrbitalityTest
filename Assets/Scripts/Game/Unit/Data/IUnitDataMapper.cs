using Game.Service.Data;
using Game.UI.Game.View.Unit.Data;

namespace Game.Unit.Data
{
    public interface IUnitDataMapper
    {
        UnitDataContext MapData(UserData userData);
    }
}