using CameraPreset;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CameraPreset
{
    //Управление канвасами, пройтись по всем, чтобы спратать или показать
    [InitializeOnLoad]
    public class СanvasAddToHierarchy 
    {
        static Dictionary<int, CameraPresets> qualityDictionary = new Dictionary<int, CameraPresets>()
        {
            { 0, (CameraPresets)Enum.GetValues(typeof(CameraPresets)).GetValue(0)},
            { 1, (CameraPresets)Enum.GetValues(typeof(CameraPresets)).GetValue(1)},
            { 2, (CameraPresets)Enum.GetValues(typeof(CameraPresets)).GetValue(2)},
        };
        //public GameObject _canvasObj;
        //public GridLayoutGroup gridGroup;
        //public Canvas canvas;
        //public CanvasScaler canvasScaler;

        public delegate void СanvasAddedToHierarchyHandler();
        public static event СanvasAddedToHierarchyHandler СanvasAddedToHierarchy;

        static СanvasAddToHierarchy()
        {
            // Подписка на событие при загрузке Unity Editor
            CameraPresetAddToHierarchy.CameraPresetAddToHierarhcy += AddCanvasToHierarchy;
        }

        static void AddCanvasToHierarchy()
        {
            Debug.Log(CameraPreset.currentPreset);
            // Создание Canvas
            GameObject _canvasArrowsObj = new GameObject("CanvasArrows", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(GridLayoutGroup), typeof(CanvasArrowsSettings));
            if (СanvasAddedToHierarchy != null)
                СanvasAddedToHierarchy();
            //if(CanvasArrowsSettings.instance != null)
            //    CanvasArrowsSettings.instance.SetCanvas();
        }
    }
}