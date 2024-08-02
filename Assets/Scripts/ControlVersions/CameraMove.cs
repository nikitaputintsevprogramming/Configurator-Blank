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
        }

        private void OnDragMoveForward(PointerEventData data)
        {
            if (SingletonCamera.Instance != null)
            {
                Vector3 _moveTo = new Vector3(SingletonCamera.Instance.h * SingletonCamera.Instance.settingsCamera._speedMovement * Time.deltaTime, 0, SingletonCamera.Instance.v * SingletonCamera.Instance.settingsCamera._speedMovement * Time.deltaTime);
                 
                if (Input.touchCount == 1)
                {
                    Camera.main.transform.Translate(_moveTo, Space.Self);
                }
            }
            else
            {
                Debug.LogWarning("SingletonCamera.Instance не инициализирован");
            }
        }
    }
}