using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.Events;

namespace Assets.Scripts.ControlVersions
{
    [RequireComponent(typeof(Image))]
    public class SwipeCameraController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _speed;

        private float v;
        private float h;

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

            Vector3 _moveTo = new Vector3(h * _speed * Time.deltaTime, 0, v * _speed * Time.deltaTime);
            Vector3 _rotTo = new Vector3(0, h * _speed * Time.deltaTime, 0);
            Vector3 _leanTo = new Vector3(-v * _speed * Time.deltaTime, 0, 0);

            v = eventData.delta.y;
            h = eventData.delta.x;

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

        public void OnEndDrag(PointerEventData eventData)
        {
            if (e_OnEndDrag != null)
                e_OnEndDrag(eventData);

            v = 0;
            h = 0;

        }

        private void KeybMove()
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
        }
    }
}