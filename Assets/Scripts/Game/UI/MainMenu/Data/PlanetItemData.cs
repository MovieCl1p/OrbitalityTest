using Core.UI;
using UnityEngine;

namespace Game.UI.MainMenu.Data
{
    public class PlanetItemData : IDataContext
    {
        public string PlanetId { get; }
        public Sprite Sprite { get; }
        public int Health { get; }
        public float Period { get; }

        public PlanetItemData(Sprite sprite, string planetId, int health, float period)
        {
            Sprite = sprite;
            PlanetId = planetId;
            Health = health;
            Period = period;
        }

        public void Dispose()
        {
        }
    }
}