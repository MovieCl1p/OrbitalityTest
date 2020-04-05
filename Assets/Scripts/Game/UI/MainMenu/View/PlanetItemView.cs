using System;
using Core.UI.Components;
using Game.UI.MainMenu.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu.View
{
    public class PlanetItemView : BaseComponent<PlanetItemData>
    {
        public event Action<string> OnClick;
        
        [SerializeField] 
        private Image _image;
        
        [SerializeField] 
        private Text _healthLabel;
        
        [SerializeField] 
        private Text _velocityLabel;
        
        [SerializeField] 
        private Button _button;

        protected override void Start()
        {
            base.Start();
            _button.onClick.AddListener(OnButtonClick);
        }

        protected override void OnContextUpdate(PlanetItemData context)
        {
            base.OnContextUpdate(context);
            _image.sprite = context.Sprite;
            _healthLabel.text = string.Format("Health: {0}", context.Health);
            _velocityLabel.text = string.Format("Velocity: {0}", context.Period);
        }
        
        private void OnButtonClick()
        {
            OnClick?.Invoke(DataContext.PlanetId);
        }

        protected override void OnReleaseResources()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            base.OnReleaseResources();
        }
    }
}