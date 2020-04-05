using System;
using System.Collections.Generic;
using Core.Config;
using Core.UI;
using Game.Config;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class ResourceCache : IResourceCache
    {
        private const string UiConfigPath = "Configs/UIConfig";
        private const string PrefabConfigPath = "Configs/PrefabReferences";
        private const string GameConfigPath = "Configs/GameConfig";

        private readonly Dictionary<Type, BaseView> _viewMap = new Dictionary<Type, BaseView>();

        private UIConfig _uiConfig;
        private PrefabReferencesConfig _prefabConfig;
        private GameConfig _gameConfig;

        public PrefabReferencesConfig PrefabConfig
        {
            get { return _prefabConfig; }
        }

        public GameConfig GameConfig
        {
            get { return _gameConfig; }
        }

        public void Init()
        {
            _uiConfig = Resources.Load<UIConfig>(UiConfigPath);
            if (_uiConfig == null)
            {
                Debug.LogError("No UIConfig by path: " + UiConfigPath);
                return;
            }

            foreach (ViewConfig config in _uiConfig.Views)
            {
                _viewMap.Add(config.View.GetType(), config.View);
            }

            _prefabConfig = Resources.Load<PrefabReferencesConfig>(PrefabConfigPath);
            _gameConfig = Resources.Load<GameConfig>(GameConfigPath);
        }

        public T GetView<T>() where T : BaseView
        {
            if (!_viewMap.ContainsKey(typeof(T)))
            {
                Debug.LogError("No view found with Id: " + typeof(T));
                return null;
            }

            return _viewMap[typeof(T)] as T;
        }
        
        public T CreateAsset<T>(PrefabAsset prefabReference, Transform parent) where T : MonoBehaviour
        {
            var prefab = GameObject.Instantiate(prefabReference.Prefab, parent);
            return prefab.GetComponent<T>();
        }
    }
}