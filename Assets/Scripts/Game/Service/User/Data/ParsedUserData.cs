using System;

namespace Game.Service.Data
{
    [Serializable]
    public class ParsedUserData
    {
        public string PlanetId;
        public string RocketId;
        public int Score;

        public ParsedUserData()
        {
        }
        
        public ParsedUserData(UserData userData)
        {
            PlanetId = userData.PlanetId;
            RocketId = userData.RocketId;
            Score = userData.Score.Value;
        }
    }
}