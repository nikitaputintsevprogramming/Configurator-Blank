using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ControlVersions
{
    [DisallowMultipleComponent]
    public class TouchTracking : MonoBehaviour
    {
        private void Start()
        {
            SwipeCameraController.e_OnDrag += OnDragMoveTracking;
            SwipeCameraController.e_OnEndDrag += OnEndDragOverTracking;
        }

        private void OnDragMoveTracking(PointerEventData data)
        {
            SingletonCamera.Instance.v = data.delta.y;
            SingletonCamera.Instance.h = data.delta.x;
        }

        private void OnEndDragOverTracking(PointerEventData data)
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
    }
}