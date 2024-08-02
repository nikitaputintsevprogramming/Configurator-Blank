using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.ControlVersions
{
    public class CameraMove : MonoBehaviour
    {
        private void Start()
        {
            SwipeCameraController.e_OnDrag += OnDragMoveForward;
            SwipeCameraController.e_OnEndDrag += OnEndDragoveForward;
        }

        private void OnDragMoveForward(PointerEventData data)
        {
            Vector3 _rotTo = new Vector3(0, SingletonCamera.instance.h * SingletonCamera.instance.settingsCamera._speed * Time.deltaTime, 0); // поворот
            Vector3 _leanTo = new Vector3(-SingletonCamera.instance.v * SingletonCamera.instance.settingsCamera._speed * Time.deltaTime, 0, 0); // наклон

            Vector3 _moveTo = new Vector3(SingletonCamera.instance.h * SingletonCamera.instance.settingsCamera._speed* Time.deltaTime, 0, SingletonCamera.instance.v * SingletonCamera.instance.settingsCamera._speed._speed * Time.deltaTime);

            SingletonCamera.instance.v = data.delta.y;
            SingletonCamera.instance.h = data.delta.x;

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

        private void OnEndDragoveForward(PointerEventData data)
        {
            SingletonCamera.instance.v = 0;
            SingletonCamera.instance.h = 0;
        }

        private void KeybMove()
        {
            SingletonCamera.instance.v = Input.GetAxis("Vertical");
            SingletonCamera.instance.h = Input.GetAxis("Horizontal");
        }
    }
}