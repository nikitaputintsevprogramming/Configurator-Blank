using System;
using System.Collections;
using UnityEngine;

namespace Snap
{
    public class ChangeCameraStates : MonoBehaviour
    {
        private Snapshots snapshots;
        private Markers markers;
        [SerializeField] private GameObject canvasConfig;

        public CameraStates _currentCameraState = CameraStates.MainCamera;

        private void Start()
        {
            ButtonConfig.e_OnButtonConfig += EnableConfigScene;
            snapshots = FindObjectOfType<Snapshots>();
            markers = FindObjectOfType<Markers>();
        }

        public void EnableConfigScene()
        {
            if(canvasConfig.activeInHierarchy)
            {
                ChangeStateOn(0);
            }
        }

        public void ChangeStateOn(int indexState)
        {
            //_currentCameraState = _currentCameraState != CameraStates.MainCamera ? CameraStates.SnapshotCamera : CameraStates.MainCamera;
            _currentCameraState = (CameraStates)Enum.GetValues(typeof(CameraStates)).GetValue(indexState);
            ChangeState(_currentCameraState);
        }

        public void ChangeState(CameraStates cameraState)
        {
            _currentCameraState = cameraState;

            switch (cameraState)
            {
                case CameraStates.MainCamera:
                    Debug.Log("MainCamera");
                    MainCameraOn();
                    break;

                case CameraStates.SnapshotCamera:
                    Debug.Log("SnapshotCamera");
                    SnapshotCameraOn();
                    break;

                case CameraStates.MarkersCamera:
                    Debug.Log("MarkersCamera");
                    MarkersCameraOn();
                    break;
            }
        }

        private void MainCameraOn()
        {
            Camera.main.gameObject.SetActive(true);
            snapshots.EnableConfigScene(false);
            markers.EnableConfigScene(false);
        }

        private void SnapshotCameraOn()
        {
            Camera.main.gameObject.SetActive(false);
            snapshots.EnableConfigScene(true);
            markers.EnableConfigScene(false);
        }

        private void MarkersCameraOn()
        {
            Camera.main.gameObject.SetActive(false);
            snapshots.EnableConfigScene(false);
            markers.EnableConfigScene(true);
        }
    }

    public enum CameraStates
    {
        MainCamera,
        SnapshotCamera,
        MarkersCamera,
    }
}