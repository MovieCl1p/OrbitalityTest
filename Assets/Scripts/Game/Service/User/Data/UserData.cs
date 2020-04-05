using Core.UI;

namespace Game.Service.Data
{
    public class UserData
    {
        public string PlanetId { get; set; }
        public string RocketId { get; }
        public BindableField<int> Score { get; }

        public UserData(string planetId, string rocketId)
        {
            PlanetId = planetId;
            RocketId = rocketId;
            Score = new BindableField<int>(0);
        }

        public UserData(ParsedUserData parsedData)
        {
            PlanetId = parsedData.PlanetId;
            RocketId = parsedData.RocketId;
            Score = new BindableField<int>(parsedData.Score);
        }
    }
}