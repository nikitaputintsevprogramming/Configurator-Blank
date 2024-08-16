using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CameraPreset
{
    [ExecuteInEditMode]
    [InitializeOnLoad]
    public class CanvasSettings : MonoBehaviour
    {
        static CanvasSettings()
        {
            // Подписка на событие при загрузке Unity Editor
            СanvasAddToHierarchy.СanvasAddedToHierarchy += SetParentCanvas;
        }

        static void SetParentCanvas()
        {
            // Вызываем SetParent для всех экземпляров CanvasSettings
            var canvasSettingsInstances = FindObjectsOfType<CanvasSettings>();
            foreach (var instance in canvasSettingsInstances)
            {
                instance.SetParent();
            }
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
                if(TestLog.enableLog)
                    Debug.LogWarning("Camera preset object not found!");
            }
        }
    }
}
