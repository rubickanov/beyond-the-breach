using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.SO
{
    public class ListAdder : MonoBehaviour
    {
        public RuntimeList runtimeList;

        private void Awake()
        {
            runtimeList.AddToList(gameObject);
        }

        private void OnDisable()
        {
            runtimeList.RemoveToList(gameObject);
        }
    }
}
