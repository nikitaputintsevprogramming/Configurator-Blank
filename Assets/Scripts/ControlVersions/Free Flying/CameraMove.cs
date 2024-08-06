using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.ControlVersions
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(TouchTracking), typeof(TrailTouch))] 
    public class CameraMove : MonoBehaviour
    {
        void OnEnable()
        {
            SingletonCamera.DestroyCameraSettings += DestroyRequire;
        }

        private void Start()
        {
            SwipeCameraController.e_OnDrag += OnDragMoveForward;
        }

        private void OnDragMoveForward(PointerEventData data)
        {
            if (SingletonCamera.Instance != null)
            {
                Vector3 _moveTo = new Vector3(SingletonCamera.Instance.h * SingletonCamera.Instance.settingsCamera._speedMovement * Time.deltaTime, 0, SingletonCamera.Instance.v * SingletonCamera.Instance.settingsCamera._speedMovement * Time.deltaTime);
                 
                if (Input.touchCount == 1)
                {
                    Camera.main.transform.Translate(_moveTo, SingletonCamera.Instance.RotateCamera360 ? Space.Self : Space.World);
                }
            }
            else
            {
                Debug.LogWarning("SingletonCamera.Instance не инициализирован");
            }
        }

        void DestroyRequire()
        {
            DestroyImmediate(gameObject.GetComponent<CameraMove>());
        }
    }
}