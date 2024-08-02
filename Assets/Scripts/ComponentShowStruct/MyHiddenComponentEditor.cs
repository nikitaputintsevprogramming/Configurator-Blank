using Assets.Scripts.ControlVersions;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ComponentShowStruct
{
    [CustomEditor(typeof(SwipeCameraController))]
    public class MyHiddenComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Оставляем пустым, чтобы скрыть отображение в инспекторе
        }
    }
}