using System;
using Core;
using Game.Unit;
using UnityEngine;

namespace Game.UI.Game.View
{
    public class BlackHoleComponent : BaseMonoBehaviour
    {
        [SerializeField] 
        private float _dX;
        
        [SerializeField] 
        private float _dY;
        
        [SerializeField] 
        private float _period;

        private float _currentTime;
        
        public void Update()
        {
            _currentTime += 1 * Time.deltaTime;
            float dT = Mathf.Lerp(0, 1, _currentTime / _period);
            transform.localPosition = UnitExtension.EvaluateEllipse(dT, _dX, _dY);

            if (_currentTime >= _period)
            {
                _currentTime = 0;
            }
        }
    }
}