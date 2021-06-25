using System;

namespace Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAssetAttribute : Attribute
    {
        public string AssetName;
        public InjectAssetAttribute(string name)
        {
            AssetName = name;
        }
    }
}
