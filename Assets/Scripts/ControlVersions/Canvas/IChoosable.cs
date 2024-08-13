using UnityEngine;

namespace CameraPreset
{
    public interface IChoosable
    {
        CameraPresets AssociatedPreset { get; }
        bool IsActiveForPreset(CameraPresets preset);

        void SetActiveObj(bool isActive);
    }
}
