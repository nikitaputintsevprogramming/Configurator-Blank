using UnityEditor;
using UnityEngine;

namespace CameraPreset
{
    [ExecuteAlways]
    [InitializeOnLoad]
    public class CanvasRotateAround : MonoBehaviour, IChoosable
    {
        [SerializeField] private CameraPresets associatedPreset = CameraPresets.RotateAround;

        public CameraPresets AssociatedPreset => associatedPreset;

        public bool IsActiveForPreset(CameraPresets preset)
        {
            return AssociatedPreset == preset;
        }

        public void SetActiveObj(bool isActive)
        {
            transform.gameObject.SetActive(isActive);
            gameObject.hideFlags = isActive ? HideFlags.None : HideFlags.HideInHierarchy;
        }
    }
}