using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ControlVersions
{
    [DisallowMultipleComponent]
    public class TouchTrackingOLD : MonoBehaviour
    {
        private void Start()
        {
            SwipeCameraControllerOLD.e_OnDrag += OnDragMoveTracking;
            SwipeCameraControllerOLD.e_OnEndDrag += OnEndDragOverTracking;
        }

        private void OnDragMoveTracking(PointerEventData data)
        {
            SingletonCameraOLD.Instance.v = data.delta.y;
            SingletonCameraOLD.Instance.h = data.delta.x;
        }

        private void OnEndDragOverTracking(PointerEventData data)
        {
            if (SingletonCameraOLD.Instance != null)
            {
                SingletonCameraOLD.Instance.v = 0;
                SingletonCameraOLD.Instance.h = 0;
            }
            else
            {
                Debug.LogWarning("SingletonCamera.Instance не инициализирован");
            }
        }
    }
}