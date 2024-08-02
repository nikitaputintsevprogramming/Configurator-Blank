using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ControlVersions
{
    public class TrailTouch : MonoBehaviour
    {
        [SerializeField] List<GameObject> _tracks;

        DefaultControls.Resources knob;

        private void Start()
        {
            SwipeCameraController.e_OnBeginDrag += OnBeginDragTrails;
            SwipeCameraController.e_OnDrag += OnDragTrails;
            SwipeCameraController.e_OnEndDrag += OnEndDragTrails;
        }

        void OnBeginDragTrails(PointerEventData data)
        {
            var _track = new GameObject("trailStep", typeof(RectTransform), typeof(Image));

            _track.transform.SetParent(transform);
            _track.GetComponent<RectTransform>().localPosition = Vector2.zero;
            _track.GetComponent<RectTransform>().localScale = Vector3.one;
            _track.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            _track.GetComponent<Image>().sprite = Resources.Load<Sprite>("trailStep");
            _track.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
            _tracks.Add(_track);
        }

        void OnDragTrails(PointerEventData data)
        {
            List<Touch> _touches = new List<Touch>();
            _touches.Clear();
            _touches = new List<Touch>(Input.touches);

            for (int i = 0; i < _touches.Count; i++)
            {
                if (i < _tracks.Count)
                {
                    float pointX = Input.touches[i].position.x - transform.GetComponent<RectTransform>().sizeDelta.x / 2;
                    float pointY = Input.touches[i].position.y - transform.GetComponent<RectTransform>().sizeDelta.y / 2;
                    _tracks[i].GetComponent<RectTransform>().localPosition = new Vector2(pointX, pointY);
                }
            }
        }

        void OnEndDragTrails(PointerEventData data)
        {

            for (int i = _tracks.Count - 1; i >= 0; i--)
            {
                Destroy(_tracks[i]);
                _tracks.RemoveAt(i);
            }
        }
    }
}