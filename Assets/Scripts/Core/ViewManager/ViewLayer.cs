using System.Collections.Generic;
using Core.UI;
using Core.ViewManager.Data;
using UnityEngine;

namespace Core.ViewManager
{
    public class ViewLayer : BaseMonoBehaviour
    {
        [SerializeField] 
        private LayerType _layerType;
        
        private readonly List<BaseView> _openViews = new List<BaseView>();

        public LayerType LayerType
        {
            get { return _layerType; }
        }

        public void AddView(BaseView view)
        {
            _openViews.Add(view);
        }

        public void RemoveAllViews()
        {
            for (int i = _openViews.Count - 1; i >= 0; i--)
            {
                RemoveView(_openViews[i]);
            }
        }

        public void RemoveView(BaseView view)
        {
            _openViews.Remove(view);
            Destroy(view.gameObject);
        }
    }
}