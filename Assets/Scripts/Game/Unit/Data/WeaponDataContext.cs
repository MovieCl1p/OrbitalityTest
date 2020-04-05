using Core.UI;
using UnityEngine;

namespace Game.Unit.Data
{
    public class WeaponDataContext : IDataContext
    {
        public float Acceleration { get; }
        public float Cooldown { get; }
        public int Damage { get; }
        public float Weight { get; }
        public Sprite Sprite { get; }

        public WeaponDataContext(float acceleration, float cooldown, int damage, float weight,
            Sprite sprite)
        {
            Acceleration = acceleration;
            Cooldown = cooldown;
            Damage = damage;
            Weight = weight;
            Sprite = sprite;
        }

        public void Dispose()
        {
        }
    }
}