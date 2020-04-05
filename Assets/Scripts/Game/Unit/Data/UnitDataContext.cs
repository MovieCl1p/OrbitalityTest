using System;
using Core.UI;
using Game.Config;
using Game.Unit.Data;
using UnityEngine;

namespace Game.UI.Game.View.Unit.Data
{
    public class UnitDataContext : IDataContext
    {
        public bool IsPlayer { get; set; }
        
        public string Id { get; }
        
        public Vector2 EllipseCenter { get; }
        
        public float RotationPeriod { get; }
        
        public float RotationVelocity { get; }
        
        public float StartPosition { get; set; }
        
        public Sprite PlanetSprite { get; }
        
        public float MaxHealth { get; }
        
        public BindableField<int> CurrentHealth { get; set; }
        
        public WeaponDataContext Weapon { get; }

        public UnitDataContext(PlanetConfig planetConfig, RocketConfig rocketConfig, bool isPlayer = false)
        {
            Id = Guid.NewGuid().ToString();
            CurrentHealth = new BindableField<int>(planetConfig.Health);
            MaxHealth = planetConfig.Health;
            RotationPeriod = planetConfig.RotationPeriod;
            RotationVelocity = planetConfig.RotationVelocity;
            PlanetSprite = planetConfig.PlanetSprite;
            EllipseCenter = planetConfig.EllipseTrajectory;

            Weapon = new WeaponDataContext(rocketConfig.Acceleration, rocketConfig.Cooldown, rocketConfig.Damage, rocketConfig.Weight, rocketConfig.Sprite);
            
            IsPlayer = isPlayer;
        }
        
        public void Dispose()
        {
            CurrentHealth.UnBindAll();
        }
    }
}