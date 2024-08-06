using UnityEditor;
using UnityEngine;

//[InitializeOnLoad]
//public class HideInInspectorOnAddEditor
//{
//    static HideInInspectorOnAddEditor()
//    {
//        EditorApplication.hierarchyChanged += OnHierarchyChanged;
//    }

//    private static void OnHierarchyChanged()
//    {
//        // Получаем все объекты в сцене
//        var allObjects = GameObject.FindObjectsOfType<MonoBehaviour>();

//        foreach (var obj in allObjects)
//        {
//            var type = obj.GetType();

//            // Проверяем, есть ли у класса атрибут HideInInspectorOnAdd
//            var attributes = type.GetCustomAttributes(typeof(Assets.Scripts.ControlVersions.HideInInspectorOnAddAttribute), true);
//            if (attributes.Length > 0)
//            {
//                // Скрываем компонент в инспекторе
//                obj.hideFlags = HideFlags.HideInInspector;
//            }
//        }

//        // Обновляем инспектор
//        EditorApplication.RepaintHierarchyWindow();
//    }
//}
