using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteAlways]
    [InitializeOnLoadAttribute]
    public class HierarchyMonitor : MonoBehaviour // Работает и без наследования
    {
        static HierarchyMonitor()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        static void OnHierarchyChanged()
        {
            var all = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            var numberVisible =
                all.Where(obj => obj.name.StartsWith("Arrow")).Count();
            Debug.LogFormat("There are currently {0} GameObjects with name Arrow.", numberVisible);
        }
    }
}