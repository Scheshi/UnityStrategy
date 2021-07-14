using System;
using System.Linq;
using System.Reflection;
using Abstractions;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(menuName = "Injections/Asset Collection")]
    public class UnitCollection: ScriptableObject
    {
        [SerializeField] private GameObject[] assets;

        private IUnit FindObject(string assetName)
        {
            return assets.FirstOrDefault(x => x.name == assetName).GetComponent<IUnit>();
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