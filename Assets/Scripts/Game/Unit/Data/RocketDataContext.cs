using Core.UI;
using UnityEngine;

namespace Game.Unit.Data
{
    public class RocketDataContext : IDataContext
    {
        public Vector3 Position { get; }
        public float Rotation { get; }
        public string OriginPlanetId { get; }
        public WeaponDataContext Weapon { get; }

        public RocketDataContext(Vector3 position,float rotation, string originPlanetId, WeaponDataContext weapon)
        {
            Position = position;
            Rotation = rotation;
            OriginPlanetId = originPlanetId;
            Weapon = weapon;
        }


        public void Dispose()
        {
        }
    }
}