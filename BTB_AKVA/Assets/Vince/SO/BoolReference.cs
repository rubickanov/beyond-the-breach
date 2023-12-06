using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Vince.SO
{
    [CreateAssetMenu(fileName = "BoolReference", menuName = "VinceSO/BoolReference")]
    public class BoolReference : ScriptableObject
    {
        public bool value;

        private void OnDisable()
        {
            value = false;
        }

        public void SetBool(bool value)
        {
            this.value = value;
        }

    }
}
