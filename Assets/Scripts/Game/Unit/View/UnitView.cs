using System;
using System.Collections;
using Core;
using Core.Binder;
using Core.UI.Components;
using Game.Service.Input;
using Game.UI.Game.View.Unit.Data;
using Game.Unit.Mediator;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Unit.View
{
    public class UnitView : BaseComponent<UnitDataContext>, IUnitView
    {
        public event Action OnCoolDownFinished;
        public event Action<int> OnDamageTriggered;
        
        [SerializeField] 
        private Image _arrow;
        
        [SerializeField] 
        private Image _planetImage;
        
        [SerializeField] 
        private Image _coolDownImage;
        
        [SerializeField] 
        private Image _healthImage;
        
        [SerializeField] 
        private GravityEffector _gravityEffector;
        
        [Inject]
        public IInputService InputService { get; set; }

        private bool _isUpdating;
        private float _currentTime;
        private float _angle;
        private float _coolDownTime;

        public float Rotation
        {
            get { return _angle; }
        }
        
        protected override void OnContextUpdate(UnitDataContext context)
        {
            base.OnContextUpdate(context);

            _gravityEffector.Id = context.Id;
            _isUpdating = true;
            _currentTime = context.StartPosition;
            _planetImage.sprite = context.PlanetSprite;

            context.CurrentHealth.Bind(OnHealthChange);
        }

        private void OnHealthChange(int data)
        {
            _healthImage.fillAmount = data / DataContext.MaxHealth;
        }

        public void StartCoolDown()
        {
            _coolDownImage.gameObject.SetActive(true);
            StartCoroutine(CoolDown());
        }

        private IEnumerator CoolDown()
        {
            while (_coolDownTime < DataContext.Weapon.Cooldown)
            {
                _coolDownTime += Time.deltaTime;
                _coolDownImage.fillAmount = _coolDownTime / DataContext.Weapon.Cooldown;
                yield return null;
            }

            _coolDownImage.gameObject.SetActive(false);
            _coolDownTime = 0;
            OnCoolDownFinished?.Invoke();
        }

        public void Update()
        {
            if (!_isUpdating)
            {
                return;
            }
            
            _currentTime += DataContext.RotationVelocity * Time.deltaTime;
            float dT = Mathf.Lerp(0, 1, _currentTime / DataContext.RotationPeriod);
            transform.localPosition = UnitExtension.EvaluateEllipse(dT, DataContext.EllipseCenter.x, DataContext.EllipseCenter.y);

            if (_currentTime >= DataContext.RotationPeriod)
            {
                _currentTime = 0;
            }
        }
        
        

        protected override void OnReleaseResources()
        {
            base.OnReleaseResources();
        }

        public void ApplyDamage(int damage)
        {
            OnDamageTriggered?.Invoke(damage);    
        }

        public void Rotate(float angle)
        {
            _arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            _angle = angle;
        }
    }
}