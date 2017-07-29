using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class TransformExtensions
    {
        public static void ClearChildren(this Transform t)
        {
            for (var i = 0; i < t.childCount; i++)
            {
                Object.Destroy(t.GetChild(i).gameObject);
            }
        }

        public static GameObject FindObjectIncludingDisabled(this GameObject parent, string name)
        {
            var components = parent.GetComponentsInChildren(typeof(Transform), true);
            foreach (var transform in components)
            {
                if (transform.name == name)
                {
                    return transform.gameObject;
                }
            }

            return null;
        }

        public static List<T> FindObjectOfTypeIncludingDisabled<T>(this GameObject parent) where T : Object
        {
            var components = parent.GetComponentsInChildren(typeof(T), true);
            return components.Cast<T>().ToList();
        }
    }
}
