using System;
using Game.UI.Game.View;
using UnityEngine;

namespace Game.Effect
{
    public class BlackHoleEffect : MonoBehaviour
    {
        [SerializeField]
        private BlackHoleComponent _blackHole;
        
        [SerializeField]
        private Camera _camera;
        
        [SerializeField]
        private float _radius;

        [SerializeField]
        private Material _effectMaterial;
        
        private float _ration;

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                _blackHole.gameObject.SetActive(value);
            }
        }

        public void OnEnable()
        {
            _ration = 1.0f / _camera.aspect;
        }

        private Vector2 _position;
        private bool _active;

        public void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (_effectMaterial == null || _blackHole == null || !Active)
            {
                Graphics.Blit(src, dest);
                return;
            }
            
            Vector3 screenPosition = _camera.WorldToScreenPoint(_blackHole.transform.position);

            if (screenPosition.z > 0)
            {
                _position = new Vector2(screenPosition.x / _camera.pixelWidth, screenPosition.y / _camera.pixelHeight);
                _effectMaterial.SetVector("_Position", _position);
                _effectMaterial.SetFloat("_Ratio", _ration);
                _effectMaterial.SetFloat("_Rad", _radius);
                _effectMaterial.SetFloat("_Distance", Vector3.Distance(_blackHole.transform.position, transform.position));
                
                Graphics.Blit(src, dest, _effectMaterial);
            }

        }
    }
}