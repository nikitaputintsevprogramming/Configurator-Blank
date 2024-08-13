using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.ControlVersions
{

    //[RequireComponent(typeof(TouchTracking), typeof(TrailTouch))] 
    public class CameraMoveOLD : MonoBehaviour
    {
        void OnEnable()
        {
            SingletonCameraOLD.DestroyCameraSettings += DestroyRequire;
        }

        private void Start()
        {
            SwipeCameraControllerOLD.e_OnDrag += OnDragMoveForward;
        }

        private void OnDragMoveForward(PointerEventData data)
        {
            if (SingletonCameraOLD.Instance != null)
            {
                Vector3 _moveTo = new Vector3(SingletonCameraOLD.Instance.h * SingletonCameraOLD.Instance.settingsCamera._speedMovement * Time.deltaTime, 0, SingletonCameraOLD.Instance.v * SingletonCameraOLD.Instance.settingsCamera._speedMovement * Time.deltaTime);
                 
                if (Input.touchCount == 1)
                {
                    Camera.main.transform.Translate(_moveTo, SingletonCameraOLD.Instance.RotateCamera360 ? Space.Self : Space.World);
                }
            }
            else
            {
                Debug.LogWarning("SingletonCamera.Instance не инициализирован");
            }
        }

        void DestroyRequire()
        {
            DestroyImmediate(gameObject.GetComponent<CameraMoveOLD>());
        }
    }
}