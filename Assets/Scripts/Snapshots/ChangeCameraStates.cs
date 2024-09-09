using System.Collections;
using UnityEngine;

namespace Snap
{
    public class ChangeCameraStates : MonoBehaviour
    {
        public CameraStates _currentCameraState = CameraStates.MainCamera;

        private void Start()
        {
            ButtonConfig.e_OnButtonConfig += EnableConfigScene;
        }

        public void EnableConfigScene()
        {
            _currentCameraState = _currentCameraState != CameraStates.MainCamera ? CameraStates.SnapshotCamera : CameraStates.MainCamera;
        }

        public void ChangeState(CameraStates cameraState)
        {
            _currentCameraState = cameraState;

            switch (cameraState)
            {
                case CameraStates.SnapshotCamera:
                    
                    break;

                case CameraStates.MainCamera:
                    
                    break;

                case CameraStates.MarkersCamera:

                    break;
            }
        }
    }

    public enum CameraStates
    {
        MainCamera,
        SnapshotCamera,
        MarkersCamera,
    }
}