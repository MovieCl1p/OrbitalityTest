using System.Collections.Generic;
using UnityEngine;

namespace Core.Config
{
    public class UIConfig : ScriptableObject
    {
        [SerializeField] 
        private List<ViewConfig> _views;

        [HideInInspector, SerializeField] 
        private string _pathToAssets;
        
        public List<ViewConfig> Views => _views;

        public string PathToAssets
        {
            get { return _pathToAssets; }
            set { _pathToAssets = value; }
        }
    }
}