using System;
using System.Collections.Generic;
using System.Linq;
using Game.Service.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Config
{
    [Serializable]
    public class EnemyConfig
    {
        public string PlanetId;
        public string RocketId;
    }
    
    [Serializable]
    public class PlanetConfig
    {
        public string Id;
        [Range(1, 100)]
        public float RotationPeriod;
        [Range(1, 100)]
        public float RotationVelocity;
        public Vector2 EllipseTrajectory;
        public Sprite PlanetSprite;
        [Range(1, 500)]
        public int Health;
    }
    
    [Serializable]
    public class RocketConfig
    {
        public string Id;
        [Range(1, 100)]
        public float Acceleration;
        [Range(1, 500)]
        public int Damage;
        [Range(1, 50)]
        public float Weight;
        [Range(1, 20)]
        public float Cooldown;
        public Sprite Sprite;
    }
    
    public class GameConfig : ScriptableObject
    {
        public List<PlanetConfig> Planets;
        public List<RocketConfig> Rockets;
        public List<EnemyConfig> Enemies;

        [Range(1, 50)]
        public int MinEnemiesAmount;
        
        [Range(1, 50)]
        public int MaxEnemiesAmount;
        
        public PlanetConfig GetPlanet(string planetId)
        {
            return Planets.FirstOrDefault(x => x.Id.Equals(planetId));
        }
        
        public RocketConfig GetRocket(string rocketId)
        {
            return Rockets.FirstOrDefault(x => x.Id.Equals(rocketId));
        }

        public List<UserData> GetEnemies()
        {
            List<UserData> result = new List<UserData>();
            
            int count = Random.Range(MinEnemiesAmount, MaxEnemiesAmount);
            for (int i = 0; i < count; i++)
            {
                int index = i % Enemies.Count;
                result.Add(new UserData(Enemies[index].PlanetId, Enemies[index].RocketId));
            }
            
            return result;
        }
    }
}