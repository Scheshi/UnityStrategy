using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(menuName = "Injections/Asset Collection")]
    public class AssetCollection: ScriptableObject
    {
        [SerializeField] private GameObject[] assets;

        private GameObject FindObject(string assetName)
        {
            return assets.FirstOrDefault(x => x.name == assetName);
        }

        public T InjectAsset<T>(T obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                    BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                InjectAssetAttribute attribute = fieldInfo.GetCustomAttribute<InjectAssetAttribute>();
                if (attribute != null)
                {
                    fieldInfo.SetValue(obj, FindObject(attribute.AssetName));
                }
            }
            return obj;
        }
    }
}