using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CameraPreset
{
    //Управление канвасами, пройтись по всем, чтобы спратать или показать
    [InitializeOnLoad]
    public class СanvasAddToHierarchy 
    {
        static Dictionary<int, string> CanvasNameDictionary = new Dictionary<int, string>()
        {
            { 0, Enum.GetValues(typeof(CameraPresets)).GetValue(0).ToString()},
            //{ 1, (CameraPresets)Enum.GetValues(typeof(CameraPresets)).GetValue(1)}, for Dictionary<int, CameraPresets>
            { 1, Enum.GetValues(typeof(CameraPresets)).GetValue(1).ToString()},
            { 2, Enum.GetValues(typeof(CameraPresets)).GetValue(2).ToString()},
        };

        public delegate void СanvasAddedToHierarchyHandler();
        public static event СanvasAddedToHierarchyHandler СanvasAddedToHierarchy;

        static СanvasAddToHierarchy()
        {
            // Подписка на событие при загрузке Unity Editor
            CameraPresetAddToHierarchy.CameraPresetAddToHierarhcy += AddCanvasToHierarchy;
        }

        static void AddCanvasToHierarchy()
        {
            if(TestLog.enableLog)
                Debug.Log("AddCanvasToHierarchy called");
            for (int i = CanvasNameDictionary.Keys.Count - 1; i >= 0; i--)
            {
                GameObject _canvasArrowsObj = new GameObject("Canvas" + CanvasNameDictionary[i], typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(GridLayoutGroup), typeof(CanvasSettings));

                // Получаем текущее значение CameraPresets
                CameraPresets preset = (CameraPresets)i;

                // Если сопоставление существует, добавляем компонент
                if (CameraPreset.presetComponentMap.TryGetValue(preset, out Type componentType))
                {
                    _canvasArrowsObj.AddComponent(componentType);
                }
                else
                {
                    Debug.LogWarning($"No component assigned for CameraPreset: {preset}");
                }

                if (СanvasAddedToHierarchy != null)
                {
                    СanvasAddedToHierarchy();
                }
            }
        }
    }
}