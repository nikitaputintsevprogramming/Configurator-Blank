using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class SwipeCameraController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed;

    private float v;
    private float h;

    [SerializeField] List<GameObject> _tracks;
    DefaultControls.Resources knob;

    public void OnBeginDrag(PointerEventData eventData)
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

    public void OnDrag(PointerEventData eventData)
    {
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

    public void OnEndDrag(PointerEventData eventData)
    {
        v = 0;
        h = 0;

        for (int i = _tracks.Count - 1; i >= 0; i--)
        {
            Destroy(_tracks[i]);
            _tracks.RemoveAt(i);
        }
    }

    private void KeybMove()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
    }
}