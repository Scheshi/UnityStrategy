using System;
using Abstractions;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(menuName = "Models/ProduceValue")]
    public class ProduceModel: ScriptableModel<float>
    {
        public event Action OnStartProduce = () => { };
        public event Action OnEndProduce = () => { };

        public void StartProduce()
        {
            OnStartProduce.Invoke();
        }

        public void EndProduce()
        {
            OnEndProduce.Invoke();
        }

    }
}