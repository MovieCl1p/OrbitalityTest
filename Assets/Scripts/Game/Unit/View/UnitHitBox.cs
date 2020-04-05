using Core;
using Game.Unit.Mediator;
using UnityEngine;

namespace Game.Unit.View
{
    public class UnitHitBox : BaseMonoBehaviour, IDamageApplicable
    {
        [SerializeField] 
        private UnitView _unitView;
        
        public string Id
        {
            get { return _unitView.DataContext.Id; }
        }
        
        public void ApplyDamage(int damage)
        {
            _unitView.ApplyDamage(damage);
        }
    }
}