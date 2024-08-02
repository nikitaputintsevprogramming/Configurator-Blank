using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.ControlVersions
{
    [HideInInspectorOnAdd]
    [RequireComponent(typeof(TouchTracking), typeof(TrailTouch))] 
    public class CameraMove : MonoBehaviour
    {
        private void Start()
        {
            SwipeCameraController.e_OnDrag += OnDragMoveForward;
            SwipeCameraController.e_OnEndDrag += OnEndDragoveForward;
        }

        private void OnDragMoveForward(PointerEventData data)
        {
            if (SingletonCamera.Instance != null)
            {
                Vector3 _rotTo = new Vector3(0, SingletonCamera.Instance.h * SingletonCamera.Instance.settingsCamera._speed * Time.deltaTime, 0); // поворот
                Vector3 _leanTo = new Vector3(-SingletonCamera.Instance.v * SingletonCamera.Instance.settingsCamera._speed * Time.deltaTime, 0, 0); // наклон

                Vector3 _moveTo = new Vector3(SingletonCamera.Instance.h * SingletonCamera.Instance.settingsCamera._speed * Time.deltaTime, 0, SingletonCamera.Instance.v * SingletonCamera.Instance.settingsCamera._speed * Time.deltaTime);
                 
                SingletonCamera.Instance.v = data.delta.y;
                SingletonCamera.Instance.h = data.delta.x;

                if (Input.touchCount >= 2)
                {
                    Camera.main.transform.Rotate(_rotTo, Space.World);
                    Camera.main.transform.Rotate(_leanTo, Space.Self);
                }
                else
                {
                    Camera.main.transform.Translate(_moveTo, Space.Self);
                }
            }
            else
            {
                Debug.LogWarning("SingletonCamera.Instance не инициализирован");
            }
        }

        private void OnEndDragoveForward(PointerEventData data)
        {
            if (SingletonCamera.Instance != null)
            {
                SingletonCamera.Instance.v = 0;
                SingletonCamera.Instance.h = 0;
            }
            else
            {
                Debug.LogWarning("SingletonCamera.Instance не инициализирован");
            }
        }

        private void KeybMove()
        {
            SingletonCamera.Instance.v = Input.GetAxis("Vertical");
            SingletonCamera.Instance.h = Input.GetAxis("Horizontal");
        }
    }
}