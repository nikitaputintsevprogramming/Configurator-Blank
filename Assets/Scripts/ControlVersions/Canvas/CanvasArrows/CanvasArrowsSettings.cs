using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CameraPreset
{
    [ExecuteInEditMode]  // Позволяет скрипту выполняться в режиме редактирования
    [InitializeOnLoad]
    public class CanvasArrowsSettings : MonoBehaviour
    {
        public static CanvasArrowsSettings Instance;

        static CanvasArrowsSettings()
        {
            // Подписка на событие при загрузке Unity Editor
            СanvasAddToHierarchy.СanvasAddedToHierarchy += SetParentCanvas;
        }

        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                DestroyImmediate(gameObject);
            }
        }

        static void SetParentCanvas()
        {
            Debug.Log("SetParentCanvas");
            if (Instance != null)
                Instance.SetParent();
        }

        public void SetParent()
        {
            var cameraPresetObject = GameObject.Find("Camera preset");
            if (cameraPresetObject != null)
            {
                transform.SetParent(cameraPresetObject.transform, false);
            }
            else
            {
                Debug.LogWarning("Camera preset object not found!");
            }
        }
    }
}
