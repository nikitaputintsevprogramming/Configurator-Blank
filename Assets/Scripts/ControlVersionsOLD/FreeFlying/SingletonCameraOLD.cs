using System;
using System.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.ControlVersions
{
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(HiddenChildrenComponents))] //, typeof(CameraMove), typeof(CameraRotate))] 
    public class SingletonCameraOLD : MonoBehaviour
    {
        public delegate void DestroyCameraSettingsHandler();
        public static event DestroyCameraSettingsHandler DestroyCameraSettings;

        public bool RotateCamera360;
        public static SingletonCameraOLD Instance { get; private set; }
        public SettingsCamera settingsCamera;

        [NonSerialized] public float v;
        [NonSerialized] public float h;

        private void Reset()
        {
            gameObject.AddComponent<CameraMoveOLD>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<CameraRotateOLD>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<SwipeCameraControllerOLD>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<TouchTrackingOLD>().hideFlags = HideFlags.HideInInspector;
            gameObject.AddComponent<TrailTouchOLD>().hideFlags = HideFlags.HideInInspector;
        }

        private void OnDestroy()
        {
            DestroyChildrenComponents();
        }
        
        private void DestroyChildrenComponents()
        {
            if (!Application.isPlaying)
            {
                DestroyImmediate(gameObject.GetComponent<CameraMoveOLD>(), false);
                DestroyImmediate(gameObject.GetComponent<CameraRotateOLD>(), false);
                DestroyImmediate(gameObject.GetComponent<SwipeCameraControllerOLD>(), false);
                DestroyImmediate(gameObject.GetComponent<TouchTrackingOLD>(), false);
                DestroyImmediate(gameObject.GetComponent<TrailTouchOLD>(), false);
            }
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