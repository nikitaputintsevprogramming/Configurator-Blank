using UnityEditor;
using UnityEngine;

namespace CameraPreset
{
    public class CanvasFreeFly : MonoBehaviour, IChoosable
    {
        [SerializeField] private CameraPresets associatedPreset = CameraPresets.FreeFly;

        public CameraPresets AssociatedPreset => associatedPreset;

        public bool IsActiveForPreset(CameraPresets preset)
        {
            return AssociatedPreset == preset;
        }

        public void SetActiveObj(bool isActive)
        {
            transform.gameObject.SetActive(isActive);
        }
    }
}