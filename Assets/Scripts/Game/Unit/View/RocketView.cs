using System;
using Core.UI.Components;
using Game.Unit.Data;
using Game.Unit.Mediator;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Unit.View
{
    public class RocketView : BaseComponent<RocketDataContext>, IDamageDealer, IGravityApplicable
    {
        [SerializeField] 
        private Rigidbody2D _rigidbody;
        
        [SerializeField] 
        private Image _image;
        
        private float _currentTime;
        
        public float Mass
        {
            get { return _rigidbody.mass; }
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }

        protected override void OnContextUpdate(RocketDataContext context)
        {
            base.OnContextUpdate(context);

            _image.sprite = context.Weapon.Sprite;
            _rigidbody.mass = context.Weapon.Weight;
            transform.position = context.Position;
            transform.rotation = Quaternion.Euler(0, 0, context.Rotation - 90);
        }

        public event Action OnRelease;

        public string Id
        {
            get { return DataContext.OriginPlanetId; }
        }

        public void ApplyGravity(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }

        public void FixedUpdate()
        {
            _rigidbody.AddForce(transform.up * DataContext.Weapon.Acceleration);
        }

        public void Update()
        {
            if (_rigidbody.velocity != Vector2.zero)
            {
                Vector2 v = _rigidbody.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
                _image.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            IDamageApplicable applicable = other.transform.GetComponent<IDamageApplicable>();
            if (applicable == null || applicable.Id == DataContext.OriginPlanetId)
            {
                return;
            }

            applicable.ApplyDamage(DataContext.Weapon.Damage);
            
            Destroy(gameObject);
        }

        protected override void OnReleaseResources()
        {
            OnRelease?.Invoke();
            base.OnReleaseResources();
        }
    }
}