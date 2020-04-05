using UnityEngine;

namespace Core
{
    public class SingletonBaseMonoBehaviour<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
    {
        public bool IsOverrideOnAwake = true;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var temp = FindObjectOfType<T>();

                    if (temp == null)
                    {
                        Debug.LogError($"There is no object of type '{nameof(T)}'. Instance cannot be accessed.");
                    }
                    else
                    {
                        _instance = temp;
                    }
                }

                return _instance;
            }
        }

        protected override void Awake()
        {
            if (_instance == null || IsOverrideOnAwake)
            {
                _instance = this as T;
            }
        }
    }
}