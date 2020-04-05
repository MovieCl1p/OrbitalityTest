using Core.UI;
using Game.Config;
using UnityEngine;

namespace Core
{
    public interface IResourceCache
    {
        PrefabReferencesConfig PrefabConfig { get; }
        GameConfig GameConfig { get; }

        void Init();

        T GetView<T>() where T : BaseView;

        T CreateAsset<T>(PrefabAsset prefabReference, Transform parent) where T : MonoBehaviour;
    }
}