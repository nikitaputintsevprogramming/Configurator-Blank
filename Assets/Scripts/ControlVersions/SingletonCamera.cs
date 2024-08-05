using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.ControlVersions
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(HiddenChildrenComponents))] //, typeof(CameraMove), typeof(CameraRotate))] 
    public class SingletonCamera : MonoBehaviour
    {
        public delegate void DestroyCameraSettingsHandler();
        public static event DestroyCameraSettingsHandler DestroyCameraSettings;

        public static SingletonCamera Instance { get; private set; }
        public SettingsCamera settingsCamera;

        [NonSerialized] public float v;
        [NonSerialized] public float h;

        private void Reset()
        {
            gameObject.AddComponent<CameraMove>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<CameraRotate>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<SwipeCameraController>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<TouchTracking>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<TrailTouch>().hideFlags = HideFlags.HideInInspector;
        }

        private void OnDestroy()
        {
            //DestroyCameraSettings();
            DestroyImmediate(gameObject.GetComponent<CameraMove>());
            DestroyImmediate(gameObject.GetComponent<CameraRotate>());
            DestroyImmediate(gameObject.GetComponent<SwipeCameraController>());
            DestroyImmediate(gameObject.GetComponent<TouchTracking>());
            DestroyImmediate(gameObject.GetComponent<TrailTouch>());
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}