using System;
using System.Collections.Generic;
using Core;
using Core.Binder;
using Core.Dispatcher;
using Core.UI;
using Game.Config;
using Game.Service;
using Game.Service.Data;
using Game.UI.MainMenu.Data;
using Game.Unit.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu.View
{
    public class MainMenuScreen : BaseView
    {
        public Action OnStartButtonClick;
        public Action OnSaveClick;
        public Action OnLoadClick;
        public Action<string> OnSelectPlanet;
        
        [SerializeField] 
        private Button _startButton;

        [SerializeField] 
        private Image _userPlaneImage;
        
        [SerializeField] 
        private Text _scoreLabel;
        
        [SerializeField] 
        private Transform _content;
        
        [SerializeField] 
        private Button _saveButton;
        
        [SerializeField] 
        private Button _loadButton;
        
        [Inject]
        public IUserService UserService { get; set; }
        
        [Inject]
        public IResourceCache ResourceCache { get; set; }
        
        private List<PlanetItemView> _items = new List<PlanetItemView>();
        
        protected override void Start()
        {
            base.Start();
            
            _startButton.onClick.AddListener(OnStartClick);
            _saveButton.onClick.AddListener(SaveClick);
            _loadButton.onClick.AddListener(LoadClick);

            UpdateData();
        }

        private void LoadClick()
        {
            OnLoadClick?.Invoke();
        }

        private void SaveClick()
        {
            OnSaveClick?.Invoke();
        }

        public void UpdateData()
        {
            var userData = UserService.GetUserData();
            _userPlaneImage.sprite = ResourceCache.GameConfig.GetPlanet(userData.PlanetId).PlanetSprite;
            _scoreLabel.text = string.Format("Score: {0}", userData.Score);

            UpdateScroll();
        }

        private void UpdateScroll()
        {
            for (int i = 0; i < ResourceCache.GameConfig.Planets.Count; i++)
            {
                var config = ResourceCache.GameConfig.Planets[i];
                PlanetItemView item;
                if (i >= _items.Count)
                {
                    item = CreateItem();
                    _items.Add(item);
                }
                else
                {
                    item = _items[i];
                }
                
                item.SetContext(new PlanetItemData(config.PlanetSprite, config.Id, config.Health, config.RotationPeriod));
                item.OnClick += SelectPlanet;
            }
        }

        private void SelectPlanet(string planetId)
        {
            _userPlaneImage.sprite = ResourceCache.GameConfig.GetPlanet(planetId).PlanetSprite;
            OnSelectPlanet?.Invoke(planetId);
        }

        private PlanetItemView CreateItem()
        {
            return Instantiate(ResourceCache.PrefabConfig.PlanetItem, _content);
        }

        private void OnStartClick()
        {
            OnStartButtonClick?.Invoke();
        }

        protected override void OnReleaseResources()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].OnClick -= SelectPlanet;
            }
            _items.Clear();
            
            _startButton.onClick.RemoveListener(OnStartClick);
            _saveButton.onClick.RemoveListener(SaveClick);
            _loadButton.onClick.RemoveListener(LoadClick);
            base.OnReleaseResources();
        }
    }
}