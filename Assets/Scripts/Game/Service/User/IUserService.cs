using Game.Service.Data;

namespace Game.Service
{
    public interface IUserService
    {
        UserData GetUserData();
        void AddScore();
        void LoadData();
        void SaveData();
        void ChangePlanet(string id);
    }
}