using StateMachine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Snap
{
    public class ChangeCameraStates : MonoBehaviour
    {
        public GameObject m_MainCamera;
        private Snapshots snapshots;
        private Markers markers;
        [SerializeField] private GameObject canvasConfig;

        public CameraStates _currentCameraState = CameraStates.MainCamera;

        private void Start()
        {
            ButtonConfig.e_OnButtonConfig += EnableConfigScene;
            //m_MainCamera = Camera.main;
            snapshots = FindObjectOfType<Snapshots>();
            markers = FindObjectOfType<Markers>();

            ChangeStateOn(0);
        }

        public void EnableConfigScene()
        {
            if(canvasConfig.activeInHierarchy)
            {
                ChangeStateOn(0);
                FindObjectOfType<StateMachineButtons>().ChangeState(States.WithoutMarkers);
            }
            else
            {
                ChangeStateOn(1);
                canvasConfig.SetActive(true);
                FindObjectOfType<StateMachineButtons>().ChangeState(States.WithoutMarkers);
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
            canvasConfig.SetActive(false);

            m_MainCamera.gameObject.SetActive(true);
            snapshots.EnableConfigScene(false);
            markers.EnableConfigScene(false);
        }

        private void SnapshotCameraOn()
        {
            m_MainCamera.gameObject.SetActive(false);
            snapshots.EnableConfigScene(true);
            markers.EnableConfigScene(false);
        }

        private void MarkersCameraOn()
        {
            m_MainCamera.gameObject.SetActive(false);
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