using System;
using System.Collections.Generic;
using Core;
using Game.Unit.Mediator;
using UnityEngine;

namespace Game.Unit.View
{
    public class GravityEffector : BaseMonoBehaviour
    {
        [SerializeField]
        private float _g = 50;
        
        [SerializeField]
        private float _mass;
        
        public string Id { get; set; }
        
        private List<IGravityApplicable> _applayables = new List<IGravityApplicable>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            IGravityApplicable applicable = other.transform.GetComponent<IGravityApplicable>();
            if (applicable == null || applicable.Id == Id)
            {
                return;
            }

            if (_applayables.Contains(applicable))
            {
                return;
            }

            applicable.OnRelease += () => { _applayables.Remove(applicable); };
            
            _applayables.Add(applicable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IGravityApplicable applicable = other.transform.GetComponent<IGravityApplicable>();
            if (applicable == null)
            {
                return;
            }
            
            if (_applayables.Contains(applicable))
            {
                _applayables.Remove(applicable);
            }
        }

        public void FixedUpdate()
        {
            foreach (IGravityApplicable applayable in _applayables)
            {
                Vector3 direction = transform.position - applayable.Position;
                float distance = direction.magnitude;
                float forceMagnitude = _g * (_mass * applayable.Mass) / Mathf.Pow(distance, 2);
                Vector3 force = direction.normalized * forceMagnitude;
            
                applayable.ApplyGravity(force);
            }
        }
        
    }
}