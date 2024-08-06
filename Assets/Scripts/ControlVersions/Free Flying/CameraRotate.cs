using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ControlVersions
{
    [DisallowMultipleComponent]
    public class CameraRotate : MonoBehaviour
    {
        private void Start()
        {
            SwipeCameraController.e_OnDrag += OnDragCameraRotate;
        }

        private void OnDragCameraRotate(PointerEventData data)
        {
            if (SingletonCamera.Instance != null)
            {
                Vector3 _rotTo = new Vector3(0, SingletonCamera.Instance.h * SingletonCamera.Instance.settingsCamera._speedRotate * Time.deltaTime, 0); // поворот
                Vector3 _leanTo = new Vector3(-SingletonCamera.Instance.v * SingletonCamera.Instance.settingsCamera._speedRotate * Time.deltaTime, 0, 0); // наклон

                if (Input.touchCount >= 2)
                {
                    Camera.main.transform.Rotate(_rotTo, Space.World);
                    Camera.main.transform.Rotate(_leanTo, Space.Self);
                }
            }
        }
    }
}