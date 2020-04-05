using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Binder
{
    public class BindManager
    {
        private static readonly Dictionary<Type, BindItem> _bindDictionary = new Dictionary<Type, BindItem>();

        public static BindItem Bind<T>()
        {
            var type = typeof(T);
            BindItem targetBind;
            if (!_bindDictionary.TryGetValue(type, out targetBind))
            {
                var bindItem = new BindItem(_bindDictionary);
                _bindDictionary.Add(type, bindItem);
                return bindItem;
            }

            return targetBind;
        }

        public static T GetInstance<T>(object key = null)
        {
            var type = typeof(T);

            BindItem bindItem;
            if (_bindDictionary.TryGetValue(type, out bindItem))
            {
                return bindItem.GetInstance<T>(key);
            }

            Debug.LogError("BindManager : " + typeof(T).Name + " inject not found!!!");
            return default;
        }

        public static object GetInject(Type type)
        {
            BindItem bindItem;
            var result = _bindDictionary.TryGetValue(type, out bindItem);
            if (result)
            {
                var inject = bindItem.GetInject(type);
                return inject;
            }

            Debug.LogError("BindManager : " + type.Name + " inject not found!!!");
            return null;
        }
    }
}