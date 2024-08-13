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
        static Dictionary<int, string> qualityDictionary = new Dictionary<int, string>()
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
            Debug.Log("AddCanvasToHierarchy called");
            for (int i = qualityDictionary.Keys.Count - 1; i >= 0; i--)
            {
                GameObject _canvasArrowsObj = new GameObject("Canvas" + qualityDictionary[i], typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(GridLayoutGroup), typeof(CanvasSettings));

                // Добавляем специфический скрипт для каждого Canvas
                switch (i)
                {
                    case 0:
                        _canvasArrowsObj.AddComponent<CanvasRotateAround>();
                        break;
                    case 1:
                        _canvasArrowsObj.AddComponent<CanvasFreeFly>();
                        break;
                    case 2:
                        _canvasArrowsObj.AddComponent<CanvasButtons>();
                        break;
                }

                if (СanvasAddedToHierarchy != null)
                {
                    СanvasAddedToHierarchy();
                }
            }
        }
    }
}