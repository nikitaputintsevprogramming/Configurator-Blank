using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.ControlVersions
{
    public class CameraMove : MonoBehaviour
    {
        //[SerializeField] private Camera _camera;
        //[SerializeField] private float _speed;

        private void Start()
        {
            //SwipeCameraController.e_OnBeginDrag += OnBeginDragTrails;
            SwipeCameraController.e_OnDrag += OnDragMoveForward;
            SwipeCameraController.e_OnEndDrag += OnEndDragoveForward;
        }

        private void OnDragMoveForward(PointerEventData data)
        {
            Vector3 _rotTo = new Vector3(0, SingletonCamera.instance.h * _speed * Time.deltaTime, 0); // поворот
            Vector3 _leanTo = new Vector3(-SingletonCamera.instance.v * _speed * Time.deltaTime, 0, 0); // наклон

            Vector3 _moveTo = new Vector3(SingletonCamera.instance.h * _speed * Time.deltaTime, 0, SingletonCamera.instance.v * _speed * Time.deltaTime);

            SingletonCamera.instance.v = data.delta.y;
            SingletonCamera.instance.h = data.delta.x;

            if (Input.touchCount >= 2)
            {
                _camera.transform.Rotate(_rotTo, Space.World);
                _camera.transform.Rotate(_leanTo, Space.Self);
            }
            else
            {
                _camera.transform.Translate(_moveTo, Space.Self);
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