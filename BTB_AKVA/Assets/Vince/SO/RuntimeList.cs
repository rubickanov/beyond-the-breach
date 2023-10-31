using Codice.CM.Common.Selectors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.SO
{
    [CreateAssetMenu(fileName = "NewRuntimeList", menuName = "VinceSO/RuntimeList")]
    public class RuntimeList : ScriptableObject
    {
        public List<GameObject> items = new List<GameObject>();

        public void AddToList(GameObject item)
        {
            items.Add(item);
        }

        public void RemoveToList(GameObject item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }
        }

        private void OnDisable()
        {
            items.Clear();
        }

        public float Count => items.Count;
        public List<GameObject> Items => items;
    }
}
