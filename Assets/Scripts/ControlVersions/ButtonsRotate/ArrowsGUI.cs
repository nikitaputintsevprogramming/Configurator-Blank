using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

namespace ArrowControl
{
    public enum QualityEnum
    {
        FullHD,
        UHD_4K,
    };

    [ExecuteAlways]
    public class ArrowsGUI : MonoBehaviour
    {
        static Dictionary<int, string> sides = new Dictionary<int, string>()
        {
            { 0, "Left"},
            { 1, "Right"},
            { 2, "Up"},
            { 3, "Down"},
        };

        static Dictionary<string, Vector2> qualityDictionary = new Dictionary<string, Vector2>()
        {
            { "FullHD", new Vector2(1920, 1080)},
            { "UHD_4K", new Vector2(3840, 2160)},
        };

        [SerializeField] private Vector2 _resolutionScreen = qualityDictionary[QualityEnum.FullHD.ToString()];
        private GameObject _canvasObj;
        private List<GameObject> _buttons = new List<GameObject>();
        [SerializeField] private QualityEnum Quality = QualityEnum.FullHD;//����� ����������� ��� ��������

        [Range(1, 4)]
        [SerializeField] private int _amountButtons;

        private void OnValidate()
        {
            Reset();
            // ���������� �������� ������
            EditorApplication.delayCall += DeleteExcess;
            _resolutionScreen = qualityDictionary[Quality.ToString()];
        }

        [ContextMenu("Reset Canvas")]
        void DeleteExcess()
        {
            // ���������� �������� ����� �������, ��� ��� RemoveAt ���������� ������ � �������
            // https://ru.stackoverflow.com/questions/992441
            for (int i = _buttons.Count -1; i > 0; i--)
            {
                if (_buttons[i] == null)
                {
                    _buttons.RemoveAt(i);
                }
                if(i >= _amountButtons)
                {
                    if (Application.isPlaying)
                        Destroy(_buttons[i].gameObject);
                    else
                        DestroyImmediate(_buttons[i].gameObject);
                    _buttons.RemoveAt(i);
                }
            }
        }

        private void Start()
        {
            EditorApplication.hierarchyChanged += DeleteExcess;
            //EditorApplication.hierarchyChanged += () => DoDelete();
        }

        private void Reset()
        {
            if (!_canvasObj || !_canvasObj.gameObject.activeInHierarchy)
            {
                //Debug.Log("��� �������");
                CreateCanvas();
            }
            if(_buttons.Count < _amountButtons)
            {
                AddButtons();
                AddComponents();
            }
        }

        private void CreateCanvas()
        {
            // �������� Canvas
            _canvasObj = new GameObject("canvasArrows", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            _canvasObj.transform.SetParent(transform);
        }

        private void AddComponents()
        {
            Canvas canvas = _canvasObj.GetComponent<Canvas>();
            CanvasScaler canvasScaler = _canvasObj.GetComponent<CanvasScaler>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(_resolutionScreen.x, _resolutionScreen.y);
            canvasScaler.matchWidthOrHeight = 0.5f;

            GridLayoutGroup gridGroup = _canvasObj.AddComponent<GridLayoutGroup>();
            gridGroup.childAlignment = TextAnchor.LowerCenter;
        }

        private void AddButtons()
        {
            for (int i = _buttons.Count; _buttons.Count < _amountButtons; i++)
            {
                _buttons.Add(new GameObject("Arrow" + sides[i], typeof(Image), typeof(Button)));
                _buttons[i].transform.SetParent(_canvasObj.transform);
            }
        }

        private void OnDestroy()
        {
            DestroyImmediate(_canvasObj);
        }
    }
}

//buttonObj.transform.SetParent(_canvasObj.transform);

//RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
//rectTransform.sizeDelta = size;
//rectTransform.anchoredPosition = anchoredPosition;
//rectTransform.anchorMin = new Vector2(0.5f, 0);
//rectTransform.anchorMax = new Vector2(0.5f, 0);
//rectTransform.pivot = new Vector2(0.5f, 0.5f);

//Button button = buttonObj.GetComponent<Button>();