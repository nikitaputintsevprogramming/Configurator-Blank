using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEditor;

namespace Assets.Scripts.ControlVersions
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public class SwipeCameraControllerOLD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public delegate void OnBeginDragHandler(PointerEventData data);
        public static event OnBeginDragHandler e_OnBeginDrag;

        public delegate void OnDragHandler(PointerEventData data);
        public static event OnDragHandler e_OnDrag;

        public delegate void OnEndDragHandler(PointerEventData data);
        public static event OnEndDragHandler e_OnEndDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (e_OnBeginDrag != null)
                e_OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (e_OnDrag != null)
                e_OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (e_OnEndDrag != null)
                e_OnEndDrag(eventData);
        }
    }
}