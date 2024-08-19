using UnityEditor;
using UnityEngine;

namespace CameraPreset
{
    [ExecuteAlways]
    [InitializeOnLoad]
    public class CanvasRotateAround : MonoBehaviour, IChoosable
    {
        private CameraPresets associatedPreset = CameraPresets.RotateAround;

        public CameraPresets AssociatedPreset => associatedPreset;

        public bool IsActiveForPreset(CameraPresets preset)
        {
            return AssociatedPreset == preset;
        }
        #if UNITY_EDITOR
        public void SetActiveObj(bool isActive)
        {
            transform.gameObject.SetActive(isActive);
            gameObject.hideFlags = isActive ? HideFlags.None : HideFlags.HideInHierarchy;
        }
        #endif
    }
}