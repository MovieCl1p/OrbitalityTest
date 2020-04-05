using Game.Effect;
using UnityEngine;

namespace Game.Service
{
    public class EffectService : IEffectService
    {
        private BlackHoleEffect _effect;
        
        public void Init()
        {
            _effect = Camera.main.GetComponent<BlackHoleEffect>();
        }

        public void ShowEffect(bool show)
        {
            _effect.Active = show;
        }
    }
}