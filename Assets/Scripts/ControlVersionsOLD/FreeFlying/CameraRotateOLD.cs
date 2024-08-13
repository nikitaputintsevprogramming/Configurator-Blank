using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ControlVersions
{
    [DisallowMultipleComponent]
    public class CameraRotateOLD : MonoBehaviour
    {
        private void Start()
        {
            SwipeCameraControllerOLD.e_OnDrag += OnDragCameraRotate;
        }

        private void OnDragCameraRotate(PointerEventData data)
        {
            if (SingletonCameraOLD.Instance != null)
            {
                Vector3 _rotTo = new Vector3(0, SingletonCameraOLD.Instance.h * SingletonCameraOLD.Instance.settingsCamera._speedRotate * Time.deltaTime, 0); // поворот
                Vector3 _leanTo = new Vector3(-SingletonCameraOLD.Instance.v * SingletonCameraOLD.Instance.settingsCamera._speedRotate * Time.deltaTime, 0, 0); // наклон

                if (Input.touchCount >= 2)
                {
                    Camera.main.transform.Rotate(_rotTo, Space.World);
                    Camera.main.transform.Rotate(_leanTo, Space.Self);
                }
            }
        }
    }
}