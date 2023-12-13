using System;
using UnityEngine;

namespace AKVA.Player
{
    [CreateAssetMenu]
    public class FloatReference : ScriptableObject
    {
        
        public float value = 0.5f;
        public float minValue = 0.1f;
        public float maxValue = 1.0f;

        public void SetValue(float _value)
        {
            value = _value;
        }
    }
}
