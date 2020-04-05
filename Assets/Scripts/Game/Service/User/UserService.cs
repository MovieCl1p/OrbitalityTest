using System.IO;
using Game.Service.Data;
using UnityEngine;

namespace Game.Service
{
    public class UserService : IUserService
    {
        private const string _dataPath = "UserData.json";
        
        private UserData _userData;
        
        public UserData GetUserData()
        {
            if (_userData == null)
            {
                LoadData();
            }
            
            return _userData;
        }

        public void AddScore()
        {
            _userData.Score.Value++;
            SaveData();
        }

        public void SaveData()
        {
            ParsedUserData parsedData  = new ParsedUserData(_userData);
            string json = JsonUtility.ToJson(parsedData, true);
            var path = Path.Combine(Application.persistentDataPath, _dataPath);
            File.WriteAllText(path, json);
        }

        public void ChangePlanet(string id)
        {
            _userData.PlanetId = id;
        }

        public void LoadData()
        {
            var path = Path.Combine(Application.persistentDataPath, _dataPath);
            if (!File.Exists(path))
            {
                _userData = new UserData(Default());
            }
            
            string json = File.ReadAllText(path);
            ParsedUserData parsedData = JsonUtility.FromJson<ParsedUserData>(json);
            _userData = new UserData(parsedData);
        }

        private ParsedUserData Default()
        {
            return new ParsedUserData { PlanetId = "1", RocketId = "1", Score = 0 };
        }
    }
}