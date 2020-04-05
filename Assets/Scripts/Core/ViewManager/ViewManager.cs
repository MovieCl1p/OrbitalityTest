using System;
using System.Collections.Generic;
using Core.Binder;
using Core.UI;
using Core.ViewManager.Data;
using UnityEngine;

namespace Core.ViewManager
{
    public struct ViewStruct
    {
        public LayerType Layer { get; private set; }

        public ViewStruct(LayerType layerId)
        {
            Layer = layerId;
        }
    }

    public class ViewManager : SingletonBaseMonoBehaviour<ViewManager>
    {
        [SerializeField]
        private List<ViewLayer> _layers;

        private Dictionary<LayerType, ViewLayer> _layersMap;
        private Dictionary<Type, ViewStruct> _viewDictionary;

        private IResourceCache _resourceCache;
        
        public void Init()
        {
            _viewDictionary = new Dictionary<Type, ViewStruct>();
            _layersMap = new Dictionary<LayerType, ViewLayer>();
            _resourceCache = BindManager.GetInstance<IResourceCache>();

            foreach (ViewLayer layer in _layers)
            {
                _layersMap.Add(layer.LayerType, layer);
            }
        }

        public T SetView<T>() where T : BaseView
        {
            return SetViewInternal<T>(GetLayer(typeof(T)));
        }

        private T SetViewInternal<T>(ViewLayer layer) where T : BaseView
        {
            if (!_viewDictionary.ContainsKey(typeof(T)))
            {
                throw new Exception("Can't find view " + typeof(T));
            }

            var view = GameObject.Instantiate(_resourceCache.GetView<T>(), layer.transform, false);
            view.Layer = layer;

            layer.AddView(view);

            return view;
        }

        public void RemoveAllViews(string exception = null)
        {
            foreach (ViewLayer layer in _layers)
            {
                layer.RemoveAllViews();
            }
        }

        public void RemoveView(BaseView view)
        {
            view.Layer.RemoveView(view);
        }

        public void RegisterView(Type viewType, LayerType layerId)
        {
            _viewDictionary[viewType] = new ViewStruct(layerId);
        }
        
        private ViewLayer GetLayer(Type viewType)
        {
            LayerType layer = _viewDictionary[viewType].Layer;
            return GetLayerById(layer);
        }

        private ViewLayer GetLayerById(LayerType layerId)
        {
            if (!_layersMap.ContainsKey(layerId))
            {
                throw new Exception("Can't find layer with such id " + layerId);
            }

            return _layersMap[layerId];
        }
    }
}